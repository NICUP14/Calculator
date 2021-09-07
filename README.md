# DESIGN
<img src="calculator.png" width = "300" height = "400">

# INTRODUCTION
A minimalist feature-rich keyboard-centric reverse polish notation arbitrary precision calculator with built-in keybind and history buffer support. \
The project's main goal is to provide a better experience for people who expect more from their default plain calculator.

## Feature tracker
- [x] Order of operations
- [x] Infinite precision decimals
- [ ] Automatic expression calculation
- [ ] Simple keyboard layout navigation
- [ ] Easy to use sign/parenthesis/conditional insertion functionality
- [ ] Efficient usage of calculator via keybindings
- [ ] Undo/redo functionality via history buffer

## Keybinds
Keybind | Action
--------|-------
Backspace | Backspace
CTRL + c | Copy
CTRL + v | paste
c | Clear
a | Add
s | Subtract
m | Multiply
d | Divide
e | Exponentiation
u | Undo
r | Redo
s | Insert sign
p | Insert parentesis

## Expression syntax
Expressions follow the standard mathematical syntax. \
Decimal: An infinite precision base 10 rational number (can contain only digits and a period). \
Operator: A string with special meaning implementing operations on decimals (non-alphanumeric). \
An expression is the result of combining decimals and operators together in a string.

Operator | Operation
---------|----------
\** | Exponentiation
\* | Multiplication
\/ | Division
\+ | Addition
\- | Subtraction

## Error codes
Error | Meaning
------|--------
DecimalNullError | Decimal is either empty or is not assigned
DecimalFormatError | Decimal's period/sign is misplaced
DecimalInvalidError | Decimal contains non-alphanumeric charcters
DecimalPrecisionError | Decimal's division precision is a negative
DecimalDivisionError | Decimal is divided by zero
ExpressionNullError | Expression is either empty or is not assigned
ExpressionSyntaxError | Expression is missing matching parenthesis/operator/operand
ExpressionElementError | Expression contains unidentified element
ExpressionAssignmentError | Expression contains invalid assignment
CalculatorInternalError | Calculator's internal code encountered an error while parsing expression

**Warning: This is a single-threaded application which does not provide any failsafe for expressions that might are too big to compute** \
**Warning: Any expression that returned either DecimalNullError, ExpressionNullError or CalculatorInternalError should be reported, beacause they were not meant to occur.**
