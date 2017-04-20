using System;
using System.Linq;
using MathsParser;
using NUnit.Framework;

namespace MathsParserTests
{
	[TestFixture]
	public class BracketTokeniserTests
	{
		[Test]
		public void TokeniseReturnsSingleTokenForUnbracketedExpression()
		{
			var tokens = BracketTokeniser.Tokenise("abc").ToList();
			Assert.AreEqual(1, tokens.Count);
			Assert.IsFalse(tokens[0].IsBracketed);
			Assert.AreEqual("abc", tokens[0].Value);
		}

		[Test]
		public void TokeniseReturnsTokenForEachElement()
		{
			var tokens = BracketTokeniser.Tokenise("(ab)cd(ef)").ToList();
			Assert.AreEqual(3, tokens.Count);

			Assert.IsTrue(tokens[0].IsBracketed);
			Assert.AreEqual("ab", tokens[0].Value);

			Assert.IsFalse(tokens[1].IsBracketed);
			Assert.AreEqual("cd", tokens[1].Value);

			Assert.IsTrue(tokens[2].IsBracketed);
			Assert.AreEqual("ef", tokens[2].Value);
		}

		[Test]
		public void TokeniseHandlesOneLevelOfBrackets()
		{
			var tokens = BracketTokeniser.Tokenise("(a(b))cd(ef)").ToList();
			Assert.AreEqual(3, tokens.Count);

			Assert.IsTrue(tokens[0].IsBracketed);
			Assert.AreEqual("a(b)", tokens[0].Value);

			Assert.IsFalse(tokens[1].IsBracketed);
			Assert.AreEqual("cd", tokens[1].Value);

			Assert.IsTrue(tokens[2].IsBracketed);
			Assert.AreEqual("ef", tokens[2].Value);
		}

		[Test]
		public void TokeniseThrowsExceptionOnMismatchedBrackets()
		{
			Assert.Throws<ArgumentException>(() => BracketTokeniser.Tokenise("(a"));
		}
	}
}
