using System;
using System.Collections.Generic;
using System.Text;

namespace Calculator
{
    // Expression parser implementation
    class Expression
	{
		// Convert expression from infix to postfix notation
		public static string ToPostfix(string infix)
		{
			if (string.IsNullOrEmpty(infix))
				throw new Exception("ExpressionNullError");

			// Split infix expression
			StringBuilder infixSubstringBuilder = new StringBuilder(infix.Length);
			ElementType infixCharElementType, previousInfixCharElementType = ElementType.Undefined;
			List<Tuple<string, ElementType>> infixSubstringTuples = new List<Tuple<string, ElementType>>();
			foreach (char infixChar in infix)
			{
				if (infixChar == '(' || infixChar == ')')
					infixCharElementType = infixChar == '(' ? ElementType.OpeningParenthesis : ElementType.ClosingParenthesis;
				else if (infixChar == '.' || char.IsDigit(infixChar))
					infixCharElementType = ElementType.Decimal;
				else
					infixCharElementType = ElementType.Operator;

				// Split if element is parenthesis or element types do not match
				bool infixCharIsParenthesis = infixCharElementType == ElementType.OpeningParenthesis || infixCharElementType == ElementType.ClosingParenthesis;
				bool previousInfixCharIsParenthesis = previousInfixCharElementType == ElementType.OpeningParenthesis || previousInfixCharElementType == ElementType.ClosingParenthesis;
				if (previousInfixCharElementType != ElementType.Undefined && (infixCharIsParenthesis || previousInfixCharIsParenthesis || infixCharElementType != previousInfixCharElementType))
				{
					infixSubstringTuples.Add(new Tuple<string, ElementType>(infixSubstringBuilder.ToString(), previousInfixCharElementType));
					infixSubstringBuilder.Clear();
				}
				infixSubstringBuilder.Append(infixChar);
				previousInfixCharElementType = infixCharElementType;
			}
			infixSubstringTuples.Add(new Tuple<string, ElementType>(infixSubstringBuilder.ToString(), previousInfixCharElementType));

			// Convert expression to postfix via shunting yard algorithm
			Stack<string> operatorStack = new Stack<string>();
			StringBuilder postfixBuilder = new StringBuilder(infix.Length);
			ElementType previousType = ElementType.OpeningParenthesis;
			foreach (Tuple<string, ElementType> infixSubstringTuple in infixSubstringTuples)
			{
				string infixSubstring = infixSubstringTuple.Item1;
				ElementType type = infixSubstringTuple.Item2;

				// Element handler
				switch (type)
				{
					case ElementType.Decimal:
						postfixBuilder.Append(infixSubstring);
						postfixBuilder.Append(' ');
						break;

					case ElementType.OpeningParenthesis:
						if (previousType == ElementType.Decimal)
							throw new Exception("ExpressionSyntaxError");
						operatorStack.Push("(");
						break;

					case ElementType.ClosingParenthesis:
						if (previousType == ElementType.OpeningParenthesis || previousType == ElementType.Operator)
							throw new Exception("ExpressionSyntaxError");
						while (operatorStack.Count > 0 && operatorStack.Peek() != "(")
						{
							postfixBuilder.Append(operatorStack.Pop());
							postfixBuilder.Append(" ");
						}
						if (operatorStack.Count == 0)
							throw new Exception("ExpressionSyntaxError");
						operatorStack.Pop();
						break;

					case ElementType.Operator:
						if (!_operatorDict.ContainsKey(infixSubstring))
							throw new Exception("ExpressionElementError");
						if (previousType == ElementType.OpeningParenthesis)
                        {
							if (infixSubstring == "+")
								break;
							else if (infixSubstring == "-")
								postfixBuilder.Append("0 ");
                        }
						while (operatorStack.Count > 0 && operatorStack.Peek() != "(" && _operatorDict[infixSubstring].CompareTo(_operatorDict[operatorStack.Peek()]) <= 0)
						{
							postfixBuilder.Append(operatorStack.Pop());
							postfixBuilder.Append(" ");
						}
						operatorStack.Push(infixSubstring);
						break;
				}
				previousType = type;
			}
			while (operatorStack.Count > 0)
			{
				if (operatorStack.Peek() == "(")
					throw new Exception("ExpressionSyntaxError");
				postfixBuilder.Append(operatorStack.Pop());
				postfixBuilder.Append(" ");
			}
			postfixBuilder.Remove(postfixBuilder.Length - 1, 1);

			return postfixBuilder.ToString();
		}

		// Convert expression from postfix to infix notation
		public static string ToInfix(string postfix)
		{
			if (string.IsNullOrEmpty(postfix))
				throw new Exception("ExpressionNullError");

			Stack<string> operandStack = new Stack<string>();
			foreach (string postfixSubstring in postfix.Split(" "))
			{
				if (stringIsDecimal(postfixSubstring))
					operandStack.Push(postfixSubstring);
				else
				{
					if (operandStack.Count < 2)
						throw new Exception("ExpressionSyntaxError");

					// Operation handler
					string operand = operandStack.Pop();
					string operand2 = operandStack.Pop();
					(operand, operand2) = (operand2, operand);
					if (CalculateExpression)
					{
						Decimal resultDecimal = new Decimal();
						Decimal operandAsDecimal = new Decimal(operand);
						Decimal operand2AsDecimal = new Decimal(operand2);

						// Perform operation by type
						switch (_operatorDict[postfixSubstring].Type)
						{
							case OperatorType.Multiplication:
								resultDecimal = Decimal.Multiply(operandAsDecimal, operand2AsDecimal);
								break;

							case OperatorType.Division:
								resultDecimal = Decimal.Division(operandAsDecimal, operand2AsDecimal);
								break;

							case OperatorType.Addition:
								resultDecimal = Decimal.Add(operandAsDecimal, operand2AsDecimal);
								break;

							case OperatorType.Subtraction:
								resultDecimal = Decimal.Subtract(operandAsDecimal, operand2AsDecimal);
								break;
						}
						operandStack.Push(resultDecimal.ToString());
					}
					else
						operandStack.Push($"({operand}{postfixSubstring}{operand2})");
				}
			}

			return CalculateExpression ? new Decimal(operandStack.Peek()).ToString() : operandStack.Peek();
		}

		// Checks if string is decimal
		private static bool stringIsDecimal(string str)
		{
			if (string.IsNullOrEmpty(str))
				return false;

			int strIndex = str.Length > 1 && (str[0] == '+' || str[0] == '-') ? 1 : 0;
			while (strIndex < str.Length)
			{
				if (str[strIndex] != '.' && !char.IsDigit(str[strIndex]))
					return false;
				strIndex++;
			}
			return true;
		}

		public enum ElementType
		{
			Undefined,
			Decimal,
			Operator,
			OpeningParenthesis,
			ClosingParenthesis,
		}

		public enum OperatorType
		{
			Multiplication,
			Division,
			Addition,
			Subtraction,
		}

		// Expression's fields
		public static bool CalculateExpression = true;
		private static Dictionary<string, Operator> _operatorDict = new Dictionary<string, Operator>
        {
            {"*", new Operator(2, OperatorType.Multiplication)},
            {"/", new Operator(2, OperatorType.Division)},
            {"+", new Operator(1, OperatorType.Addition)},
            {"-", new Operator(1, OperatorType.Subtraction)},
        };

	}
}
