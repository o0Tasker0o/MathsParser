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

		[Test]
		public void RunThrowsExceptionForUnknownFunction()
		{
			Assert.Throws<ArgumentException>(() => FunctionRunner.Run("NotAFunction", 1.0));
		}
	}
}
