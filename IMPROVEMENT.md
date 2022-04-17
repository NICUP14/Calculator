# Calculator-related improvements

# Bugs
Expression "+" and "-" throws System.InvalidOperationException inside ExpressionParser
Expression "0.25+2*" evaluates to "2.25*2.25", breaking the order of operations
