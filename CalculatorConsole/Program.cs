using System;
using MathsParser;

namespace CalculatorConsole
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var parser = new Parser();

			while (true)
			{
				Console.WriteLine("Please enter an expression to calculate:");
				var input = Console.ReadLine();

				if (string.IsNullOrEmpty(input))
				{
					break;
				}

				Console.WriteLine($"{input} = {parser.Parse(input)}");
				Console.WriteLine();
			}
		}
	}
}
