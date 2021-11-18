namespace Calculator
{
    class ParenthesisToken : Token
    {
        private ParenthesisToken(ParenthesisTokenType parenthesisTokenType) : base(TokenType.Parenthesis)
        {
            _parenthesisTokenType = parenthesisTokenType;
        }

        public bool IsOpeningParenthesisToken()
        {
            return _parenthesisTokenType == ParenthesisTokenType.OpeningParenthesis;
        }

        public bool IsClosingParenthesisToken()
        {
            return _parenthesisTokenType == ParenthesisTokenType.ClosingParenthesis;
        }

        public override string ToString()
        {
            if (_parenthesisTokenType == ParenthesisTokenType.OpeningParenthesis)
                return TokenRepresentation.OpeningParenthesisToken;
            else
                return TokenRepresentation.ClosingParenthesisToken;
        }

        private enum ParenthesisTokenType
        {
            OpeningParenthesis,
            ClosingParenthesis,
        }

        private ParenthesisTokenType _parenthesisTokenType;

        public static readonly ParenthesisToken OpeningParenthesis = new ParenthesisToken(ParenthesisTokenType.OpeningParenthesis);
        public static readonly ParenthesisToken ClosingParenthesis = new ParenthesisToken(ParenthesisTokenType.ClosingParenthesis);
    }
}
