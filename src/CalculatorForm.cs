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
            _buttonArray = new Button[20];
            _expressionBuilder = new ExpressionBuilder();
        }

        private void CalculatorForm_FormLoad(object sender, EventArgs e)
        {
            /// Configure button unique identifiers
            clearButton.Tag = ButtonTag.Clear;
            deleteButton.Tag = ButtonTag.Delete;
            signButton.Tag = ButtonTag.ChangeSign;
            paranthesisButton.Tag = ButtonTag.InsertParanthesis;
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
            _buttonArray[1] = deleteButton;
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

        private void CalculatorForm_ButtonClick(object sender, EventArgs e)
        {
            ButtonTag senderButtonTag = (ButtonTag)(sender as Button).Tag;
            UpdateExpressionBuilder(senderButtonTag);
            UpdateLabels();
        }
        
        private void CalculatorForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if(_keyCharLookup.ContainsKey(keyChar))
            {
                ButtonTag senderButtonTag = _keyCharLookup[keyChar];
                UpdateExpressionBuilder(senderButtonTag);
                UpdateLabels();
            }
        }

        private void UpdateExpressionBuilder(ButtonTag senderButtonTag)
        {
            try
            {
                switch (senderButtonTag)
                {
                    case ButtonTag.Undefined:
                        return;
                    case ButtonTag.Clear:
                        _expressionBuilder.Clear();
                        break;
                    case ButtonTag.Delete:
                        _expressionBuilder.RemoveLastCharacter();
                        break;
                    case ButtonTag.ChangeSign:
                        _expressionBuilder.ChangeSign();
                        break;
                    case ButtonTag.InsertParanthesis:
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
                        _expressionBuilder.AppendToken(OperatorToken.Multiplication.Clone());
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
                    button.Enabled = true;
                    button.BackColor = _buttonDefaultBackColorLookup[(ButtonTag)button.Tag];
                }
            }

            /// Coloured warning handler
            catch (Exception ex)
            {
                if (ex is ExpressionBuilderInsertParenthesisError)
                {
                    paranthesisButton.Enabled = false;
                    paranthesisButton.BackColor = _buttonErrorBackColor;
                }
                else if (ex is ExpressionBuilderAppendPeriodDecimalTokenError)
                {
                    periodButton.Enabled = false;
                    periodButton.BackColor = _buttonErrorBackColor;
                }
                else if (ex is ExpressionBuilderAppendOperatorTokenError)
                {
                    divisionButton.Enabled = false;
                    additionButton.Enabled = false;
                    subtractionButton.Enabled = false;
                    multiplicationButton.Enabled = false;

                    divisionButton.BackColor = _buttonErrorBackColor;
                    additionButton.BackColor = _buttonErrorBackColor;
                    subtractionButton.BackColor = _buttonErrorBackColor;
                    multiplicationButton.BackColor = _buttonErrorBackColor;

                }
                else if (ex is ExpressionBuilderAppendDecimalTokenError)
                {
                    periodButton.Enabled = false;
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

                    periodButton.BackColor = _buttonErrorBackColor;
                    zeroButton.BackColor = _buttonErrorBackColor;
                    oneButton.BackColor = _buttonErrorBackColor;
                    twoButton.BackColor = _buttonErrorBackColor;
                    threeButton.BackColor = _buttonErrorBackColor;
                    fourButton.BackColor = _buttonErrorBackColor;
                    fiveButton.BackColor = _buttonErrorBackColor;
                    sixButton.BackColor = _buttonErrorBackColor;
                    sevenButton.BackColor = _buttonErrorBackColor;
                    eightButton.BackColor = _buttonErrorBackColor;
                    nineButton.BackColor = _buttonErrorBackColor;
                }
                else if (ex is ExpressionBuilderAppendMultiplicationOrDivisionOperatorTokenError)
                {
                    divisionButton.Enabled = false;
                    multiplicationButton.Enabled = false;

                    divisionButton.BackColor = _buttonErrorBackColor;
                    multiplicationButton.BackColor = _buttonErrorBackColor;
                }
            }
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
            InsertParanthesis,
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

        private Button[] _buttonArray;
        private ExpressionBuilder _expressionBuilder;
        private readonly Color _buttonErrorBackColor = Color.FromArgb(255, 105, 97);
        private Dictionary<ButtonTag, Color> _buttonDefaultBackColorLookup = new Dictionary<ButtonTag, Color>();

        private static readonly Dictionary<char, ButtonTag> _keyCharLookup = new Dictionary<char, ButtonTag>
        {
            {(char)27, ButtonTag.Clear},
            {'\b',     ButtonTag.Delete},
            {'!',      ButtonTag.ChangeSign},
            {'s',      ButtonTag.ChangeSign},
            {'(',      ButtonTag.InsertParanthesis},
            {')',      ButtonTag.InsertParanthesis},
            {'p',      ButtonTag.InsertParanthesis},
            {'/',      ButtonTag.Division},
            {'+',      ButtonTag.Addition},
            {'-',      ButtonTag.Subtraction},
            {'*',      ButtonTag.Multiplication},
            {(char)13, ButtonTag.CalculateResult},
            {'=',      ButtonTag.CalculateResult},
            {'.',      ButtonTag.Period},
            {'0',      ButtonTag.Zero},
            {'1',      ButtonTag.One},
            {'2',      ButtonTag.Two},
            {'3',      ButtonTag.Three},
            {'4',      ButtonTag.Four},
            {'5',      ButtonTag.Five},
            {'6',      ButtonTag.Six},
            {'7',      ButtonTag.Seven},
            {'8',      ButtonTag.Eight},
            {'9',      ButtonTag.Nine},
        };
    }
}
