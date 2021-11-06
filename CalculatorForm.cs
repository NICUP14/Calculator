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
            expressionBuilder = new StringBuilder(expressionBuilderCapacity);
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
            int expressionLength = expressionBuilder.Length;
            if (expressionLength == 0)
                expressionBuilder.Append("-");
            else
            {
                char lastExpressionParserChar = expressionBuilder[expressionBuilder.Length - 1];
                if (lastExpressionParserChar == '(' || lastExpressionParserChar == ')')
                    expressionBuilder.Append("-");
                else if (lastExpressionParserChar == '.' || char.IsDigit(lastExpressionParserChar))
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
                    if (lastExpressionParserChar == '+' || lastExpressionParserChar == '-')
                        expressionBuilder[expressionBuilder.Length - 1] = lastExpressionParserChar == '+' ? '-' : '+';
                    else
                        expressionBuilder.Append("(-");
                }
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

            int expressionLength = expressionBuilder.Length;
            if (expressionLength == 0)
                expressionBuilder.Append('(');
            else
            {
                char lastExpressionParserChar = expressionBuilder[expressionBuilder.Length - 1];
                if (lastExpressionParserChar == ')' || lastExpressionParserChar == '.' || char.IsDigit(lastExpressionParserChar))
                    expressionBuilder.Append(unclosedOpeningParenthesisCount == 0 ? "*(" : ')');
                else
                    expressionBuilder.Append('(');
            }
        }

        private void UpdateLabels()
        {
            string expression = expressionBuilder.ToString();
            expressionLabel.Text = expression;
            try
            {
                int openParenthesisCount = 0;
                foreach (char expressionChar in expression)
                    if (expressionChar == '(')
                        openParenthesisCount++;
                    else if(expressionChar == ')')
                        openParenthesisCount--;


                string result = ExpressionParser.Calculate(expression + new string(')', openParenthesisCount)).ToString();
                resultLabel.Text = result;
            }
            catch(Exception e)
            {
                if (e is not ExpressionParserNullError)
                    resultLabel.Text = "Error";
                else
                    expressionLabel.Text = resultLabel.Text = string.Empty;
            }
        }


        private StringBuilder expressionBuilder;
        private const int expressionBuilderCapacity = 500;
    }
}
