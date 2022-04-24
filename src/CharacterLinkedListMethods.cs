using System;
using System.Collections.Generic;
using System.Linq;

namespace Calculator
{
    /// <summary>
    ///	Implements additional methods for linked lists 
    /// </summary>
	static class CharacterLinkedListMethods
    {

        /// <summary>
        /// Indicates whether the specified list contains only the "zero" character
        /// </summary>
        /// <param name="linkedList"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static bool ContainsOnlyZeroes(LinkedList<char> linkedList)
        {
            if (linkedList is null)
                throw new ArgumentNullException(nameof(linkedList));

            return Enumerable.Count(linkedList, linkedListChar => linkedListChar == '0') == linkedList.Count;
        }

        /// <summary>
        // Removes all occurrences of the "zero" characters from the beginning of a list.
        /// </summary>
        /// <param name="linkedList"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static int TrimLeadingZeroes(LinkedList<char> linkedList)
        {
            if (linkedList is null)
                throw new ArgumentNullException(nameof(linkedList));

            int trimCount = 0;
            while (linkedList.Count > 1 && linkedList.First.Value == '0')
            {
                trimCount++;
                linkedList.RemoveFirst();
            }

            return trimCount;
        }

        /// <summary>
        // Removes all occurrences of the "zero" characters from the end of a list
        /// </summary>
        /// <param name="linkedList"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static int TrimTrailingZeroes(LinkedList<char> linkedList)
        {
            if (linkedList is null)
                throw new ArgumentNullException(nameof(linkedList));

            int trimCount = 0;
            while (linkedList.Count > 1 && linkedList.Last.Value == '0')
            {
                linkedList.RemoveLast();
                trimCount++;
            }
            return trimCount;
        }

        /// <summary>
        /// Transfers the specified number of nodes from the beginning of the first LinkedList to the end of the other
        /// </summary>
        /// <param name="firstLinkedList"></param>
        /// <param name="secondLinkedList"></param>
        /// <param name="nodeCount"></param>
        public static void TransferRight(LinkedList<char> firstLinkedList, LinkedList<char> secondLinkedList, int nodeCount)
        {
            if (firstLinkedList is null)
                throw new ArgumentNullException(nameof(firstLinkedList));

            if (secondLinkedList is null)
                throw new ArgumentNullException(nameof(secondLinkedList));

            LinkedListNode<char> firstLinkedListNode = firstLinkedList.Last;
            for (int nodeRange = 0; nodeRange < nodeCount; nodeRange++)
            {
                if (firstLinkedListNode is null)
                    secondLinkedList.AddFirst('0');
                else
                {
                    firstLinkedList.Remove(firstLinkedListNode);
                    secondLinkedList.AddFirst(firstLinkedListNode);
                    firstLinkedListNode = firstLinkedList.Last;
                }
            }

            if (firstLinkedListNode is null)
                firstLinkedList.AddLast('0');
        }

        /// <summary>
        /// Transfer the specified number of nodes from the beginning of the second list to the end of the other
        /// </summary>
        /// <param name="firstLinkedList"></param>
        /// <param name="secondLinkedList"></param>
        /// <param name="nodeCount"></param>
        public static void TransferLeft(LinkedList<char> firstLinkedList, LinkedList<char> secondLinkedList, int nodeCount)
        {
            if (firstLinkedList is null)
                throw new ArgumentNullException(nameof(firstLinkedList));

            if (secondLinkedList is null)
                throw new ArgumentNullException(nameof(secondLinkedList));

            LinkedListNode<char> secondLinkedListNode = secondLinkedList.First;
            for (int nodeRange = 0; nodeRange < nodeCount; nodeRange++)
            {
                if (secondLinkedListNode is null)
                    firstLinkedList.AddLast('0');
                else
                {
                    secondLinkedList.RemoveFirst();
                    firstLinkedList.AddLast(secondLinkedListNode);
                    secondLinkedListNode = secondLinkedList.First;
                }
            }

            if (secondLinkedListNode is null)
                secondLinkedList.AddLast('0');
        }

        /// <summary>
        /// Compares specified lists representing unsigned integers and returns an indication of their relative values
        /// </summary>
        /// <param name="comparand"></param>
        /// <param name="comparator"></param>
        /// <param name="respectCount"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static int Compare(LinkedList<char> comparand, LinkedList<char> comparator, bool respectCount = true)
        {
            if (comparand is null)
                throw new ArgumentNullException(nameof(comparand));

            if (comparator is null)
                throw new ArgumentNullException(nameof(comparator));

            if (respectCount && comparand.Count != comparator.Count)
                return comparand.Count.CompareTo(comparator.Count);

            LinkedListNode<char> comparandNode = comparand.First, comparatorNode = comparator.First;
            while (comparandNode is not null && comparatorNode is not null && comparandNode.Value == comparatorNode.Value)
            {
                comparandNode = comparandNode.Next;
                comparatorNode = comparatorNode.Next;
            }

            if (comparandNode is null || comparatorNode is null)
                return 0;

            return comparandNode.Value.CompareTo(comparatorNode.Value);
        }

        /// <summary>
        /// Adds specified lists representing unsigned integers
        /// </summary>
        /// <param name="firstAddend"></param>
        /// <param name="secondAddend"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static LinkedList<char> Add(LinkedList<char> firstAddend, LinkedList<char> secondAddend)
        {
            if (firstAddend is null)
                throw new ArgumentNullException(nameof(firstAddend));

            if (secondAddend is null)
                throw new ArgumentNullException(nameof(secondAddend));

            LinkedList<char> result = new();
            LinkedListNode<char> firstAddendNode = firstAddend.Last, secondAddendNode = secondAddend.Last;

            int sum, carry = 0;
            while (firstAddendNode is not null || secondAddendNode is not null)
            {
                sum = 0;
                if (firstAddendNode is not null)
                {
                    sum += firstAddendNode.Value - '0';
                    firstAddendNode = firstAddendNode.Previous;
                }
                if (secondAddendNode is not null)
                {
                    sum += secondAddendNode.Value - '0';
                    secondAddendNode = secondAddendNode.Previous;
                }

                sum += carry;
                carry = sum / 10;
                result.AddFirst((char)(sum % 10 + '0'));
            }

            if (carry != 0)
                result.AddFirst((char)(carry + '0'));

            return result;
        }

        /// <summary>
        /// Multiplies specified lists representing unsigned integers
        /// </summary>
        /// <param name="multiplicand"></param>
        /// <param name="multiplicator"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static LinkedList<char> Multiply(LinkedList<char> multiplicand, LinkedList<char> multiplicator)
        {
            if (multiplicand is null)
                throw new ArgumentNullException(nameof(multiplicand));

            if (multiplicator is null)
                throw new ArgumentNullException(nameof(multiplicator));


            // Fills the result list with zero numeric characters
            LinkedList<char> result = new(new string('0', multiplicand.Count + multiplicator.Count));

            LinkedListNode<char> multiplicandNode = multiplicand.Last, multiplicatorNode, resultNode;

            int resultNodeSkipCount = 0, product, carry;
            while (multiplicandNode is not null)
            {
                if (multiplicandNode.Value != '0')
                {
                    resultNode = result.Last;
                    multiplicatorNode = multiplicator.Last;

                    for (int resultRange = 0; resultRange < resultNodeSkipCount; resultRange++)
                        resultNode = resultNode.Previous;

                    carry = 0;
                    while (multiplicatorNode is not null)
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

        /// <summary>
        /// Subtracts specified lists representing unsigned integers
        /// </summary>
        /// <param name="minuend"></param>
        /// <param name="subtrahend"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static LinkedList<char> Subtract(LinkedList<char> minuend, LinkedList<char> subtrahend)
        {
            if (minuend is null)
                throw new ArgumentNullException(nameof(minuend));

            if (subtrahend is null)
                throw new ArgumentNullException(nameof(subtrahend));

            LinkedList<char> result = new();
            LinkedListNode<char> minuendNode = minuend.Last, subtrahendNode = subtrahend.Last;

            int difference, carry = 0;
            while (minuendNode is not null || subtrahendNode is not null)
            {
                difference = 0;
                if (minuendNode is not null)
                {
                    difference += minuendNode.Value - '0';
                    minuendNode = minuendNode.Previous;
                }
                if (subtrahendNode is not null)
                {
                    difference -= subtrahendNode.Value - '0';
                    subtrahendNode = subtrahendNode.Previous;
                }

                difference -= carry;
                if (difference >= 0)
                    carry = 0;
                else
                {
                    difference += 10;
                    carry = 1;
                }

                result.AddFirst((char)(difference + '0'));
            }

            return result;
        }

        /// <summary>
        /// Return the index of a list in the array that is less than or equal to the specified list
        /// </summary>
        /// <param name="linkedList"></param>
        /// <param name="linkedListArray"></param>
        /// <returns></returns>
        public static int LEBinarySearch(LinkedList<char> linkedList, LinkedList<char>[] linkedListArray)
        {
            int leftIndex = 0;
            int rightIndex = linkedListArray.Length - 1;

            while (leftIndex <= rightIndex)
            {
                int midIndex = (leftIndex + rightIndex) / 2;
                int comparison = Compare(linkedList, linkedListArray[midIndex]);

                if (comparison == 0)
                    return midIndex;

                if (comparison > 0)
                    leftIndex = midIndex + 1;
                if (comparison < 0)
                    rightIndex = midIndex - 1;
            }

            if (leftIndex > linkedListArray.Length - 1)
                return linkedListArray.Length - 1;

            return rightIndex;
        }
    }
}
