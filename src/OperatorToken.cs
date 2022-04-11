namespace Calculator
{
    class OperatorToken : Token
    {
        private OperatorToken(int precedence, OperatorTokenType operatorType, string stringRepresentation) : base(TokenType.Operator, stringRepresentation)
        {
            _precedence = precedence;
            _operatorTokenType = operatorType;
        }

        public override bool Equals(object obj)
        {
            if(obj is not OperatorToken)
                return base.Equals(obj);
            return _operatorTokenType == (obj as OperatorToken)._operatorTokenType;
        }

        public override OperatorToken Clone()
        {
            return new OperatorToken(_precedence, _operatorTokenType, _stringRepresentation);
        }

        public static int Compare(OperatorToken operatorToken, OperatorToken operatorToken2)
        {
            return operatorToken._precedence.CompareTo(operatorToken2._precedence);
        }

        public enum OperatorTokenType
        {
            Division,
            Addition,
            Subtraction,
            Multiplication,
        };

        private int _precedence;
        private OperatorTokenType _operatorTokenType;

        ///  Operator token constants
        public static readonly OperatorToken Division =       new OperatorToken(3, OperatorTokenType.Division, TokenStringRepresentation.Division);
        public static readonly OperatorToken Addition =       new OperatorToken(2, OperatorTokenType.Addition, TokenStringRepresentation.Addition);
        public static readonly OperatorToken Subtraction =    new OperatorToken(2, OperatorTokenType.Subtraction, TokenStringRepresentation.Subtraction);
        public static readonly OperatorToken Multiplication = new OperatorToken(3, OperatorTokenType.Multiplication, TokenStringRepresentation.Multiplication);
    }
}
