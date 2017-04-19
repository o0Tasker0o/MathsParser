using System;
using MathsParser;
using NUnit.Framework;

namespace MathsParserTests
{
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
			Assert.AreEqual(1.0f, _parser.Parse($"Abs({input})"));
		}

		[Test]
		public void ParseThrowsExceptionForNonNumericInputs()
		{
			Assert.Throws<ArgumentException>(() => _parser.Parse("Abs(THIS IS NOT A NUMBER)"));
		}

		[Test]
		public void ParseAddsNumbers()
		{
			Assert.AreEqual(6.0f, _parser.Parse("1+2+3"));
		}

		[Test]
		public void ParseReplacesFunctionsThenSolvesAddition()
		{
			Assert.AreEqual(6.0f, _parser.Parse("1+2+Abs(-3)"));
		}

		[Test]
		public void ParseIgnoresWhitespace()
		{
			Assert.AreEqual(6.0f, _parser.Parse("1\n+\r2+\tAbs( -3   )"));
		}
	}
}
