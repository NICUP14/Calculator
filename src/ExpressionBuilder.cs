using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Calculator
{
    /// <summary>
    /// Represents an abstract, mutable array of tokens
    /// </summary>
    class ExpressionBuilder
    {
        /// <summary>
        /// Initializes a new instance of the ExpressionBuilder class
        /// </summary>
        public ExpressionBuilder()
        {
            _tokenLinkedList = new LinkedList<Token>();
        }

        /// <summary>
        /// Appends the required closing parantheses to validate expression 
        /// </summary>
        public void CompleteParantheses()
        {
            /// Determines the number of unclosed opening parantheses
            int unmatchedParanthesisCount = 0;
            unmatchedParanthesisCount += _tokenLinkedList.Count(token => token.Equals(ParanthesisToken.OpeningParenthesis));
            unmatchedParanthesisCount -= _tokenLinkedList.Count(token => token.Equals(ParanthesisToken.ClosingParenthesis));

            /// Appends the required closing parantheses to validate the internal representation
            for (int tokenListRange = 0; tokenListRange < unmatchedParanthesisCount; tokenListRange++)
                _tokenLinkedList.AddLast(ParanthesisToken.ClosingParenthesis.Clone());
        }

        /// <summary>
        /// Converts the internal representation to an array of tokens
        /// </summary>
        /// <returns></returns>
        public Token[] ToTokenArray()
        {
            /// Determines the number of unclosed opening parantheses
            int tokenArrayLength = _tokenLinkedList.Count;
            tokenArrayLength += _tokenLinkedList.Count(token => token.Equals(ParanthesisToken.OpeningParenthesis));
            tokenArrayLength -= _tokenLinkedList.Count(token => token.Equals(ParanthesisToken.ClosingParenthesis));

            Token[] tokenArray = new Token[tokenArrayLength];
            _tokenLinkedList.CopyTo(tokenArray, 0);

            /// Appends the required closing parantheses to validate the token array
            for (int tokenListRange = _tokenLinkedList.Count; tokenListRange < tokenArrayLength; tokenListRange++)
                tokenArray[tokenListRange] = ParanthesisToken.ClosingParenthesis;

            return tokenArray;
        }

        /// <summary>
        /// Returns built expression in string format
        /// </summary>
        public override string ToString()
        {
            /// Determines the required capacity and initializes the StringBuilder object
            int stringRepresentationBuilderCapacity = Enumerable.Sum(_tokenLinkedList, token => token.Length);
            StringBuilder stringRepresentationBuilder = new(stringRepresentationBuilderCapacity);

            stringRepresentationBuilder.AppendJoin(string.Empty, _tokenLinkedList);

            return stringRepresentationBuilder.ToString();
        }

        public void Clear()
        {
            _tokenLinkedList.Clear();
        }

        /// <summary>
        /// Removes the last character from the last token in the list
        /// </summary>
        /// <exception cref="ExpressionBuilderRemoveLastCharacterException"></exception>
        public void RemoveLastCharacter()
        {
            if (_tokenLinkedList.Count == 0)
                throw new ExpressionBuilderRemoveLastCharacterException();

            Token lastTokenListToken = _tokenLinkedList.Last.Value;
            lastTokenListToken.RemoveLastCharacter();

            if (lastTokenListToken.Length == 0)
                _tokenLinkedList.RemoveLast();

            savedOperatorToken = null;
            savedDecimalToken = null;
        }

        /// <summary>
        /// Changes the sign based on the last token
        /// </summary>
        public void ChangeSign()
        {
            if (_tokenLinkedList.Count == 0)
                _tokenLinkedList.AddLast(OperatorToken.Subtraction);
            else
            {
                LinkedListNode<Token> lastTokenListNode = _tokenLinkedList.Last;
                Token lastTokenListToken = lastTokenListNode.Value;

                /// Operator token mark
                if (lastTokenListToken.Equals(Token.Operator))
                {
                    /// Addition and subtraction operators mark
                    if (lastTokenListToken.Equals(OperatorToken.Addition) || lastTokenListToken.Equals(OperatorToken.Subtraction))
                        lastTokenListNode.Value = (lastTokenListToken.Equals(OperatorToken.Addition) ? OperatorToken.Subtraction : OperatorToken.Addition).Clone();

                    /// Multiplication and division operators mark
                    else
                    {
                        _tokenLinkedList.AddLast(ParanthesisToken.OpeningParenthesis.Clone());
                        _tokenLinkedList.AddLast(OperatorToken.Subtraction.Clone());
                    }
                }

                /// Decimal token mark
                else if (lastTokenListToken.Equals(Token.Decimal))
                {
                    if (_tokenLinkedList.Count <= 1)
                        _tokenLinkedList.AddFirst(OperatorToken.Subtraction.Clone());
                    else
                    {
                        LinkedListNode<Token> previousTokenListNode = lastTokenListNode.Previous;
                        Token previousTokenListToken = previousTokenListNode.Value;

                        if (previousTokenListToken.Equals(ParanthesisToken.OpeningParenthesis))
                            _tokenLinkedList.AddAfter(previousTokenListNode, OperatorToken.Subtraction.Clone());
                        else if (previousTokenListToken.Equals(OperatorToken.Addition) || previousTokenListToken.Equals(OperatorToken.Subtraction))
                            previousTokenListNode.Value = (previousTokenListToken.Equals(OperatorToken.Addition) ? OperatorToken.Subtraction : OperatorToken.Addition).Clone();
                        else
                        {
                            _tokenLinkedList.AddAfter(previousTokenListNode, OperatorToken.Subtraction.Clone());
                            _tokenLinkedList.AddAfter(previousTokenListNode, ParanthesisToken.OpeningParenthesis.Clone());
                        }
                    }
                }

                /// Parenthesis token mark
                else if (lastTokenListToken.Equals(Token.Parenthesis))
                {
                    /// Opening parenthesis token mark
                    if (lastTokenListToken.Equals(ParanthesisToken.OpeningParenthesis))
                        _tokenLinkedList.AddLast(OperatorToken.Subtraction.Clone());

                    /// Closing parenthesis token mark
                    else if (lastTokenListToken.Equals(ParanthesisToken.ClosingParenthesis))
                    {
                        LinkedListNode<Token> previousTokenListNode = NodeBeforeParanthesesGroup(lastTokenListNode);

                        if (previousTokenListNode is null)
                            _tokenLinkedList.AddFirst(OperatorToken.Subtraction.Clone());
                        else
                        {
                            Token previousTokenListToken = previousTokenListNode.Value;

                            if (previousTokenListToken.Equals(ParanthesisToken.OpeningParenthesis))
                                _tokenLinkedList.AddAfter(previousTokenListNode, OperatorToken.Subtraction.Clone());
                            else if (previousTokenListToken.Equals(OperatorToken.Addition) || previousTokenListToken.Equals(OperatorToken.Subtraction))
                                previousTokenListNode.Value = previousTokenListToken.Equals(OperatorToken.Addition) ? OperatorToken.Subtraction.Clone() : OperatorToken.Addition.Clone();
                            else
                            {
                                _tokenLinkedList.AddAfter(previousTokenListNode, OperatorToken.Subtraction.Clone());
                                _tokenLinkedList.AddAfter(previousTokenListNode, ParanthesisToken.OpeningParenthesis.Clone());
                            }
                        }
                    }
                }
            }

            savedOperatorToken = null;
            savedDecimalToken = null;
        }

        /// <summary>
        /// Wraps the ExpressionParser.Calculate method and enables the use of consecutive operations
        /// </summary>
        /// <returns></returns>
        public Decimal Calculate()
        {
            if (_tokenLinkedList.Count == 0)
                return new("0");

            Token lastTokenListToken = _tokenLinkedList.Last();

            if (lastTokenListToken.Equals(Token.Decimal))
                (lastTokenListToken as DecimalToken).Reformat();

            /// Detects whether the expression uses consecutive operations
            if (lastTokenListToken.Equals(Token.Operator))
            {
                _tokenLinkedList.RemoveLast();

                savedOperatorToken = lastTokenListToken;

                Decimal savedDecimal = ExpressionParser.Calculate(ToTokenArray());
                savedDecimalToken = new DecimalToken(savedDecimal);
            }

            /// Handles the "consecutive operation" case
            if (savedOperatorToken is not null && savedDecimalToken is not null)
            {
                Decimal expressionResult = ExpressionParser.Calculate(ToTokenArray());
                DecimalToken expressionResultDecimalToken = new(expressionResult);

                _tokenLinkedList.Clear();
                _tokenLinkedList.AddLast(expressionResultDecimalToken);
                _tokenLinkedList.AddLast(savedOperatorToken);
                _tokenLinkedList.AddLast(savedDecimalToken);
            }

            return ExpressionParser.Calculate(ToTokenArray());
        }

        /// <summary>
        /// Inserts/Appends parantheses based on the last token
        /// </summary>
        public void InsertParenthesis()
        {
            if (_tokenLinkedList.Count == 0)
                _tokenLinkedList.AddLast(ParanthesisToken.OpeningParenthesis.Clone());
            else
            {
                LinkedListNode<Token> lastTokenListNode = _tokenLinkedList.Last;
                Token lastTokenListToken = lastTokenListNode.Value;

                /// Operator token and opening parenthesis token mark
                if (lastTokenListToken.Equals(Token.Operator) || lastTokenListToken.Equals(ParanthesisToken.OpeningParenthesis))
                    _tokenLinkedList.AddLast(ParanthesisToken.OpeningParenthesis.Clone());
                else
                {
                    int unmatchedParenthesisCount = 0;
                    unmatchedParenthesisCount += _tokenLinkedList.Count(token => token.Equals(ParanthesisToken.OpeningParenthesis));
                    unmatchedParenthesisCount -= _tokenLinkedList.Count(token => token.Equals(ParanthesisToken.ClosingParenthesis));

                    if (unmatchedParenthesisCount > 0)
                    {
                        if (lastTokenListToken.Equals(Token.Decimal))
                            (lastTokenListToken as DecimalToken).Reformat();

                        _tokenLinkedList.AddLast(ParanthesisToken.ClosingParenthesis.Clone());
                    }
                    else
                    {
                        /// Decimal token mark
                        if (lastTokenListToken.Equals(Token.Decimal))
                            _tokenLinkedList.AddBefore(lastTokenListNode, ParanthesisToken.OpeningParenthesis.Clone());

                        /// Closing parenthesis token mark
                        else if (lastTokenListToken.Equals(ParanthesisToken.ClosingParenthesis))
                        {
                            LinkedListNode<Token> previousTokenListNode = NodeBeforeParanthesesGroup(lastTokenListNode) ?? _tokenLinkedList.First;
                            _tokenLinkedList.AddAfter(previousTokenListNode, ParanthesisToken.OpeningParenthesis.Clone());
                        }
                    }
                }
            }

            savedOperatorToken = null;
            savedDecimalToken = null;
        }

        /// <summary>
        /// Appends the specified decimal token to the expression
        /// </summary>
        /// <param name="decimalToken"></param>
        public void AppendToken(DecimalToken decimalToken)
        {
            if (_tokenLinkedList.Count == 0)
                _tokenLinkedList.AddLast(decimalToken.Clone());
            else
            {
                Token lastToken = _tokenLinkedList.Last.Value;

                /// Decimal token mark
                if (lastToken.Equals(Token.Decimal))
                {
                    if ((lastToken as DecimalToken).PeriodCount + decimalToken.PeriodCount == 2)
                        throw new ExpressionBuilderAppendPeriodDecimalTokenException();
                    _tokenLinkedList.Last.Value = DecimalToken.Concat(lastToken as DecimalToken, decimalToken);
                }

                else
                {
                    /// Closing parenthesis token mark
                    if (lastToken.Equals(ParanthesisToken.ClosingParenthesis))
                        throw new ExpressionBuilderAppendDecimalTokenException();

                    _tokenLinkedList.AddLast(decimalToken.Clone());
                }
            }

            savedOperatorToken = null;
            savedDecimalToken = null;
        }

        /// <summary>
        /// Appends the specified operator token to the expression
        /// </summary>
        /// <param name="operatorToken"></param>
        public void AppendToken(OperatorToken operatorToken)
        {
            if (_tokenLinkedList.Count == 0)
            {
                if (!operatorToken.Equals(OperatorToken.Addition) && !operatorToken.Equals(OperatorToken.Subtraction))
                    throw new ExpressionBuilderAppendMultiplicationOrDivisionOperatorTokenException();

                _tokenLinkedList.AddLast(operatorToken.Clone());
            }
            else
            {
                bool operatorTokenIsAdditionOrSubtraction = operatorToken.Equals(OperatorToken.Addition) || operatorToken.Equals(OperatorToken.Subtraction);
                LinkedListNode<Token> lastTokenListNode = _tokenLinkedList.Last;
                Token lastTokenListToken = lastTokenListNode.Value;

                /// Operator token mark
                if (lastTokenListToken.Equals(Token.Operator))
                {
                    if (_tokenLinkedList.Count == 1 && !operatorTokenIsAdditionOrSubtraction)
                        throw new ExpressionBuilderAppendMultiplicationOrDivisionOperatorTokenException();

                    if (_tokenLinkedList.Count > 1)
                    {
                        Token previousTokenListToken = lastTokenListNode.Previous.Value;
                        if (previousTokenListToken.Equals(ParanthesisToken.OpeningParenthesis) && !operatorTokenIsAdditionOrSubtraction)
                            throw new ExpressionBuilderAppendMultiplicationOrDivisionOperatorTokenException();
                    }

                    lastTokenListNode.Value = operatorToken.Clone();
                }

                /// Decimal token mark
                else if (lastTokenListToken.Equals(Token.Decimal))
                {
                    if (lastTokenListToken.Equals(Token.Decimal))
                        (lastTokenListToken as DecimalToken).Reformat();

                    _tokenLinkedList.AddLast(operatorToken.Clone());
                }

                /// Parenthesis token mark
                else if (lastTokenListToken.Equals(Token.Parenthesis))
                {
                    if (lastTokenListToken.Equals(ParanthesisToken.OpeningParenthesis) && !operatorTokenIsAdditionOrSubtraction)
                        throw new ExpressionBuilderAppendMultiplicationOrDivisionOperatorTokenException();

                    _tokenLinkedList.AddLast(operatorToken.Clone());
                }
            }

            savedOperatorToken = null;
            savedDecimalToken = null;
        }

        /// <summary>
        /// Returns the node before the last parantheses group
        /// </summary>
        /// <param name="tokenListNode"></param>
        /// <returns></returns>
        private static LinkedListNode<Token> NodeBeforeParanthesesGroup(LinkedListNode<Token> tokenListNode)
        {
            if (tokenListNode is null)
                throw new ArgumentNullException(nameof(tokenListNode));

            int nestedParenthesisLevel = 0;
            LinkedListNode<Token> previousTokenListNode = tokenListNode;

            do
            {
                Token previousTokenListToken = previousTokenListNode.Value;

                if (previousTokenListToken.Equals(ParanthesisToken.ClosingParenthesis))
                    nestedParenthesisLevel++;
                if (previousTokenListToken.Equals(ParanthesisToken.OpeningParenthesis))
                    nestedParenthesisLevel--;

                previousTokenListNode = previousTokenListNode.Previous;
            }
            while (previousTokenListNode is not null && nestedParenthesisLevel != 0);

            return previousTokenListNode;
        }

        private Token savedOperatorToken;
        private DecimalToken savedDecimalToken;
        private readonly LinkedList<Token> _tokenLinkedList;
    }
}
