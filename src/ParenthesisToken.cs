namespace Calculator
{
    class ParenthesisToken : Token
    {
        private ParenthesisToken(ParenthesisTokenType parenthesisTokenType) : base(TokenType.Parenthesis)
        {
            _parenthesisTokenType = parenthesisTokenType;

            switch(parenthesisTokenType)
            {
                case ParenthesisTokenType.OpeningParenthesis:
                    _stringRepresentation = TokenStringRepresentation.OpeningParenthesisToken;
                    break;
                case ParenthesisTokenType.ClosingParenthesis:
                    _stringRepresentation = TokenStringRepresentation.ClosingParenthesisToken;
                    break;
            }
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

        public static readonly ParenthesisToken OpeningParenthesis = new ParenthesisToken(ParenthesisTokenType.OpeningParenthesis);
        public static readonly ParenthesisToken ClosingParenthesis = new ParenthesisToken(ParenthesisTokenType.ClosingParenthesis);
    }
}
