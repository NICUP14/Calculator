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

        /// <summary>
        /// Returns built expression in string format
        /// </summary>
        /// <param name="completeParentheses"></param>
        /// <returns></returns>
        public string ToExpression(bool completeParentheses = false)
        {
            int expressionStringBuilderCapacity = 0;
            int unmatchedParenthesisCount = 0;

            if (completeParentheses)
            {
                unmatchedParenthesisCount += _tokenList.Count(token => token.Equals(ParenthesisToken.OpeningParenthesis));
                unmatchedParenthesisCount -= _tokenList.Count(token => token.Equals(ParenthesisToken.ClosingParenthesis));

                /// Determine the length of the required closing parenthesis tokens
                expressionStringBuilderCapacity += unmatchedParenthesisCount * ParenthesisToken.ClosingParenthesis.Length;
            }

            foreach (Token token in _tokenList)
                expressionStringBuilderCapacity += token.Length;

            StringBuilder expressionStringBuilder = new StringBuilder(expressionStringBuilderCapacity);

            foreach (Token token in _tokenList)
                expressionStringBuilder.Append(token);

            /// Append required parenthesis to the string builder instance
            for (int parenthesisCount = 0; parenthesisCount < unmatchedParenthesisCount; parenthesisCount++)
                expressionStringBuilder.Append(ParenthesisToken.ClosingParenthesis);

            return expressionStringBuilder.ToString();
        }

        public void Clear()
        {
            _tokenList.Clear();
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
                        lastTokenListNode.Value = lastTokenListToken.Equals(OperatorToken.Addition) ? OperatorToken.Subtraction : OperatorToken.Addition;

                    /// Multiplication and division operators mark
                    else
                    {
                        _tokenList.AddLast(ParenthesisToken.OpeningParenthesis);
                        _tokenList.AddLast(OperatorToken.Subtraction);
                    }
                }

                /// Decimal token mark
                else if (lastTokenListToken.Equals(Token.Decimal))
                {
                    if (_tokenList.Count <= 1)
                        _tokenList.AddFirst(OperatorToken.Subtraction);
                    else
                    {
                        LinkedListNode<Token> previousTokenListNode = lastTokenListNode.Previous;
                        Token previousTokenListToken = previousTokenListNode.Value;

                        if (previousTokenListToken.Equals(ParenthesisToken.OpeningParenthesis))
                            _tokenList.AddAfter(previousTokenListNode, OperatorToken.Subtraction);
                        else if (previousTokenListToken.Equals(OperatorToken.Addition) || previousTokenListToken.Equals(OperatorToken.Subtraction))
                            previousTokenListNode.Value = previousTokenListToken.Equals(OperatorToken.Addition) ? OperatorToken.Subtraction : OperatorToken.Addition;
                        else
                        {
                            _tokenList.AddAfter(previousTokenListNode, OperatorToken.Subtraction);
                            _tokenList.AddAfter(previousTokenListNode, ParenthesisToken.OpeningParenthesis);
                        }
                    }
                }

                /// Parenthesis token mark
                else if (lastTokenListToken.Equals(Token.Parenthesis))
                {
                    /// Opening parenthesis token mark
                    if (lastTokenListToken.Equals(ParenthesisToken.OpeningParenthesis))
                        _tokenList.AddLast(OperatorToken.Subtraction);

                    /// Closing parenthesis token mark
                    else if (lastTokenListToken.Equals(ParenthesisToken.ClosingParenthesis))
                    {
                        LinkedListNode<Token> previousTokenListNode = NodeBeforeParanthesesGroup(lastTokenListNode);

                        if (previousTokenListNode is null)
                            _tokenList.AddFirst(OperatorToken.Subtraction);
                        else
                        {
                            Token previousTokenListToken = previousTokenListNode.Value;

                            if (previousTokenListToken.Equals(ParenthesisToken.OpeningParenthesis))
                                _tokenList.AddAfter(previousTokenListNode, OperatorToken.Subtraction);
                            else if (previousTokenListToken.Equals(OperatorToken.Addition) || previousTokenListToken.Equals(OperatorToken.Subtraction))
                                previousTokenListNode.Value = previousTokenListToken.Equals(OperatorToken.Addition) ? OperatorToken.Subtraction : OperatorToken.Addition;
                            else
                            {
                                _tokenList.AddAfter(previousTokenListNode, OperatorToken.Subtraction);
                                _tokenList.AddAfter(previousTokenListNode, ParenthesisToken.OpeningParenthesis);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Inserts/Appends paranthesis based on the last token
        /// </summary>
        public void InsertParenthesis()
        {
            if (_tokenList.Count == 0)
                _tokenList.AddLast(ParenthesisToken.OpeningParenthesis);
            else
            {
                LinkedListNode<Token> lastTokenListNode = _tokenList.Last;
                Token lastTokenListToken = lastTokenListNode.Value;

                /// Operator token and opening parenthesis token mark
                if (lastTokenListToken.Equals(Token.Operator) || lastTokenListToken.Equals(ParenthesisToken.OpeningParenthesis))
                {
                    _tokenList.AddLast(ParenthesisToken.OpeningParenthesis);
                }
                else
                {
                    int unmatchedParenthesisCount = 0;
                    unmatchedParenthesisCount += _tokenList.Count(token => token.Equals(ParenthesisToken.OpeningParenthesis));
                    unmatchedParenthesisCount -= _tokenList.Count(token => token.Equals(ParenthesisToken.ClosingParenthesis));

                    if (unmatchedParenthesisCount > 0)
                    {
                        if (lastTokenListToken.Equals(Token.Decimal) && (lastTokenListToken as DecimalToken).EndsWithPeriod())
                            throw new ExpressionBuilderInsertParenthesisError();

                        _tokenList.AddLast(ParenthesisToken.ClosingParenthesis);
                    }
                    else
                    {
                        /// Decimal token mark
                        if (lastTokenListToken.Equals(Token.Decimal))
                            _tokenList.AddBefore(lastTokenListNode, ParenthesisToken.OpeningParenthesis);

                        /// Closing parenthesis token mark
                        else if (lastTokenListToken.Equals(ParenthesisToken.ClosingParenthesis))
                        {
                            LinkedListNode<Token> previousTokenListNode = NodeBeforeParanthesesGroup(lastTokenListNode) ?? _tokenList.First;
                            _tokenList.AddAfter(previousTokenListNode, ParenthesisToken.OpeningParenthesis);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Appends specified decimal token to the expression
        /// </summary>
        /// <param name="decimalToken"></param>
        public void AppendToken(DecimalToken decimalToken)
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
                if (lastToken.Equals(ParenthesisToken.ClosingParenthesis))
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
        }

        /// <summary>
        /// Appends specified decimal token to the expression
        /// </summary>
        /// <param name="operatorToken"></param>
        public void AppendToken(OperatorToken operatorToken)
        {
            if (_tokenList.Count == 0)
            {
                if (operatorToken.Equals(OperatorToken.Addition) || operatorToken.Equals(OperatorToken.Subtraction))
                    _tokenList.AddLast(operatorToken);
            }
            else
            {
                bool operatorTokenIsAdditionOrSubtraction = operatorToken.Equals(OperatorToken.Addition) || operatorToken.Equals(OperatorToken.Subtraction);
                LinkedListNode<Token> lastTokenListNode = _tokenList.Last;
                Token lastTokenListToken = lastTokenListNode.Value;

                /// Operator token mark
                if (lastTokenListToken.Equals(Token.Operator))
                {
                    if (_tokenList.Count > 1)
                    {
                        Token previousTokenListToken = lastTokenListNode.Previous.Value;
                        if (previousTokenListToken.Equals(ParenthesisToken.OpeningParenthesis) && !operatorTokenIsAdditionOrSubtraction)
                            throw new ExpressionBuilderAppendOperatorTokenError();
                    }

                    lastTokenListNode.Value = operatorToken;
                }

                /// Decimal token mark
                else if (lastTokenListToken.Equals(Token.Decimal))
                {
                    if (!(lastTokenListToken as DecimalToken).EndsWithPeriod())
                        _tokenList.AddLast(operatorToken);
                }

                /// Parenthesis token mark
                else if (lastTokenListToken.Equals(Token.Parenthesis))
                {
                    if (lastTokenListToken.Equals(ParenthesisToken.OpeningParenthesis) && !operatorTokenIsAdditionOrSubtraction)
                        throw new ExpressionBuilderAppendOperatorTokenError();
                    _tokenList.AddLast(operatorToken);
                }
            }
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

                if (previousTokenListToken.Equals(ParenthesisToken.ClosingParenthesis))
                    nestedParenthesisLevel++;
                if (previousTokenListToken.Equals(ParenthesisToken.OpeningParenthesis))
                    nestedParenthesisLevel--;

                previousTokenListNode = previousTokenListNode.Previous;
            }
            while (previousTokenListNode != null && nestedParenthesisLevel != 0);

            return previousTokenListNode;
        }

        readonly LinkedList<Token> _tokenList;
    }
}
