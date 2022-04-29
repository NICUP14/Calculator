using System;

namespace Calculator
{
    /// <summary>
    /// Defines exception messages for the Decimal, ExpressionParser and ExpressionBuilder classes
    /// </summary>
    static class ExceptionMessage
    {
        public static string DecimalFormatException = "DecimalFormatException";
        public static string DecimalArithmeticException = "DecimalArithmeticException";
        public static string ExpressionParserSyntaxException = "ExpressionParserSyntaxException";
        public static string ExpressionParserTokenException = "ExpressionParserTokenException";
        public static string ExpressionBuilderInsertParenthesisException = "ExpressionBuilderInsertTokenException";
        public static string ExpressionBuilderAppendDecimalTokenException = "ExpressionBuilderAppendDecimalTokenException";
        public static string ExpressionBuilderAppendOperatorTokenException = "ExpressionBuilderAppendOperatorTokenException";
        public static string ExpressionBuilderRemoveTrailingCharacterException = "ExpressionBuilderRemoveLastCharacterException";
        public static string ExpressionBuilderAppendPeriodDecimalTokenException = "ExpressionBuilderAppendPeriodDecimalTokenException";
        public static string ExpressionBuilderAppendMultiplicationOrDivisionOperatorTokenException = "ExpressionBuilderAppendMultiplicationOrDivisionException";
    }

    /// <summary>
    /// The exception that is thrown when the format of the decimal constructor argument is invalid
    /// </summary>
    class DecimalFormatException : Exception
    {
        public DecimalFormatException() : base(ExceptionMessage.DecimalFormatException)
        {

        }
    }

    /// <summary>
    /// The exception that is thrown for errors in the decimal division operation
    /// </summary>
    class DecimalArithmeticException : Exception
    {
        public DecimalArithmeticException() : base(ExceptionMessage.DecimalArithmeticException)
        {

        }
    }

    /// <summary>
    /// The exception that is thrown for syntax errors in the ExpressionBuilder.Calculate methods
    /// </summary>
    class ExpressionParserSyntaxException : Exception
    {
        public ExpressionParserSyntaxException() : base(ExceptionMessage.ExpressionParserSyntaxException)
        {

        }
    }

    /// <summary>
    /// The exception that is thrown for unrecognized tokens in the ExpressionBuilder.Parse method
    /// </summary>
    class ExpressionParserTokenException : Exception
    {
        public ExpressionParserTokenException() : base(ExceptionMessage.ExpressionParserTokenException)
        {

        }
    }

    /// <summary>
    /// The exception that is thrown by ExpressionBuilder.InsertParenthesis
    /// </summary>
    class ExpressionBuilderInsertParenthesisException : Exception
    {
        public ExpressionBuilderInsertParenthesisException() : base(ExceptionMessage.ExpressionBuilderInsertParenthesisException)
        {

        }
    }

    /// <summary>
    /// The exception that is thrown by ExpressionBuilder.AppendToken(DecimalToken)
    /// </summary>
    class ExpressionBuilderAppendDecimalTokenException : Exception
    {
        public ExpressionBuilderAppendDecimalTokenException() : base(ExceptionMessage.ExpressionBuilderAppendDecimalTokenException)
        {

        }
    }

    /// <summary>
    /// The exception that is thrown by ExpressionBuilder.AppendToken(OperatorToken)
    /// </summary>
    class ExpressionBuilderAppendOperatorTokenException : Exception
    {
        public ExpressionBuilderAppendOperatorTokenException() : base(ExceptionMessage.ExpressionBuilderAppendOperatorTokenException)
        {

        }
    }

    /// <summary>
    /// The exception that is thrown by ExpressionBuilder.RemoveTrailingCharacter
    /// </summary>
    class ExpressionBuilderRemoveTrailingCharacterException : Exception
    {
        public ExpressionBuilderRemoveTrailingCharacterException() : base(ExceptionMessage.ExpressionBuilderRemoveTrailingCharacterException)
        {

        }
    }

    /// <summary>
    /// The exception that is thrown by ExpressionBuilder.AppendToken(DecimalToken.Period)
    /// </summary>
    class ExpressionBuilderAppendPeriodDecimalTokenException : Exception
    {
        public ExpressionBuilderAppendPeriodDecimalTokenException() : base(ExceptionMessage.ExpressionBuilderAppendPeriodDecimalTokenException)
        {

        }
    }

    /// <summary>
    /// The exception that is thrown by ExpressionBuilder.AppendToken(OperatorToken.Multiplication) or by ExpressionBuilder.AppendToken(OperatorToken.Division)
    /// </summary>
    class ExpressionBuilderAppendMultiplicationOrDivisionOperatorTokenException : Exception
    {
        public ExpressionBuilderAppendMultiplicationOrDivisionOperatorTokenException() : base(ExceptionMessage.ExpressionBuilderAppendMultiplicationOrDivisionOperatorTokenException)
        {

        }
    }
}
