using System.Text;
using System.Collections.Generic;

namespace Calculator
{
    /// <summary>
    /// Expression manipulator implementation
    /// WARNING: This is still in development, do not use it!
    /// </summary>
    class ExpressionManipulator : ExpressionParser
    {
        public ExpressionManipulator(string expression = "")
        {
            _tokenList = new LinkedList<Token>();

            /// Converts expression to an intermediate to a token linked list
            if (!string.IsNullOrEmpty(expression))
            {
                Token[] tokenArray = ExpressionParser.ConvertExpressionToTokenArray(expression);
                foreach (Token token in tokenArray)
                    _tokenList.AddLast(token);
            }
        }

        public override string ToString()
        {
            /// Initialize string builder
            int expressionStringBuilderCapacity = 0;
            foreach (Token token in _tokenList)
                expressionStringBuilderCapacity += token.ToString().Length;
            StringBuilder expressionStringBuilder = new StringBuilder(expressionStringBuilderCapacity);

            /// Build string representation
            foreach (Token token in _tokenList)
                expressionStringBuilder.Append(token);

            return expressionStringBuilder.ToString();
        }

        public void AppendToken(Token token)
        {

        }

        readonly LinkedList<Token> _tokenList;
    }
}
