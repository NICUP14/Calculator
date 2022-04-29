using System;
using System.Linq;
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
            _buttonDefaultStatePropertyLookup = new Dictionary<ExpressionBuilderId, ButtonStateProperty>();
        }

        private void CalculatorForm_FormLoad(object sender, EventArgs e)
        {
            /// Links buttons to their unique ExpressionBuilderId
            clearButton.Tag = ExpressionBuilderId.Clear;
            calculateButton.Tag = ExpressionBuilderId.Calculate;
            changeSignButton.Tag = ExpressionBuilderId.ChangeSign;
            deleteButton.Tag = ExpressionBuilderId.RemoveLastCharacter;
            insertParenthesisButton.Tag = ExpressionBuilderId.InsertParenthesis;
            periodButton.Tag = ExpressionBuilderId.AppendPeriodDecimalToken;
            zeroButton.Tag = ExpressionBuilderId.AppendZeroDecimalToken;
            oneButton.Tag = ExpressionBuilderId.AppendOneDecimalToken;
            twoButton.Tag = ExpressionBuilderId.AppendTwoDecimalToken;
            threeButton.Tag = ExpressionBuilderId.AppendThreeDecimalToken;
            fourButton.Tag = ExpressionBuilderId.AppendFourDecimalToken;
            fiveButton.Tag = ExpressionBuilderId.AppendFiveDecimalToken;
            sixButton.Tag = ExpressionBuilderId.AppendSixDecimalToken;
            sevenButton.Tag = ExpressionBuilderId.AppendSevenDecimalToken;
            eightButton.Tag = ExpressionBuilderId.AppendEightDecimalToken;
            nineButton.Tag = ExpressionBuilderId.AppendNineDecimalToken;
            additionButton.Tag = ExpressionBuilderId.AppendAdditionOperatorToken;
            subtractionButton.Tag = ExpressionBuilderId.AppendSubtractionOperatorToken;
            divisionButton.Tag = ExpressionBuilderId.AppendDivisionOperatorToken;
            multiplicationButton.Tag = ExpressionBuilderId.AppendMultiplicationOperatorToken;

            /// Acquires the references of all the buttons
            _buttonArray = buttonTableLayoutPanel.Controls.OfType<Button>().ToArray();

            /// Acquires the default color values of all the buttons
            foreach (Button button in _buttonArray)
            {
                ButtonStateProperty buttonDefaultProperty = new()
                {
                    BackColor = button.BackColor,
                    BorderColor = button.FlatAppearance.BorderColor
                };

                _buttonDefaultStatePropertyLookup.Add((ExpressionBuilderId)button.Tag, buttonDefaultProperty);
            }

            /// Sets the Form's focus back on the calculateButton
            calculateButton.Focus();
        }

        private void CalculatorForm_ButtonClick(object sender, EventArgs e)
        {
            if (sender is null)
                throw new ArgumentNullException(nameof(sender));

            ProcessExpressionBuilderId((ExpressionBuilderId)(sender as Button).Tag);

            /// Sets the Form's focus back on the calculateButton
            calculateButton.Focus();
        }

        private void CalculatorForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (sender is null)
                throw new ArgumentNullException(nameof(sender));

            /// Determines whether the specified KeyChar is a recognized key combination
            if (!_expressionBuilderIdLookup.ContainsKey(e.KeyChar))
                return;

            ProcessExpressionBuilderId(_expressionBuilderIdLookup[e.KeyChar]);
        }

        /// <summary>
        /// Executes the appropriate ExpressionBuilder method based on the specified identifier
        /// </summary>
        /// <param name="expressionBuilderId"></param>
        private void ProcessExpressionBuilderId(ExpressionBuilderId expressionBuilderId)
        {
            try
            {
                switch (expressionBuilderId)
                {
                    case ExpressionBuilderId.Clear:
                        _expressionBuilder.Clear();
                        break;
                    case ExpressionBuilderId.ChangeSign:
                        _expressionBuilder.ChangeSign();
                        break;
                    case ExpressionBuilderId.InsertParenthesis:
                        _expressionBuilder.InsertParenthesis();
                        break;
                    case ExpressionBuilderId.RemoveLastCharacter:
                        _expressionBuilder.RemoveTrailingCharacter();
                        break;
                    case ExpressionBuilderId.AppendPeriodDecimalToken:
                        _expressionBuilder.AppendToken(DecimalToken.Period);
                        break;
                    case ExpressionBuilderId.AppendZeroDecimalToken:
                        _expressionBuilder.AppendToken(DecimalToken.Zero);
                        break;
                    case ExpressionBuilderId.AppendOneDecimalToken:
                        _expressionBuilder.AppendToken(DecimalToken.One);
                        break;
                    case ExpressionBuilderId.AppendTwoDecimalToken:
                        _expressionBuilder.AppendToken(DecimalToken.Two);
                        break;
                    case ExpressionBuilderId.AppendThreeDecimalToken:
                        _expressionBuilder.AppendToken(DecimalToken.Three);
                        break;
                    case ExpressionBuilderId.AppendFourDecimalToken:
                        _expressionBuilder.AppendToken(DecimalToken.Four);
                        break;
                    case ExpressionBuilderId.AppendFiveDecimalToken:
                        _expressionBuilder.AppendToken(DecimalToken.Five);
                        break;
                    case ExpressionBuilderId.AppendSixDecimalToken:
                        _expressionBuilder.AppendToken(DecimalToken.Six);
                        break;
                    case ExpressionBuilderId.AppendSevenDecimalToken:
                        _expressionBuilder.AppendToken(DecimalToken.Seven);
                        break;
                    case ExpressionBuilderId.AppendEightDecimalToken:
                        _expressionBuilder.AppendToken(DecimalToken.Eight);
                        break;
                    case ExpressionBuilderId.AppendNineDecimalToken:
                        _expressionBuilder.AppendToken(DecimalToken.Nine);
                        break;
                    case ExpressionBuilderId.AppendAdditionOperatorToken:
                        _expressionBuilder.AppendToken(OperatorToken.Addition);
                        break;
                    case ExpressionBuilderId.AppendSubtractionOperatorToken:
                        _expressionBuilder.AppendToken(OperatorToken.Subtraction);
                        break;
                    case ExpressionBuilderId.AppendDivisionOperatorToken:
                        _expressionBuilder.AppendToken(OperatorToken.Division);
                        break;
                    case ExpressionBuilderId.AppendMultiplicationOperatorToken:
                        _expressionBuilder.AppendToken(OperatorToken.Multiplication);
                        break;
                }

                UpdateExpressionResultLabel(expressionBuilderId);
                UpdateExpressionLabelText();

                SetButtonState(_buttonArray, ButtonStateId.Default);
            }

            /// Sets the appropriate Button's states based on the thrown Exception
            catch (Exception ex)
            {
                if (ex is ExpressionParserSyntaxException || ex is ExpressionParserTokenException)
                {
                    SetButtonState(new Button[] { calculateButton }, ButtonStateId.Error);
                }

                else if (ex is ExpressionBuilderRemoveTrailingCharacterException)
                {
                    SetButtonState(new Button[] { deleteButton }, ButtonStateId.Error);
                }

                else if (ex is ExpressionBuilderAppendPeriodDecimalTokenException)
                {
                    SetButtonState(new Button[] { periodButton }, ButtonStateId.Error);
                }

                else if (ex is ExpressionBuilderAppendMultiplicationOrDivisionOperatorTokenException)
                {
                    SetButtonState(new Button[]
                    {
                        divisionButton,
                        multiplicationButton
                    }, ButtonStateId.Error);
                }

                else if (ex is ExpressionBuilderAppendOperatorTokenException)
                {
                    SetButtonState(new Button[]
                    {
                        additionButton,
                        subtractionButton,
                        divisionButton,
                        multiplicationButton
                    }, ButtonStateId.Error);
                }

                else if (ex is ExpressionBuilderAppendDecimalTokenException)
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
                    }, ButtonStateId.Error);
                }

                else throw;
            }
        }

        /// <summary>
        /// Updates the Text property of the CalculatorForm.ExpressionLabel
        /// </summary>
        private void UpdateExpressionLabelText()
        {
            string expression = _expressionBuilder.ToString();
            expressionLabel.Text = expression;
        }

        /// <summary>
        /// Updates the Text property of the CalculatorForm.ExpressionResultLabel based on the specified ExpressionBuilderId
        /// 
        /// </summary>
        /// <param name="clearExpressionResultLabel"></param>
        private void UpdateExpressionResultLabel(ExpressionBuilderId expressionBuilderId)
        {
            if (expressionBuilderId == ExpressionBuilderId.Calculate)
            {
                string expressionResult = _expressionBuilder.Calculate().ToString();
                expressionResultLabel.Text = expressionResult;

                _expressionBuilder.CompleteParentheses();
            }

            expressionResultLabel.Text = string.Empty;
        }

        /// <summary>
        /// Sets the specified Button's color values based on the specified state
        /// </summary>
        /// <param name="buttonArray"></param>
        /// <param name="buttonState"></param>
        private void SetButtonState(Button[] buttonArray, ButtonStateId buttonState)
        {
            foreach (Button button in buttonArray)
            {
                /// Handles the ButtonState.Error case
                if (buttonState == ButtonStateId.Error)
                {
                    button.Enabled = false;
                    button.BackColor = ButtonErrorStateProperty.BackColor;
                    button.FlatAppearance.BorderColor = ButtonErrorStateProperty.BorderColor;
                }

                /// Handles the ButtonState.Default case
                else
                {
                    ExpressionBuilderId expressionBuilderId = (ExpressionBuilderId)button.Tag;
                    ButtonStateProperty buttonDefaultState = _buttonDefaultStatePropertyLookup[expressionBuilderId];

                    button.Enabled = true;
                    button.BackColor = buttonDefaultState.BackColor;
                    button.FlatAppearance.BorderColor = buttonDefaultState.BorderColor;
                }
            }
        }

        /// <summary>
        /// This enumeration defines the identifiers of all the ExpressionBuilder methods
        /// </summary>
        private enum ExpressionBuilderId
        {
            Clear,
            Calculate,
            ChangeSign,
            InsertParenthesis,
            RemoveLastCharacter,
            AppendPeriodDecimalToken,
            AppendZeroDecimalToken,
            AppendOneDecimalToken,
            AppendTwoDecimalToken,
            AppendThreeDecimalToken,
            AppendFourDecimalToken,
            AppendFiveDecimalToken,
            AppendSixDecimalToken,
            AppendSevenDecimalToken,
            AppendEightDecimalToken,
            AppendNineDecimalToken,
            AppendAdditionOperatorToken,
            AppendSubtractionOperatorToken,
            AppendDivisionOperatorToken,
            AppendMultiplicationOperatorToken,
        }

        /// <summary>
        /// This enumeration defines the identifiers of all the Button states
        /// </summary>
        private enum ButtonStateId
        {
            Error,
            Default
        }

        /// <summary>
        /// This struct defines the default color values of the ButtonState.Error
        /// </summary>
        private static readonly ButtonStateProperty ButtonErrorStateProperty = new()
        {
            BackColor = Color.FromArgb(255, 105, 97),
            BorderColor = Color.FromArgb(255, 105, 97)
        };

        /// <summary>
        /// Links a KeyPressEventArgs.KeyChar to its appropriate ExpressionBuilder method
        /// </summary>
        private readonly Dictionary<char, ExpressionBuilderId> _expressionBuilderIdLookup = new()
        {
            { (char)Keys.Escape, ExpressionBuilderId.Clear },
            { (char)Keys.Back, ExpressionBuilderId.RemoveLastCharacter },
            { '!', ExpressionBuilderId.ChangeSign },
            { 's', ExpressionBuilderId.ChangeSign },
            { '(', ExpressionBuilderId.InsertParenthesis },
            { ')', ExpressionBuilderId.InsertParenthesis },
            { 'p', ExpressionBuilderId.InsertParenthesis },
            { '/', ExpressionBuilderId.AppendDivisionOperatorToken },
            { '+', ExpressionBuilderId.AppendAdditionOperatorToken },
            { '-', ExpressionBuilderId.AppendSubtractionOperatorToken },
            { '*', ExpressionBuilderId.AppendMultiplicationOperatorToken },
            { (char)Keys.Enter, ExpressionBuilderId.Calculate },
            { '.', ExpressionBuilderId.AppendPeriodDecimalToken },
            { '0', ExpressionBuilderId.AppendZeroDecimalToken },
            { '1', ExpressionBuilderId.AppendOneDecimalToken },
            { '2', ExpressionBuilderId.AppendTwoDecimalToken },
            { '3', ExpressionBuilderId.AppendThreeDecimalToken },
            { '4', ExpressionBuilderId.AppendFourDecimalToken },
            { '5', ExpressionBuilderId.AppendFiveDecimalToken },
            { '6', ExpressionBuilderId.AppendSixDecimalToken },
            { '7', ExpressionBuilderId.AppendSevenDecimalToken },
            { '8', ExpressionBuilderId.AppendEightDecimalToken },
            { '9', ExpressionBuilderId.AppendNineDecimalToken },
        };

        private struct ButtonStateProperty
        {
            public Color BackColor;
            public Color BorderColor;
        }

        private Button[] _buttonArray;
        private readonly ExpressionBuilder _expressionBuilder;
        private readonly Dictionary<ExpressionBuilderId, ButtonStateProperty> _buttonDefaultStatePropertyLookup;
    }
}
