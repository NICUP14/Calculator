namespace Calculator_backend
{
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
