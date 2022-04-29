namespace Calculator
{
    class ParenthesisToken : Token
    {
        /// <summary>
        /// Initializes a new instance of the ParenthesisToken class.
        /// This constructor is used by the OperatorToken's internal methods
        /// </summary>
        /// <param name="parenthesisTokenType"></param>
        /// <param name="stringRepresentation"></param>
        private ParenthesisToken(ParenthesisTokenType parenthesisTokenType, string stringRepresentation) : base(TokenType.Parenthesis, stringRepresentation)
        {
            _parenthesisTokenType = parenthesisTokenType;
        }

        public override bool Equals(object obj)
        {
            if (obj is not ParenthesisToken)
                return base.Equals(obj);
            return _parenthesisTokenType == (obj as ParenthesisToken)._parenthesisTokenType;
        }

        public override int GetHashCode()
        {
            throw new System.NotImplementedException();
        }

        public override ParenthesisToken Clone()
        {
            return new ParenthesisToken(_parenthesisTokenType, _stringRepresentation);
        }

        /// <summary>
        /// This enumeration defines the identifiers of all the ParenthesisToken constants
        /// </summary>
        private enum ParenthesisTokenType
        {
            OpeningParenthesis,
            ClosingParenthesis,
        }

        private readonly ParenthesisTokenType _parenthesisTokenType;

        /// Defines ParenthesisToken constants
        public static readonly ParenthesisToken OpeningParenthesis = new(ParenthesisTokenType.OpeningParenthesis, TokenStringRepresentation.OpeningParenthesis);
        public static readonly ParenthesisToken ClosingParenthesis = new(ParenthesisTokenType.ClosingParenthesis, TokenStringRepresentation.ClosingParenthesis);
    }
}
