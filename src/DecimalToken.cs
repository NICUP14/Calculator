namespace Calculator
{
    class DecimalToken : Token
    {
        public DecimalToken(string stringRepresentation = "") : base(TokenType.Decimal, stringRepresentation)
        {
        }

        public Decimal ToDecimal()
        {
            return new Decimal(_stringRepresentation);
        }

        public bool EndsWithPeriod()
        {
            return !string.IsNullOrEmpty(_stringRepresentation) && _stringRepresentation.EndsWith('.');
        }

        public bool StartsWithPeriod()
        {
            return !string.IsNullOrEmpty(_stringRepresentation) && _stringRepresentation.StartsWith('.');
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
    }
}
