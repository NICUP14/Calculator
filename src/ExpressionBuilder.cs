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
        /// Builds expressions from saved tokens and returns it in string format
        /// </summary>
        /// <param name="autocompleteParentheses"></param>
        /// <returns></returns>
        public string ToExpression(bool autocompleteParentheses = false)
        {
            int expressionStringBuilderCapacity = 0;

            int unclosedParenthesisCount = 0;
            if (autocompleteParentheses)
            {
                foreach (Token token in _tokenList)
                {
                    if (token.Equals(ParenthesisToken.OpeningParenthesis))
                        unclosedParenthesisCount++;
                    else if (token.Equals(ParenthesisToken.ClosingParenthesis))
                        unclosedParenthesisCount--;
                }

                /// Determine the length of the required closing parenthesis tokens
                expressionStringBuilderCapacity += unclosedParenthesisCount * ParenthesisToken.ClosingParenthesis.Length;
            }

            /// Determine the length of the saved tokens
            foreach (Token token in _tokenList)
                expressionStringBuilderCapacity += token.Length;

            StringBuilder expressionStringBuilder = new StringBuilder(expressionStringBuilderCapacity);


            /// Append saved tokens to string builder
            foreach (Token token in _tokenList)
                expressionStringBuilder.Append(token);

            if(autocompleteParentheses)
                /// Append required parenthesis to the string builder instance
                for (int parenthesisCount = 0; parenthesisCount < unclosedParenthesisCount; parenthesisCount++)
                    expressionStringBuilder.Append(ParenthesisToken.ClosingParenthesis);

            return expressionStringBuilder.ToString();
        }

        /// <summary>
        /// Removes all saved tokens from the expression builder instance
        /// </summary>
        public void Clear()
        {
            _tokenList.Clear();
        }

        /// <summary>
        /// Changes the sign of the last token or parenthesis group
        /// </summary>
        public void ChangeSign()
        {
            if (_tokenList.Count == 0)
                _tokenList.AddLast(OperatorToken.Subtraction);
            else
            {
                LinkedListNode<Token> lastTokenListNode = _tokenList.Last;
                Token lastTokenListToken = lastTokenListNode.Value;

                /// Operator token
                if (lastTokenListToken.Equals(Token.Operator))
                {
                    /// Multiplication and division operator tokens
                    if (!lastTokenListToken.Equals(OperatorToken.Addition) && !lastTokenListToken.Equals(OperatorToken.Subtraction))
                    {
                        _tokenList.AddLast(ParenthesisToken.OpeningParenthesis);
                        _tokenList.AddLast(OperatorToken.Subtraction);
                    }
                    else
                        /// Adition and subtration operator tokens
                        lastTokenListNode.Value = lastTokenListToken.Equals(OperatorToken.Addition) ? OperatorToken.Subtraction : OperatorToken.Addition;
                }

                /// Decimal token
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

                /// Parenthesis token
                else if (lastTokenListToken.Equals(Token.Parenthesis))
                {
                    /// Opening parenthesis token
                    if (lastTokenListToken.Equals(ParenthesisToken.OpeningParenthesis))
                        _tokenList.AddLast(OperatorToken.Subtraction);

                    /// Closing parenthesis token
                    else if (lastTokenListToken.Equals(ParenthesisToken.ClosingParenthesis))
                    {
                        int nestedParenthesisLevel = 0;
                        LinkedListNode<Token> tokenListNode = lastTokenListNode;

                        do
                        {
                            if (tokenListNode.Value.Equals(ParenthesisToken.ClosingParenthesis))
                                nestedParenthesisLevel++;
                            if (tokenListNode.Value.Equals(ParenthesisToken.OpeningParenthesis))
                                nestedParenthesisLevel--;
                            tokenListNode = tokenListNode.Previous;
                        }
                        while (tokenListNode != null && nestedParenthesisLevel != 0);

                        if (tokenListNode == null)
                            _tokenList.AddFirst(OperatorToken.Subtraction);
                        else
                        {
                            Token tokenListToken = tokenListNode.Value;

                            if (tokenListToken.Equals(ParenthesisToken.OpeningParenthesis))
                                _tokenList.AddAfter(tokenListNode, OperatorToken.Subtraction);
                            else if (tokenListToken.Equals(OperatorToken.Addition) || tokenListToken.Equals(OperatorToken.Subtraction))
                                tokenListNode.Value = tokenListToken.Equals(OperatorToken.Addition) ? OperatorToken.Subtraction : OperatorToken.Addition;
                            else
                            {
                                _tokenList.AddAfter(tokenListNode, OperatorToken.Subtraction);
                                _tokenList.AddAfter(tokenListNode, ParenthesisToken.OpeningParenthesis);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        public void InsertParenthesis()
        {
            if (_tokenList.Count == 0)
                _tokenList.AddLast(ParenthesisToken.OpeningParenthesis);
            else
            {
                LinkedListNode<Token> lastTokenListNode = _tokenList.Last;
                Token lastTokenListToken = lastTokenListNode.Value;

                /// Operator token and opening parenthesis token
                if (lastTokenListToken.Equals(Token.Operator) || lastTokenListToken.Equals(ParenthesisToken.OpeningParenthesis))
                {
                    _tokenList.AddLast(ParenthesisToken.OpeningParenthesis);
                }
                else
                {
                    int unclosedParenthesisCount = 0;
                    foreach (Token token in _tokenList)
                    {
                        if (token.Equals(ParenthesisToken.OpeningParenthesis))
                            unclosedParenthesisCount++;
                        if (token.Equals(ParenthesisToken.ClosingParenthesis))
                            unclosedParenthesisCount--;
                    }

                    if (unclosedParenthesisCount > 0)
                    {
                        if (lastTokenListToken.Equals(Token.Decimal) && (lastTokenListToken as DecimalToken).EndsWithPeriod())
                            throw new ExpressionBuilderInsertParenthesisError();

                        _tokenList.AddLast(ParenthesisToken.ClosingParenthesis);
                    }
                    else
                    {
                        /// Decimal token
                        if (lastTokenListToken.Equals(Token.Decimal))
                            _tokenList.AddBefore(lastTokenListNode, ParenthesisToken.OpeningParenthesis);

                        /// Closing parenthesis token
                        else if (lastTokenListToken.Equals(ParenthesisToken.ClosingParenthesis))
                        {
                            int nestedParenthesisLevel = 0;
                            LinkedListNode<Token> tokenListNode = lastTokenListNode;

                            do
                            {
                                if (tokenListNode.Value.Equals(ParenthesisToken.ClosingParenthesis))
                                    nestedParenthesisLevel++;
                                if (tokenListNode.Value.Equals(ParenthesisToken.OpeningParenthesis))
                                    nestedParenthesisLevel--;
                                tokenListNode = tokenListNode.Previous;
                            }
                            while (tokenListNode != null && nestedParenthesisLevel != 0);
                            _tokenList.AddAfter(tokenListNode ?? _tokenList.First, ParenthesisToken.OpeningParenthesis);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Appends decimal token to the list of saved tokens
        /// </summary>
        /// <param name="decimalToken"></param>
        public void AppendToken(DecimalToken decimalToken)
        {
            if (_tokenList.Count == 0)
            {
                if (decimalToken.EndsWithPeriod())
                    throw new ExpressionBuilderAppendTokenError();

                _tokenList.AddLast(decimalToken);
            }
            else
            {
                Token lastToken = _tokenList.Last.Value;

                /// Closing parenthesis token
                if (lastToken.Equals(ParenthesisToken.ClosingParenthesis))
                    throw new ExpressionBuilderAppendTokenError();

                /// Decimal token
                if (lastToken.Equals(Token.Decimal))
                {
                    if ((lastToken as DecimalToken).PeriodCount + decimalToken.PeriodCount == 2)
                        throw new ExpressionBuilderAppendTokenError();
                    _tokenList.Last.Value = DecimalToken.Concatenate(lastToken as DecimalToken, decimalToken);
                }

                /// Any other token
                else
                {
                    if (decimalToken.EndsWithPeriod())
                        throw new ExpressionBuilderAppendTokenError();

                    _tokenList.AddLast(decimalToken);
                }
            }
        }

        /// <summary>
        /// Appends the string representation of a specified operator token to this instance
        /// </summary>
        /// <param name="operatorToken"></param>
        public void AppendToken(OperatorToken operatorToken)
        {
            ///

            if (_tokenList.Count == 0)
            {
                /// Addition and subtraction operator token (special case)
                if (operatorToken.Equals(OperatorToken.Addition) && operatorToken.Equals(OperatorToken.Subtraction))
                    _tokenList.AddLast(operatorToken);
            }
            else
            {
                bool operatorTokenIsNotAdditionOrSubtraction = !operatorToken.Equals(OperatorToken.Addition) && !operatorToken.Equals(OperatorToken.Subtraction);
                LinkedListNode<Token> lastTokenListNode = _tokenList.Last;
                Token lastTokenFromList = lastTokenListNode.Value;

                /// Operator token
                if (lastTokenFromList.Equals(Token.Operator))
                {
                    if (_tokenList.Count > 1)
                    {
                        Token previousTokenListToken = lastTokenListNode.Previous.Value;
                        if (previousTokenListToken.Equals(ParenthesisToken.OpeningParenthesis) && operatorTokenIsNotAdditionOrSubtraction)
                            throw new ExpressionBuilderAppendTokenError();
                    }

                    lastTokenListNode.Value = operatorToken;
                }

                /// Decimal token
                else if (lastTokenFromList.Equals(Token.Decimal))
                {
                    if (!(lastTokenFromList as DecimalToken).EndsWithPeriod())
                        _tokenList.AddLast(operatorToken);
                }

                /// Parenthesis token
                else if (lastTokenFromList.Equals(Token.Parenthesis))
                {
                    if (lastTokenFromList.Equals(ParenthesisToken.OpeningParenthesis) && operatorTokenIsNotAdditionOrSubtraction)
                        throw new ExpressionBuilderAppendTokenError();
                    _tokenList.AddLast(operatorToken);
                }
            }
        }

        readonly LinkedList<Token> _tokenList;
    }
}
