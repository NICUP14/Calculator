using System;
using System.Collections.Generic;
using System.Text;

namespace Calculator
{
	// Arbitrary precision decimal implementation
	class Decimal
	{
		private Decimal(bool isPositive, LinkedList<char> integerPart, LinkedList<char> fractionalPart)
		{
			_isPositive = isPositive;
			_integerPart = integerPart;
			_fractionalPart = fractionalPart;
		}

		// Constructor decimal by string representation
		public Decimal(string decimalString = "0")
		{
			if (string.IsNullOrEmpty(decimalString))
				throw new Exception("DecimalNullError");

			// Split string representation into integer and fractional parts
			int periodIndex = decimalString.LastIndexOf('.');
			if (periodIndex == -1)
			{
				_integerPart = CharacterLinkedListExtension.StringToLinkedList(decimalString);
				_fractionalPart = new LinkedList<char>();
				_fractionalPart.AddLast('0');
			}
			else
			{
				string decimalSubstring;
				decimalSubstring = decimalString.Substring(0, periodIndex);
				_integerPart = CharacterLinkedListExtension.StringToLinkedList(decimalSubstring);
				decimalSubstring = decimalString.Substring(periodIndex + 1, decimalString.Length - periodIndex - 1);
				_fractionalPart = CharacterLinkedListExtension.StringToLinkedList(decimalSubstring);
				if (_integerPart.Count == 0 || _fractionalPart.Count == 0)
					throw new Exception("DecimalPeriodError");
			}

			// Extract sign from integer part
			if (_integerPart.First.Value == '+' || _integerPart.First.Value == '-')
			{
				_isPositive = _integerPart.First.Value == '+';
				_integerPart.RemoveFirst();
				if (_integerPart.Count == 0)
					throw new Exception("DecimalPeriodError");
			}
			if (CharacterLinkedListExtension.IsAllZeroes(_integerPart) == true && CharacterLinkedListExtension.IsAllZeroes(_fractionalPart) == true)
				_isPositive = true;

			// Remove and validate integer and fractional parts
			if (_integerPart.Count > 1 && _integerPart.First.Value == '0')
				throw new Exception("DecimalInvalidError");
			foreach(char integerChar in _integerPart)
				if(!char.IsDigit(integerChar))
					throw new Exception("DecimalInvalidError");
			CharacterLinkedListExtension.RemoveLastZeroes(_fractionalPart);
			foreach(char fractionalChar in _fractionalPart)
				if(!char.IsDigit(fractionalChar))
					throw new Exception("DecimalInvalidError");
		}

		// Decimal's string representation
		public override string ToString()
		{
			// Initialize string builder
			bool fractionalPartIsZero = CharacterLinkedListExtension.IsAllZeroes(_fractionalPart);
			int decimalStringLength = _integerPart.Count;
			decimalStringLength += _isPositive ? 0 : 1;
			decimalStringLength += fractionalPartIsZero ? 0 : _fractionalPart.Count + 1;
			StringBuilder decimalStringBuilder = new StringBuilder(decimalStringLength);

			// Build string representation
			if (_isPositive == false)
				decimalStringBuilder.Append('-');
			foreach (char integerPartNodeValue in _integerPart)
				decimalStringBuilder.Append(integerPartNodeValue);
			if (fractionalPartIsZero == false)
			{
				decimalStringBuilder.Append('.');
				foreach (char fractionalPartNodeValue in _fractionalPart)
					decimalStringBuilder.Append(fractionalPartNodeValue);
			}

			return decimalStringBuilder.ToString();
		}

		// Decimal's addition operation
		public static Decimal Add(Decimal addend, Decimal addend2)
		{
			// Convert decimals to addition routine format
			int exponent = Math.Max(addend._fractionalPart.Count, addend2._fractionalPart.Count);
			LinkedList<char> addendAsInteger = decimalToLinkedList(addend, exponent);
			LinkedList<char> addend2AsInteger = decimalToLinkedList(addend2, exponent);

			// Addition routine
			bool resultIsPositive;
			LinkedList<char> result = CharacterLinkedListExtension.SignedAdd(out resultIsPositive, addend._isPositive, addendAsInteger, addend2._isPositive, addend2AsInteger);

			// Delimit integer and fractional parts
			LinkedList<char> fractionalPart = new LinkedList<char>();
			CharacterLinkedListExtension.ShiftRight(result, fractionalPart, exponent);
			LinkedList<char> integerPart = result;

			// Remove padding from integer and fractional parts
			CharacterLinkedListExtension.RemoveFirstZeroes(integerPart);
			CharacterLinkedListExtension.RemoveLastZeroes(fractionalPart);

			return new Decimal(resultIsPositive, integerPart, fractionalPart);
		}

		// Decimal's subtraction operation (wrapper for addition)
		public static Decimal Subtract(Decimal minuend, Decimal subtrahend)
		{
			Decimal auxiliary = new Decimal(!subtrahend._isPositive, subtrahend._integerPart, subtrahend._fractionalPart);
			return Add(minuend, auxiliary);
		}

		// Decimal's multiplication operation
		public static Decimal Multiply(Decimal multiplicand, Decimal multiplicator)
		{
			// Convert decimals to multiplication routine format
			LinkedList<char> multiplicandAsInteger = decimalToLinkedList(multiplicand);
			LinkedList<char> multiplicatorAsInteger = decimalToLinkedList(multiplicator);
			CharacterLinkedListExtension.RemoveFirstZeroes(multiplicandAsInteger);
			CharacterLinkedListExtension.RemoveFirstZeroes(multiplicandAsInteger);
			int exponent = multiplicand._fractionalPart.Count + multiplicator._fractionalPart.Count;
			exponent -= CharacterLinkedListExtension.RemoveLastZeroes(multiplicandAsInteger);
			exponent -= CharacterLinkedListExtension.RemoveLastZeroes(multiplicatorAsInteger);

			// Multiplication routine
			LinkedList<char> result;
			if (CharacterLinkedListExtension.IsAllZeroes(multiplicandAsInteger) || CharacterLinkedListExtension.IsAllZeroes(multiplicatorAsInteger))
			{
				result = new LinkedList<char>();
				result.AddLast('0');
			}
			else
				result = multiplicatorAsInteger.Count == 1 && multiplicatorAsInteger.First.Value == '1' ? CharacterLinkedListExtension.Clone(multiplicandAsInteger) : CharacterLinkedListExtension.Multiply(multiplicandAsInteger, multiplicatorAsInteger);
			CharacterLinkedListExtension.RemoveFirstZeroes(result);

			// Delimit integer and fractional parts
			LinkedList<char> integerPart = result;
			LinkedList<char> fractionalPart = new LinkedList<char>();
			if (exponent > 0)
				CharacterLinkedListExtension.ShiftRight(integerPart, fractionalPart, exponent);
			else
				CharacterLinkedListExtension.ShiftLeft(integerPart, fractionalPart, -exponent);

			// Remove padding from integer and fractional parts
			CharacterLinkedListExtension.RemoveFirstZeroes(integerPart);
			CharacterLinkedListExtension.RemoveLastZeroes(fractionalPart);

			return new Decimal(multiplicand._isPositive == multiplicator._isPositive, integerPart, fractionalPart);
		}

		public static Decimal Division(Decimal dividend, Decimal divisor)
		{
			// Convert decimals to division routine format
			LinkedList<char> dividendAsInteger = decimalToLinkedList(dividend);
			LinkedList<char> divisorAsInteger = decimalToLinkedList(divisor);
			if (CharacterLinkedListExtension.IsAllZeroes(divisorAsInteger))
				throw new Exception("DecimalDivisionError");
			CharacterLinkedListExtension.RemoveFirstZeroes(dividendAsInteger);
			CharacterLinkedListExtension.RemoveFirstZeroes(divisorAsInteger);
			int exponentOffset = CharacterLinkedListExtension.RemoveLastZeroes(dividendAsInteger) - CharacterLinkedListExtension.RemoveLastZeroes(divisorAsInteger) + divisor._fractionalPart.Count - dividend._fractionalPart.Count;
			int intermediateExponent;
			if (CharacterLinkedListExtension.Compare(dividendAsInteger, divisorAsInteger) > 0)
				intermediateExponent = dividendAsInteger.Count - divisorAsInteger.Count + (CharacterLinkedListExtension.Compare(dividendAsInteger, divisorAsInteger, false)) > 0 ? 1 : 0;
			else
				intermediateExponent = 1;
			int fractionalPrecision = exponentOffset + DivisionPrecision;

			// Division routine
			LinkedList<char> integerPart = new LinkedList<char>();
			LinkedList<char> fractionalPart;
			if (CharacterLinkedListExtension.IsAllZeroes(dividendAsInteger))
			{
				fractionalPart = new LinkedList<char>();
				fractionalPart.AddLast('0');
			}
			else if (CharacterLinkedListExtension.Compare(dividendAsInteger, divisorAsInteger) == 0)
			{
				fractionalPart = new LinkedList<char>();
				fractionalPart.AddLast('1');
			}
			else
				fractionalPart = divisorAsInteger.Count == 1 && divisorAsInteger.First.Value == '1' ? CharacterLinkedListExtension.Clone(dividendAsInteger) : CharacterLinkedListExtension.Divide(dividendAsInteger, divisorAsInteger, fractionalPrecision);
			CharacterLinkedListExtension.ShiftLeft(integerPart, fractionalPart, intermediateExponent);
			if (exponentOffset > 0)
				CharacterLinkedListExtension.ShiftLeft(integerPart, fractionalPart, exponentOffset);
			else
				CharacterLinkedListExtension.ShiftRight(integerPart, fractionalPart, -exponentOffset);

			// Remove padding from integer and fractional parts
			CharacterLinkedListExtension.RemoveFirstZeroes(integerPart);
			CharacterLinkedListExtension.RemoveLastZeroes(fractionalPart);

			return new Decimal(dividend._isPositive == divisor._isPositive, integerPart, fractionalPart);
		}

		// Convert decimal to linked list representing an unsigned integer
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

		// Decimal's fields
		public static int DivisionPrecision = 10;
		private bool _isPositive = true;
		private LinkedList<char> _integerPart, _fractionalPart;
	}
}
