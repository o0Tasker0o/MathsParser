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
			const float input = -1.0f;

			Assert.AreEqual(Math.Abs(input), FunctionRunner.Run(function, input));
		}

		[TestCase("atn")]
		[TestCase("Atn")]
		public void RunHandlesArcTangent(string function)
		{
			const float input = -1.0f;

			Assert.AreEqual((float) Math.Atan(input), FunctionRunner.Run(function, input));
		}

		[TestCase("cos")]
		[TestCase("Cos")]
		public void RunHandlesCosine(string function)
		{
			const float input = -1.0f;

			Assert.AreEqual((float)Math.Cos(input), FunctionRunner.Run(function, input));
		}

		[TestCase("exp")]
		[TestCase("Exp")]
		public void RunHandlesExponent(string function)
		{
			const float input = -1.0f;

			Assert.AreEqual((float) Math.Exp(input), FunctionRunner.Run(function, input));
		}

		[Test]
		public void RunThrowsExceptionForUnknownFunction()
		{
			Assert.Throws<ArgumentException>(() => FunctionRunner.Run("NotAFunction", 1.0f));
		}
	}
}
