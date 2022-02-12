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
            clearButton.Tag = ButtonTag.Clear;
            undoButton.Tag = ButtonTag.Delete;
            signButton.Tag = ButtonTag.ChangeSign;
            parenthesisButton.Tag = ButtonTag.InsertParenthesis;
            divisionButton.Tag = ButtonTag.Division;
            additionButton.Tag = ButtonTag.Addition;
            subtractionButton.Tag = ButtonTag.Subtraction;
            multiplicationButton.Tag = ButtonTag.Multiplication;
            periodButton.Tag = ButtonTag.Period;
            zeroButton.Tag = ButtonTag.Zero;
            oneButton.Tag = ButtonTag.One;
            twoButton.Tag = ButtonTag.Two;
            threeButton.Tag = ButtonTag.Three;
            fourButton.Tag = ButtonTag.Four;
            fiveButton.Tag = ButtonTag.Five;
            sixButton.Tag = ButtonTag.Six;
            sevenButton.Tag = ButtonTag.Seven;
            eightButton.Tag = ButtonTag.Eight;
            nineButton.Tag = ButtonTag.Nine;
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
                    expressionManipulator.ChangeSign();
                    break;
                case ButtonTag.InsertParenthesis:
                    expressionManipulator.InsertParenthesis();
                    break;
                case ButtonTag.Division:
                    expressionManipulator.AppendToken(OperatorToken.Division);
                    break;
                case ButtonTag.Addition:
                    expressionManipulator.AppendToken(OperatorToken.Addition);
                    break;
                case ButtonTag.Subtraction:
                    expressionManipulator.AppendToken(OperatorToken.Subtraction);
                    break;
                case ButtonTag.Multiplication:
                    expressionManipulator.AppendToken(OperatorToken.Multiplication);
                    break;
                case ButtonTag.CalculateResult:
                    break;
                case ButtonTag.Period:
                    expressionManipulator.AppendToken(new DecimalToken("."));
                    break;
                case ButtonTag.Zero:
                    expressionManipulator.AppendToken(new DecimalToken("0"));
                    break;
                case ButtonTag.One:
                    expressionManipulator.AppendToken(new DecimalToken("1"));
                    break;
                case ButtonTag.Two:
                    expressionManipulator.AppendToken(new DecimalToken("2"));
                    break;
                case ButtonTag.Three:
                    expressionManipulator.AppendToken(new DecimalToken("3"));
                    break;
                case ButtonTag.Four:
                    expressionManipulator.AppendToken(new DecimalToken("4"));
                    break;
                case ButtonTag.Five:
                    expressionManipulator.AppendToken(new DecimalToken("5"));
                    break;
                case ButtonTag.Six:
                    expressionManipulator.AppendToken(new DecimalToken("6"));
                    break;
                case ButtonTag.Seven:
                    expressionManipulator.AppendToken(new DecimalToken("7"));
                    break;
                case ButtonTag.Eight:
                    expressionManipulator.AppendToken(new DecimalToken("8"));
                    break;
                case ButtonTag.Nine:
                    expressionManipulator.AppendToken(new DecimalToken("9"));
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
