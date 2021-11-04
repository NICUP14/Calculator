using System;

namespace Calculator_backend
{
    class ErrorMessage
    {
        public const string DecimalNullError = "DecimalNullError";
        public const string DecimalPeriodError = "DecimalPeriodError";
        public const string DecimalInvalidError = "DecimalInvalidError";
        public const string DecimalDivisionError = "DecimalDivisiorError";
        public const string ExpressionParserNullError = "ExpressionParserNullError";
        public const string ExpressionParserSyntaxError = "ExpressionParserSyntaxError";
        public const string ExpressionParserTokenError = "ExpressionParserTokenError";
    }

    /// <summary>
    /// Occurs when a decimal is constructed from a null or an empty string
    /// </summary>
    class DecimalNullError : Exception
    {
        public DecimalNullError() : base(ErrorMessage.DecimalNullError)
        {

        }
    }

    /// <summary>
    /// Occurs when a decimal is constructed from a string with a misplaced period
    /// </summary>
    class DecimalPeriodError : Exception
    {
        public DecimalPeriodError() : base(ErrorMessage.DecimalPeriodError)
        {

        }
    }

    /// <summary>
    /// Occurs when a decimal is constructed from a string with invalid characters
    /// </summary>
    class DecimalInvalidError : Exception
    {
        public DecimalInvalidError() : base(ErrorMessage.DecimalInvalidError)
        {

        }
    }

    /// <summary>
    /// Occurs when the decimal's internal division precision is less than one or a decimal is divided by zero
    /// </summary>
    class DecimalDivisionError : Exception
    {
        public DecimalDivisionError() : base(ErrorMessage.DecimalDivisionError)
        {

        }
    }

    /// <summary>
    /// Occurs when the expression parser encounters a null or an empty expression
    /// </summary>
    class ExpressionParserNullError : Exception
    {
        public ExpressionParserNullError() : base(ErrorMessage.ExpressionParserNullError)
        {

        }
    }

    /// <summary>
    /// Occurs when the expression parser detects a syntactically invalid expression
    /// </summary>
    class ExpressionParserSyntaxError : Exception
    {
        public ExpressionParserSyntaxError() : base(ErrorMessage.ExpressionParserSyntaxError)
        {

        }
    }

    /// <summary>
    /// Occurs when the expression parser detects an undefined token
    /// </summary>
    class ExpressionParserTokenError : Exception
    {
        public ExpressionParserTokenError() : base(ErrorMessage.ExpressionParserTokenError)
        {

        }
    }
}
