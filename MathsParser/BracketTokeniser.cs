using System;
using System.Collections.Generic;

namespace MathsParser
{
	public class BracketTokeniser
	{
		public static IEnumerable<BracketToken> Tokenise(string expression)
		{
			var tokens = new List<BracketToken>();

			var openingBracketPosition = expression.IndexOf("(", StringComparison.InvariantCulture);
			var closingBracketPosition = -1;

			while (openingBracketPosition != -1)
			{
				openingBracketPosition++;

				closingBracketPosition = GetClosingBracketPosition(expression, openingBracketPosition);

				tokens.Add(new BracketToken
				{
					IsBracketed = true,
					Value = expression.Substring(openingBracketPosition, closingBracketPosition - openingBracketPosition)
				});

				openingBracketPosition = expression.IndexOf("(", closingBracketPosition, StringComparison.InvariantCulture);

				if (openingBracketPosition != -1)
				{
					var intermediateExpression = expression.Substring(closingBracketPosition + 1, (openingBracketPosition - closingBracketPosition) - 1);

					tokens.Add(new BracketToken
					{
						IsBracketed = false,
						Value = intermediateExpression
					});
				}
			}

			var finalExpression = expression.Substring(closingBracketPosition + 1);

			if (!string.IsNullOrEmpty(finalExpression))
			{
				tokens.Add(new BracketToken
				{
					IsBracketed = false,
					Value = finalExpression
				});
			}

			return tokens;
		}

		private static int GetClosingBracketPosition(string expression, int startPosition)
		{
			var bracketDepth = 0;

			for (var index = startPosition; index < expression.Length; ++index)
			{
				if (bracketDepth == 0 && expression[index] == ')')
				{
					return index;
				}

				if (expression[index] == '(')
				{
					bracketDepth++;
				}
				else if (expression[index] == ')')
				{
					bracketDepth--;
				}
			}

			throw new ArgumentException("Mismatching brackets found");
		}
	}
}
