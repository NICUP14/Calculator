using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Calculator
{
    public partial class CalculatorForm : Form
    {
        public CalculatorForm()
        {
            InitializeComponent();
            _expressionBuilder = new ExpressionBuilder();
            _buttonDefaultStateLookup = new Dictionary<FunctionTag, ButtonDefaultState>();
        }

        private void CalculatorForm_FormLoad(object sender, EventArgs e)
        {
            /// Links buttons to their unique identifiers
            clearButton.Tag = FunctionTag.Clear;
            deleteButton.Tag = FunctionTag.RemoveLastCharacter;
            changeSignButton.Tag = FunctionTag.ChangeSign;
            insertParanthesisButton.Tag = FunctionTag.InsertParanthesis;
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

            /// Acquires the references of all the buttons
            _buttonArray = buttonTableLayoutPanel.Controls.OfType<Button>().ToArray();

            /// Acquires the default properties of all the buttons
            foreach (Button button in _buttonArray)
            {
                ButtonDefaultState buttonDefaultState = new()
                {
                    BackColor = button.BackColor,
                    BorderColor = button.FlatAppearance.BorderColor
                };

                _buttonDefaultStateLookup.Add((FunctionTag)button.Tag, buttonDefaultState);
            }
        }

        private void CalculatorForm_ButtonClick(object sender, EventArgs e)
        {
            if (sender is null)
                throw new ArgumentNullException(nameof(sender));

            ProcessFunctionTag((FunctionTag)(sender as Button).Tag);

            /// Sets the calculate button back in focus
            calculateButton.Focus();
        }

        private void CalculatorForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (sender is null)
                throw new ArgumentNullException(nameof(sender));

            /// Determines whether the pressed combination is recognized
            if (!_hotkeyLookup.ContainsKey(e.KeyChar))
                return;

            ProcessFunctionTag(_hotkeyLookup[e.KeyChar]);
        }

        /// <summary>
        /// Links front-end components to their back-end counterpars
        /// </summary>
        /// <param name="functionTag"></param>
        private void ProcessFunctionTag(FunctionTag functionTag)
        {
            if (functionTag == FunctionTag.Undefined)
                return;

            try
            {
                switch (functionTag)
                {
                    case FunctionTag.Clear:
                        _expressionBuilder.Clear();
                        break;
                    case FunctionTag.ChangeSign:
                        _expressionBuilder.ChangeSign();
                        break;
                    case FunctionTag.InsertParanthesis:
                        _expressionBuilder.InsertParenthesis();
                        break;
                    case FunctionTag.RemoveLastCharacter:
                        _expressionBuilder.RemoveLastCharacter();
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
                        _expressionBuilder.AppendToken(OperatorToken.Multiplication);
                        break;
                    case FunctionTag.Period:
                        _expressionBuilder.AppendToken(DecimalToken.Period);
                        break;
                    case FunctionTag.Zero:
                        _expressionBuilder.AppendToken(DecimalToken.Zero);
                        break;
                    case FunctionTag.One:
                        _expressionBuilder.AppendToken(DecimalToken.One);
                        break;
                    case FunctionTag.Two:
                        _expressionBuilder.AppendToken(DecimalToken.Two);
                        break;
                    case FunctionTag.Three:
                        _expressionBuilder.AppendToken(DecimalToken.Three);
                        break;
                    case FunctionTag.Four:
                        _expressionBuilder.AppendToken(DecimalToken.Four);
                        break;
                    case FunctionTag.Five:
                        _expressionBuilder.AppendToken(DecimalToken.Five);
                        break;
                    case FunctionTag.Six:
                        _expressionBuilder.AppendToken(DecimalToken.Six);
                        break;
                    case FunctionTag.Seven:
                        _expressionBuilder.AppendToken(DecimalToken.Seven);
                        break;
                    case FunctionTag.Eight:
                        _expressionBuilder.AppendToken(DecimalToken.Eight);
                        break;
                    case FunctionTag.Nine:
                        _expressionBuilder.AppendToken(DecimalToken.Nine);
                        break;
                }

                UpdateExpressionResultLabel(functionTag != FunctionTag.Calculate);
                UpdateExpressionLabel();

                SetButtonState(_buttonArray, ButtonState.Default);
            }

            /// Handles the "colored warning" case
            catch (ExpressionBuilderRemoveLastCharacterException)
            {
                SetButtonState(new Button[] { deleteButton }, ButtonState.Error);
            }
            catch (ExpressionBuilderAppendPeriodDecimalTokenException)
            {
                SetButtonState(new Button[] { periodButton }, ButtonState.Error);
            }
            catch (ExpressionBuilderAppendMultiplicationOrDivisionOperatorTokenException)
            {
                SetButtonState(new Button[]
                {
                    divisionButton,
                    multiplicationButton
                }, ButtonState.Error);
            }
            catch (ExpressionBuilderAppendOperatorTokenException)
            {
                SetButtonState(new Button[]
                {
                    divisionButton,
                    additionButton,
                    subtractionButton,
                    multiplicationButton
                }, ButtonState.Error);
            }
            catch (ExpressionBuilderAppendDecimalTokenException)
            {
                SetButtonState(new Button[]
                {
                    zeroButton,
                    oneButton,
                    twoButton,
                    threeButton,
                    fourButton,
                    fiveButton,
                    sixButton,
                    sevenButton,
                    eightButton,
                    nineButton,
                    periodButton
                }, ButtonState.Error);
            }
        }

        private void UpdateExpressionLabel()
        {
            string expression = _expressionBuilder.ToString();
            expressionLabel.Text = expression;
        }

        private void UpdateExpressionResultLabel(bool clearExpressionResultLabel = false)
        {
            if (clearExpressionResultLabel == true)
            {
                expressionResultLabel.Text = string.Empty;
                return;
            }

            string expressionResult = _expressionBuilder.Calculate().ToString();
            expressionResultLabel.Text = expressionResult;

            _expressionBuilder.CompleteParantheses();
        }

        private void SetButtonState(Button[] buttonArray, ButtonState buttonState)
        {
            foreach (Button button in buttonArray)
            {
                button.Enabled = buttonState == ButtonState.Default;

                if (buttonState == ButtonState.Error)
                {
                    button.BackColor = ButtonErrorColor;
                    button.FlatAppearance.BorderColor = ButtonErrorColor;
                }
                else
                {
                    ButtonDefaultState buttonDefaultState = _buttonDefaultStateLookup[(FunctionTag)button.Tag];

                    button.BackColor = buttonDefaultState.BackColor;
                    button.FlatAppearance.BorderColor = buttonDefaultState.BorderColor;
                }
            }
        }

        /// <summary>
        /// Defines function unique identifiers
        /// </summary>
        private enum FunctionTag
        {
            Undefined,
            Clear,
            ChangeSign,
            InsertParanthesis,
            RemoveLastCharacter,
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

        private enum ButtonState
        {
            Error,
            Default
        }

        private readonly Dictionary<FunctionTag, ButtonDefaultState> _buttonDefaultStateLookup;
        private struct ButtonDefaultState
        {
            public Color BackColor;
            public Color BorderColor;
        }

        private readonly Dictionary<char, FunctionTag> _hotkeyLookup = new()
        {
            { (char)Keys.Escape, FunctionTag.Clear },
            { (char)Keys.Back, FunctionTag.RemoveLastCharacter },
            { '!', FunctionTag.ChangeSign },
            { 's', FunctionTag.ChangeSign },
            { '(', FunctionTag.InsertParanthesis },
            { ')', FunctionTag.InsertParanthesis },
            { 'p', FunctionTag.InsertParanthesis },
            { '/', FunctionTag.Division },
            { '+', FunctionTag.Addition },
            { '-', FunctionTag.Subtraction },
            { '*', FunctionTag.Multiplication },
            { (char)Keys.Enter, FunctionTag.Calculate },
            { '.', FunctionTag.Period },
            { '0', FunctionTag.Zero },
            { '1', FunctionTag.One },
            { '2', FunctionTag.Two },
            { '3', FunctionTag.Three },
            { '4', FunctionTag.Four },
            { '5', FunctionTag.Five },
            { '6', FunctionTag.Six },
            { '7', FunctionTag.Seven },
            { '8', FunctionTag.Eight },
            { '9', FunctionTag.Nine },
        };

        private Button[] _buttonArray;
        private readonly ExpressionBuilder _expressionBuilder;
        private static readonly Color ButtonErrorColor = Color.FromArgb(255, 105, 97);
    }
}
