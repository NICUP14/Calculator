namespace Calculator
{
    struct Operator
    {
        public Operator(int precedence, Expression.OperatorType type)
        {
            _precedence = precedence;
            Type = type;
        }

        public int CompareTo(Operator op)
        {
            return _precedence.CompareTo(op._precedence);
        }

        public Expression.OperatorType Type;
        private int _precedence;
    }

}
