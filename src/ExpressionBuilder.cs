using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace Calculator
{
    /// <summary>
    /// Represents an abstract, mutable array of tokens. This class cannot be inherited
    /// </summary>
    sealed class ExpressionBuilder
    {
        /// <summary>
        /// Initializes a new instance of the ExpressionBuilder class
        /// </summary>
        public ExpressionBuilder()
        {
            _tokenLinkedList = new LinkedList<Token>();
        }

        /// <summary>
        /// Returns a string that represents the current ExpressionBuilder instance
        /// </summary>
        public override string ToString()
        {
            if (_tokenLinkedList.Count == 0)
                return string.Empty;

            /// Determines the required capacity of the StringBuilder instance
            int stringRepresentationBuilderCapacity = Enumerable.Sum(_tokenLinkedList, token => token.Length);

            StringBuilder stringRepresentationBuilder = new(stringRepresentationBuilderCapacity);
            stringRepresentationBuilder.AppendJoin(string.Empty, _tokenLinkedList);

            return stringRepresentationBuilder.ToString();
        }

        /// <summary>
        /// Returns an array of tokens that represents the current ExpressionBuilder instance
        /// </summary>
        /// <returns></returns>
        public Token[] ToTokenArray()
        {
            if (_tokenLinkedList.Count == 0)
                return Array.Empty<Token>();

            /// Determines the number of unclosed opening parentheses
            int tokenArrayLength = _tokenLinkedList.Count;
            tokenArrayLength += _tokenLinkedList.Count(token => token.Equals(ParenthesisToken.OpeningParenthesis));
            tokenArrayLength -= _tokenLinkedList.Count(token => token.Equals(ParenthesisToken.ClosingParenthesis));

            Token[] tokenArray = new Token[tokenArrayLength];
            _tokenLinkedList.CopyTo(tokenArray, 0);

            /// Appends the required closing parentheses to validate the array of tokens representation
            for (int tokenListRange = _tokenLinkedList.Count; tokenListRange < tokenArrayLength; tokenListRange++)
                tokenArray[tokenListRange] = ParenthesisToken.ClosingParenthesis;

            return tokenArray;
        }

        /// <summary>
        /// Removes all the tokens from the ExpressionBuilder instance
        /// </summary>
        public void Clear()
        {
            _tokenLinkedList.Clear();
        }

        /// <summary>
        /// Appends the required closing parentheses to validate expression 
        /// </summary>
        public void CompleteParentheses()
        {
            /// Determines the number of unclosed OpeningParentheses tokens present in the LinkedList
            int unclosedParenthesisCount = 0;
            unclosedParenthesisCount += _tokenLinkedList.Count(token => token.Equals(ParenthesisToken.OpeningParenthesis));
            unclosedParenthesisCount -= _tokenLinkedList.Count(token => token.Equals(ParenthesisToken.ClosingParenthesis));

            /// Appends the required ClosingParentheses to validate the linked list representation
            for (int tokenListRange = 0; tokenListRange < unclosedParenthesisCount; tokenListRange++)
                _tokenLinkedList.AddLast(ParenthesisToken.ClosingParenthesis.Clone());
        }

        /// <summary>
        /// Removes the trailing character of the last token present in the abstract array
        /// </summary>
        /// <exception cref="ExpressionBuilderRemoveTrailingCharacterException"></exception>
        public void RemoveTrailingCharacter()
        {
            if (_tokenLinkedList.Count == 0)
                throw new ExpressionBuilderRemoveTrailingCharacterException();

            Token lastTokenListToken = _tokenLinkedList.Last();
            lastTokenListToken.RemoveTrailingCharacter();

            /// Determines whether to remove the last token in the LinkedList
            if (lastTokenListToken.Length == 0)
                _tokenLinkedList.RemoveLast();

            /// Breaks the consecutive operation chain
            savedOperatorToken = null;
            savedDecimalToken = null;
        }

        /// <summary>
        /// Wraps the ExpressionParser.Calculate method.
        /// Enables the use of consecutive operation chains
        /// </summary>
        /// <returns></returns>
        public Decimal Calculate()
        {
            if (_tokenLinkedList.Count == 0)
                return Decimal.Zero;

            /// Wraps ExpressionParser.Calculate(ToTokenArray()) for readability purposes
            Func<Decimal> calculateTokenArray = () => ExpressionParser.Calculate(ToTokenArray());

            Token lastTokenListToken = _tokenLinkedList.Last();

            if (lastTokenListToken.Equals(Token.Decimal))
                (lastTokenListToken as DecimalToken).Reformat();

            /// Detects whether the expression is a consecutive operation chain
            else if (lastTokenListToken.Equals(Token.Operator))
            {
                _tokenLinkedList.RemoveLast();

                savedDecimalToken = new(calculateTokenArray());
                savedOperatorToken = lastTokenListToken as OperatorToken;
            }

            /// Checks if there is an active consecutive operation chain
            if (savedOperatorToken is not null && savedDecimalToken is not null)
            {
                DecimalToken expressionResultDecimalToken = new(calculateTokenArray());

                /// Advances the consecutive operation chain
                _tokenLinkedList.Clear();
                _tokenLinkedList.AddLast(expressionResultDecimalToken);
                _tokenLinkedList.AddLast(savedOperatorToken);
                _tokenLinkedList.AddLast(savedDecimalToken);
            }

            return calculateTokenArray();
        }

        /// <summary>
        /// Negates the sign of the last token or parenthesis group present in the abstract array
        /// </summary>
        public void ChangeSign()
        {
            if (_tokenLinkedList.Count == 0)
                _tokenLinkedList.AddLast(OperatorToken.Subtraction);

            else
            {
                LinkedListNode<Token> lastTokenListNode = _tokenLinkedList.Last;
                Token lastTokenListToken = lastTokenListNode.Value;

                /// Checks whether if the last token is an OperatorToken
                if (lastTokenListToken.Equals(Token.Operator))
                {
                    /// Handles the OperatorToken.Addition and OperatorToken.Subtraction case
                    if (lastTokenListToken.Equals(OperatorToken.Addition) || lastTokenListToken.Equals(OperatorToken.Subtraction))
                        lastTokenListNode.Value = (lastTokenListToken.Equals(OperatorToken.Addition) ? OperatorToken.Subtraction : OperatorToken.Addition).Clone();

                    /// Handles the OperatorToken.Multiplication and OperatorToken.Division case
                    else
                    {
                        _tokenLinkedList.AddLast(ParenthesisToken.OpeningParenthesis.Clone());
                        _tokenLinkedList.AddLast(OperatorToken.Subtraction.Clone());
                    }
                }

                /// Checks whether if the last token is a DecimalToken
                else if (lastTokenListToken.Equals(Token.Decimal))
                {
                    if (_tokenLinkedList.Count == 1)
                        _tokenLinkedList.AddFirst(OperatorToken.Subtraction.Clone());

                    else
                    {
                        LinkedListNode<Token> penultimateTokenListNode = lastTokenListNode.Previous;
                        Token penultimateTokenListToken = penultimateTokenListNode.Value;

                        /// Handles the ParanthesisToken.OpeningParenthesis before the last token case
                        if (penultimateTokenListToken.Equals(ParenthesisToken.OpeningParenthesis))
                            _tokenLinkedList.AddAfter(penultimateTokenListNode, OperatorToken.Subtraction.Clone());

                        /// Handles the OperatorToken.Addition or OperatorToken.Subtraction before the last token case
                        else if (penultimateTokenListToken.Equals(OperatorToken.Addition) || penultimateTokenListToken.Equals(OperatorToken.Subtraction))
                            penultimateTokenListNode.Value = (penultimateTokenListToken.Equals(OperatorToken.Addition) ? OperatorToken.Subtraction : OperatorToken.Addition).Clone();

                        /// Handles the OperatorToken.Multiplication or OperatorToken.Division before the last token case
                        else if (penultimateTokenListToken.Equals(OperatorToken.Division) || penultimateTokenListToken.Equals(OperatorToken.Multiplication))
                        {
                            _tokenLinkedList.AddAfter(penultimateTokenListNode, OperatorToken.Subtraction.Clone());
                            _tokenLinkedList.AddAfter(penultimateTokenListNode, ParenthesisToken.OpeningParenthesis.Clone());
                        }
                    }
                }

                /// Checks whether if the last token is a ParenthesisToken
                else if (lastTokenListToken.Equals(Token.Parenthesis))
                {
                    /// Handles the ParenthesisToken.OpeningParanthesis case
                    if (lastTokenListToken.Equals(ParenthesisToken.OpeningParenthesis))
                        _tokenLinkedList.AddLast(OperatorToken.Subtraction.Clone());

                    /// Handles the ParenthesisToken.ClosingParenthesis case
                    else if (lastTokenListToken.Equals(ParenthesisToken.ClosingParenthesis))
                    {
                        LinkedListNode<Token> precedentTokenListNode = NodeBeforeParenthesesGroup(lastTokenListNode);

                        if (precedentTokenListNode is null)
                            _tokenLinkedList.AddFirst(OperatorToken.Subtraction.Clone());

                        else
                        {
                            Token precedentTokenListToken = precedentTokenListNode.Value;

                            /// Handles the ParenthesisToken.ClosingParenthesis before the parenthesis group case
                            if (precedentTokenListToken.Equals(ParenthesisToken.OpeningParenthesis))
                                _tokenLinkedList.AddAfter(precedentTokenListNode, OperatorToken.Subtraction.Clone());

                            /// Handles the OperatorToken.Addition or OperatorToken.Subtraction before the parenthesis group case
                            else if (precedentTokenListToken.Equals(OperatorToken.Addition) || precedentTokenListToken.Equals(OperatorToken.Subtraction))
                                precedentTokenListNode.Value = (precedentTokenListToken.Equals(OperatorToken.Addition) ? OperatorToken.Subtraction : OperatorToken.Addition).Clone();

                            /// Handles the OperatorToken.Multiplication or OperatorToken.Division before the parenthesis group case
                            else if (precedentTokenListToken.Equals(OperatorToken.Division) || precedentTokenListToken.Equals(OperatorToken.Multiplication))
                            {
                                _tokenLinkedList.AddAfter(precedentTokenListNode, OperatorToken.Subtraction.Clone());
                                _tokenLinkedList.AddAfter(precedentTokenListNode, ParenthesisToken.OpeningParenthesis.Clone());
                            }
                        }
                    }
                }
            }

            /// Breaks the consecutive operation chain
            savedOperatorToken = null;
            savedDecimalToken = null;
        }

        /// <summary>
        /// Inserts a ParenthesisToken at the best predicted position in the abstract array.
        /// The prediction is based on the last token or parenthesis group present in the abstract array and the number of unclosed ParenthesisToken.OpeningParenthesis tokens
        /// </summary>
        public void InsertParenthesis()
        {
            if (_tokenLinkedList.Count == 0)
                _tokenLinkedList.AddLast(ParenthesisToken.OpeningParenthesis.Clone());

            else
            {
                LinkedListNode<Token> lastTokenListNode = _tokenLinkedList.Last;
                Token lastTokenListToken = lastTokenListNode.Value;

                /// Checks whether if the last token is an OperatorToken or a ParenthesisToken.OpeningParenthesis
                if (lastTokenListToken.Equals(Token.Operator) || lastTokenListToken.Equals(ParenthesisToken.OpeningParenthesis))
                    _tokenLinkedList.AddLast(ParenthesisToken.OpeningParenthesis.Clone());
                else
                {
                    /// Determines the number of unclosed ParenthesisToken.OpeningParentheses tokens present in the LinkedList
                    int unclosedParenthesisCount = 0;
                    unclosedParenthesisCount += _tokenLinkedList.Count(token => token.Equals(ParenthesisToken.OpeningParenthesis));
                    unclosedParenthesisCount -= _tokenLinkedList.Count(token => token.Equals(ParenthesisToken.ClosingParenthesis));

                    /// Checks whether if there are unclosed ParenthesisToken.OpeningParenthesis
                    if (unclosedParenthesisCount > 0)
                    {
                        if (lastTokenListToken.Equals(Token.Decimal))
                            (lastTokenListToken as DecimalToken).Reformat();

                        _tokenLinkedList.AddLast(ParenthesisToken.ClosingParenthesis.Clone());
                    }

                    else
                    {
                        /// Handles the DecimalToken case
                        if (lastTokenListToken.Equals(Token.Decimal))
                            _tokenLinkedList.AddBefore(lastTokenListNode, ParenthesisToken.OpeningParenthesis.Clone());

                        /// Handles the ParenthesisToken.ClosingParenthesis case
                        else if (lastTokenListToken.Equals(ParenthesisToken.ClosingParenthesis))
                        {
                            LinkedListNode<Token> previousTokenListNode = NodeBeforeParenthesesGroup(lastTokenListNode) ?? _tokenLinkedList.First;

                            _tokenLinkedList.AddAfter(previousTokenListNode, ParenthesisToken.OpeningParenthesis.Clone());
                        }
                    }
                }
            }

            /// Breaks the consecutive operation chain
            savedOperatorToken = null;
            savedDecimalToken = null;
        }

        /// <summary>
        /// Appends the specified DecimalToken to the abstract array
        /// </summary>
        /// <param name="decimalToken"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ExpressionBuilderAppendPeriodDecimalTokenException"></exception>
        /// <exception cref="ExpressionBuilderAppendDecimalTokenException"></exception>
        public void AppendToken(DecimalToken decimalToken)
        {
            if (decimalToken is null)
                throw new ArgumentNullException(nameof(decimalToken));

            if (_tokenLinkedList.Count == 0)
                _tokenLinkedList.AddLast(decimalToken.Clone());

            else
            {
                Token lastToken = _tokenLinkedList.Last.Value;

                /// Handles the DecimalToken case
                if (lastToken.Equals(Token.Decimal))
                {
                    if ((lastToken as DecimalToken).PeriodCount + decimalToken.PeriodCount == 2)
                        throw new ExpressionBuilderAppendPeriodDecimalTokenException();

                    _tokenLinkedList.Last.Value = DecimalToken.Concat(lastToken as DecimalToken, decimalToken);
                }

                else
                {
                    /// Handles the ParenthesisToken.ClosingParenthesis
                    if (lastToken.Equals(ParenthesisToken.ClosingParenthesis))
                        throw new ExpressionBuilderAppendDecimalTokenException();

                    /// Handles the non-DecimalToken case
                    _tokenLinkedList.AddLast(decimalToken.Clone());
                }
            }

            /// Breaks the consecutive operation chain
            savedOperatorToken = null;
            savedDecimalToken = null;
        }

        /// <summary>
        /// Appends the specified OperatorToken to the abstract array
        /// </summary>
        /// <param name="operatorToken"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ExpressionBuilderAppendMultiplicationOrDivisionOperatorTokenException"></exception>
        public void AppendToken(OperatorToken operatorToken)
        {
            if (operatorToken is null)
                throw new ArgumentNullException(nameof(operatorToken));

            if (_tokenLinkedList.Count == 0)
            {
                /// Handles the OperatorToken.Multiplication and OperatorToken.Division on an empty LinkedList case
                if (!operatorToken.Equals(OperatorToken.Addition) && !operatorToken.Equals(OperatorToken.Subtraction))
                    throw new ExpressionBuilderAppendMultiplicationOrDivisionOperatorTokenException();

                _tokenLinkedList.AddLast(operatorToken.Clone());
            }
            else
            {
                bool operatorTokenIsAdditionOrSubtraction = operatorToken.Equals(OperatorToken.Addition) || operatorToken.Equals(OperatorToken.Subtraction);

                LinkedListNode<Token> lastTokenListNode = _tokenLinkedList.Last;
                Token lastTokenListToken = lastTokenListNode.Value;

                /// Checks whether if the last token is an OperatorToken
                if (lastTokenListToken.Equals(Token.Operator))
                {
                    /// Handles the OperatorToken.Multiplication and OperatorToken.Division as the only OperatorToken present in the LinkedList case
                    if (_tokenLinkedList.Count == 1 && !operatorTokenIsAdditionOrSubtraction)
                        throw new ExpressionBuilderAppendMultiplicationOrDivisionOperatorTokenException();

                    if (_tokenLinkedList.Count > 1)
                    {
                        Token precedentTokenListToken = lastTokenListNode.Previous.Value;

                        /// Handles the OperatorToken.Multiplication and OperatorToken.Division after a ParenthesisToken.OpeningParenthesis token case
                        if (precedentTokenListToken.Equals(ParenthesisToken.OpeningParenthesis) && !operatorTokenIsAdditionOrSubtraction)
                            throw new ExpressionBuilderAppendMultiplicationOrDivisionOperatorTokenException();
                    }

                    lastTokenListNode.Value = operatorToken.Clone();
                }

                /// Checks whether if the last token is a DecimalToken
                else if (lastTokenListToken.Equals(Token.Decimal))
                {
                    if (lastTokenListToken.Equals(Token.Decimal))
                        (lastTokenListToken as DecimalToken).Reformat();

                    _tokenLinkedList.AddLast(operatorToken.Clone());
                }

                /// Checks whether if the last token is a ParenthesisToken
                else if (lastTokenListToken.Equals(Token.Parenthesis))
                {
                    /// Handles the OperatorToken.Multiplication and OperatorToken.Division after a ParenthesisToken.OpeningParenthesis token case
                    if (lastTokenListToken.Equals(ParenthesisToken.OpeningParenthesis) && !operatorTokenIsAdditionOrSubtraction)
                        throw new ExpressionBuilderAppendMultiplicationOrDivisionOperatorTokenException();

                    _tokenLinkedList.AddLast(operatorToken.Clone());
                }
            }

            /// Breaks the consecutive operation chain
            savedOperatorToken = null;
            savedDecimalToken = null;
        }

        /// <summary>
        /// Returns the LinkedListNode situated in front of the parenthesis group ended by the specified LinkedListNode.
        /// The LinkedListNode should point to a ParenthesisToken.ClosingParenthesis
        /// </summary>
        /// <param name="tokenListNode"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        private static LinkedListNode<Token> NodeBeforeParenthesesGroup(LinkedListNode<Token> tokenListNode)
        {
            if (tokenListNode is null)
                throw new ArgumentNullException(nameof(tokenListNode));

            int nestedParenthesisLevel = 0;
            LinkedListNode<Token> precedentTokenListNode = tokenListNode;

            do
            {
                Token precedentTokenListToken = precedentTokenListNode.Value;

                nestedParenthesisLevel += Convert.ToInt32(precedentTokenListToken.Equals(ParenthesisToken.ClosingParenthesis));
                nestedParenthesisLevel -= Convert.ToInt32(precedentTokenListToken.Equals(ParenthesisToken.OpeningParenthesis));

                precedentTokenListNode = precedentTokenListNode.Previous;
            }
            while (precedentTokenListNode is not null && nestedParenthesisLevel != 0);

            return precedentTokenListNode;
        }

        private OperatorToken savedOperatorToken = null;
        private DecimalToken savedDecimalToken = null;
        private readonly LinkedList<Token> _tokenLinkedList;
    }
}
