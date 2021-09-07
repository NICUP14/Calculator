using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator
{
    public partial class CalculatorForm : Form
    {
        public CalculatorForm()
        {
            expressionBuilder = new StringBuilder(expressionMaxLength);
            InitializeComponent();
        }

        // Set button tags after the form loaded
        private void formLoad(object sender, EventArgs e)
        {
            //string expression = "2*1.15";
            //expressionLabel.Text = expression;
            //expressionBuilder.Append(expression);

            clearButton.Tag = ButtonTag.Clear;
            signButton.Tag = ButtonTag.Sign;
            parenthesisButton.Tag = ButtonTag.Parenthesis;
            exponentiationButton.Tag = ButtonTag.Undefined;
            multiplicationButton.Tag = ButtonTag.Multiplication;
            divisionButton.Tag = ButtonTag.Division;
            additionButton.Tag = ButtonTag.Addition;
            subtractionButton.Tag = ButtonTag.Subtraction;
            periodButton.Tag = ButtonTag.Period;
            zeroButton.Tag = ButtonTag.Zero;
            oneButton.Tag  = ButtonTag.One;
            twoButton.Tag = ButtonTag.Two;
            threeButton.Tag = ButtonTag.Three;
            fourButton.Tag = ButtonTag.Four;
            fiveButton.Tag = ButtonTag.Five;
            sixButton.Tag = ButtonTag.Six;
            sevenButton.Tag = ButtonTag.Seven;
            eightButton.Tag = ButtonTag.Eight;
            nineButton.Tag = ButtonTag.Nine;

        }

        /// Update form when button is clicked
        private void buttonClick(object sender, EventArgs e)
        {
            Button button = (sender as Button);
            switch((ButtonTag)button.Tag)
            {
                case ButtonTag.Undefined:
                    return;
                case ButtonTag.Clear:
                    expressionBuilder.Clear();
                    break;
                case ButtonTag.Sign:
                    AppendSign();
                    break;
                case ButtonTag.Parenthesis:
                    AppendParenthesis();
                    break;
                case ButtonTag.Multiplication:
                    expressionBuilder.Append('*');
                    break;
                case ButtonTag.Division:
                    expressionBuilder.Append('/');
                    break;
                case ButtonTag.Addition:
                    expressionBuilder.Append('+');
                    break;
                case ButtonTag.Subtraction:
                    expressionBuilder.Append('-');
                    break;
                case ButtonTag.Period:
                    expressionBuilder.Append('.');
                    break;
                case ButtonTag.Zero:
                    expressionBuilder.Append('0');
                    break;
                case ButtonTag.One:
                    expressionBuilder.Append('1');
                    break;
                case ButtonTag.Two:
                    expressionBuilder.Append('2');
                    break;
                case ButtonTag.Three:
                    expressionBuilder.Append('3');
                    break;
                case ButtonTag.Four:
                    expressionBuilder.Append('4');
                    break;
                case ButtonTag.Five:
                    expressionBuilder.Append('5');
                    break;
                case ButtonTag.Six:
                    expressionBuilder.Append('6');
                    break;
                case ButtonTag.Seven:
                    expressionBuilder.Append('7');
                    break;
                case ButtonTag.Eight:
                    expressionBuilder.Append('8');
                    break;
                case ButtonTag.Nine:
                    expressionBuilder.Append('9');
                    break;
            }
            UpdateLabels();
        }

        private void AppendSign()
        {
            char lastExpressionChar = expressionBuilder[expressionBuilder.Length - 1];
            if(lastExpressionChar == '(' || lastExpressionChar == ')')
                expressionBuilder.Append("-");
            else if (lastExpressionChar == '.' || char.IsDigit(lastExpressionChar))
            {
                int expressionIndex = expressionBuilder.Length - 1;
                while (expressionIndex > 0 && (expressionBuilder[expressionIndex] == '.' || char.IsDigit(expressionBuilder[expressionIndex])))
                    expressionIndex--;
                char expressionChar = expressionBuilder[expressionIndex];
                if (expressionChar == '+' || expressionChar == '-')
                    expressionBuilder[expressionIndex] = expressionChar == '+' ? '-' : '+';
                else
                {
                    if (expressionIndex == 0)
                        expressionBuilder.Insert(expressionIndex, "-");
                    else
                        expressionBuilder.Insert(expressionIndex + 1, "(-");
                }
            }
            else
            {
                if (lastExpressionChar == '+' || lastExpressionChar == '-')
                    expressionBuilder[expressionBuilder.Length - 1] = lastExpressionChar == '+' ? '-' : '+';
                else
                    expressionBuilder.Append("(-");
            }
        }

        private void AppendParenthesis()
        {
            int unclosedOpeningParenthesisCount = 0;
            foreach (char expressionChar in expressionBuilder.ToString())
            {
                if (expressionChar == '(')
                    unclosedOpeningParenthesisCount++;
                else if (expressionChar == ')')
                    unclosedOpeningParenthesisCount--;
            }

            char lastExpressionChar = expressionBuilder[expressionBuilder.Length - 1];
            if (lastExpressionChar == ')' || lastExpressionChar == '.' || char.IsDigit(lastExpressionChar))
                expressionBuilder.Append(unclosedOpeningParenthesisCount == 0 ? "*(" : ')');
            else
                expressionBuilder.Append('(');
        }

        private void UpdateLabels()
        {
            string expression = expressionBuilder.ToString();
            expressionLabel.Text = expression;
            try
            {
                string result = Expression.ToInfix(Expression.ToPostfix(expression));
                resultLabel.Text = result;
            }
            catch(Exception e)
            {
                resultLabel.Text = e.Message;
            }
        }

        private enum ButtonTag
        {
            Undefined,
            Sign,
            Parenthesis,
            Clear,
            Multiplication,
            Division,
            Addition,
            Subtraction,
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

        private const int expressionMaxLength = 500;
        private StringBuilder expressionBuilder;
    }
}
