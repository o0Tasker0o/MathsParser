using System;
using MathsParser;
using NUnit.Framework;

namespace MathsParserTests
{
	//TODO investigate rounding errors in these tests
	[TestFixture]
	public class ParserTests
	{
		private Parser _parser;

		[SetUp]
		public void Setup()
		{
			_parser = new Parser();
		}

		[TestCase("1")]
		[TestCase("1.0")]
		[TestCase("-1")]
		[TestCase("-1.0")]
		public void ParseReturnsAbsoluteValueOfInput(string input)
		{
			Assert.AreEqual(1.0, _parser.Parse($"Abs({input})"));
		}

		[Test]
		public void ParseReturnsArctangentOfInput()
		{
			const double input = 1.0;
			Assert.AreEqual(FunctionRunner.Run("atn", input), _parser.Parse($"Atn({input})"), 0.000001);
		}

		[Test]
		public void ParseReturnsCosineOfInput()
		{
			const double input = 1.0;
			Assert.AreEqual(FunctionRunner.Run("cos", input), _parser.Parse($"Cos({input})"), 0.000001);
		}

		[Test]
		public void ParseReturnsExponentialOfInput()
		{
			const double input = 1.0;

			Assert.AreEqual(FunctionRunner.Run("exp", input), _parser.Parse($"exp({input})"), 0.000001);
		}

		[Test]
		public void ParseThrowsExceptionForNonNumericInputs()
		{
			Assert.Throws<ArgumentException>(() => _parser.Parse("Abs(THIS IS NOT A NUMBER)"));
		}

		[Test]
		public void ParseAddsNumbers()
		{
			Assert.AreEqual(6.0, _parser.Parse("1+2+3"));
		}

		[Test]
		public void ParseReplacesFunctionsThenSolvesAddition()
		{
			Assert.AreEqual(6.0, _parser.Parse("1+2+Abs(-3)"));
		}

		[Test]
		public void ParseIgnoresWhitespace()
		{
			Assert.AreEqual(6.0, _parser.Parse("1\n+\r2+\tAbs( -3   )"));
		}

		[Test]
		public void ParseRecursivelySolves()
		{
			Assert.AreEqual(6.0, _parser.Parse("1+2+Abs(Abs(Abs(1)) + Abs(2))"));
		}
	}
}
