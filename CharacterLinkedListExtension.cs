using System;
using System.Text;
using System.Collections.Generic;

namespace Calculator
{
	// Extension methods on linked lists
	static class CharacterLinkedListExtension
	{
		// Convert string to linked list
		public static LinkedList<char> StringToLinkedList(string str)
		{
			LinkedList<char> list = new LinkedList<char>();
			foreach (char strChar in str)
				list.AddLast(strChar);
			return list;
		}

		// Convert linked list to string
		public static string LinkedListToString(LinkedList<char> list)
		{
			StringBuilder strBuilder = new StringBuilder(list.Count);
			foreach (char listChar in list)
				strBuilder.Append(listChar);
			return strBuilder.ToString();
		}

		// Deep copy linked list
		public static LinkedList<char> Clone(LinkedList<char> list)
		{
			LinkedList<char> newList = new LinkedList<char>();
			foreach (char listChar in list)
				newList.AddLast(listChar);
			return newList;
		}

		// Checks if linked list contains only digits
		public static bool IsAllDigits(LinkedList<char> list)
		{
			if (list.Count == 0)
				return false;
			foreach (char listChar in list)
				if (!char.IsDigit(listChar))
					return false;
			return true;
		}

		// Checks if linked list contains only character zero
		public static bool IsAllZeroes(LinkedList<char> list)
		{
			if (list.Count == 0)
				return false;
			foreach (char listChar in list)
				if (listChar != '0')
					return false;
			return true;
		}

		// Removes a sequence of character zero from beginning of linked list
		public static int RemoveFirstZeroes(LinkedList<char> list)
		{
			LinkedListNode<char> listNode = list.First, auxiliaryNode;
			int nodeCount = 0;
			while (list.Count > 1 && listNode.Value == '0')
			{
				auxiliaryNode = listNode.Next;
				list.Remove(listNode);
				listNode = auxiliaryNode;
				nodeCount++;
			}
			return nodeCount;
		}

		// Removes a sequence of character zero from end of linked list
		public static int RemoveLastZeroes(LinkedList<char> list)
		{
			LinkedListNode<char> listNode = list.Last, auxiliaryNode;
			int nodeCount = 0;
			while (list.Count > 1 && listNode.Value == '0')
			{
				auxiliaryNode = listNode.Previous;
				list.Remove(listNode);
				listNode = auxiliaryNode;
				nodeCount++;
			}
			return nodeCount;
		}

		// Shift specified number of nodes from first linked list to the other
		public static void ShiftRight(LinkedList<char> list, LinkedList<char> list2, int nodeCount)
		{
			LinkedListNode<char> listNode = list.Last, auxiliaryNode;
			while (nodeCount > 0)
			{
				if (listNode != null)
				{
					auxiliaryNode = listNode.Previous;
					list.Remove(listNode);
					list2.AddFirst(listNode);
					listNode = auxiliaryNode;
				}
				else
					list2.AddFirst('0');
				nodeCount--;
			}
			if (listNode == null)
				list.AddLast('0');
		}

		// Shift specified number of nodes from second linked list to the other
		public static void ShiftLeft(LinkedList<char> list, LinkedList<char> list2, int nodeCount)
		{
			LinkedListNode<char> listNode2 = list2.First, auxiliaryNode;
			while (nodeCount > 0)
			{
				if (listNode2 != null)
				{
					auxiliaryNode = listNode2.Next;
					list2.Remove(listNode2);
					list.AddLast(listNode2);
					listNode2 = auxiliaryNode;
				}
				else
					list.AddLast('0');
				nodeCount--;
			}
			if (listNode2 == null)
				list2.AddFirst('0');
		}

		// Compare linked lists representing unsigned integers
		public static int Compare(LinkedList<char> comparand, LinkedList<char> comparator, bool respectCount = true)
		{
			if (respectCount)
			{
				if (comparand.Count > comparator.Count)
					return 1;
				else if (comparand.Count < comparator.Count)
					return -1;
			}
			LinkedListNode<char> comparandNode = comparand.First, comparatorNode = comparator.First;
			while (comparandNode != null && comparatorNode != null)
			{
				if (comparandNode.Value > comparatorNode.Value)
					return 1;
				else if (comparandNode.Value < comparatorNode.Value)
					return -1;
				comparandNode = comparandNode.Next;
				comparatorNode = comparatorNode.Next;
			}
			return 0;
		}

		// Add linked lists representing unsigned integers
		public static LinkedList<char> Add(LinkedList<char> addend, LinkedList<char> addend2)
		{
			LinkedList<char> result = new LinkedList<char>();
			LinkedListNode<char> addendNode = addend.Last;
			LinkedListNode<char> addend2Node = addend2.Last;
			int sum = 0, carry = 0;
			while (addendNode != null || addend2Node != null)
			{
				sum = 0;
				if (addendNode != null)
				{
					sum += addendNode.Value - '0';
					addendNode = addendNode.Previous;
				}
				if (addend2Node != null)
				{
					sum += addend2Node.Value - '0';
					addend2Node = addend2Node.Previous;
				}
				sum += carry;
				carry = sum / 10;
				result.AddFirst((char)(sum % 10 + '0'));
			}
			if (carry != 0)
				result.AddFirst((char)(carry + '0'));
			return result;
		}

		// Add linked lists representing signed integers
		public static LinkedList<char> SignedAdd(out bool resultIsPositive, bool addendIsPositive, LinkedList<char> addend, bool addend2IsPositive, LinkedList<char> addend2)
		{
			LinkedList<char> result = new LinkedList<char>();
			bool addendIsZero = CharacterLinkedListExtension.IsAllZeroes(addend);
			bool addend2IsZero = CharacterLinkedListExtension.IsAllZeroes(addend2);
			int addendComparison = CharacterLinkedListExtension.Compare(addend, addend2);
			if (addendIsZero == true)
			{
				resultIsPositive = addend2IsPositive;
				result = Clone(addend2);
			}
			else if (addend2IsZero == true)
			{
				resultIsPositive = addendIsPositive;
				result = Clone(addend);
			}
			else if (addendIsPositive == addend2IsPositive)
			{
				resultIsPositive = addendIsPositive;
				result = CharacterLinkedListExtension.Add(addend, addend2);
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
						result = CharacterLinkedListExtension.Subtract(addend, addend2);
					else
						result = CharacterLinkedListExtension.Subtract(addend2, addend);
				}
			}

			return result;
		}

		// Multiply linked lists representing unsigned integers
		public static LinkedList<char> Multiply(LinkedList<char> multiplicand, LinkedList<char> multiplicator)
		{
			// Preallocate space for new list
			LinkedList<char> result = new LinkedList<char>();
			for (int resultRange = 0; resultRange < multiplicand.Count + multiplicator.Count; resultRange++)
				result.AddLast('0');

			LinkedListNode<char> multiplicandNode = multiplicand.Last, multiplicatorNode, resultNode;
			int resultNodeSkipCount = 0, product, carry = 0;
			while (multiplicandNode != null)
			{
				if (multiplicandNode.Value != '0')
				{
					resultNode = result.Last;
					for (int resultRange = 0; resultRange < resultNodeSkipCount; resultRange++)
						resultNode = resultNode.Previous;
					multiplicatorNode = multiplicator.Last;
					carry = 0;
					while (multiplicatorNode != null)
					{
						product = (resultNode.Value - '0') + (multiplicandNode.Value - '0') * (multiplicatorNode.Value - '0') + carry;
						carry = product / 10;
						resultNode.Value = (char)(product % 10 + '0');
						resultNode = resultNode.Previous;
						multiplicatorNode = multiplicatorNode.Previous;
					}
					if (carry != 0)
						resultNode.Value = (char)(carry + '0');
				}
				resultNodeSkipCount++;
				multiplicandNode = multiplicandNode.Previous;
			}
			return result;
		}

		// Division of lists representing unsigned integers
		public static LinkedList<char> Divide(LinkedList<char> dividend, LinkedList<char> divisor, int fractionalPrecision)
		{
			// Precompute first 10 multiples of divisor
			LinkedList<char> auxiliary = new LinkedList<char>();
			LinkedList<char>[] divisorMultiples = new LinkedList<char>[10];
			auxiliary.AddLast('0');
			for (int divisorMultiplesIndex = 0; divisorMultiplesIndex < 10; divisorMultiplesIndex++)
			{
				divisorMultiples[divisorMultiplesIndex] = auxiliary;
				auxiliary = Add(auxiliary, divisor);
			}
			auxiliary.Clear();

			LinkedList<char> result = new LinkedList<char>();
			LinkedListNode<char> dividendNode = dividend.First;
			int quotient;
			while (dividendNode != null && Compare(auxiliary, divisor) < 0)
			{
				auxiliary.AddLast(dividendNode.Value);
				dividendNode = dividendNode.Next;
			}
			while ((dividendNode != null || IsAllZeroes(auxiliary) == false) && fractionalPrecision >= 0)
			{
				RemoveFirstZeroes(auxiliary);
				quotient = LessThanOrEqualBinarySearch(auxiliary, divisorMultiples);
				auxiliary = Subtract(auxiliary, divisorMultiples[quotient]);
				result.AddLast((char)(quotient + '0'));
				if (dividendNode == null)
				{
					auxiliary.AddLast('0');
					fractionalPrecision--;
				}
				else
				{
					auxiliary.AddLast(dividendNode.Value);
					dividendNode = dividendNode.Next;
				}
			}
			return result;
		}

		// Subtract linked lists representing unsigned integers (first list must be bigger than the other)
		public static LinkedList<char> Subtract(LinkedList<char> minuend, LinkedList<char> subtrahend)
		{
			LinkedList<char> result = new LinkedList<char>();
			LinkedListNode<char> minuendNode = minuend.Last;
			LinkedListNode<char> subtrahendNode = subtrahend.Last;
			int difference, carry = 0;
			while (minuendNode != null || subtrahendNode != null)
			{
				difference = 0;
				if (minuendNode != null)
				{
					difference += minuendNode.Value - '0';
					minuendNode = minuendNode.Previous;
				}
				if (subtrahendNode != null)
				{
					difference -= subtrahendNode.Value - '0';
					subtrahendNode = subtrahendNode.Previous;
				}
				difference -= carry;
				if (difference < 0)
				{
					difference += 10;
					carry = 1;
				}
				else
					carry = 0;
				result.AddFirst((char)(difference + '0'));
			}
			return result;
		}

		// Searches index of linked list from linked list array that is smaller than or equal than specified list
		public static int LessThanOrEqualBinarySearch(LinkedList<char> list, LinkedList<char>[] array)
		{
			int leftIndex = 0;
			int rightIndex = array.Length - 1;
			while (leftIndex <= rightIndex)
			{
				int midIndex = (leftIndex + rightIndex) / 2;
				if (Compare(list, array[midIndex]) > 0)
					leftIndex = midIndex + 1;
				else if (Compare(list, array[midIndex]) < 0)
					rightIndex = midIndex - 1;
				else
					return midIndex;
			}
			if (leftIndex > array.Length - 1)
				return array.Length - 1;
			return rightIndex;
		}
	}
}
