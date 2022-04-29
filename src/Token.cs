namespace Calculator
{
    /// <summary>
    /// Defines string representation for the constants in the OperatorToken, ParenthesisToken classes
    /// </summary>
    static class TokenStringRepresentation
    {
        public const string Division = "÷";
        public const string Addition = "+";
        public const string Subtraction = "-";
        public const string Multiplication = "×";
        public const string OpeningParenthesis = "(";
        public const string ClosingParenthesis = ")";
    }

    /// <summary>
    /// Represents a base class used to implement all expression tokens
    /// </summary>
    class Token
    {
        /// <summary>
        /// Initializes a new instance of the Token class.
        /// This constructor is used by all derived classes
        /// </summary>
        /// <param name="tokenType"></param>
        /// <param name="stringRepresentation"></param>
        protected Token(TokenType tokenType, string stringRepresentation = "")
        {
            if (stringRepresentation is null)
                throw new System.ArgumentNullException(nameof(stringRepresentation));

            _tokenType = tokenType;
            _stringRepresentation = stringRepresentation;
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// This equality implementation is cast-sensitive
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj is not Token)
                return false;

            return _tokenType == (obj as Token)._tokenType;
        }

        public override int GetHashCode()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Returns a string that represents the current Token
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return _stringRepresentation;
        }

        /// <summary>
        ///  Creates a shallow copy of the current Token
        /// </summary>
        /// <returns></returns>
        public virtual Token Clone()
        {
            return new Token(_tokenType, _stringRepresentation);
        }

        /// <summary>
        /// Trims the last character from the instance's string representation
        /// </summary>
        public virtual void RemoveTrailingCharacter()
        {
            _stringRepresentation = _stringRepresentation.Remove(_stringRepresentation.Length - 1);
        }

        /// <summary>
        /// This enumeration defines the identifiers of all the derived Token classes
        /// </summary>
        public enum TokenType
        {
            Decimal,
            Operator,
            Undefined,
            Parenthesis
        }

        /// <summary>
        /// Returns the length of the string representation 
        /// </summary>
        public int Length
        {
            get
            {
                return _stringRepresentation.Length;
            }
        }

        private readonly TokenType _tokenType;
        public string _stringRepresentation;

        /// Defines Token constants
        public static readonly Token Decimal = new(TokenType.Decimal);
        public static readonly Token Operator = new(TokenType.Operator);
        public static readonly Token Undefined = new(TokenType.Undefined);
        public static readonly Token Parenthesis = new(TokenType.Parenthesis);
    }
}
