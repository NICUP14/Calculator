using System.Text;

namespace Calculator
{
    /// <summary>
    /// Expression manipulator implementation
    /// WARNING: This is still in development, do not use it!
    /// </summary>
    class ExpressionManipulator : ExpressionParser
    {
        public ExpressionManipulator(int expressionBuilderCapacity)
        {
            _expressionStringBuilder = new StringBuilder(expressionBuilderCapacity);
        }

        public override string ToString()
        {
            return _expressionStringBuilder.ToString();
        }

        public void Clear()
        {
            _expressionStringBuilder.Clear();
        }

        public void AppendDigitOrPeriod(char chr)
        {
            if (_expressionStringBuilder.Length != 0)
            {
                Token[] tokenArray = ConvertExpressionToTokenArray(_expressionStringBuilder.ToString());
                //Token lastToken = getLastToken(tokenArray);
                //if (lastToken.IsParenthesisToken() && (lastToken as ParenthesisToken).IsClosingParenthesisToken())
                    throw new ExpressionBuilderAppendError();
            }
            _expressionStringBuilder.Append(chr);
        }

        public void AppendOperator(OperatorToken operatorToken)
        {
            bool operatorTokenIsAdditionOrSubtractionOperatorToken = operatorToken.IsAdditionOperatorToken() || operatorToken.IsSubtractionOperatorToken();
            if (_expressionStringBuilder.Length == 0)
            {
                if (operatorTokenIsAdditionOrSubtractionOperatorToken)
                    throw new ExpressionBuilderAppendError();
            }
            else
            {
                Token[] tokenArray = ConvertExpressionToTokenArray(_expressionStringBuilder.ToString());
                Token lastToken = tokenArray[tokenArray.Length - 1];
                if (lastToken.IsOperatorToken())
                    throw new ExpressionBuilderAppendError();
                else
                {
                    if (lastToken.IsParenthesisToken() && (lastToken as ParenthesisToken).IsOpeningParenthesisToken() && !operatorTokenIsAdditionOrSubtractionOperatorToken)
                        throw new ExpressionBuilderAppendError();
                }
            }
            _expressionStringBuilder.Append(operatorToken);
        }

        private Token getLastNonNullToken(Token[] tokenArray)
        {
            Token lastToken = Token.Undefined;
            foreach (Token token in tokenArray)
                if (token != null)
                    lastToken = token;
            return lastToken;
        }

        StringBuilder _expressionStringBuilder;
    }
}
