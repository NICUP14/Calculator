namespace Calculator
{
    class DecimalToken : Token
    {
        /// <summary>
        /// Initializes a new instance of the DecimalToken class that matches the value of the specified string
        /// </summary>
        /// <param name="stringRepresentation"></param>
        /// <exception cref="System.ArgumentNullException"></exception>
        public DecimalToken(string stringRepresentation) : base(TokenType.Decimal)
        {
            if (stringRepresentation is null)
                throw new System.ArgumentNullException(nameof(stringRepresentation));

            _stringRepresentation = stringRepresentation;
            _decimalRepresentation = new Decimal(stringRepresentation);
        }

        /// <summary>
        /// Initializes a new instance of the DecimalToken class that matches the value of the specified Decimal
        /// </summary>
        /// <param name="decimalRepresentation"></param>
        /// <exception cref="System.ArgumentNullException"></exception>
        public DecimalToken(Decimal decimalRepresentation) : base(TokenType.Decimal)
        {
            if (decimalRepresentation is null)
                throw new System.ArgumentNullException(nameof(decimalRepresentation));

            _decimalRepresentation = decimalRepresentation;
            _stringRepresentation = decimalRepresentation.ToString();
        }

        public override DecimalToken Clone()
        {
            return new DecimalToken(_stringRepresentation);
        }

        public override void RemoveTrailingCharacter()
        {
            base.RemoveTrailingCharacter();

            _decimalRepresentation = new Decimal(_stringRepresentation);
        }

        /// <summary>
        /// Returns an array of tokens that represents the current DecimalToken instance
        /// </summary>
        /// <returns></returns>
        public Decimal ToDecimal()
        {
            return _decimalRepresentation;
        }

        /// <summary>
        /// Unifies all the accepted Decimal constructor formats.
        /// This implementation uses the format defined by Decimal.ToString()
        /// </summary>
        public void Reformat()
        {
            _stringRepresentation = _decimalRepresentation.ToString();
        }

        /// <summary>
        /// Concatenates the string representations of the values of two instances of the DecimalToken class
        /// </summary>
        /// <param name="firstDecimalToken"></param>
        /// <param name="secondDecimalToken"></param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        public static DecimalToken Concat(DecimalToken firstDecimalToken, DecimalToken secondDecimalToken)
        {
            if (firstDecimalToken is null)
                throw new System.ArgumentNullException(nameof(firstDecimalToken));

            if (secondDecimalToken is null)
                throw new System.ArgumentNullException(nameof(secondDecimalToken));

            return new DecimalToken(firstDecimalToken._stringRepresentation + secondDecimalToken._stringRepresentation);
        }

        public int PeriodCount
        {
            get { return System.Linq.Enumerable.Count(_stringRepresentation, character => character == '.'); }
        }

        private Decimal _decimalRepresentation;

        ///  Decimal token constants
        public static readonly DecimalToken Zero = new("0");
        public static readonly DecimalToken One = new("1");
        public static readonly DecimalToken Two = new("2");
        public static readonly DecimalToken Three = new("3");
        public static readonly DecimalToken Four = new("4");
        public static readonly DecimalToken Five = new("5");
        public static readonly DecimalToken Six = new("6");
        public static readonly DecimalToken Seven = new("7");
        public static readonly DecimalToken Eight = new("8");
        public static readonly DecimalToken Nine = new("9");
        public static readonly DecimalToken Period = new(".");
    }
}
