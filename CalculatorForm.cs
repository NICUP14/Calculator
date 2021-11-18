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
            expressionStringBuilder = new StringBuilder(expressionStringBuilderCapacity);
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
                    expressionStringBuilder.Clear();
                    break;
                case ButtonTag.Sign:
                    ChangeSign();
                    break;
                case ButtonTag.Parenthesis:
                    InsertParenthesis();
                    break;
                case ButtonTag.Multiplication:
                    expressionStringBuilder.Append("×");
                    break;
                case ButtonTag.Division:
                    expressionStringBuilder.Append("/");
                    break;
                case ButtonTag.Addition:
                    expressionStringBuilder.Append("+");
                    break;
                case ButtonTag.Subtraction:
                    expressionStringBuilder.Append("-");
                    break;
                case ButtonTag.Period:
                    expressionStringBuilder.Append(".");
                    break;
                case ButtonTag.Zero:
                    expressionStringBuilder.Append("0");
                    break;
                case ButtonTag.One:
                    expressionStringBuilder.Append("1");
                    break;
                case ButtonTag.Two:
                    expressionStringBuilder.Append("2");
                    break;
                case ButtonTag.Three:
                    expressionStringBuilder.Append("3");
                    break;
                case ButtonTag.Four:
                    expressionStringBuilder.Append("4");
                    break;
                case ButtonTag.Five:
                    expressionStringBuilder.Append("5");
                    break;
                case ButtonTag.Six:
                    expressionStringBuilder.Append("6");
                    break;
                case ButtonTag.Seven:
                    expressionStringBuilder.Append("7");
                    break;
                case ButtonTag.Eight:
                    expressionStringBuilder.Append("8");
                    break;
                case ButtonTag.Nine:
                    expressionStringBuilder.Append("9");
                    break;
            }

            UpdateLabels();
        }

        private void ChangeSign()
        {
            if (expressionStringBuilder.Length == 0)
                expressionStringBuilder.Append("-");
            else
            {
                char lastExpressionParserChar = expressionStringBuilder[expressionStringBuilder.Length - 1];
                if (lastExpressionParserChar == '(' || lastExpressionParserChar == ')')
                    expressionStringBuilder.Append("-");
                else if (lastExpressionParserChar == '.' || char.IsDigit(lastExpressionParserChar))
                {
                    int expressionIndex = expressionStringBuilder.Length - 1;
                    while (expressionIndex > 0 && (expressionStringBuilder[expressionIndex] == '.' || char.IsDigit(expressionStringBuilder[expressionIndex])))
                        expressionIndex--;
                    char expressionChar = expressionStringBuilder[expressionIndex];
                    if (expressionChar == '+' || expressionChar == '-')
                        expressionStringBuilder[expressionIndex] = expressionChar == '+' ? '-' : '+';
                    else
                    {
                        if (expressionIndex == 0)
                            expressionStringBuilder.Insert(expressionIndex, "-");
                        else
                            expressionStringBuilder.Insert(expressionIndex + 1, "(-");
                    }
                }
                else
                {
                    if (lastExpressionParserChar == '+' || lastExpressionParserChar == '-')
                        expressionStringBuilder[expressionStringBuilder.Length - 1] = lastExpressionParserChar == '+' ? '-' : '+';
                    else
                        expressionStringBuilder.Append("(-");
                }
            }
        }

        private void InsertParenthesis()
        {
            int unclosedOpeningParenthesisCount = 0;
            foreach (char expressionChar in expressionStringBuilder.ToString())
            {
                if (expressionChar == '(')
                    unclosedOpeningParenthesisCount++;
                else if (expressionChar == ')')
                    unclosedOpeningParenthesisCount--;
            }

            int expressionLength = expressionStringBuilder.Length;
            if (expressionLength == 0)
                expressionStringBuilder.Append('(');
            else
            {
                char lastExpressionParserChar = expressionStringBuilder[expressionStringBuilder.Length - 1];
                if (lastExpressionParserChar == ')' || lastExpressionParserChar == '.' || char.IsDigit(lastExpressionParserChar))
                    expressionStringBuilder.Append(unclosedOpeningParenthesisCount == 0 ? "×(" : ')');
                else
                    expressionStringBuilder.Append('(');
            }
        }

        private void UpdateLabels()
        {
            string expression = expressionStringBuilder.ToString();
            expressionLabel.Text = expression;
            try
            {
                int openParenthesisCount = 0;
                foreach (char expressionChar in expression)
                    if (expressionChar == '(')
                        openParenthesisCount++;
                    else if(expressionChar == ')')
                        openParenthesisCount--;


                string result = ExpressionParser.Calculate(expression + new string(')', openParenthesisCount));
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

        private enum ButtonTag
        {
            Undefined,
            Clear,
            Undo,
            Sign,
            Parenthesis,
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

        private StringBuilder expressionStringBuilder;
        private const int expressionStringBuilderCapacity = 500;
    }
}
