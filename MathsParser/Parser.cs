using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace MathsParser
{
	public class Parser
	{
		private readonly IEnumerable<string> _availableFunctions = new List<string>
		{
			"abs",
			"atn",
			"cos",
			"exp",
			"int",
			"fix"
		};

		public double Parse(string expression)
		{
			foreach (var availableFunction in _availableFunctions)
			{
				expression = ReplaceFunctions(expression, availableFunction);
			}

			var elements = expression.Split('+');
			var numberElements = new List<double>(elements.Length);

			foreach (var element in elements)
			{
				double parsedElement;

				if (!double.TryParse(element, out parsedElement))
				{
					throw new ArgumentException($"{element} is not a valid number");
				}

				numberElements.Add(parsedElement);
			}

			return numberElements.Sum(number => number);
		}

		private string ReplaceFunctions(string expression, string function)
		{
			var absolutePosition = expression.IndexOf(function, StringComparison.InvariantCultureIgnoreCase);

			while (absolutePosition != -1)
			{
				var openingBracketPosition = expression.IndexOf("(", absolutePosition, StringComparison.InvariantCulture) + 1;
				var closingBracketPosition = GetClosingBracketPosition(expression, openingBracketPosition);

				var functionInput = expression.Substring(openingBracketPosition, closingBracketPosition - openingBracketPosition);

				var absoluteExpressionToReplace = expression.Substring(absolutePosition, (closingBracketPosition - absolutePosition) + 1);

				var parsedInput = Parse(functionInput);

				var absoluteValue = FunctionRunner.Run(function, parsedInput);

				expression = expression.Replace(absoluteExpressionToReplace, absoluteValue.ToString(CultureInfo.InvariantCulture));

				absolutePosition = expression.IndexOf("abs", StringComparison.InvariantCultureIgnoreCase);
			}

			return expression;
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

			return expression.IndexOf(")", startPosition, StringComparison.InvariantCulture);
		}
	}
}
