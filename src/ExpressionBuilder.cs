using System.Text;
using System.Collections.Generic;

namespace Calculator
{
    /// <summary>
    /// Expression manipulator implementation
    /// WARNING: This is still in development!
    /// </summary>
    class ExpressionBuilder : ExpressionParser
    {
        public ExpressionBuilder(string expression = "")
        {
            _tokenList = new LinkedList<Token>();

            /// Converts expression to an intermediate to a token linked list
            if (!string.IsNullOrEmpty(expression))
            {
                Token[] tokenArray = ExpressionParser.ConvertExpressionToTokenArray(expression);
                foreach (Token token in tokenArray)
                    _tokenList.AddLast(token);
            }
        }

        public string ToExpression(bool autocompleteParentheses = false)
        {
            int expressionStringBuilderCapacity = 0;

            int unclosedParenthesisCount = 0;
            if (autocompleteParentheses)
            {
                foreach (Token token in _tokenList)
                {
                    if (token == ParenthesisToken.OpeningParenthesis)
                        unclosedParenthesisCount++;
                    else if (token == ParenthesisToken.ClosingParenthesis)
                        unclosedParenthesisCount--;
                }
                expressionStringBuilderCapacity += unclosedParenthesisCount * ParenthesisToken.ClosingParenthesis.Length;
            }

            /// Initialize string builder
            foreach (Token token in _tokenList)
                expressionStringBuilderCapacity += token.ToString().Length;
            StringBuilder expressionStringBuilder = new StringBuilder(expressionStringBuilderCapacity);


            /// Build string representation
            foreach (Token token in _tokenList)
                expressionStringBuilder.Append(token);

            if(autocompleteParentheses)
                for (int parenthesisCount = 0; parenthesisCount < unclosedParenthesisCount; parenthesisCount++)
                    expressionStringBuilder.Append(ParenthesisToken.ClosingParenthesis);

            return expressionStringBuilder.ToString();
        }

        public void Clear()
        {
            _tokenList.Clear();
        }

        public void InsertParenthesis()
        {
            if (_tokenList.Count == 0)
                _tokenList.AddLast(ParenthesisToken.OpeningParenthesis);
            else
            {
                LinkedListNode<Token> lastTokenListNode = _tokenList.Last;
                Token lastTokenListToken = lastTokenListNode.Value;

                if (lastTokenListToken is OperatorToken || lastTokenListToken == ParenthesisToken.OpeningParenthesis)
                    _tokenList.AddLast(ParenthesisToken.OpeningParenthesis);
                else
                {
                    int unclosedParenthesisCount = 0;
                    foreach (Token token in _tokenList)
                    {
                        if (token == ParenthesisToken.OpeningParenthesis)
                            unclosedParenthesisCount++;
                        else if (token == ParenthesisToken.ClosingParenthesis)
                            unclosedParenthesisCount--;
                    }

                    if (unclosedParenthesisCount > 0)
                        _tokenList.AddLast(ParenthesisToken.ClosingParenthesis);
                    else
                    {
                        if (lastTokenListToken is DecimalToken)
                            _tokenList.AddBefore(lastTokenListNode, ParenthesisToken.OpeningParenthesis);
                        else if (lastTokenListToken == ParenthesisToken.ClosingParenthesis)
                        {
                            int nestedParenthesisLevel = 0;
                            LinkedListNode<Token> tokenListNode = lastTokenListNode;

                            do
                            {
                                if (tokenListNode.Value == ParenthesisToken.ClosingParenthesis)
                                    nestedParenthesisLevel++;
                                else if (tokenListNode.Value == ParenthesisToken.OpeningParenthesis)
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

        public void AppendDecimalToken(DecimalToken decimalToken)
        {
            if (_tokenList.Count == 0)
                _tokenList.AddLast(decimalToken);
            else
            {
                Token lastToken = _tokenList.Last.Value;

                if (lastToken == ParenthesisToken.ClosingParenthesis)
                    return;

                if(lastToken is not DecimalToken)
                    _tokenList.AddLast(decimalToken);
                else
                    _tokenList.Last.Value = DecimalToken.Concatenate(lastToken as DecimalToken, decimalToken);
            }
        }

        public void AppendOperatorToken(OperatorToken operatorToken)
        {
            
            bool operatorTokenIsNotAdditionOrSubtraction = operatorToken != OperatorToken.Addition && operatorToken != OperatorToken.Subtraction;

            if(_tokenList.Count == 0)
            {
                if (operatorTokenIsNotAdditionOrSubtraction)
                    return;

                _tokenList.AddLast(operatorToken);
            }
            else
            {
                LinkedListNode<Token> lastTokenListNode = _tokenList.Last;
                Token lastTokenFromList = lastTokenListNode.Value;

                if (lastTokenFromList is OperatorToken)
                {
                    if(_tokenList.Count > 1)
                    {
                        Token previousTokenListToken = lastTokenListNode.Previous.Value;

                        if (previousTokenListToken == ParenthesisToken.OpeningParenthesis && operatorTokenIsNotAdditionOrSubtraction)
                            return;
                    }    

                    lastTokenListNode.Value = operatorToken;
                }
                else if (lastTokenFromList is DecimalToken)
                {
                    if ((lastTokenFromList as DecimalToken).EndsWithPeriod())
                        return;

                    _tokenList.AddLast(operatorToken);
                }
                else if(lastTokenFromList is ParenthesisToken)
                {
                    if (lastTokenFromList == ParenthesisToken.OpeningParenthesis && operatorTokenIsNotAdditionOrSubtraction)
                        return;

                    _tokenList.AddLast(operatorToken);
                }
            }
        }

        readonly LinkedList<Token> _tokenList;
    }
}
