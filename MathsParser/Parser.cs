using System;
using System.Globalization;
using System.Linq;

namespace MathsParser
{
	public class Parser
	{
		public float Parse(string expression)
		{
			expression = ReplaceAbsolutes(expression);
			var elements = expression.Split('+');
			var numberElements = elements.Select(float.Parse);

			return numberElements.Sum(number => number);
		}

		private string ReplaceAbsolutes(string expression)
		{
			var absolutePosition = expression.IndexOf("abs", StringComparison.InvariantCultureIgnoreCase);

			while (absolutePosition != -1)
			{
				var openingBracketPosition = expression.IndexOf("(", absolutePosition, StringComparison.InvariantCulture) + 1;
				var closingBracketPosition = expression.IndexOf(")", openingBracketPosition, StringComparison.InvariantCulture);

				var functionInput = expression.Substring(openingBracketPosition, closingBracketPosition - openingBracketPosition);

				float inputValue;

				if (!float.TryParse(functionInput, out inputValue))
				{
					throw new ArgumentException($"{nameof(expression)} must be a valid number");
				}

				var absoluteExpressionToReplace = expression.Substring(absolutePosition, (closingBracketPosition - absolutePosition) + 1);
				var absoluteValue = Math.Abs(float.Parse(functionInput));
				expression = expression.Replace(absoluteExpressionToReplace, absoluteValue.ToString(CultureInfo.InvariantCulture));

				absolutePosition = expression.IndexOf("abs", StringComparison.InvariantCultureIgnoreCase);
			}

			return expression;
		}
	}
}
