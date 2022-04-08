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
            _buttonDefaultBackColorLookup = new Dictionary<FunctionTag, Color>();
        }

        private void CalculatorForm_FormLoad(object sender, EventArgs e)
        {
            /// Configure button unique identifiers
            clearButton.Tag = FunctionTag.Clear;
            deleteButton.Tag = FunctionTag.Delete;
            signButton.Tag = FunctionTag.ChangeSign;
            paranthesisButton.Tag = FunctionTag.InsertParanthesis;
            divisionButton.Tag = FunctionTag.Division;
            additionButton.Tag = FunctionTag.Addition;
            subtractionButton.Tag = FunctionTag.Subtraction;
            multiplicationButton.Tag = FunctionTag.Multiplication;
            calculateButton.Tag = FunctionTag.Calculate;
            periodButton.Tag = FunctionTag.Period;
            zeroButton.Tag = FunctionTag.Zero;
            oneButton.Tag = FunctionTag.One;
            twoButton.Tag = FunctionTag.Two;
            threeButton.Tag = FunctionTag.Three;
            fourButton.Tag = FunctionTag.Four;
            fiveButton.Tag = FunctionTag.Five;
            sixButton.Tag = FunctionTag.Six;
            sevenButton.Tag = FunctionTag.Seven;
            eightButton.Tag = FunctionTag.Eight;
            nineButton.Tag = FunctionTag.Nine;

            /// Add button references to button array
            _buttonArray[0] = clearButton;
            _buttonArray[1] = deleteButton;
            _buttonArray[2] = signButton;
            _buttonArray[3] = paranthesisButton;
            _buttonArray[4] = divisionButton;
            _buttonArray[5] = additionButton;
            _buttonArray[6] = subtractionButton;
            _buttonArray[7] = multiplicationButton;
            _buttonArray[8] = calculateButton;
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
                _buttonDefaultBackColorLookup.Add((FunctionTag)button.Tag, button.BackColor);
        }

        private void CalculatorForm_ButtonClick(object sender, EventArgs e)
        {
            FunctionTag senderButtonTag = (FunctionTag)(sender as Button).Tag;
            ProcessFunctionTag(senderButtonTag);
            UpdateLabels();
            Focus();
        }
        
        private void CalculatorForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if (!_hotkeyLookup.ContainsKey(keyChar))
                return;

            FunctionTag senderButtonTag = _hotkeyLookup[keyChar];
            ProcessFunctionTag(senderButtonTag);
            UpdateLabels();
        }

        private void ProcessFunctionTag(FunctionTag senderButtonTag)
        {
            if (senderButtonTag == FunctionTag.Undefined)
                return;

            try
            {
                switch (senderButtonTag)
                {
                    case FunctionTag.Clear:
                        _expressionBuilder.Clear();
                        break;
                    case FunctionTag.Delete:
                        _expressionBuilder.RemoveLastCharacter();
                        break;
                    case FunctionTag.ChangeSign:
                        _expressionBuilder.ChangeSign();
                        break;
                    case FunctionTag.InsertParanthesis:
                        _expressionBuilder.InsertParenthesis();
                        break;
                    case FunctionTag.Division:
                        _expressionBuilder.AppendToken(OperatorToken.Division);
                        break;
                    case FunctionTag.Addition:
                        _expressionBuilder.AppendToken(OperatorToken.Addition);
                        break;
                    case FunctionTag.Subtraction:
                        _expressionBuilder.AppendToken(OperatorToken.Subtraction);
                        break;
                    case FunctionTag.Multiplication:
                        _expressionBuilder.AppendToken(OperatorToken.Multiplication.Clone());
                        break;
                    case FunctionTag.Calculate:
                        CalculateResult();
                        break;
                    case FunctionTag.Period:
                        _expressionBuilder.AppendToken(new DecimalToken("."));
                        break;
                    case FunctionTag.Zero:
                        _expressionBuilder.AppendToken(new DecimalToken("0"));
                        break;
                    case FunctionTag.One:
                        _expressionBuilder.AppendToken(new DecimalToken("1"));
                        break;
                    case FunctionTag.Two:
                        _expressionBuilder.AppendToken(new DecimalToken("2"));
                        break;
                    case FunctionTag.Three:
                        _expressionBuilder.AppendToken(new DecimalToken("3"));
                        break;
                    case FunctionTag.Four:
                        _expressionBuilder.AppendToken(new DecimalToken("4"));
                        break;
                    case FunctionTag.Five:
                        _expressionBuilder.AppendToken(new DecimalToken("5"));
                        break;
                    case FunctionTag.Six:
                        _expressionBuilder.AppendToken(new DecimalToken("6"));
                        break;
                    case FunctionTag.Seven:
                        _expressionBuilder.AppendToken(new DecimalToken("7"));
                        break;
                    case FunctionTag.Eight:
                        _expressionBuilder.AppendToken(new DecimalToken("8"));
                        break;
                    case FunctionTag.Nine:
                        _expressionBuilder.AppendToken(new DecimalToken("9"));
                        break;
                }

                foreach (Button button in _buttonArray)
                {
                    button.Enabled = true;
                    button.BackColor = _buttonDefaultBackColorLookup[(FunctionTag)button.Tag];
                }
            }

            /// Coloured warning handler
            catch (Exception ex)
            {
                if (ex is ExpressionBuilderInsertParenthesisError)
                {
                    paranthesisButton.Enabled = false;
                    paranthesisButton.BackColor = ButtonErrorBackColor;
                }
                else if(ex is ExpressionBuilderRemoveLastCharacterError)
                {
                    deleteButton.Enabled = false;
                    deleteButton.BackColor = ButtonErrorBackColor;
                }
                else if (ex is ExpressionBuilderAppendPeriodDecimalTokenError)
                {
                    periodButton.Enabled = false;
                    periodButton.BackColor = ButtonErrorBackColor;
                }
                else if (ex is ExpressionBuilderAppendOperatorTokenError)
                {
                    divisionButton.Enabled = false;
                    additionButton.Enabled = false;
                    subtractionButton.Enabled = false;
                    multiplicationButton.Enabled = false;

                    divisionButton.BackColor = ButtonErrorBackColor;
                    additionButton.BackColor = ButtonErrorBackColor;
                    subtractionButton.BackColor = ButtonErrorBackColor;
                    multiplicationButton.BackColor = ButtonErrorBackColor;

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

                    periodButton.BackColor = ButtonErrorBackColor;
                    zeroButton.BackColor = ButtonErrorBackColor;
                    oneButton.BackColor = ButtonErrorBackColor;
                    twoButton.BackColor = ButtonErrorBackColor;
                    threeButton.BackColor = ButtonErrorBackColor;
                    fourButton.BackColor = ButtonErrorBackColor;
                    fiveButton.BackColor = ButtonErrorBackColor;
                    sixButton.BackColor = ButtonErrorBackColor;
                    sevenButton.BackColor = ButtonErrorBackColor;
                    eightButton.BackColor = ButtonErrorBackColor;
                    nineButton.BackColor = ButtonErrorBackColor;
                }
                else if (ex is ExpressionBuilderAppendMultiplicationOrDivisionOperatorTokenError)
                {
                    divisionButton.Enabled = false;
                    multiplicationButton.Enabled = false;

                    divisionButton.BackColor = ButtonErrorBackColor;
                    multiplicationButton.BackColor = ButtonErrorBackColor;
                }
                else if(ex is ExpressionParserSyntaxError || ex is DecimalPeriodError || ex is DecimalDivisionError)
                {
                    calculateButton.Enabled = false;
                    calculateButton.BackColor = ButtonErrorBackColor;
                }
            }
        }

        private void CalculateResult()
        {
            Token[] tokenArray = _expressionBuilder.ToTokenArray();
            DecimalToken decimalToken = new DecimalToken(ExpressionParser.Calculate(tokenArray).ToString());

            _expressionBuilder.Clear();
            _expressionBuilder.AppendToken(decimalToken);

            UpdateLabels();
        }

        private void UpdateLabels()
        {
            string expression = _expressionBuilder.ToString();
            expressionLabel.Text = expression;

            try
            {
                Token[] tokenArray = _expressionBuilder.ToTokenArray();
                resultLabel.Text = ExpressionParser.Calculate(tokenArray).ToString();
            }
            catch(Exception ex)
            {
                if (ex is not ExpressionParserNullError)
                    resultLabel.Text = "Error";
                else
                    expressionLabel.Text = resultLabel.Text = string.Empty;
            }
        }

        private enum FunctionTag
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
            Calculate,
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
        private Dictionary<FunctionTag, Color> _buttonDefaultBackColorLookup;
        private readonly Dictionary<char, FunctionTag> _hotkeyLookup  = new Dictionary<char, FunctionTag>
        {
            {(char)Keys.Escape, FunctionTag.Clear},
            {(char)Keys.Back,   FunctionTag.Delete},
            {'!',               FunctionTag.ChangeSign},
            {'s',               FunctionTag.ChangeSign},
            {'(',               FunctionTag.InsertParanthesis},
            {')',               FunctionTag.InsertParanthesis},
            {'p',               FunctionTag.InsertParanthesis},
            {'/',               FunctionTag.Division},
            {'+',               FunctionTag.Addition},
            {'-',               FunctionTag.Subtraction},
            {'*',               FunctionTag.Multiplication},
            {(char)Keys.Enter,  FunctionTag.Calculate},
            {'.',               FunctionTag.Period},
            {'0',               FunctionTag.Zero},
            {'1',               FunctionTag.One},
            {'2',               FunctionTag.Two},
            {'3',               FunctionTag.Three},
            {'4',               FunctionTag.Four},
            {'5',               FunctionTag.Five},
            {'6',               FunctionTag.Six},
            {'7',               FunctionTag.Seven},
            {'8',               FunctionTag.Eight},
            {'9',               FunctionTag.Nine},
        };

        public static Color ButtonErrorBackColor = Color.FromArgb(255, 105, 97);
    }
}
