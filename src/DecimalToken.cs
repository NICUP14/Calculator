namespace Calculator
{
    class DecimalToken : Token
    {
        public DecimalToken(string stringRepresentation) : base(TokenType.Decimal)
        {
            _stringRepresentation  = stringRepresentation;
            _decimalRepresentation = new Decimal(stringRepresentation);
        }

        public DecimalToken(Decimal decimalRepresentation) : base(TokenType.Decimal)
        {
            _decimalRepresentation = decimalRepresentation;
            _stringRepresentation = decimalRepresentation.ToString();
        }

        public override string ToString()
        {
            return _stringRepresentation;
        }

        public override DecimalToken Clone()
        {
            return new DecimalToken(_stringRepresentation);
        }

        public override void RemoveLastCharacter()
        {
            base.RemoveLastCharacter();
            _decimalRepresentation = new Decimal(_stringRepresentation);
        }

        public Decimal ToDecimal()
        {
            return _decimalRepresentation;
        }

        public void Reformat()
        {
            _stringRepresentation = _decimalRepresentation.ToString();
        }

        public static DecimalToken Concatenate(DecimalToken decimalToken, DecimalToken decimalToken2)
        {
            return new DecimalToken(decimalToken._stringRepresentation + decimalToken2._stringRepresentation);
        }

        public int PeriodCount
        {
            get
            {
                int periodCount = 0;
                foreach (char decimalCharacter in _stringRepresentation)
                    if (decimalCharacter == '.')
                        periodCount++;

                return periodCount;
            }
        }

        private Decimal _decimalRepresentation;
    }
}
