using System;

namespace Calculator
{
    class ErrorMessage
    {
        public const string DecimalNullError =                                                  "DecimalNullError";
        public const string DecimalPeriodError =                                                "DecimalPeriodError";
        public const string DecimalInvalidError =                                               "DecimalInvalidError";
        public const string DecimalDivisionError =                                              "DecimalDivisionError";
        public const string ExpressionParserNullError =                                         "ExpressionParserNullError";
        public const string ExpressionParserSyntaxError =                                       "ExpressionParserSyntaxError";
        public const string ExpressionParserTokenError =                                        "ExpressionParserTokenError";
        public const string ExpressionBuilderNullError =                                        "ExpressionBuilderNullError";
        public const string ExpressionBuilderInsertParenthesisError =                                "ExpressionBuilderInsertTokenError";
        public const string ExpressionBuilderAppendDecimalTokenError =                          "ExpressionBuilderAppendDecimalTokenError";
        public const string ExpressionBuilderAppendOperatorTokenError =                         "ExpressionBuilderAppendOperatorTokenError";
        public const string ExpressionBuilderRemoveLastCharacterError =                         "ExpressionBuilderRemoveLastCharacterError";
        public const string ExpressionBuilderAppendPeriodDecimalTokenError =                    "ExpressionBuilderAppendPeriodDecimalTokenError";
        public const string ExpressionBuilderAppendMultiplicationOrDivisionOperatorTokenError = "ExpressionBuilderAppendMultiplicationOrDivisionError";
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

    class ExpressionBuilderNullError : Exception
    {
        public ExpressionBuilderNullError() : base(ErrorMessage.ExpressionBuilderNullError)
        {

        }
    }

    /// <summary>
    /// Occurs when the expression builder detects the call to InsertParenthesis that would break expression validity
    /// </summary>
    class ExpressionBuilderInsertParenthesisError : Exception
    {
        public ExpressionBuilderInsertParenthesisError() : base(ErrorMessage.ExpressionBuilderInsertParenthesisError)
        {

        }
    }

    /// <summary>
    /// Occurs when the expression builder detects the call to AppendToken that would break expression validity
    /// </summary>
    class ExpressionBuilderAppendDecimalTokenError : Exception
    {
        public ExpressionBuilderAppendDecimalTokenError() : base(ErrorMessage.ExpressionBuilderAppendDecimalTokenError)
        {

        }
    }

    /// <summary>
    /// Occurs when the expression builder detects the call to AppendToken that would break expression validity
    /// </summary>
    class ExpressionBuilderAppendOperatorTokenError : Exception
    {
        public ExpressionBuilderAppendOperatorTokenError() : base(ErrorMessage.ExpressionBuilderAppendOperatorTokenError)
        {

        }
    }

    class ExpressionBuilderRemoveLastCharacterError : Exception
    {
        public ExpressionBuilderRemoveLastCharacterError() : base(ErrorMessage.ExpressionBuilderRemoveLastCharacterError)
        {

        }
    }

    /// <summary>
    /// Occurs when the expression builder detects the call to AppendToken that would break expression validity
    /// </summary>
    class ExpressionBuilderAppendPeriodDecimalTokenError : Exception
    {
        public ExpressionBuilderAppendPeriodDecimalTokenError() : base(ErrorMessage.ExpressionBuilderAppendPeriodDecimalTokenError)
        {

        }
    }

    class ExpressionBuilderAppendMultiplicationOrDivisionOperatorTokenError : Exception
    {
        public ExpressionBuilderAppendMultiplicationOrDivisionOperatorTokenError() : base(ErrorMessage.ExpressionBuilderAppendMultiplicationOrDivisionOperatorTokenError)
        {

        }
    }
}
