using System;

namespace Calculator
{
    static class ExceptionMessage
    {
        public static string DecimalFormatException = "DecimalFormatException";
        public static string DecimalArithmeticException = "DecimalArithmeticException";
        public static string ExpressionParserSyntaxException = "ExpressionParserSyntaxException";
        public static string ExpressionParserTokenException = "ExpressionParserTokenException";
        public static string ExpressionBuilderEmptyException = "ExpressionBuilderEmptyException";
        public static string ExpressionBuilderInsertParenthesisException = "ExpressionBuilderInsertTokenException";
        public static string ExpressionBuilderAppendDecimalTokenException = "ExpressionBuilderAppendDecimalTokenException";
        public static string ExpressionBuilderAppendOperatorTokenException = "ExpressionBuilderAppendOperatorTokenException";
        public static string ExpressionBuilderRemoveLastCharacterException = "ExpressionBuilderRemoveLastCharacterException";
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
    /// Occurs when the expression parser detects a syntactically invalid expression
    /// </summary>
    class ExpressionParserSyntaxException : Exception
    {
        public ExpressionParserSyntaxException() : base(ExceptionMessage.ExpressionParserSyntaxException)
        {

        }
    }

    /// <summary>
    /// Occurs when the expression parser detects an undefined token
    /// </summary>
    class ExpressionParserTokenException : Exception
    {
        public ExpressionParserTokenException() : base(ExceptionMessage.ExpressionParserTokenException)
        {

        }
    }

    class ExpressionBuilderEmptyException : Exception
    {
        public ExpressionBuilderEmptyException() : base(ExceptionMessage.ExpressionBuilderEmptyException)
        {

        }
    }

    /// <summary>
    /// Occurs when the expression builder detects the call to InsertParenthesis that would break expression validity
    /// </summary>
    class ExpressionBuilderInsertParenthesisException : Exception
    {
        public ExpressionBuilderInsertParenthesisException() : base(ExceptionMessage.ExpressionBuilderInsertParenthesisException)
        {

        }
    }

    /// <summary>
    /// Occurs when the expression builder detects the call to AppendToken that would break expression validity
    /// </summary>
    class ExpressionBuilderAppendDecimalTokenException : Exception
    {
        public ExpressionBuilderAppendDecimalTokenException() : base(ExceptionMessage.ExpressionBuilderAppendDecimalTokenException)
        {

        }
    }

    /// <summary>
    /// Occurs when the expression builder detects the call to AppendToken that would break expression validity
    /// </summary>
    class ExpressionBuilderAppendOperatorTokenException : Exception
    {
        public ExpressionBuilderAppendOperatorTokenException() : base(ExceptionMessage.ExpressionBuilderAppendOperatorTokenException)
        {

        }
    }

    class ExpressionBuilderRemoveLastCharacterException : Exception
    {
        public ExpressionBuilderRemoveLastCharacterException() : base(ExceptionMessage.ExpressionBuilderRemoveLastCharacterException)
        {

        }
    }

    /// <summary>
    /// Occurs when the expression builder detects the call to AppendToken that would break expression validity
    /// </summary>
    class ExpressionBuilderAppendPeriodDecimalTokenException : Exception
    {
        public ExpressionBuilderAppendPeriodDecimalTokenException() : base(ExceptionMessage.ExpressionBuilderAppendPeriodDecimalTokenException)
        {

        }
    }

    class ExpressionBuilderAppendMultiplicationOrDivisionOperatorTokenException : Exception
    {
        public ExpressionBuilderAppendMultiplicationOrDivisionOperatorTokenException() : base(ExceptionMessage.ExpressionBuilderAppendMultiplicationOrDivisionOperatorTokenException)
        {

        }
    }
}
