namespace Calculator
{
    class OperatorToken : Token
    {
        private OperatorToken(int precedence, OperatorTokenType operatorType, string stringRepresentation = "") : base(TokenType.Operator, stringRepresentation)
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

        public static readonly OperatorToken Addition = new OperatorToken(2, OperatorTokenType.Addition, TokenStringRepresentation.AdditionOperatorToken);
        public static readonly OperatorToken Subtraction = new OperatorToken(2, OperatorTokenType.Subtraction, TokenStringRepresentation.SubtractionOperatorToken);
        public static readonly OperatorToken Division = new OperatorToken(3, OperatorTokenType.Division, TokenStringRepresentation.DivisionOperatorToken);
        public static readonly OperatorToken Multiplication = new OperatorToken(3, OperatorTokenType.Multiplication, TokenStringRepresentation.MultiplicationOperatorToken);
    }
}
