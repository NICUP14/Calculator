using System;
using System.Text;
using System.Windows.Forms;

namespace Calculator
{
    public partial class CalculatorForm : Form
    {
        public CalculatorForm()
        {
            InitializeComponent();
            expressionManipulator = new ExpressionBuilder();
        }

        private void FormLoad(object sender, EventArgs e)
        {
            this.clearButton.Tag = ButtonTag.Clear;
            this.undoButton.Tag = ButtonTag.Delete;
            this.signButton.Tag = ButtonTag.ChangeSign;
            this.parenthesisButton.Tag = ButtonTag.InsertParenthesis;
            this.divisionButton.Tag = ButtonTag.Division;
            this.additionButton.Tag = ButtonTag.Addition;
            this.subtractionButton.Tag = ButtonTag.Subtraction;
            this.multiplicationButton.Tag = ButtonTag.Multiplication;
            this.periodButton.Tag = ButtonTag.Period;
            this.zeroButton.Tag = ButtonTag.Zero;
            this.oneButton.Tag = ButtonTag.One;
            this.twoButton.Tag = ButtonTag.Two;
            this.threeButton.Tag = ButtonTag.Three;
            this.fourButton.Tag = ButtonTag.Four;
            this.fiveButton.Tag = ButtonTag.Five;
            this.sixButton.Tag = ButtonTag.Six;
            this.sevenButton.Tag = ButtonTag.Seven;
            this.eightButton.Tag = ButtonTag.Eight;
            this.nineButton.Tag = ButtonTag.Nine;
        }

        /// <summary>
        /// Button click handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonClick(object sender, EventArgs e)
        {
            switch((ButtonTag)(sender as Button).Tag)
            {
                case ButtonTag.Undefined:
                    return;
                case ButtonTag.Clear:
                    expressionManipulator.Clear();
                    break;
                case ButtonTag.Delete:
                    break;
                case ButtonTag.ChangeSign:
                    break;
                case ButtonTag.InsertParenthesis:
                    expressionManipulator.InsertParenthesis();
                    break;
                case ButtonTag.Division:
                    expressionManipulator.AppendOperatorToken(OperatorToken.Division);
                    break;
                case ButtonTag.Addition:
                    expressionManipulator.AppendOperatorToken(OperatorToken.Addition);
                    break;
                case ButtonTag.Subtraction:
                    expressionManipulator.AppendOperatorToken(OperatorToken.Subtraction);
                    break;
                case ButtonTag.Multiplication:
                    expressionManipulator.AppendOperatorToken(OperatorToken.Multiplication);
                    break;
                case ButtonTag.CalculateResult:
                    break;
                case ButtonTag.Period:
                    expressionManipulator.AppendDecimalToken(new DecimalToken("."));
                    break;
                case ButtonTag.Zero:
                    expressionManipulator.AppendDecimalToken(new DecimalToken("0"));
                    break;
                case ButtonTag.One:
                    expressionManipulator.AppendDecimalToken(new DecimalToken("1"));
                    break;
                case ButtonTag.Two:
                    expressionManipulator.AppendDecimalToken(new DecimalToken("2"));
                    break;
                case ButtonTag.Three:
                    expressionManipulator.AppendDecimalToken(new DecimalToken("3"));
                    break;
                case ButtonTag.Four:
                    expressionManipulator.AppendDecimalToken(new DecimalToken("4"));
                    break;
                case ButtonTag.Five:
                    expressionManipulator.AppendDecimalToken(new DecimalToken("5"));
                    break;
                case ButtonTag.Six:
                    expressionManipulator.AppendDecimalToken(new DecimalToken("6"));
                    break;
                case ButtonTag.Seven:
                    expressionManipulator.AppendDecimalToken(new DecimalToken("7"));
                    break;
                case ButtonTag.Eight:
                    expressionManipulator.AppendDecimalToken(new DecimalToken("8"));
                    break;
                case ButtonTag.Nine:
                    expressionManipulator.AppendDecimalToken(new DecimalToken("9"));
                    break;
            }

            UpdateLabels();
        }

        private void UpdateLabels()
        {
            string uncompleteExpression = expressionManipulator.ToExpression();
            expressionLabel.Text = uncompleteExpression;

            try
            {
                string completeExpression = expressionManipulator.ToExpression(true);
                resultLabel.Text = ExpressionParser.Calculate(completeExpression);
            }
            catch(Exception e)
            {
                if (e is not ExpressionParserNullError)
                    resultLabel.Text = "Error";
                else
                    expressionLabel.Text = resultLabel.Text = string.Empty;
            }
        }

        private enum ButtonTag
        {
            Undefined,
            Clear,
            Delete,
            ChangeSign,
            InsertParenthesis,
            Division,
            Addition,
            Subtraction,
            Multiplication,
            CalculateResult,
            Period,
            Zero,
            One,
            Two,
            Three,
            Four,
            Five,
            Six,
            Seven,
            Eight,
            Nine
        }

        private ExpressionBuilder expressionManipulator;

    }
}
