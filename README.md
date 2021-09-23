# Calculator

![](Calculator.png) \
A modern arbitrary precision reverse polish notation mathematical calculator with built-in keyboard shortcuts and history buffer support.

## Feature tracker

- [ ] Settings button
- [x] Order of operations
- [x] Infinite precision decimals
- [X] Automatic expression calculation
- [X] Application resizing
- [X] Easy to use sign/parenthesis insertion functionality
- [ ] Auto detect text overflow in expression/result label
- [ ] Keyboard shorcut support
- [ ] History buffer support
- [ ] Refactoring/Comments

## Keybinds

Keybind | Action
--------|-------
C | Copy
V | paste
c | Clear
s | Insert sign
p | Insert parentesis
\+ | Add
\- | Subtract
\* | Multiply
/ | Divide
^ | Exponentiation
Up | Undo
Down | Redo

## Expression

Operator | Operation
---------|----------
\*\* | Exponentiation
\* | Multiplication
/ | Division
\+ | Addition
\- | Subtraction

## Warnings

This is a single-threaded application which does not provide any failsafe for expressions that might are too big to compute
