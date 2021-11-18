namespace Calculator
{
    static class TokenRepresentation
    {
        public const string OpeningParenthesisToken =     "(";
        public const string ClosingParenthesisToken =     ")";
        public const string AdditionOperatorToken =       "+";
        public const string SubtractionOperatorToken =    "-";
        public const string MultiplicationOperatorToken = "×";
        public const string DivisionOperatorToken =       "/";
    }

    class Token
    {
        protected Token(TokenType tokenType)
        {
            _tokenType = tokenType;
        }

        public bool IsUndefined()
        {
            return _tokenType == Token.TokenType.Undefined;
        }

        public bool IsDecimalToken()
        {
            return _tokenType == TokenType.Decimal;
        }

        public bool IsOperatorToken()
        {
            return _tokenType == TokenType.Operator;
        }

        public bool IsParenthesisToken()
        {
            return _tokenType == Token.TokenType.Parenthesis;
        }

        public override string ToString()
        {
            return _tokenType.ToString();
        }

        public enum TokenType
        {
            Undefined,
            Decimal,
            Operator,
            Parenthesis
        }

        private TokenType _tokenType;

        public static readonly Token Undefined = new Token(TokenType.Undefined);
    }
}
