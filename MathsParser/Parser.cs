using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace MathsParser
{
	public class Parser
	{
		public double Parse(string expression)
		{
			foreach (var availableFunction in FunctionRunner.AvailableFunctions)
			{
				expression = ReplaceFunctions(expression, availableFunction);
			}

			var elements = expression.Split('+');

			if (elements.Length > 1)
			{
				return ParseAddition(elements);
			}

			elements = expression.Split('*');

			if (elements.Length > 1)
			{
				return ParseMultiplication(elements);
			}

			elements = expression.Split('/');

			if (elements.Length > 1)
			{
				return ParseDivision(elements);
			}

			double parsedElement;

			if (!double.TryParse(elements[0], out parsedElement))
			{
				throw new ArgumentException($"{elements[0]} is not a valid number");
			}

			return parsedElement;
		}

		private double ParseDivision(IReadOnlyCollection<string> elements)
		{
			var numberElements = new List<double>(elements.Count);

			numberElements.AddRange(elements.Select(Parse));

			return numberElements.Aggregate((a, b) => a / b);
		}

		private double ParseMultiplication(IReadOnlyCollection<string> elements)
		{
			var numberElements = new List<double>(elements.Count);

			numberElements.AddRange(elements.Select(Parse));

			return numberElements.Aggregate((a, b) => a * b);
		}

		private double ParseAddition(IReadOnlyCollection<string> elements)
		{
			var numberElements = new List<double>(elements.Count);

			numberElements.AddRange(elements.Select(Parse));

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
