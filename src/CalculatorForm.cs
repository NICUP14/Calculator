using System;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Calculator
{
    public partial class CalculatorForm : Form
    {
        public CalculatorForm()
        {
            InitializeComponent();
            _expressionBuilder = new ExpressionBuilder();
        }

        private void FormLoad(object sender, EventArgs e)
        {
            errorBackColor = expressionLabel.BackColor;

            /// Configure button unique identifiers
            clearButton.Tag = ButtonTag.Clear;
            undoButton.Tag = ButtonTag.Delete;
            signButton.Tag = ButtonTag.ChangeSign;
            paranthesisButton.Tag = ButtonTag.InsertParenthesis;
            divisionButton.Tag = ButtonTag.Division;
            additionButton.Tag = ButtonTag.Addition;
            subtractionButton.Tag = ButtonTag.Subtraction;
            multiplicationButton.Tag = ButtonTag.Multiplication;
            calculateResultButton.Tag = ButtonTag.CalculateResult;
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

            /// Add button references to button array
            _buttonArray[0] = clearButton;
            _buttonArray[1] = undoButton;
            _buttonArray[2] = signButton;
            _buttonArray[3] = paranthesisButton;
            _buttonArray[4] = divisionButton;
            _buttonArray[5] = additionButton;
            _buttonArray[6] = subtractionButton;
            _buttonArray[7] = multiplicationButton;
            _buttonArray[8] = calculateResultButton;
            _buttonArray[9] = periodButton;
            _buttonArray[10] = zeroButton;
            _buttonArray[11] = oneButton;
            _buttonArray[12] = twoButton;
            _buttonArray[13] = threeButton;
            _buttonArray[14] = fourButton;
            _buttonArray[15] = fiveButton;
            _buttonArray[16] = sixButton;
            _buttonArray[17] = sevenButton;
            _buttonArray[18] = eightButton;
            _buttonArray[19] = nineButton;

            foreach (Button button in _buttonArray)
                _buttonDefaultBackColorLookup.Add((ButtonTag)button.Tag, button.BackColor);
        }

        /// <summary>
        /// Button click handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonClick(object sender, EventArgs e)
        {
            try
            {
                switch ((ButtonTag)(sender as Button).Tag)
                {
                    case ButtonTag.Undefined:
                        return;
                    case ButtonTag.Clear:
                        _expressionBuilder.Clear();
                        break;
                    case ButtonTag.Delete:
                        break;
                    case ButtonTag.ChangeSign:
                        _expressionBuilder.ChangeSign();
                        break;
                    case ButtonTag.InsertParenthesis:
                        _expressionBuilder.InsertParenthesis();
                        break;
                    case ButtonTag.Division:
                        _expressionBuilder.AppendToken(OperatorToken.Division);
                        break;
                    case ButtonTag.Addition:
                        _expressionBuilder.AppendToken(OperatorToken.Addition);
                        break;
                    case ButtonTag.Subtraction:
                        _expressionBuilder.AppendToken(OperatorToken.Subtraction);
                        break;
                    case ButtonTag.Multiplication:
                        _expressionBuilder.AppendToken(OperatorToken.Multiplication);
                        break;
                    case ButtonTag.CalculateResult:
                        break;
                    case ButtonTag.Period:
                        _expressionBuilder.AppendToken(new DecimalToken("."));
                        break;
                    case ButtonTag.Zero:
                        _expressionBuilder.AppendToken(new DecimalToken("0"));
                        break;
                    case ButtonTag.One:
                        _expressionBuilder.AppendToken(new DecimalToken("1"));
                        break;
                    case ButtonTag.Two:
                        _expressionBuilder.AppendToken(new DecimalToken("2"));
                        break;
                    case ButtonTag.Three:
                        _expressionBuilder.AppendToken(new DecimalToken("3"));
                        break;
                    case ButtonTag.Four:
                        _expressionBuilder.AppendToken(new DecimalToken("4"));
                        break;
                    case ButtonTag.Five:
                        _expressionBuilder.AppendToken(new DecimalToken("5"));
                        break;
                    case ButtonTag.Six:
                        _expressionBuilder.AppendToken(new DecimalToken("6"));
                        break;
                    case ButtonTag.Seven:
                        _expressionBuilder.AppendToken(new DecimalToken("7"));
                        break;
                    case ButtonTag.Eight:
                        _expressionBuilder.AppendToken(new DecimalToken("8"));
                        break;
                    case ButtonTag.Nine:
                        _expressionBuilder.AppendToken(new DecimalToken("9"));
                        break;
                }

                foreach (Button button in _buttonArray)
                {
                    button.Enabled   = true;
                    button.BackColor = _buttonDefaultBackColorLookup[(ButtonTag)button.Tag];
                }
            }
            catch(Exception ex)
            {
                if (ex is ExpressionBuilderInsertParenthesisError)
                {
                    paranthesisButton.Enabled = false;
                    paranthesisButton.BackColor = errorBackColor;

                    // paranthesisButton.BackColor = _buttonErrorBackColor[(ButtonTag)paranthesisButton.Tag];
                }
                else if (ex is ExpressionBuilderAppendPeriodDecimalTokenError)
                {

                    periodButton.Enabled = false;
                    periodButton.BackColor = _buttonErrorBackColor[(ButtonTag)periodButton.Tag];
                }
                else if (ex is ExpressionBuilderAppendOperatorTokenError)
                {
                    divisionButton.Enabled = false;
                    divisionButton.BackColor = _buttonErrorBackColor[(ButtonTag)divisionButton.Tag];

                    multiplicationButton.Enabled = false;
                    multiplicationButton.BackColor = _buttonErrorBackColor[(ButtonTag)multiplicationButton.Tag];
                }
                else if (ex is ExpressionBuilderAppendDecimalTokenError)
                {
                    zeroButton.Enabled = false;
                    oneButton.Enabled = false;
                    twoButton.Enabled = false;
                    threeButton.Enabled = false;
                    fourButton.Enabled = false;
                    fiveButton.Enabled = false;
                    sixButton.Enabled = false;
                    sevenButton.Enabled = false;
                    eightButton.Enabled = false;
                    nineButton.Enabled = false;

                    zeroButton.BackColor  = errorBackColor;
                    oneButton.BackColor   = errorBackColor;
                    twoButton.BackColor   = errorBackColor;
                    threeButton.BackColor = errorBackColor;
                    fourButton.BackColor  = errorBackColor;
                    fiveButton.BackColor  = errorBackColor;
                    sixButton.BackColor   = errorBackColor;
                    sevenButton.BackColor = errorBackColor;
                    eightButton.BackColor = errorBackColor;
                    nineButton.BackColor  = errorBackColor;

                    // zeroButton.BackColor = _buttonErrorBackColor[(ButtonTag)zeroButton.Tag];
                    // oneButton.BackColor = _buttonErrorBackColor[(ButtonTag)oneButton.Tag];
                    // twoButton.BackColor = _buttonErrorBackColor[(ButtonTag)twoButton.Tag];
                    // threeButton.BackColor = _buttonErrorBackColor[(ButtonTag)zeroButton.Tag];
                    // fourButton.BackColor = _buttonErrorBackColor[(ButtonTag)fourButton.Tag];
                    // fiveButton.BackColor = _buttonErrorBackColor[(ButtonTag)fiveButton.Tag];
                    // sixButton.BackColor = _buttonErrorBackColor[(ButtonTag)sixButton.Tag];
                    // sevenButton.BackColor = _buttonErrorBackColor[(ButtonTag)sevenButton.Tag];
                    // eightButton.BackColor = _buttonErrorBackColor[(ButtonTag)eightButton.Tag];
                    // nineButton.BackColor = _buttonErrorBackColor[(ButtonTag)nineButton.Tag];
                }
            }


            UpdateLabels();
        }

        private void UpdateLabels()
        {
            string expression = _expressionBuilder.ToExpression();
            expressionLabel.Text = expression;

            try
            {
                string autocompletedExpression = _expressionBuilder.ToExpression(true);
                resultLabel.Text = ExpressionParser.Calculate(autocompletedExpression);
            }
            catch(Exception ex)
            {
                if (ex is not ExpressionParserNullError)
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

        Color errorBackColor;
        Dictionary<ButtonTag, Color> _buttonDefaultBackColorLookup = new Dictionary<ButtonTag, Color>();
        Dictionary<ButtonTag, Color> _buttonErrorBackColor = new Dictionary<ButtonTag, Color>
        {
            {ButtonTag.Clear,             Color.FromArgb(0, 105, 105, 105)},
            {ButtonTag.Delete,            Color.FromArgb(0,  105, 105, 105)},
            {ButtonTag.ChangeSign,        Color.FromArgb(0, 105, 105, 105)},
            {ButtonTag.InsertParenthesis, Color.FromArgb(0, 105, 105, 105)},
            {ButtonTag.Division,          Color.FromArgb(0, 105, 105, 105)},
            {ButtonTag.Addition,          Color.FromArgb(0, 105, 105, 105)},
            {ButtonTag.Subtraction,       Color.FromArgb(0, 105, 105, 105)},
            {ButtonTag.Multiplication,    Color.FromArgb(0, 105, 105, 105)},
            {ButtonTag.CalculateResult,   Color.FromArgb(0, 105, 105, 105)},
            {ButtonTag.Period,            Color.FromArgb(0, 169, 169, 169)},
            {ButtonTag.Zero,              Color.FromArgb(0, 169, 169, 169)},
            {ButtonTag.One,               Color.FromArgb(0, 169, 169, 169)},
            {ButtonTag.Two,               Color.FromArgb(0, 169, 169, 169)},
            {ButtonTag.Three,             Color.FromArgb(0, 169, 169, 169)},
            {ButtonTag.Four,              Color.FromArgb(0, 169, 169, 169)},
            {ButtonTag.Five,              Color.FromArgb(0, 169, 169, 169)},
            {ButtonTag.Six,               Color.FromArgb(0, 169, 169, 169)},
            {ButtonTag.Seven,             Color.FromArgb(0, 169, 169, 169)},
            {ButtonTag.Eight,             Color.FromArgb(0, 169, 169, 169)},
            {ButtonTag.Nine,              Color.FromArgb(0, 169, 169, 169)}
        };

        private Button[] _buttonArray = new Button[20];
        private ExpressionBuilder _expressionBuilder;
    }
}
