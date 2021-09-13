using System;

namespace Calculator
{
    // Error message definitions
    class ErrorMessage
    {
        public const string DecimalNullErrorMessage = "Decimal can not be an empty string";
        public const string DecimalPeriodErrorMessage = "Decimal has a misplaced period";
        public const string DecimalInvalidErrorMessage = "Decimal contains invalid characters";
        public const string DecimalDivisionErrorMessage = "Decimal can not be divided";
        public const string ExpressionNullErrorMessage = "Expression can not be an empty string";
        public const string ExpressionSyntaxErrorMessage = "Expression is missing a parenthesis/operand/operator";
        public const string ExpressionElementMessage = "Expression contains invalid operator/characters";
    }

    class DecimalNullError : Exception
    {
        public DecimalNullError() : base(ErrorMessage.DecimalNullErrorMessage)
        {

        }
    }

    class DecimalPeriodError : Exception
    {
        public DecimalPeriodError() : base(ErrorMessage.DecimalPeriodErrorMessage)
        {

        }
    }

    class DecimalInvalidError : Exception
    {
        public DecimalInvalidError() : base(ErrorMessage.DecimalInvalidErrorMessage)
        {

        }
    }

    class DecimalDivisionError : Exception
    {
        public DecimalDivisionError() : base(ErrorMessage.DecimalDivisionErrorMessage)
        {

        }
    }

    class ExpressionNullError : Exception
    {
        public ExpressionNullError() : base(ErrorMessage.ExpressionNullErrorMessage)
        {

        }
    }

    class ExpressionSyntaxError : Exception
    {
        public ExpressionSyntaxError() : base(ErrorMessage.ExpressionSyntaxErrorMessage)
        {

        }
    }

    class ExpressionElementError : Exception
    {
        public ExpressionElementError() : base(ErrorMessage.ExpressionElementMessage)
        {

        }
    }
}
