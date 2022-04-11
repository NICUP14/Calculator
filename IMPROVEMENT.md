# Calculator-related improvements

# Decimal.cs

- [X] Support multiple decimal formats

# DecimalToken.cs

- [X] Implement Reformat method

## CalculatorForm.cs

- [ ] Add useful comments

## ExpressionBuilder.cs

- [ ] Add useful comments
- [X] Call DecimalToken.Reformat
- [X] Reformat methods
- [X] Use predefined Count method
- [X] Fix "123+" + "Insert Paranthesis => Unresponsive
- [X] Implement CompleteParantheses method
- [X] Call CompleteParantheses inside Calculate method

## OperatorToken.cs

- [X] Remove unused IsAdditionOrSubtraction

## ExpressionParser.cs

- [X] Implement token cloning
- [X] Instanciate tokens uniquely
- [X] Feed ExpressionBuilder's token array to ExpressionParser
