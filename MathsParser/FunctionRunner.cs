using System;

namespace MathsParser
{
	public class FunctionRunner
	{
		public static float Run(string function, float input)
		{
			function = function.ToLower();

			switch (function)
			{
				case "abs":
					return Math.Abs(input);
				case "atn":
					return (float) Math.Atan(input);
				case "cos":
					return (float) Math.Cos(input);
				default:
					throw new ArgumentException($"{function} is not recognised");
			}
		}
	}
}
