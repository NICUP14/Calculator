using System;
using System.Collections.Generic;
using System.Text;

namespace Calculator
{
    /// <summary>
    /// Implements methods to parse and calculate mathematical expressions
    /// </summary>
    static class ExpressionParser
    {
        /// <summary>
        /// Converts the specified string to an abstract array of tokens
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static Token[] Parse(string expression)
        {
            if (expression is null)
                throw new ArgumentNullException(nameof(expression));

            if (expression.Length == 0)
                return Array.Empty<Token>();

            /// Removes all spaces from the expression
            expression = expression.Replace(" ", string.Empty);

            /// Infers the token type of a character of a token's string representation
            static Token.TokenType characterToTokenType(char character)
            {
                if (char.IsDigit(character) || character == '.')
                    return Token.TokenType.Decimal;

                if (character == '(' || character == ')')
                    return Token.TokenType.Parenthesis;

                return Token.TokenType.Operator;
            }

            /// Indicates whether to extract the current token's string representation
            static bool PerformSplit(Token.TokenType firstTokenType, Token.TokenType secondTokenType)
            {
                bool secondTokenTypeIsUndefined = secondTokenType == Token.TokenType.Undefined;
                bool firstTokenTypeIsParenthesis = firstTokenType == Token.TokenType.Parenthesis;
                bool secondTokenTypeIsParenthesis = secondTokenType == Token.TokenType.Parenthesis;

                return !secondTokenTypeIsUndefined && (firstTokenTypeIsParenthesis || secondTokenTypeIsParenthesis || firstTokenType != secondTokenType);
            }

            Token.TokenType tokenType, previousTokenType = Token.TokenType.Undefined;

            /// Determines the length of the array of tokens
            int tokenArrayLength = 1;
            foreach (char expressionChar in expression)
            {
                tokenType = characterToTokenType(expressionChar);

                if (PerformSplit(tokenType, previousTokenType))
                    tokenArrayLength++;

                previousTokenType = tokenType;
            }

            /// Converts the specified token's string representation to its token counterpart
            static Token StringToToken(string stringRepresentation)
            {
                if (stringRepresentation is null)
                    throw new ArgumentNullException(nameof(stringRepresentation));

                int digitAndPeriodCount = System.Linq.Enumerable.Count(
                    stringRepresentation, character => char.IsDigit(character) || character == '.');

                if (digitAndPeriodCount == stringRepresentation.Length)
                {
                    try
                    {
                        return new DecimalToken(stringRepresentation);
                    }

                    /// Up-casts the thrown DecimalFormatException
                    catch (DecimalFormatException)
                    {
                        throw new ExpressionParserTokenException();
                    }
                }

                /// Throws an exception in case the token isn't recognized
                if (!tokenLookup.ContainsKey(stringRepresentation))
                    throw new ExpressionParserTokenException();

                return tokenLookup[stringRepresentation];
            }

            previousTokenType = Token.TokenType.Undefined;

            /// Splits the expression into an array of tokens
            int tokenArrayIndex = 0;
            Token[] tokenArray = new Token[tokenArrayLength];
            StringBuilder tokenBuilder = new(expression.Length);
            foreach (char expressionChar in expression)
            {
                tokenType = characterToTokenType(expressionChar);

                if (PerformSplit(tokenType, previousTokenType))
                {
                    Token token = StringToToken(tokenBuilder.ToString());

                    tokenArray[tokenArrayIndex++] = token;
                    tokenBuilder.Clear();
                }

                tokenBuilder.Append(expressionChar);
                previousTokenType = tokenType;
            }

            tokenArray[tokenArrayIndex++] = StringToToken(tokenBuilder.ToString());

            return tokenArray;
        }

        /// <summary>
        /// Constructs an equivalent of the initial infix expression
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static string ReverseParse(Token[] tokenArray)
        {
            if (tokenArray is null)
                throw new ArgumentNullException(nameof(tokenArray));

            if (tokenArray.Length == 0)
                return string.Empty;

            /// Re-arranges a shallow copy of the array of tokens into postfix order
            Token[] tokenArrayClone = tokenArray.Clone() as Token[];
            ReorderTokenArray(ref tokenArrayClone);

            /// Reconstructs the expression
            Stack<string> operandStack = new();
            foreach (Token token in tokenArrayClone)
            {
                if (token is null)
                    continue;

                if (token.Equals(Token.Decimal))
                    operandStack.Push(token.ToString());

                else if (token.Equals(Token.Operator))
                {
                    if (operandStack.Count < 2)
                        throw new ExpressionParserSyntaxException();

                    /// Extracts the operands from the stack
                    string operand = operandStack.Pop();
                    string operand2 = operandStack.Pop();
                    (operand, operand2) = (operand2, operand);

                    /// Pushes the reconstructed sub-expression back onto the stack
                    operandStack.Push(TokenStringRepresentation.OpeningParenthesis + operand + token + operand2 + TokenStringRepresentation.ClosingParenthesis);
                }
            }

            if (operandStack.Count > 1)
                throw new ExpressionParserSyntaxException();

            return operandStack.Peek();
        }

        /// <summary>
        /// Calculates an array of tokens representing an expression and returns its result in Decimal format
        /// </summary>
        /// <param name="tokenArray"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static Decimal Calculate(Token[] tokenArray)
        {
            if (tokenArray is null)
                throw new ArgumentNullException(nameof(tokenArray));

            if (tokenArray.Length == 0)
                return Decimal.Zero;

            /// Re-arranges a shallow copy of the array of tokens into postfix order
            Token[] tokenArrayClone = tokenArray.Clone() as Token[];
            ReorderTokenArray(ref tokenArrayClone);

            return CalculateTokenArray(tokenArrayClone);
        }

        /// <summary>
        /// Calculates an expression and returns its result in Decimal format
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static Decimal Calculate(string expression)
        {
            if (expression is null)
                throw new ArgumentNullException(nameof(expression));

            if (expression.Length == 0)
                return Decimal.Zero;

            /// Re-arranges the parsed array of tokens into postfix order
            Token[] tokenArray = Parse(expression);
            ReorderTokenArray(ref tokenArray);

            return CalculateTokenArray(tokenArray);
        }

        /// <summary>
        /// Calculates an array of tokens representing an expression in postfix order and returns its result in Decimal format
        /// </summary>
        /// <param name="tokenArray"></param>
        /// <returns></returns>
        /// <exception cref="ExpressionParserSyntaxException"></exception>
        private static Decimal CalculateTokenArray(Token[] tokenArray)
        {
            if (tokenArray is null)
                throw new ArgumentNullException(nameof(tokenArray));

            /// Algorithm based on the postfix notation evaluation algorithm

            /*
                For each an every token:
                    If it's a number
                        Push the number onto the stack
                    If it's an operator
                        Pop and swap operands from the stack
                        Evaluate the operator and push the result back to the stack
                Pop result from the stack
            */

            /// Calculates the array of tokens
            Stack<Decimal> operandStack = new();
            foreach (Token token in tokenArray)
            {
                if (token is null)
                    continue;

                if (token.Equals(Token.Decimal))
                    operandStack.Push((token as DecimalToken).ToDecimal());

                else if (token.Equals(Token.Operator))
                {
                    if (operandStack.Count < 2)
                        throw new ExpressionParserSyntaxException();

                    /// Extracts operands from the stack
                    Decimal operand = operandStack.Pop();
                    Decimal operand2 = operandStack.Pop();
                    (operand, operand2) = (operand2, operand);


                    /// Evaluates operation based on the token operator
                    Decimal operandResult = new();
                    OperatorToken operatorToken = token as OperatorToken;

                    if (operatorToken.Equals(OperatorToken.Division))
                    {
                        try
                        {
                            operandResult = Decimal.Divide(operand, operand2);
                        }

                        /// Up-casts the thrown DecimalArithmeticException
                        catch (DecimalArithmeticException)
                        {
                            throw new ExpressionParserSyntaxException();
                        }
                    }
                    else if (operatorToken.Equals(OperatorToken.Addition))
                        operandResult = Decimal.Add(operand, operand2);
                    else if (operatorToken.Equals(OperatorToken.Subtraction))
                        operandResult = Decimal.Subtract(operand, operand2);
                    else if (operatorToken.Equals(OperatorToken.Multiplication))
                        operandResult = Decimal.Multiply(operand, operand2);

                    /// Pushes the operation result back onto the stack
                    operandStack.Push(operandResult);
                }
            }

            if (operandStack.Count > 1)
                throw new ExpressionParserSyntaxException();

            return operandStack.Peek();
        }

        /// <summary>
        /// Changes the order of the array of tokens from infix to postfix notation
        /// </summary>
        /// <param name="tokenArray"></param>
        /// <returns></returns>
        public static void ReorderTokenArray(ref Token[] tokenArray)
        {
            if (tokenArray is null)
                throw new ArgumentNullException(nameof(tokenArray));

            /// Algorithm based on Edsger W. Dijkstra's shunting-yard algorithm
            /// Additionally, this algorithm performs anti-function checks and converts unary addition and subtraction operators to their binary counter-part
            /// Function-like structures n(m), (n)m, (n)(m) are troublesome because they exploit the postfix conversion algorithm
            /// Example: infix expression "(1)(2)+" is represented in postfix as "1 2 +", which is equivalent to the infix expression "1+2"

            /*
                For each and every token:
                    If it's an operator
                        While there is an operator on the top of the stack with greater precedence:
                             Pop operator from the operator stack and add it to the postfix array
                        Push the current operator onto the operator stack
                    If it's a parenthesis
                        If it's an opening parenthesis
                            Push the current parenthesis onto the operator stack
                        If it's a closing parenthesis
                            While there isn't an opening parenthesis at the top of the stack:
                                Pop operators from the operator stack and add it to the postfix array
                            Pop the current parenthesis from the operator stack
                While there are operators on the stack:
                    Pop operator from the operator stack and add it to the postfix array
            */


            /// Re-orders the array of tokens to postfix notation
            int postfixTokenArrayIndex = 0;
            Stack<Token> operatorStack = new();
            Token previousToken = Token.Undefined;
            Token[] postfixTokenArray = new Token[tokenArray.Length + 1];
            foreach (Token token in tokenArray)
            {
                if (token is null)
                    continue;

                if (token.Equals(Token.Decimal))
                {
                    /// Checks for a function-like structure (check #1)
                    if (previousToken.Equals(ParenthesisToken.ClosingParenthesis))
                        throw new ExpressionParserSyntaxException();

                    postfixTokenArray[postfixTokenArrayIndex++] = token;
                }
                else if (token.Equals(Token.Parenthesis))
                {
                    if (token.Equals(ParenthesisToken.OpeningParenthesis))
                    {
                        /// Checks for a function-like structure (check #2)
                        if (previousToken.Equals(Token.Decimal) || previousToken.Equals(ParenthesisToken.ClosingParenthesis))
                            throw new ExpressionParserSyntaxException();

                        operatorStack.Push(token);
                    }
                    else
                    {
                        /// Checks for a function-like structure (check #3)
                        if (previousToken.Equals(ParenthesisToken.OpeningParenthesis))
                            throw new ExpressionParserSyntaxException();

                        while (operatorStack.Count > 0 && !operatorStack.Peek().Equals(ParenthesisToken.OpeningParenthesis))
                            postfixTokenArray[postfixTokenArrayIndex++] = operatorStack.Pop();

                        if (operatorStack.Count == 0)
                            throw new ExpressionParserSyntaxException();

                        operatorStack.Pop();
                    }
                }
                else if (token.Equals(Token.Operator))
                {
                    /// Converts the unary addition and subtraction operators to their binary counter-part
                    if (previousToken.Equals(Token.Undefined) || previousToken.Equals(ParenthesisToken.OpeningParenthesis))
                    {
                        if (token.Equals(OperatorToken.Addition))
                            continue;
                        else if (token.Equals(OperatorToken.Subtraction))
                            postfixTokenArray[postfixTokenArrayIndex++] = new DecimalToken("0");
                    }

                    while (operatorStack.Count > 0 && !operatorStack.Peek().Equals(Token.Parenthesis) && OperatorToken.Compare((token as OperatorToken), (operatorStack.Peek() as OperatorToken)) <= 0)
                        postfixTokenArray[postfixTokenArrayIndex++] = operatorStack.Pop();

                    operatorStack.Push(token);
                }

                previousToken = token;
            }

            while (operatorStack.Count > 0)
            {
                if (operatorStack.Peek().Equals(Token.Parenthesis))
                    throw new ExpressionParserSyntaxException();

                postfixTokenArray[postfixTokenArrayIndex++] = operatorStack.Pop();
            }

            tokenArray = postfixTokenArray;
        }

        /// <summary>
        /// Links a token's string representation to its token counter-part
        /// </summary>
        private static readonly Dictionary<string, Token> tokenLookup = new()
        {
            { TokenStringRepresentation.Division, OperatorToken.Division },
            { TokenStringRepresentation.Addition, OperatorToken.Addition },
            { TokenStringRepresentation.Subtraction, OperatorToken.Subtraction },
            { TokenStringRepresentation.Multiplication, OperatorToken.Multiplication },
            { TokenStringRepresentation.OpeningParenthesis, ParenthesisToken.OpeningParenthesis },
            { TokenStringRepresentation.ClosingParenthesis, ParenthesisToken.ClosingParenthesis },
        };
    }
}
