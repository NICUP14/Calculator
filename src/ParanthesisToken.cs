namespace Calculator
{
    class ParanthesisToken : Token
    {
        private ParanthesisToken(ParenthesisTokenType parenthesisTokenType, string stringRepresentation) : base(TokenType.Parenthesis, stringRepresentation)
        {
            _parenthesisTokenType = parenthesisTokenType;
        }

        public override bool Equals(object obj)
        {
            if (obj is not ParanthesisToken)
                return base.Equals(obj);
            return _parenthesisTokenType == (obj as ParanthesisToken)._parenthesisTokenType;
        }

        public override ParanthesisToken Clone()
        {
            return new ParanthesisToken(_parenthesisTokenType, _stringRepresentation);
        }

        private enum ParenthesisTokenType
        {
            OpeningParenthesis,
            ClosingParenthesis,
        }

        private ParenthesisTokenType _parenthesisTokenType;

        /// Parenthesis token constants
        public static readonly ParanthesisToken OpeningParenthesis = new ParanthesisToken(ParenthesisTokenType.OpeningParenthesis, TokenStringRepresentation.OpeningParenthesis);
        public static readonly ParanthesisToken ClosingParenthesis = new ParanthesisToken(ParenthesisTokenType.ClosingParenthesis, TokenStringRepresentation.ClosingParenthesis);
    }
}
