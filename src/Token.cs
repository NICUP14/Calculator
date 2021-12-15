namespace Calculator
{
    /// Rewrite the token type checking system (token.Equals(token2))
    /// Instead of using functions, compare with predefined token constants

    static class TokenStringRepresentation
    {
        public const string OpeningParenthesisToken =     "(";
        public const string ClosingParenthesisToken =     ")";
        public const string AdditionOperatorToken =       "+";
        public const string SubtractionOperatorToken =    "-";
        public const string MultiplicationOperatorToken = "×";
        public const string DivisionOperatorToken =       "÷";
    }

    class Token
    {
        protected Token(TokenType tokenType, string stringRepresentation = "")
        {
            _tokenType = tokenType;
            _stringRepresentation = stringRepresentation;
        }

        public bool HasTokenType(TokenType tokenType)
        {
            return this._tokenType == tokenType;
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
            return _stringRepresentation;
        }

        public enum TokenType
        {
            Undefined,
            Decimal,
            Operator,
            Parenthesis
        }

        public int Length
        {
            get
            {
                return _stringRepresentation.Length;
            }
        }

        private TokenType _tokenType;
        public string _stringRepresentation;

        public static readonly Token Undefined = new Token(TokenType.Undefined);
    }
}
