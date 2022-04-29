namespace Calculator
{
    class OperatorToken : Token
    {
        /// <summary>
        /// Initializes a new instance of the OperatorToken class.
        /// This constructor is used by the OperatorToken's internal methods
        /// </summary>
        /// <param name="precedence"></param>
        /// <param name="operatorType"></param>
        /// <param name="stringRepresentation"></param>
        /// <exception cref="System.ArgumentNullException"></exception>
        private OperatorToken(int precedence, OperatorTokenType operatorType, string stringRepresentation) : base(TokenType.Operator, stringRepresentation)
        {
            if (stringRepresentation is null)
                throw new System.ArgumentNullException(nameof(stringRepresentation));

            _precedence = precedence;
            _operatorTokenType = operatorType;
        }

        public override bool Equals(object obj)
        {
            if (obj is not OperatorToken)
                return base.Equals(obj);

            return _operatorTokenType == (obj as OperatorToken)._operatorTokenType;
        }

        public override int GetHashCode()
        {
            throw new System.NotImplementedException();
        }

        public override OperatorToken Clone()
        {
            return new OperatorToken(_precedence, _operatorTokenType, _stringRepresentation);
        }

        /// <summary>
        /// Compares specified OperatorToken precedence values and returns an indication of their relative values
        /// </summary>
        /// <param name="operatorToken"></param>
        /// <param name="operatorToken2"></param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        public static int Compare(OperatorToken operatorToken, OperatorToken operatorToken2)
        {
            if (operatorToken is null)
                throw new System.ArgumentNullException(nameof(operatorToken));

            if (operatorToken2 is null)
                throw new System.ArgumentNullException(nameof(operatorToken2));

            return operatorToken._precedence.CompareTo(operatorToken2._precedence);
        }

        /// <summary>
        /// This enumeration defines the identifiers of all the OperatorToken constants
        /// </summary>
        public enum OperatorTokenType
        {
            Division,
            Addition,
            Subtraction,
            Multiplication,
        };

        private readonly int _precedence;
        private readonly OperatorTokenType _operatorTokenType;

        ///  Defines OperatorToken constants
        public static readonly OperatorToken Division = new(3, OperatorTokenType.Division, TokenStringRepresentation.Division);
        public static readonly OperatorToken Addition = new(2, OperatorTokenType.Addition, TokenStringRepresentation.Addition);
        public static readonly OperatorToken Subtraction = new(2, OperatorTokenType.Subtraction, TokenStringRepresentation.Subtraction);
        public static readonly OperatorToken Multiplication = new(3, OperatorTokenType.Multiplication, TokenStringRepresentation.Multiplication);
    }
}
