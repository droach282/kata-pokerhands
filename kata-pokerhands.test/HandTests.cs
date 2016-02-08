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

		[TestCase("TS 5H 3C 4D 6S", HandRank.HighCard)]
		[TestCase("3D 3H 5C 4D 6S", HandRank.Pair)]
		[TestCase("3H 4H 3S 4S 5D", HandRank.TwoPair)]
		[TestCase("4H 4D 4C 6S 7S", HandRank.ThreeOfAKind)]
		[TestCase("2H 3S 4D 5C 6S", HandRank.Straight)]
		[TestCase("2H 4H 6H 8H TH", HandRank.Flush)]
		[TestCase("2H 2C 2S 3H 3D", HandRank.FullHouse)]
		[TestCase("2H 2C 2D 2S 3D", HandRank.FourOfAKind)]
		[TestCase("2H 3H 4H 5H 6H", HandRank.StraightFlush)]
		public void TestHandRanking(string input, HandRank expected)
		{
			var hand = new Hand(input);
			Assert.AreEqual(expected, hand.Rank);
		}
	}
}