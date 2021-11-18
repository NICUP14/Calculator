namespace Calculator
{
    class OperatorToken : Token
    {
        private OperatorToken(int precedence, OperatorTokenType operatorType) : base(TokenType.Operator)
        {
            _precedence = precedence;
            _operatorTokenType = operatorType;
        }

        public bool IsAdditionOperatorToken()
        {
            return _operatorTokenType == OperatorTokenType.Addition;
        }

        public bool IsSubtractionOperatorToken()
        {
            return _operatorTokenType == OperatorTokenType.Subtraction;
        }

        public bool IsMultiplicationOperatorToken()
        {
            return _operatorTokenType == OperatorTokenType.Multiplication;
        }

        public bool IsDivisionOperatorToken()
        {
            return _operatorTokenType == OperatorTokenType.Division;
        }

        public override string ToString()
        {
            switch (_operatorTokenType)
            {
                case OperatorTokenType.Addition:
                    return TokenRepresentation.AdditionOperatorToken;
                case OperatorTokenType.Subtraction:
                    return TokenRepresentation.SubtractionOperatorToken;
                case OperatorTokenType.Multiplication:
                    return TokenRepresentation.MultiplicationOperatorToken;
                case OperatorTokenType.Division:
                    return TokenRepresentation.DivisionOperatorToken;
                default:
                    return "?";
            }
        }

        public static int Compare(OperatorToken operatorToken, OperatorToken operatorToken2)
        {
            return operatorToken._precedence.CompareTo(operatorToken2._precedence);
        }

        public enum OperatorTokenType
        {
            Addition,
            Subtraction,
            Division,
            Multiplication,
        };

        private int _precedence;
        private OperatorTokenType _operatorTokenType;

        public static readonly OperatorToken Addition = new OperatorToken(2, OperatorTokenType.Addition);
        public static readonly OperatorToken Subtraction = new OperatorToken(2, OperatorTokenType.Subtraction);
        public static readonly OperatorToken Division = new OperatorToken(3, OperatorTokenType.Division);
        public static readonly OperatorToken Multiplication = new OperatorToken(3, OperatorTokenType.Multiplication);
    }
}
