﻿using System;
using MathsParser;
using NUnit.Framework;

namespace MathsParserTests
{
	[TestFixture]
	public class FunctionRunnerTests
	{
		[TestCase("abs")]
		[TestCase("Abs")]
		public void RunHandlesAbsolute(string function)
		{
			const double input = -1.0;

			Assert.AreEqual(Math.Abs(input), FunctionRunner.Run(function, input));
		}

		[TestCase("atn")]
		[TestCase("Atn")]
		public void RunHandlesArcTangent(string function)
		{
			const double input = -1.0;

			Assert.AreEqual(Math.Atan(input), FunctionRunner.Run(function, input));
		}

		[TestCase("cos")]
		[TestCase("Cos")]
		public void RunHandlesCosine(string function)
		{
			const double input = -1.0;

			Assert.AreEqual(Math.Cos(input), FunctionRunner.Run(function, input));
		}

		[TestCase("exp")]
		[TestCase("Exp")]
		public void RunHandlesExponent(string function)
		{
			const double input = -1.0;

			Assert.AreEqual(Math.Exp(input), FunctionRunner.Run(function, input));
		}

		[TestCase("int", -2.4, -3)]
		[TestCase("Int", 2.4, 2)]
		public void RunHandlesInt(string function, double input, int result)
		{
			Assert.AreEqual(result, FunctionRunner.Run(function, input));
		}

		[TestCase("fix", -2.4, -2)]
		[TestCase("Fix", 2.4, 2)]
		public void RunHandlesFix(string function, double input, int result)
		{
			Assert.AreEqual(result, FunctionRunner.Run(function, input));
		}

		[TestCase("log")]
		[TestCase("Log")]
		public void RunHandlesLog(string function)
		{
			const double input = -1.0;

			Assert.AreEqual(Math.Log(input), FunctionRunner.Run(function, input));
		}

		[TestCase("rnd")]
		[TestCase("Rnd")]
		public void RunHandlesRnd(string function)
		{
			var random = FunctionRunner.Run(function, 1);

			Assert.GreaterOrEqual(random, 0);
			Assert.Less(random, 1);
		}

		[TestCase("sgn", -5, -1)]
		[TestCase("Sgn", 5, 1)]
		public void RunHandlesSgn(string function, double input, int expectedSign)
		{
			Assert.AreEqual(expectedSign, FunctionRunner.Run(function, input));
		}

		[TestCase("sin")]
		[TestCase("Sin")]
		public void RunHandlesSin(string function)
		{
			const double input = -1.0;

			Assert.AreEqual(Math.Sin(input), FunctionRunner.Run(function, input));
		}

		[TestCase("sqr")]
		[TestCase("Sqr")]
		public void RunHandlesSqr(string function)
		{
			const double input = -1.0;

			Assert.AreEqual(Math.Sqrt(input), FunctionRunner.Run(function, input));
		}

		[TestCase("tan")]
		[TestCase("Tan")]
		public void RunHandlesTan(string function)
		{
			const double input = -1.0;

			Assert.AreEqual(Math.Tan(input), FunctionRunner.Run(function, input));
		}

		[Test]
		public void RunThrowsExceptionForUnknownFunction()
		{
			Assert.Throws<ArgumentException>(() => FunctionRunner.Run("NotAFunction", 1.0));
		}
	}
}
