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
		public void ParseThrowsExceptionForNonNumericInputs()
		{
			Assert.Throws<ArgumentException>(() => _parser.Parse("Abs(THIS IS NOT A NUMBER)"));
		}

		[TestCase("atn")]
		[TestCase("cos")]
		[TestCase("exp")]
		[TestCase("int")]
		[TestCase("fix")]
		public void ParseReturnsFunctions(string function)
		{
			const double input = 1.23;
			Assert.AreEqual(FunctionRunner.Run(function, input), _parser.Parse($"{function}({input})"), 0.000001);
		}

		[Test]
		public void ParseAddsNumbers()
		{
			Assert.AreEqual(1+2+3, _parser.Parse("1+2+3"));
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

		[Test]
		public void ParseMultipliesNumbers()
		{
			Assert.AreEqual(2*3, _parser.Parse("2*3"));
		}

		[Test]
		public void ParseDividesNumbers()
		{
			Assert.AreEqual(2.0 / 3.0, _parser.Parse("2/3"));
		}

		[Test]
		public void ParseHandlesNegativeNumbers()
		{
			Assert.AreEqual(-3, _parser.Parse("-3"));
		}

		[Test]
		public void ParseSubtractsNumbers()
		{
			Assert.AreEqual(2-3, _parser.Parse("2-3"));
		}

		[Test]
		public void ParseHandlesOperatorPrecedence()
		{
			Assert.AreEqual(1+2*3+4/5.0-6, _parser.Parse("1+2*3+4/5-6"));
		}

		[Test]
		public void ParseThrowsExceptionOnMismatchedBrackets()
		{
			Assert.Throws<ArgumentException>(() => _parser.Parse("Abs((2"));
		}
	}
}
