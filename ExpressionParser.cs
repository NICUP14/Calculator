using System.Text;
using static System.Console;
using System.Collections.Generic;

namespace Calculator
{
    /// <summary>
    /// Expression parser implementation
    /// </summary>
    class ExpressionParser
    {
        /// <summary>
        /// Returns result of expression in decimal format
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static Decimal Calculate(string expression)
        {
            if (string.IsNullOrEmpty(expression))
                throw new ExpressionParserNullError();

            /// Convert expression to a token array in postfix order
            Token[] tokenArray = expressionToTokenArray(expression);
            changeTokenArrayOrder(ref tokenArray);

            /// Evaluate postfix expression by using a stack

            /*
                While there are tokens to be read:
                    Read a token
                    If it's a number
                        Push the number onto the stack
                    If it's an operator
                        Pop and swap operands from the stack
                        Evaluate the operator and push the result back to the stack
                Pop result from the stack
            */
                        
            Stack<Decimal> operandStack = new Stack<Decimal>();
            foreach (Token token in tokenArray)
            {
                if (token != null)
                {
                    if (token.IsDecimalToken())
                        operandStack.Push((token as DecimalToken).ToDecimal());
                    else if (token.IsOperatorToken())
                    {
                        if (operandStack.Count < 2)
                            throw new ExpressionParserSyntaxError();

                        /// Extract operands from the stack
                        Decimal operand = operandStack.Pop();
                        Decimal operand2 = operandStack.Pop();
                        Decimal operandResult = new Decimal();
                        (operand, operand2) = (operand2, operand);

                        /// Evaluate based on the token operator
                        OperatorToken operatorToken = token as OperatorToken;
                        if (operatorToken.IsAdditionOperatorToken())
                            operandResult = Decimal.Add(operand, operand2);
                        else if (operatorToken.IsSubtractionOperatorToken())
                            operandResult = Decimal.Subtract(operand, operand2);
                        else if (operatorToken.IsMultiplicationOperatorToken())
                            operandResult = Decimal.Multiply(operand, operand2);
                        else if (operatorToken.IsDivisionOperatorToken())
                            operandResult = Decimal.Divide(operand, operand2);
                        operandStack.Push(operandResult);
                    }
                }
            }

            if (operandStack.Count > 1)
                throw new ExpressionParserSyntaxError();

            return operandStack.Peek();
        }

        /// <summary>
        /// Reconstruct expression where the order of operations is explicitly defined
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static string OrderOfOperationsOf(string expression)
        {
            if (string.IsNullOrEmpty(expression))
                throw new ExpressionParserNullError();

            /// Convert expression to a token array in postfix order
            Token[] tokenArray = expressionToTokenArray(expression);
            changeTokenArrayOrder(ref tokenArray);

            /// Evaluate postfix expression by using a stack
            /// This approach resconstructs the initial expression instead of calculating its result
            Stack<string> operandStack = new Stack<string>();
            foreach(Token token in tokenArray)
            {
                if (token != null)
                {
                    if (token.IsDecimalToken())
                        operandStack.Push(token.ToString());
                    else if (token.IsOperatorToken())
                    {
                        if (operandStack.Count < 2)
                            throw new ExpressionParserSyntaxError();

                        /// Extract operands from the stack
                        string operand = operandStack.Pop();
                        string operand2 = operandStack.Pop();
                        (operand, operand2) = (operand2, operand);

                        /// Push the reconstructed operation back onto the stack
                        operandStack.Push(TokenStringRepresentation.OpeningParenthesisToken + operand + token + operand2 + TokenStringRepresentation.ClosingParenthesisToken);
                    }
                }
            }

            if (operandStack.Count > 1)
                throw new ExpressionParserSyntaxError();

            return operandStack.Peek();
        }

        /// <summary>
        /// Returns an array of all extracted tokens from an expression
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        private static Token[] expressionToTokenArray(string expression)
        {
            /// Delimits tokens present in expression based on character type generalization
            int tokenArrayIndex = 0;
            Token[] tokenArray = new Token[expression.Length];
            StringBuilder tokenStringBuilder = new StringBuilder(expression.Length);
            Token.TokenType tokenType, previousTokenType = Token.TokenType.Undefined;
            foreach(char expressionChar in expression)
            {
                /// Determine which type of token contains the character
                if (expressionChar == '(' || expressionChar == ')')
                    tokenType = Token.TokenType.Parenthesis;
                else if (expressionChar == '.' || char.IsDigit(expressionChar))
                    tokenType = Token.TokenType.Decimal;
                else
                    tokenType = Token.TokenType.Operator;

                /// Adds the token to the array when the token type is parenthesis or it differs from the previous token type
                bool tokenTypeIsParenthesis = tokenType == Token.TokenType.Parenthesis;
                bool previousTokenTypeIsUndefined = previousTokenType == Token.TokenType.Undefined;
                bool previousTokenTypeIsParenthesis = previousTokenType == Token.TokenType.Parenthesis;
                if (!previousTokenTypeIsUndefined && (tokenTypeIsParenthesis || previousTokenTypeIsParenthesis || tokenType != previousTokenType))
                {
                    tokenArray[tokenArrayIndex++] = stringToToken(tokenStringBuilder.ToString(), previousTokenType);
                    tokenStringBuilder.Clear();
                }

                tokenStringBuilder.Append(expressionChar);
                
                previousTokenType = tokenType;
            }

            tokenArray[tokenArrayIndex++] = stringToToken(tokenStringBuilder.ToString(), previousTokenType);

            return tokenArray;
        }

        /// <summary>
        /// Changes token array's order from infix to postfix
        /// </summary>
        /// <param name="tokenArray"></param>
        /// <returns></returns>
        private static void changeTokenArrayOrder(ref Token[] tokenArray)
        {
            /// Algorithm based on Edsger W. Dijkstra's shunting-yard algorithm
            /// Additionaly, this algorithm performs anti-function checks and converts unary addition and subtraction operators to binary
            /// Function-like structures (a(b), (b)a, (a)(b), where a and b are decimals) are troublesome because they exploit the postfix conversion algorithm
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

            int postfixTokenArrayIndex = 0;
            Token previousToken = Token.Undefined;
            Stack<Token> operatorStack = new Stack<Token>();
            Token[] postfixTokenArray = new Token[tokenArray.Length + 1];
            foreach(Token token in tokenArray)
            {
                if (token != null)
                {
                    if (token == Token.Undefined)
                        throw new ExpressionParserTokenError();

                    if(token.IsDecimalToken())
                    {
                        /// Anti-function patch #1
                        if (previousToken.IsParenthesisToken() && (previousToken as ParenthesisToken).IsClosingParenthesisToken())
                            throw new ExpressionParserSyntaxError();

                        postfixTokenArray[postfixTokenArrayIndex++] = token;
                    }
                    else if(token.IsParenthesisToken())
                    {
                        if ((token as ParenthesisToken).IsOpeningParenthesisToken())
                        {
                            /// Anti-function patch #2
                            if (previousToken.IsDecimalToken() || (previousToken.IsParenthesisToken() && (previousToken as ParenthesisToken).IsClosingParenthesisToken()))
                                throw new ExpressionParserSyntaxError();

                            operatorStack.Push(token);
                        }
                        else
                        {
                            /// Anti-function patch #3
                            if (previousToken.IsParenthesisToken() && (previousToken as ParenthesisToken).IsOpeningParenthesisToken())
                                throw new ExpressionParserSyntaxError();

                            while (operatorStack.Count > 0 && !operatorStack.Peek().IsParenthesisToken())
                                postfixTokenArray[postfixTokenArrayIndex++] = operatorStack.Pop();
                            if (operatorStack.Count == 0)
                                throw new ExpressionParserSyntaxError();
                            operatorStack.Pop();
                        }
                    }
                    else if(token.IsOperatorToken())
                    {
                        /// Unary sign operator patch
                        if (previousToken.IsUndefined() || (previousToken.IsParenthesisToken() && (previousToken as ParenthesisToken).IsOpeningParenthesisToken()))
                        {
                            if ((token as OperatorToken).IsAdditionOperatorToken())
                                continue;
                            else if ((token as OperatorToken).IsSubtractionOperatorToken())
                                postfixTokenArray[postfixTokenArrayIndex++] = new DecimalToken("0");
                        }

                        while (operatorStack.Count > 0 && !operatorStack.Peek().IsParenthesisToken() && OperatorToken.Compare((token as OperatorToken), (operatorStack.Peek() as OperatorToken)) <= 0)
                            postfixTokenArray[postfixTokenArrayIndex++] = operatorStack.Pop();
                        operatorStack.Push(token);
                    }

                    previousToken = token;
                }
            }

            while(operatorStack.Count > 0)
            {
                if (operatorStack.Peek().IsParenthesisToken())
                    throw new ExpressionParserSyntaxError();
                postfixTokenArray[postfixTokenArrayIndex++] = operatorStack.Pop();
            }

            tokenArray = postfixTokenArray;
        }

        private static Token stringToToken(string str, Token.TokenType tokenType)
        {
            if (tokenType == Token.TokenType.Decimal)
                return new DecimalToken(str);
            else
                return tokenLookup.ContainsKey(str) ? tokenLookup[str] : Token.Undefined;
        }

        /// <summary>
        /// Connects a token's string representation to its definition
        /// </summary>
        private static Dictionary<string, Token> tokenLookup = new Dictionary<string, Token>
        {
            {TokenStringRepresentation.OpeningParenthesisToken,      ParenthesisToken.OpeningParenthesis},
            {TokenStringRepresentation.ClosingParenthesisToken,      ParenthesisToken.ClosingParenthesis},
            {TokenStringRepresentation.AdditionOperatorToken,        OperatorToken.Addition},
            {TokenStringRepresentation.SubtractionOperatorToken,     OperatorToken.Subtraction},
            {TokenStringRepresentation.MultiplicationOperatorToken,  OperatorToken.Multiplication},
            {TokenStringRepresentation.DivisionOperatorToken,        OperatorToken.Division},
        };
    }
}
