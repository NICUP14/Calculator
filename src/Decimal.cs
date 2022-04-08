using System;
using System.Collections.Generic;
using System.Text;
using static System.Console;

namespace Calculator
{
	/// <summary>
	/// Arbitrary precision decimal implementation
	/// </summary>
	class Decimal
	{
		/// <summary>
		/// Used by decimal's internal logic
		/// </summary>
		/// <param name="isPositive"></param>
		/// <param name="integerPart"></param>
		/// <param name="fractionalPart"></param>
		private Decimal(bool isPositive, LinkedList<char> integerPart, LinkedList<char> fractionalPart)
		{
			_isPositive = isPositive;
			_integerPart = integerPart;
			_fractionalPart = fractionalPart;
		}


		/// <summary>
		/// Construct decimal from its string representation
		/// </summary>
		/// <param name="decimalString"></param>
		public Decimal(string decimalString = "0")
		{
			if (string.IsNullOrEmpty(decimalString))
				throw new DecimalNullError();

			/// Split string representation into integer and fractional parts
			int periodIndex = decimalString.LastIndexOf('.');
			if (periodIndex == -1)
			{
				_integerPart = CharacterLinkedListMethods.ConvertStringToLinkedList(decimalString);
				_fractionalPart = new LinkedList<char>();
				_fractionalPart.AddLast('0');
			}
			else
			{
				string decimalSubstring;
				decimalSubstring = decimalString.Substring(0, periodIndex);
				_integerPart = CharacterLinkedListMethods.ConvertStringToLinkedList(decimalSubstring);
				decimalSubstring = decimalString.Substring(periodIndex + 1, decimalString.Length - periodIndex - 1);
				_fractionalPart = CharacterLinkedListMethods.ConvertStringToLinkedList(decimalSubstring);
				if (_integerPart.Count == 0 || _fractionalPart.Count == 0)
					throw new DecimalPeriodError();
			}

			/// Extract sign from integer part
			if (_integerPart.First.Value == '+' || _integerPart.First.Value == '-')
			{
				_isPositive = _integerPart.First.Value == '+';
				_integerPart.RemoveFirst();
                if (_integerPart.Count == 0)
					throw new DecimalPeriodError();
			}
			if (CharacterLinkedListMethods.IsAllZeroes(_integerPart) == true && CharacterLinkedListMethods.IsAllZeroes(_fractionalPart) == true)
				_isPositive = true;

			/// Remove and validate integer and fractional parts
			if (_integerPart.Count > 1 && _integerPart.First.Value == '0')
				throw new DecimalInvalidError();
			foreach (char integerChar in _integerPart)
				if (!char.IsDigit(integerChar))
					throw new DecimalInvalidError();
			CharacterLinkedListMethods.RemoveTrailingZeroes(_fractionalPart);
			foreach(char fractionalChar in _fractionalPart)
				if(!char.IsDigit(fractionalChar))
					throw new DecimalInvalidError();
		}

		/// <summary>
		/// Returns decimal's string representation
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			/// Initialize string builder
			bool fractionalPartIsZero = CharacterLinkedListMethods.IsAllZeroes(_fractionalPart);
			int decimalStringBuilderCapacity = _integerPart.Count;
			decimalStringBuilderCapacity += _isPositive ? 0 : 1;
			decimalStringBuilderCapacity += fractionalPartIsZero ? 0 : _fractionalPart.Count + 1;
			StringBuilder decimalStringBuilder = new StringBuilder(decimalStringBuilderCapacity);

			/// Build string representation
			if (_isPositive == false)
				decimalStringBuilder.Append('-');
			foreach (char integerPartNodeValue in _integerPart)
				decimalStringBuilder.Append(integerPartNodeValue);
			if (fractionalPartIsZero == false)
			{
				decimalStringBuilder.Append('.');
				foreach (char fractionalPartNode in _fractionalPart)
					decimalStringBuilder.Append(fractionalPartNode);
			}

			return decimalStringBuilder.ToString();
		}

		/// <summary>
		/// Performs decimal addition operation; Adds addend2 to addend
		/// </summary>
		/// <param name="addend"></param>
		/// <param name="addend2"></param>
		/// <returns></returns>
		public static Decimal Add(Decimal addend, Decimal addend2)
		{
			/// Determine shift right offset and convert decimals to addition routine format
			int shiftRightOffset = Math.Max(addend._fractionalPart.Count, addend2._fractionalPart.Count);
			LinkedList<char> addendAsInteger = decimalToLinkedList(addend, shiftRightOffset);
			LinkedList<char> addend2AsInteger = decimalToLinkedList(addend2, shiftRightOffset);

			/// Addition routine
			bool resultIsPositive;
            LinkedList<char> result = new LinkedList<char>();
			bool addendIsPositive = addend._isPositive;
			bool addend2IsPositive = addend2._isPositive;
			bool addendIsZero = CharacterLinkedListMethods.IsAllZeroes(addendAsInteger);
			bool addend2IsZero = CharacterLinkedListMethods.IsAllZeroes(addend2AsInteger);
			int addendComparison = CharacterLinkedListMethods.Compare(addendAsInteger, addend2AsInteger);
			if (addendIsZero == true)
			{
				resultIsPositive = addend2IsPositive;
				result = CharacterLinkedListMethods.Clone(addend2AsInteger);
			}
			else if (addend2IsZero == true)
			{
				resultIsPositive = addendIsPositive;
				result = CharacterLinkedListMethods.Clone(addendAsInteger);
			}
			else if (addendIsPositive == addend2IsPositive)
			{
				resultIsPositive = addendIsPositive;
				result = CharacterLinkedListMethods.Add(addendAsInteger, addend2AsInteger);
			}
			else
			{
				if (addendComparison == 0)
				{
					resultIsPositive = true;
					result.AddLast('0');
				}
				else
				{
					resultIsPositive = addendIsPositive == (addendComparison >= 0);
					if (addendComparison == 1)
						result = CharacterLinkedListMethods.Subtract(addendAsInteger, addend2AsInteger);
					else
						result = CharacterLinkedListMethods.Subtract(addend2AsInteger, addendAsInteger);
				}
			}

			/// Delimit integer and fractional parts
			LinkedList<char> integerPart = result;
			LinkedList<char> fractionalPart = new LinkedList<char>();
			CharacterLinkedListMethods.ShiftRight(integerPart, fractionalPart, shiftRightOffset);

			/// Remove padding from integer and fractional parts
			CharacterLinkedListMethods.RemoveLeadingZeroes(integerPart);
			CharacterLinkedListMethods.RemoveTrailingZeroes(fractionalPart);

			return new Decimal(resultIsPositive, integerPart, fractionalPart);
		}

		/// <summary>
		/// Performs decimal subtraction operation; Subtracts subtrahend from minuend
		/// </summary>
		/// <param name="minuend"></param>
		/// <param name="subtrahend"></param>
		/// <returns></returns>
		public static Decimal Subtract(Decimal minuend, Decimal subtrahend)
		{
			Decimal auxiliary = new Decimal(!subtrahend._isPositive, subtrahend._integerPart, subtrahend._fractionalPart);
			return Add(minuend, auxiliary);
		}

		/// <summary>
		/// Performs decimal multiplication; Multiplies multiplicand by multiplicator
		/// </summary>
		/// <param name="multiplicand"></param>
		/// <param name="multiplicator"></param>
		/// <returns></returns>
		public static Decimal Multiply(Decimal multiplicand, Decimal multiplicator)
		{
			/// Convert decimalimals to multiplication routine format
			LinkedList<char> multiplicandAsInteger = decimalToLinkedList(multiplicand);
			LinkedList<char> multiplicatorAsInteger = decimalToLinkedList(multiplicator);
			CharacterLinkedListMethods.RemoveLeadingZeroes(multiplicandAsInteger);
			CharacterLinkedListMethods.RemoveLeadingZeroes(multiplicandAsInteger);

			/// Determine shift right offset
			int shiftRightOffset = multiplicand._fractionalPart.Count + multiplicator._fractionalPart.Count;
			shiftRightOffset -= CharacterLinkedListMethods.RemoveTrailingZeroes(multiplicandAsInteger);
			shiftRightOffset -= CharacterLinkedListMethods.RemoveTrailingZeroes(multiplicatorAsInteger);

			/// Multiplication routine
			LinkedList<char> result;
			if (CharacterLinkedListMethods.IsAllZeroes(multiplicandAsInteger) || CharacterLinkedListMethods.IsAllZeroes(multiplicatorAsInteger))
			{
				result = new LinkedList<char>();
				result.AddLast('0');
			}
			else
            {
				if (multiplicandAsInteger.Count == 1 && multiplicandAsInteger.First.Value == '1')
					result = CharacterLinkedListMethods.Clone(multiplicatorAsInteger);
				else if (multiplicatorAsInteger.Count == 1 && multiplicatorAsInteger.First.Value == '1')
					result = CharacterLinkedListMethods.Clone(multiplicandAsInteger);
				else
					result = CharacterLinkedListMethods.Multiply(multiplicandAsInteger, multiplicatorAsInteger);
            }
			CharacterLinkedListMethods.RemoveLeadingZeroes(result);

			/// Delimit integer and fractional parts
			LinkedList<char> integerPart = result;
			LinkedList<char> fractionalPart = new LinkedList<char>();
			if (shiftRightOffset > 0)
				CharacterLinkedListMethods.ShiftRight(integerPart, fractionalPart, shiftRightOffset);
			else
				CharacterLinkedListMethods.ShiftLeft(integerPart, fractionalPart, -shiftRightOffset);

			/// Remove padding from integer and fractional parts
			CharacterLinkedListMethods.RemoveLeadingZeroes(integerPart);
			CharacterLinkedListMethods.RemoveTrailingZeroes(fractionalPart);

			return new Decimal(multiplicand._isPositive == multiplicator._isPositive, integerPart, fractionalPart);
		}

		/// <summary>
		/// Performs decimal division; Divides dividend by divisor
		/// </summary>
		/// <param name="dividend"></param>
		/// <param name="divisor"></param>
		/// <returns></returns>
		public static Decimal Divide(Decimal dividend, Decimal divisor)
        {
            if (DivisionPrecision <= 0)
                        throw new DecimalDivisionError();

            /// Convert decimals to division routine format
            LinkedList<char> dividendAsInteger = decimalToLinkedList(dividend);
            LinkedList<char> divisorAsInteger = decimalToLinkedList(divisor);
            if (CharacterLinkedListMethods.IsAllZeroes(divisorAsInteger))
                throw new DecimalDivisionError();

			/// Determine shift right offset
			int shiftRightOffset = 0;
			CharacterLinkedListMethods.RemoveLeadingZeroes(dividendAsInteger);
			CharacterLinkedListMethods.RemoveLeadingZeroes(divisorAsInteger);
            shiftRightOffset -= CharacterLinkedListMethods.RemoveTrailingZeroes(dividendAsInteger); 
            shiftRightOffset += CharacterLinkedListMethods.RemoveTrailingZeroes(divisorAsInteger);
            shiftRightOffset += CharacterLinkedListMethods.IsAllZeroes(dividend._fractionalPart) ? 1 : dividend._fractionalPart.Count;
            shiftRightOffset -= CharacterLinkedListMethods.IsAllZeroes(divisor._fractionalPart) ? 1 : divisor._fractionalPart.Count;

			/// Precompute first 10 multiples of divisor
			LinkedList<char> divisorAsIntegerMultiple = new LinkedList<char>();
			LinkedList<char>[] divisorMultipleArray = new LinkedList<char>[10];
			for (int divisorMultiplesIndex = 0; divisorMultiplesIndex < 10; divisorMultiplesIndex++)
			{
				divisorMultipleArray[divisorMultiplesIndex] = divisorAsIntegerMultiple;
				divisorAsIntegerMultiple = CharacterLinkedListMethods.Add(divisorAsIntegerMultiple, divisorAsInteger);
			}

			/// Division routine
			LinkedListNode<char> dividendAsIntegerNode = dividendAsInteger.First;
			LinkedList<char> integerPart = new LinkedList<char>();
			LinkedList<char> fractionalPart = new LinkedList<char>();
			LinkedList<char> auxiliary = new LinkedList<char>();
			bool quotientIsForIntegerPart = true;
			int quotient, precision = shiftRightOffset;
			while (dividendAsIntegerNode != null && CharacterLinkedListMethods.Compare(auxiliary, divisorAsInteger) < 0)
			{
				auxiliary.AddLast(dividendAsIntegerNode.Value);
				dividendAsIntegerNode = dividendAsIntegerNode.Next;
			}
			while ((dividendAsIntegerNode != null || CharacterLinkedListMethods.IsAllZeroes(auxiliary) == false) && (quotientIsForIntegerPart || precision <=  DivisionPrecision))
			{
				CharacterLinkedListMethods.RemoveLeadingZeroes(auxiliary);
				quotient = CharacterLinkedListMethods.LessThanOrEqualBinarySearch(auxiliary, divisorMultipleArray);
				auxiliary = CharacterLinkedListMethods.Subtract(auxiliary, divisorMultipleArray[quotient]);
				(quotientIsForIntegerPart ? integerPart : fractionalPart).AddLast((char)(quotient + '0'));
				if (dividendAsIntegerNode == null)
				{
					quotientIsForIntegerPart = false;
					auxiliary.AddLast('0');
					precision++;
				}
				else
				{
					auxiliary.AddLast(dividendAsIntegerNode.Value);
					dividendAsIntegerNode = dividendAsIntegerNode.Next;
				}
			}
			if(shiftRightOffset > 0)
				CharacterLinkedListMethods.ShiftRight(integerPart, fractionalPart, shiftRightOffset);
			else
				CharacterLinkedListMethods.ShiftLeft(integerPart, fractionalPart, -shiftRightOffset);
			if (fractionalPart.Count == 0)
				fractionalPart.AddLast('0');

			/// Trim fractional part to match division precision
			while (fractionalPart.Count > DivisionPrecision)
				fractionalPart.RemoveLast();

			/// Remove padding from integer and fractional parts
			CharacterLinkedListMethods.RemoveLeadingZeroes(integerPart);
			CharacterLinkedListMethods.RemoveTrailingZeroes(fractionalPart);

			return new Decimal(dividend._isPositive == divisor._isPositive, integerPart, fractionalPart);
		}

		/// <summary>
		/// Return the concatenation of integer and fractional parts
		/// </summary>
		/// <param name="dec"></param>
		/// <param name="fractionalPartCount"></param>
		/// <returns></returns>
		private static LinkedList<char> decimalToLinkedList(Decimal dec, int fractionalPartCount = 0)
		{
			LinkedList<char> newList = new LinkedList<char>();

			foreach (char integerChar in dec._integerPart)
				newList.AddLast(integerChar);
            foreach (char fractionalChar in dec._fractionalPart)
                newList.AddLast(fractionalChar);

            for (int fractionalPartRange = dec._fractionalPart.Count; fractionalPartRange < fractionalPartCount; fractionalPartRange++)
                newList.AddLast('0');

			return newList;
		}

		private bool _isPositive = true;
		public static int DivisionPrecision = 10;
		private LinkedList<char> _integerPart, _fractionalPart;
	}
}
