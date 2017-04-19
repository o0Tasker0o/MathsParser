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
			expression = expression.Trim();

			foreach (var availableFunction in FunctionRunner.AvailableFunctions)
			{
				expression = ReplaceFunctions(expression, availableFunction);
			}

			expression = ParseBrackets(expression);

			var elements = expression.Split('-');

			if (elements.Length > 1)
			{
				return ParseSubtraction(elements);
			}

			elements = expression.Split('+');

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

		private string ParseBrackets(string expression)
		{
			var bracketPosition = expression.IndexOf("(", StringComparison.InvariantCultureIgnoreCase);

			while (bracketPosition != -1)
			{
				bracketPosition += 1;
				var closingBracketPosition = GetClosingBracketPosition(expression, bracketPosition + 1);

				var bracketResult = Parse(expression.Substring(bracketPosition, closingBracketPosition - bracketPosition));

				var expressionToReplace = expression.Substring(bracketPosition - 1, (closingBracketPosition - bracketPosition) + 2);

				expression = expression.Replace(expressionToReplace, bracketResult.ToString(CultureInfo.InvariantCulture));

				bracketPosition = expression.IndexOf("(", StringComparison.InvariantCultureIgnoreCase);
			}

			return expression;
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

		private double ParseSubtraction(IReadOnlyCollection<string> elements)
		{
			var numberElements = new List<double>(elements.Count);

			for (var index = 0; index < elements.Count; ++index)
			{
				if (string.IsNullOrEmpty(elements.ElementAt(index)))
				{
					numberElements.Add(0);
				}
				else
				{
					numberElements.Add(Parse(elements.ElementAt(index)));
				}
			}

			return numberElements.Aggregate((a, b) => a - b);
		}

		private string ReplaceFunctions(string expression, string function)
		{
			var functionPosition = expression.IndexOf(function, StringComparison.InvariantCultureIgnoreCase);

			while (functionPosition != -1)
			{
				var openingBracketPosition = expression.IndexOf("(", functionPosition, StringComparison.InvariantCulture) + 1;
				var closingBracketPosition = GetClosingBracketPosition(expression, openingBracketPosition);

				var functionInput = expression.Substring(openingBracketPosition, closingBracketPosition - openingBracketPosition);

				var functionExpressionToReplace = expression.Substring(functionPosition, (closingBracketPosition - functionPosition) + 1);

				var parsedInput = Parse(functionInput);

				var functionValue = FunctionRunner.Run(function, parsedInput);

				expression = expression.Replace(functionExpressionToReplace, functionValue.ToString(CultureInfo.InvariantCulture));

				functionPosition = expression.IndexOf("abs", StringComparison.InvariantCultureIgnoreCase);
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

			throw new ArgumentException("Mismatching brackets found");
		}
	}
}
