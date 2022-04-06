namespace Calculator
{
    class ParenthesisToken : Token
    {
        private ParenthesisToken(ParenthesisTokenType parenthesisTokenType, string stringRepresentation = "") : base(TokenType.Parenthesis, stringRepresentation)
        {
            _parenthesisTokenType = parenthesisTokenType;
        }

        public override bool Equals(object obj)
        {
            if (obj is not ParenthesisToken)
                return base.Equals(obj);
            return _parenthesisTokenType == (obj as ParenthesisToken)._parenthesisTokenType;
        }

        public override ParenthesisToken Clone()
        {
            return new ParenthesisToken(_parenthesisTokenType, _stringRepresentation);
        }

        private enum ParenthesisTokenType
        {
            OpeningParenthesis,
            ClosingParenthesis,
        }

        private ParenthesisTokenType _parenthesisTokenType;

        /// Parenthesis token constants
        public static readonly ParenthesisToken OpeningParenthesis = new ParenthesisToken(ParenthesisTokenType.OpeningParenthesis, TokenStringRepresentation.OpeningParenthesis);
        public static readonly ParenthesisToken ClosingParenthesis = new ParenthesisToken(ParenthesisTokenType.ClosingParenthesis, TokenStringRepresentation.ClosingParenthesis);
    }
}
