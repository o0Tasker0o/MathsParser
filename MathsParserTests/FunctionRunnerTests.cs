using System;
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

		[Test]
		public void RunThrowsExceptionForUnknownFunction()
		{
			Assert.Throws<ArgumentException>(() => FunctionRunner.Run("NotAFunction", 1.0f));
		}
	}
}
