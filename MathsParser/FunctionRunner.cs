using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MathsParser
{
	public class FunctionRunner
	{
		public static ReadOnlyCollection<string> AvailableFunctions => new ReadOnlyCollection<string>(new List<string>
		{
			"abs",
			"atn",
			"cos",
			"exp",
			"int",
			"fix",
			"log"
		});

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
				case "log":
					return Math.Log(input);
				default:
					throw new ArgumentException($"{function} is not recognised");
			}
		}
	}
}
