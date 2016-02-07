using System;
using NUnit.Framework;

namespace kata_pokerhands.test
{
	[TestFixture]
	public class HandTests
	{
		[TestCase("2H")]
		[TestCase("2H 3H 4H")]
		[TestCase("2H 3H 4H 5H 6H 7H")]
		public void TestHandCtorThrowsWhenNotFiveCards(string badhand)
		{
			// ReSharper disable once ObjectCreationAsStatement
			Assert.Throws<ArgumentOutOfRangeException>(() => new Hand(badhand));
		}

		[TestCase("5H 3C 6D 2S 4C", "2S 3C 4C 5H 6D")]
		public void TestCtorSortsHand(string input, string expected)
		{
			var hand = new Hand(input);
			Assert.AreEqual(expected, hand.ToString());
		}
	}
}