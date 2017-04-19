using System;

namespace MathsParser
{
	public class FunctionRunner
	{
		public static double Run(string function, double input)
		{
			function = function.ToLower();

			switch (function)
			{
				case "abs":
					return Math.Abs(input);
				case "atn":
					return Math.Atan(input);
				case "cos":
					return Math.Cos(input);
				case "exp":
					return Math.Exp(input);
				case "int":
					return (int) Math.Floor(input);
				case "fix":
					return Math.Sign(input) * (int) Math.Abs(input);
				default:
					throw new ArgumentException($"{function} is not recognised");
			}
		}
	}
}
