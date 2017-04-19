using System;

namespace MathsParser
{
	public class Parser
	{
		public float Parse(string expression)
		{
			var openingBracketPosition = expression.IndexOf("(", StringComparison.InvariantCulture) + 1;
			var closingBracketPosition = expression.IndexOf(")", openingBracketPosition, StringComparison.InvariantCulture);

			var functionInput = expression.Substring(openingBracketPosition, closingBracketPosition - openingBracketPosition);

			float inputValue;

			if (!float.TryParse(functionInput, out inputValue))
			{
				throw new ArgumentException($"{nameof(expression)} must be a valid number");
			}

			return Math.Abs(float.Parse(functionInput));
		}
	}
}
