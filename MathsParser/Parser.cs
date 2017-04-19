﻿using System;
using System.Collections.Generic;
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
			var numberElements = new List<float>(elements.Length);

			foreach (var element in elements)
			{
				float parsedElement;

				if (!float.TryParse(element, out parsedElement))
				{
					throw new ArgumentException($"{element} is not a valid number");
				}

				numberElements.Add(parsedElement);
			}

			return numberElements.Sum(number => number);
		}

		private string ReplaceAbsolutes(string expression)
		{
			var absolutePosition = expression.IndexOf("abs", StringComparison.InvariantCultureIgnoreCase);

			while (absolutePosition != -1)
			{
				var openingBracketPosition = expression.IndexOf("(", absolutePosition, StringComparison.InvariantCulture) + 1;
				var closingBracketPosition = GetClosingBracketPosition(expression, openingBracketPosition);

				var functionInput = expression.Substring(openingBracketPosition, closingBracketPosition - openingBracketPosition);

				var absoluteExpressionToReplace = expression.Substring(absolutePosition, (closingBracketPosition - absolutePosition) + 1);

				var parsedInput = Parse(functionInput);

				var absoluteValue = Math.Abs(parsedInput);
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
