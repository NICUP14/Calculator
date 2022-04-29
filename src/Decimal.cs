using System;
using System.Text;
using System.Collections.Generic;

namespace Calculator
{
    /// <summary>
    /// Represents an immutable arbitrary precision floating-point number
    /// </summary>
    class Decimal
    {
        /// <summary>
        /// Initializes a new instance of the Decimal class.
        /// This constructor is used by the Decimal's internal methods
        /// </summary>
        /// <param name="isPositive"></param>
        /// <param name="integerPart"></param>
        /// <param name="fractionalPart"></param>
        /// <exception cref="ArgumentNullException"></exception>
        private Decimal(bool isPositive, LinkedList<char> integerPart, LinkedList<char> fractionalPart)
        {
            if (integerPart is null)
                throw new ArgumentNullException(nameof(integerPart));

            if (fractionalPart is null)
                throw new ArgumentNullException(nameof(fractionalPart));

            _isPositive = isPositive;
            _integerPart = integerPart;
            _fractionalPart = fractionalPart;
        }


        /// <summary>
        /// Initializes a new instance of the Decimal class that matches the value of the specified string.
        /// </summary>
        /// <param name="stringRepresentation"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="DecimalFormatException"></exception>
        public Decimal(string stringRepresentation = "")
        {
            if (stringRepresentation is null)
                throw new ArgumentNullException(nameof(stringRepresentation));

            /// Splits the string representation into its integer and fractional parts
            int periodIndex = stringRepresentation.LastIndexOf('.');
            if (periodIndex == -1)
            {
                _integerPart = new(stringRepresentation);
                _fractionalPart = new("0");
            }
            else
            {
                _integerPart = new(stringRepresentation[..periodIndex]);
                _fractionalPart = new(stringRepresentation[(periodIndex + 1)..stringRepresentation.Length]);
            }

            /// Extracts the sign present in the string representation
            if (_integerPart.Count > 0 && (_integerPart.First.Value == '+' || _integerPart.First.Value == '-'))
            {
                _isPositive = _integerPart.First.Value == '+';
                _integerPart.RemoveFirst();
            }

            if (_integerPart.Count == 0)
                _integerPart.AddLast('0');

            if (_fractionalPart.Count == 0)
                _fractionalPart.AddLast('0');

            /// Handles the signed zero case
            if (CharacterLinkedListMethods.ContainsOnlyZeroes(_integerPart) && CharacterLinkedListMethods.ContainsOnlyZeroes(_fractionalPart))
                _isPositive = true;

            /// Reformats and validates the integer part
            if (!CharacterLinkedListMethods.ContainsOnlyZeroes(_integerPart))
                CharacterLinkedListMethods.TrimLeadingZeroes(_integerPart);
            foreach (char integerPartChar in _integerPart)
                if (!char.IsDigit(integerPartChar))
                    throw new DecimalFormatException();

            /// Reformats and validates the fractional part
            if (!CharacterLinkedListMethods.ContainsOnlyZeroes(_fractionalPart))
                CharacterLinkedListMethods.TrimTrailingZeroes(_fractionalPart);
            foreach (char fractionalPartChar in _fractionalPart)
                if (!char.IsDigit(fractionalPartChar))
                    throw new DecimalFormatException();
        }

        /// <summary>
        /// Returns a string that represents the current Decimal
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            bool fractionalPartIsZero = CharacterLinkedListMethods.ContainsOnlyZeroes(_fractionalPart);

            /// Determines the required capacity of the string builder
            int stringRepresentationBuilderCapacity = 0;
            stringRepresentationBuilderCapacity += _integerPart.Count;
            stringRepresentationBuilderCapacity += Convert.ToInt32(_isPositive);
            stringRepresentationBuilderCapacity += Convert.ToInt32(!fractionalPartIsZero) * (_fractionalPart.Count + 1);

            /// Initializes the StringBuilder instance
            StringBuilder stringRepresentationBuilder = new(stringRepresentationBuilderCapacity);

            if (!_isPositive)
                stringRepresentationBuilder.Append('-');

            stringRepresentationBuilder.AppendJoin(string.Empty, _integerPart);

            if (!fractionalPartIsZero)
            {
                stringRepresentationBuilder.Append('.');
                stringRepresentationBuilder.AppendJoin(string.Empty, _fractionalPart);
            }

            return stringRepresentationBuilder.ToString();
        }

        /// <summary>
        /// Adds two specified Decimal values
        /// </summary>
        /// <param name="firstAddend"></param>
        /// <param name="secondAddend"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static Decimal Add(Decimal firstAddend, Decimal secondAddend)
        {
            if (firstAddend is null)
                throw new ArgumentNullException(nameof(firstAddend));

            if (secondAddend is null)
                throw new ArgumentNullException(nameof(secondAddend));

            /// Determines fractional part offset and convert Decimals for the addition routine
            int fractionalPartOffset = Math.Max(firstAddend._fractionalPart.Count, secondAddend._fractionalPart.Count);
            LinkedList<char> firstAddendLinkedList = DecimalToLinkedList(firstAddend, fractionalPartOffset);
            LinkedList<char> secondAddendLinkedList = DecimalToLinkedList(secondAddend, fractionalPartOffset);

            /// Performs the addition routine
            bool resultIsPositive;
            LinkedList<char> result;
            bool firstAddendIsPositive = firstAddend._isPositive;
            bool secondAddendIsPositive = secondAddend._isPositive;
            bool firstAddendContainsOnlyZeroes = CharacterLinkedListMethods.ContainsOnlyZeroes(firstAddendLinkedList);
            bool secondAddendContainsOnlyZeroes = CharacterLinkedListMethods.ContainsOnlyZeroes(secondAddendLinkedList);
            int addendComparison = CharacterLinkedListMethods.Compare(firstAddendLinkedList, secondAddendLinkedList);
            if (firstAddendContainsOnlyZeroes && secondAddendContainsOnlyZeroes)
            {
                resultIsPositive = true;
                result = new("0");
            }
            else if (firstAddendContainsOnlyZeroes)
            {
                resultIsPositive = secondAddendIsPositive;
                result = secondAddendLinkedList;
            }
            else if (secondAddendContainsOnlyZeroes)
            {
                resultIsPositive = firstAddendIsPositive;
                result = firstAddendLinkedList;
            }
            else if (firstAddendIsPositive == secondAddendIsPositive)
            {
                resultIsPositive = firstAddendIsPositive;
                result = CharacterLinkedListMethods.Add(firstAddendLinkedList, secondAddendLinkedList);
            }
            else
            {
                if (addendComparison == 0)
                {
                    resultIsPositive = true;
                    result = new("0");
                }
                else
                {
                    resultIsPositive = firstAddendIsPositive == (addendComparison >= 0);

                    if (addendComparison > 0)
                        result = CharacterLinkedListMethods.Subtract(firstAddendLinkedList, secondAddendLinkedList);
                    else
                        result = CharacterLinkedListMethods.Subtract(secondAddendLinkedList, firstAddendLinkedList);
                }
            }

            /// Splits result into integer and fractional parts
            LinkedList<char> integerPart = result;
            LinkedList<char> fractionalPart = new();
            CharacterLinkedListMethods.TransferRight(integerPart, fractionalPart, fractionalPartOffset);

            /// Removes padding from integer and fractional parts
            CharacterLinkedListMethods.TrimLeadingZeroes(integerPart);
            CharacterLinkedListMethods.TrimTrailingZeroes(fractionalPart);

            return new Decimal(resultIsPositive, integerPart, fractionalPart);
        }

        /// <summary>
        /// Subtracts two specified Decimal values
        /// </summary>
        /// <param name="minuend"></param>
        /// <param name="subtrahend"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static Decimal Subtract(Decimal minuend, Decimal subtrahend)
        {
            if (minuend is null)
                throw new ArgumentNullException(nameof(minuend));

            if (subtrahend is null)
                throw new ArgumentNullException(nameof(subtrahend));

            /// Performs Decimal addition after negating the sign of the subtrahend
            Decimal auxiliary = new(!subtrahend._isPositive, subtrahend._integerPart, subtrahend._fractionalPart);
            return Add(minuend, auxiliary);
        }

        /// <summary>
        /// Multiplies two specified Decimal values
        /// </summary>
        /// <param name="multiplicand"></param>
        /// <param name="multiplicator"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static Decimal Multiply(Decimal multiplicand, Decimal multiplicator)
        {
            if (multiplicand is null)
                throw new ArgumentNullException(nameof(multiplicand));

            if (multiplicator is null)
                throw new ArgumentNullException(nameof(multiplicator));

            /// Converts Decimals for the multiplication routine
            LinkedList<char> multiplicandLinkedList = DecimalToLinkedList(multiplicand);
            LinkedList<char> multiplicatorLinkedList = DecimalToLinkedList(multiplicator);
            CharacterLinkedListMethods.TrimLeadingZeroes(multiplicandLinkedList);
            CharacterLinkedListMethods.TrimLeadingZeroes(multiplicatorLinkedList);

            /// Determines the fractional part offset
            int fractionalPartOffset = 0;
            fractionalPartOffset += multiplicand._fractionalPart.Count;
            fractionalPartOffset += multiplicator._fractionalPart.Count;
            fractionalPartOffset -= CharacterLinkedListMethods.TrimTrailingZeroes(multiplicandLinkedList);
            fractionalPartOffset -= CharacterLinkedListMethods.TrimTrailingZeroes(multiplicatorLinkedList);

            /// Performs the multiplication routine
            LinkedList<char> result;
            if (CharacterLinkedListMethods.ContainsOnlyZeroes(multiplicandLinkedList) || CharacterLinkedListMethods.ContainsOnlyZeroes(multiplicatorLinkedList))
                result = new("0");
            else
            {
                if (multiplicandLinkedList.Count == 1 && multiplicandLinkedList.First.Value == '1')
                    result = multiplicatorLinkedList;
                else if (multiplicatorLinkedList.Count == 1 && multiplicatorLinkedList.First.Value == '1')
                    result = multiplicandLinkedList;
                else
                    result = CharacterLinkedListMethods.Multiply(multiplicandLinkedList, multiplicatorLinkedList);
            }
            CharacterLinkedListMethods.TrimLeadingZeroes(result);

            /// Splits result into integer and fractional parts
            LinkedList<char> integerPart = result;
            LinkedList<char> fractionalPart = new();

            if (fractionalPartOffset > 0)
                CharacterLinkedListMethods.TransferRight(integerPart, fractionalPart, fractionalPartOffset);
            if (fractionalPartOffset < 0)
                CharacterLinkedListMethods.TransferLeft(integerPart, fractionalPart, -fractionalPartOffset);

            /// Removes padding from integer and fractional parts
            CharacterLinkedListMethods.TrimLeadingZeroes(integerPart);
            CharacterLinkedListMethods.TrimTrailingZeroes(fractionalPart);

            return new Decimal(multiplicand._isPositive == multiplicator._isPositive, integerPart, fractionalPart);
        }

        /// <summary>
        /// Divides two specified Decimal values
        /// </summary>
        /// <param name="dividend"></param>
        /// <param name="divisor"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="DecimalArithmeticException"></exception>
        public static Decimal Divide(Decimal dividend, Decimal divisor)
        {
            if (dividend is null)
                throw new ArgumentNullException(nameof(dividend));

            if (divisor is null)
                throw new ArgumentNullException(nameof(divisor));

            if (_divisionPrecision <= 0)
                throw new DecimalArithmeticException();

            /// Converts Decimals for the division routine
            LinkedList<char> dividendLinkedList = DecimalToLinkedList(dividend);
            LinkedList<char> divisorLinkedList = DecimalToLinkedList(divisor);
            if (CharacterLinkedListMethods.ContainsOnlyZeroes(divisorLinkedList))
                throw new DecimalArithmeticException();

            CharacterLinkedListMethods.TrimLeadingZeroes(dividendLinkedList);
            CharacterLinkedListMethods.TrimLeadingZeroes(divisorLinkedList);

            /// Determines the fractional part offset
            int fractionalPartOffset = 0;
            fractionalPartOffset += dividend._fractionalPart.Count;
            fractionalPartOffset -= divisor._fractionalPart.Count;
            fractionalPartOffset -= CharacterLinkedListMethods.TrimTrailingZeroes(dividendLinkedList);
            fractionalPartOffset += CharacterLinkedListMethods.TrimTrailingZeroes(divisorLinkedList);

            /// Computes the first 10 multiples of divisor
            LinkedList<char> divisorLinkedListMultiple = new();
            LinkedList<char>[] divisorLinkedListMultipleArray = new LinkedList<char>[10];
            for (int divisorLinkedListMultipleArrayIndex = 0; divisorLinkedListMultipleArrayIndex < 10; divisorLinkedListMultipleArrayIndex++)
            {
                divisorLinkedListMultipleArray[divisorLinkedListMultipleArrayIndex] = divisorLinkedListMultiple;
                divisorLinkedListMultiple = CharacterLinkedListMethods.Add(divisorLinkedListMultiple, divisorLinkedList);
            }

            /// Performs the division routine
            LinkedList<char> auxiliary = new();
            LinkedList<char> integerPart = new();
            LinkedList<char> fractionalPart = new();
            LinkedListNode<char> dividendLinkedListNode = dividendLinkedList.First;
            bool quotientIsForIntegerPart = true;
            int quotient, precision = fractionalPartOffset;
            while (dividendLinkedListNode != null && CharacterLinkedListMethods.Compare(auxiliary, divisorLinkedList) < 0)
            {
                auxiliary.AddLast(dividendLinkedListNode.Value);
                dividendLinkedListNode = dividendLinkedListNode.Next;
            }
            while ((dividendLinkedListNode != null || CharacterLinkedListMethods.ContainsOnlyZeroes(auxiliary) == false) && (quotientIsForIntegerPart || precision <= _divisionPrecision))
            {
                CharacterLinkedListMethods.TrimLeadingZeroes(auxiliary);
                quotient = CharacterLinkedListMethods.LEBinarySearch(auxiliary, divisorLinkedListMultipleArray);
                auxiliary = CharacterLinkedListMethods.Subtract(auxiliary, divisorLinkedListMultipleArray[quotient]);

                (quotientIsForIntegerPart ? integerPart : fractionalPart).AddLast((char)(quotient + '0'));

                if (dividendLinkedListNode == null)
                {
                    quotientIsForIntegerPart = false;
                    auxiliary.AddLast('0');
                    precision++;
                }
                else
                {
                    auxiliary.AddLast(dividendLinkedListNode.Value);
                    dividendLinkedListNode = dividendLinkedListNode.Next;
                }
            }

            /// Transfers nodes based on the fractional part offset
            if (fractionalPartOffset > 0)
                CharacterLinkedListMethods.TransferRight(integerPart, fractionalPart, fractionalPartOffset);
            if (fractionalPartOffset < 0)
                CharacterLinkedListMethods.TransferLeft(integerPart, fractionalPart, -fractionalPartOffset);

            /// Truncates the fractional part to match division precision
            for (int fractionalPartRange = fractionalPart.Count; fractionalPartRange > _divisionPrecision; fractionalPartRange--)
                fractionalPart.RemoveLast();

            /// Removes padding from integer and fractional parts
            CharacterLinkedListMethods.TrimLeadingZeroes(integerPart);
            CharacterLinkedListMethods.TrimTrailingZeroes(fractionalPart);

            return new Decimal(dividend._isPositive == divisor._isPositive, integerPart, fractionalPart);
        }

        /// <summary>
        /// Returns a linked list that represents the specified Decimal
        /// </summary>
        /// <param name="dec"></param>
        /// <param name="fractionalPartPadCount"></param>
        /// <returns></returns>
        private static LinkedList<char> DecimalToLinkedList(Decimal dec, int fractionalPartPadCount = 0)
        {
            if (dec is null)
                throw new ArgumentNullException(nameof(dec));

            LinkedList<char> linkedList = new();

            foreach (char integerPartChar in dec._integerPart)
                linkedList.AddLast(integerPartChar);

            foreach (char fractionalPartChar in dec._fractionalPart)
                linkedList.AddLast(fractionalPartChar);

            for (int fractionalPartPadRange = dec._fractionalPart.Count; fractionalPartPadRange < fractionalPartPadCount; fractionalPartPadRange++)
                linkedList.AddLast('0');

            return linkedList;
        }

        private readonly bool _isPositive = true;
        private readonly LinkedList<char> _integerPart, _fractionalPart;

        private static int _divisionPrecision = 20;
        public static int DivisionPrecision
        {
            get { return _divisionPrecision; }
            set { _divisionPrecision = value; }
        }

        /// Decimal constants
        public static readonly Decimal Zero = new();
    }
}
