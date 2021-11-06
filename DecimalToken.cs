namespace Calculator
{
    class DecimalToken : Token
    {
        public DecimalToken(string decimalString) : base(TokenType.Decimal)
        {
            _decimalString = decimalString;
        }

        public Decimal ToDecimal()
        {
            return new Decimal(_decimalString);
        }

        public override string ToString()
        {
            return _decimalString;
        }

        private string _decimalString;
    }
}
