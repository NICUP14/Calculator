using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace Calculator
{
    /// <summary>
    /// Token-based expression builder implementation
    /// </summary>
    class ExpressionBuilder
    {
        public ExpressionBuilder()
        {
            _tokenList = new LinkedList<Token>();
        }

        public Token[] ToTokenArray()
        {
            int tokenArrayLength = _tokenList.Count;
            tokenArrayLength += _tokenList.Count(token => token.Equals(ParanthesisToken.OpeningParenthesis));
            tokenArrayLength -= _tokenList.Count(token => token.Equals(ParanthesisToken.ClosingParenthesis));
            Token[] tokenArray = new Token[tokenArrayLength];

            _tokenList.CopyTo(tokenArray, 0);
            for (int tokenArrayRange = _tokenList.Count; tokenArrayRange < tokenArray.Length; tokenArrayRange++)
                tokenArray[tokenArrayRange] = ParanthesisToken.ClosingParenthesis;

            return tokenArray;
        }

        /// <summary>
        /// Returns built expression in string format
        /// </summary>
        public override string ToString()
        {
            int expressionStringBuilderCapacity = 0;

            foreach (Token token in _tokenList)
                expressionStringBuilderCapacity += token.Length;

            StringBuilder expressionStringBuilder = new StringBuilder(expressionStringBuilderCapacity);

            foreach (Token token in _tokenList)
                expressionStringBuilder.Append(token);

            return expressionStringBuilder.ToString();
        }

        public void Clear()
        {
            _tokenList.Clear();
        }

        public void RemoveLastCharacter()
        {
            if (_tokenList.Count == 0)
                throw new ExpressionBuilderRemoveLastCharacterError();

            Token lastTokenListToken = _tokenList.Last.Value;
            lastTokenListToken.RemoveLastCharacter();

            if (lastTokenListToken.Length == 0)
                _tokenList.RemoveLast();

            previousOperatorToken = null;
            previousDecimalConstant = null;
        }

        /// <summary>
        /// Changes the sign based on the last token
        /// </summary>
        public void ChangeSign()
        {
            if (_tokenList.Count == 0)
                _tokenList.AddLast(OperatorToken.Subtraction);
            else
            {
                LinkedListNode<Token> lastTokenListNode = _tokenList.Last;
                Token lastTokenListToken = lastTokenListNode.Value;

                /// Operator token mark
                if (lastTokenListToken.Equals(Token.Operator))
                {
                    /// Adition and subtration operators mark
                    if(lastTokenListToken.Equals(OperatorToken.Addition) || lastTokenListToken.Equals(OperatorToken.Subtraction))
                        lastTokenListNode.Value = lastTokenListToken.Equals(OperatorToken.Addition) ? OperatorToken.Subtraction.Clone() : OperatorToken.Addition.Clone();

                    /// Multiplication and division operators mark
                    else
                    {
                        _tokenList.AddLast(ParanthesisToken.OpeningParenthesis.Clone());
                        _tokenList.AddLast(OperatorToken.Subtraction.Clone());
                    }
                }

                /// Decimal token mark
                else if (lastTokenListToken.Equals(Token.Decimal))
                {
                    if (_tokenList.Count <= 1)
                        _tokenList.AddFirst(OperatorToken.Subtraction.Clone());
                    else
                    {
                        LinkedListNode<Token> previousTokenListNode = lastTokenListNode.Previous;
                        Token previousTokenListToken = previousTokenListNode.Value;

                        if (previousTokenListToken.Equals(ParanthesisToken.OpeningParenthesis))
                            _tokenList.AddAfter(previousTokenListNode, OperatorToken.Subtraction.Clone());
                        else if (previousTokenListToken.Equals(OperatorToken.Addition) || previousTokenListToken.Equals(OperatorToken.Subtraction))
                            previousTokenListNode.Value = previousTokenListToken.Equals(OperatorToken.Addition) ? OperatorToken.Subtraction.Clone() : OperatorToken.Addition.Clone();
                        else
                        {
                            _tokenList.AddAfter(previousTokenListNode, OperatorToken.Subtraction.Clone());
                            _tokenList.AddAfter(previousTokenListNode, ParanthesisToken.OpeningParenthesis.Clone());
                        }
                    }
                }

                /// Parenthesis token mark
                else if (lastTokenListToken.Equals(Token.Parenthesis))
                {
                    /// Opening parenthesis token mark
                    if (lastTokenListToken.Equals(ParanthesisToken.OpeningParenthesis))
                        _tokenList.AddLast(OperatorToken.Subtraction.Clone());

                    /// Closing parenthesis token mark
                    else if (lastTokenListToken.Equals(ParanthesisToken.ClosingParenthesis))
                    {
                        LinkedListNode<Token> previousTokenListNode = NodeBeforeParanthesesGroup(lastTokenListNode);

                        if (previousTokenListNode is null)
                            _tokenList.AddFirst(OperatorToken.Subtraction.Clone());
                        else
                        {
                            Token previousTokenListToken = previousTokenListNode.Value;

                            if (previousTokenListToken.Equals(ParanthesisToken.OpeningParenthesis))
                                _tokenList.AddAfter(previousTokenListNode, OperatorToken.Subtraction.Clone());
                            else if (previousTokenListToken.Equals(OperatorToken.Addition) || previousTokenListToken.Equals(OperatorToken.Subtraction))
                                previousTokenListNode.Value = previousTokenListToken.Equals(OperatorToken.Addition) ? OperatorToken.Subtraction.Clone() : OperatorToken.Addition.Clone();
                            else
                            {
                                _tokenList.AddAfter(previousTokenListNode, OperatorToken.Subtraction.Clone());
                                _tokenList.AddAfter(previousTokenListNode, ParanthesisToken.OpeningParenthesis.Clone());
                            }
                        }
                    }
                }
            }

            previousOperatorToken = null;
            previousDecimalConstant = null;
        }

        public Decimal Calculate()
        {
            Token[] tokenArray = ToTokenArray();

            if (tokenArray.Length == 0)
                throw new ExpressionBuilderNullError();

            Token lastTokenArrayToken = tokenArray[tokenArray.Length - 1];
            if(lastTokenArrayToken.Equals(Token.Operator))
                tokenArray[tokenArray.Length - 1] = null;

            Decimal expressionResult = ExpressionParser.Calculate(tokenArray);

            if (lastTokenArrayToken.Equals(Token.Operator))
            {
                previousOperatorToken = lastTokenArrayToken;
                previousDecimalConstant = expressionResult;
            }

            if(previousOperatorToken is not null)
            {
                if (previousOperatorToken.Equals(OperatorToken.Division))
                    expressionResult = Decimal.Divide(expressionResult, previousDecimalConstant);
                else if (previousOperatorToken.Equals(OperatorToken.Addition))
                    expressionResult = Decimal.Add(expressionResult, previousDecimalConstant);
                else if (previousOperatorToken.Equals(OperatorToken.Subtraction))
                    expressionResult = Decimal.Subtract(expressionResult, previousDecimalConstant);
                else if (previousOperatorToken.Equals(OperatorToken.Multiplication))
                    expressionResult = Decimal.Multiply(expressionResult, previousDecimalConstant);
            }

            tokenArray[tokenArray.Length - 1] = lastTokenArrayToken;

            return expressionResult;
        }

        /// <summary>
        /// Inserts/Appends paranthesis based on the last token
        /// </summary>
        public void InsertParenthesis()
        {
            if (_tokenList.Count == 0)
                _tokenList.AddLast(ParanthesisToken.OpeningParenthesis.Clone());
            else
            {
                LinkedListNode<Token> lastTokenListNode = _tokenList.Last;
                Token lastTokenListToken = lastTokenListNode.Value;

                /// Operator token and opening parenthesis token mark
                if (lastTokenListToken.Equals(Token.Operator) || lastTokenListToken.Equals(ParanthesisToken.OpeningParenthesis))
                    _tokenList.AddLast(ParanthesisToken.OpeningParenthesis.Clone());
                else
                {
                    int unmatchedParenthesisCount = 0;
                    unmatchedParenthesisCount += _tokenList.Count(token => token.Equals(ParanthesisToken.OpeningParenthesis));
                    unmatchedParenthesisCount -= _tokenList.Count(token => token.Equals(ParanthesisToken.ClosingParenthesis));

                    if (unmatchedParenthesisCount > 0)
                    {
                        if (lastTokenListToken.Equals(Token.Decimal) && (lastTokenListToken as DecimalToken).EndsWithPeriod())
                            throw new ExpressionBuilderInsertParenthesisError();

                        _tokenList.AddLast(ParanthesisToken.ClosingParenthesis.Clone());
                    }
                    else
                    {
                        /// Decimal token mark
                        if (lastTokenListToken.Equals(Token.Decimal))
                            _tokenList.AddBefore(lastTokenListNode, ParanthesisToken.OpeningParenthesis.Clone());

                        /// Closing parenthesis token mark
                        else if (lastTokenListToken.Equals(ParanthesisToken.ClosingParenthesis))
                        {
                            LinkedListNode<Token> previousTokenListNode = NodeBeforeParanthesesGroup(lastTokenListNode) ?? _tokenList.First;
                            _tokenList.AddAfter(previousTokenListNode, ParanthesisToken.OpeningParenthesis.Clone());
                        }
                    }
                }
            }

            previousOperatorToken = null;
            previousDecimalConstant = null;
        }

        /// <summary>
        /// Appends specified decimal token to the expression
        /// </summary>
        /// <param name="decimalToken"></param>
        public void AppendToken(DecimalToken decimalToken, bool changePreviousOperatorToken = true)
        {
            if (_tokenList.Count == 0)
            {
                if (decimalToken.EndsWithPeriod())
                    throw new ExpressionBuilderAppendPeriodDecimalTokenError();

                _tokenList.AddLast(decimalToken);
            }
            else
            {
                Token lastToken = _tokenList.Last.Value;

                /// Closing parenthesis token mark
                if (lastToken.Equals(ParanthesisToken.ClosingParenthesis))
                    throw new ExpressionBuilderAppendDecimalTokenError();

                /// Decimal token mark
                if (lastToken.Equals(Token.Decimal))
                {
                    if ((lastToken as DecimalToken).PeriodCount + decimalToken.PeriodCount == 2)
                        throw new ExpressionBuilderAppendPeriodDecimalTokenError();
                    _tokenList.Last.Value = DecimalToken.Concatenate(lastToken as DecimalToken, decimalToken);
                }

                else
                {
                    if (decimalToken.EndsWithPeriod())
                        throw new ExpressionBuilderAppendPeriodDecimalTokenError();

                    _tokenList.AddLast(decimalToken);
                }
            }

            if(changePreviousOperatorToken == true)
            {
                previousOperatorToken = null;
                previousDecimalConstant = null;
            }
        }

        /// <summary>
        /// Appends specified decimal token to the expression
        /// </summary>
        /// <param name="operatorToken"></param>
        public void AppendToken(OperatorToken operatorToken)
        {
            if (_tokenList.Count == 0)
            {
                if (!operatorToken.Equals(OperatorToken.Addition) && !operatorToken.Equals(OperatorToken.Subtraction))
                    throw new ExpressionBuilderAppendMultiplicationOrDivisionOperatorTokenError();

                _tokenList.AddLast(operatorToken.Clone());
            }
            else
            {
                bool operatorTokenIsAdditionOrSubtraction = operatorToken.Equals(OperatorToken.Addition) || operatorToken.Equals(OperatorToken.Subtraction);
                LinkedListNode<Token> lastTokenListNode = _tokenList.Last;
                Token lastTokenListToken = lastTokenListNode.Value;

                /// Operator token mark
                if (lastTokenListToken.Equals(Token.Operator))
                {
                    if (_tokenList.Count == 1 && !operatorTokenIsAdditionOrSubtraction)
                        throw new ExpressionBuilderAppendMultiplicationOrDivisionOperatorTokenError();

                    if (_tokenList.Count > 1)
                    {
                        Token previousTokenListToken = lastTokenListNode.Previous.Value;
                        if (previousTokenListToken.Equals(ParanthesisToken.OpeningParenthesis) && !operatorTokenIsAdditionOrSubtraction)
                            throw new ExpressionBuilderAppendMultiplicationOrDivisionOperatorTokenError();
                    }

                    lastTokenListNode.Value = operatorToken.Clone();
                }

                /// Decimal token mark
                else if (lastTokenListToken.Equals(Token.Decimal))
                {
                    if ((lastTokenListToken as DecimalToken).EndsWithPeriod())
                        throw new ExpressionBuilderAppendOperatorTokenError();

                    _tokenList.AddLast(operatorToken.Clone());
                }

                /// Parenthesis token mark
                else if (lastTokenListToken.Equals(Token.Parenthesis))
                {
                    if (lastTokenListToken.Equals(ParanthesisToken.OpeningParenthesis) && !operatorTokenIsAdditionOrSubtraction)
                        throw new ExpressionBuilderAppendMultiplicationOrDivisionOperatorTokenError();

                    _tokenList.AddLast(operatorToken.Clone());
                }
            }

            previousOperatorToken = null;
            previousDecimalConstant = null;
        }

        /// <summary>
        /// Returns the node before the parantheses group
        /// </summary>
        /// <param name="tokenListNode"></param>
        /// <returns></returns>
        private LinkedListNode<Token> NodeBeforeParanthesesGroup(LinkedListNode<Token> tokenListNode)
        {
            int nestedParenthesisLevel = 0;
            Token previousTokenListToken;
            LinkedListNode<Token> previousTokenListNode = tokenListNode;

            do
            {
                previousTokenListToken = previousTokenListNode.Value;

                if (previousTokenListToken.Equals(ParanthesisToken.ClosingParenthesis))
                    nestedParenthesisLevel++;
                if (previousTokenListToken.Equals(ParanthesisToken.OpeningParenthesis))
                    nestedParenthesisLevel--;

                previousTokenListNode = previousTokenListNode.Previous;
            }
            while (previousTokenListNode != null && nestedParenthesisLevel != 0);

            return previousTokenListNode;
        }

        Token previousOperatorToken;
        Decimal previousDecimalConstant;
        readonly LinkedList<Token> _tokenList;
    }
}
