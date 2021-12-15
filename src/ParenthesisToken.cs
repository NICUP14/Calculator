namespace Calculator
{
    class ParenthesisToken : Token
    {
        private ParenthesisToken(ParenthesisTokenType parenthesisTokenType, string stringRepresentation = "") : base(TokenType.Parenthesis, stringRepresentation)
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

        public override bool Equals(object obj)
        {
            return base.Equals(obj) && this._parenthesisTokenType == (obj as ParenthesisToken)._parenthesisTokenType;
        }

        private enum ParenthesisTokenType
        {
            OpeningParenthesis,
            ClosingParenthesis,
        }

        private ParenthesisTokenType _parenthesisTokenType;

        public static readonly ParenthesisToken OpeningParenthesis = new ParenthesisToken(ParenthesisTokenType.OpeningParenthesis, TokenStringRepresentation.OpeningParenthesisToken);
        public static readonly ParenthesisToken ClosingParenthesis = new ParenthesisToken(ParenthesisTokenType.ClosingParenthesis, TokenStringRepresentation.ClosingParenthesisToken);
    }
}
