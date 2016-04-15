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

		[TestCase("2H 4H 3C 7S 9S", "5C 5S 6H 7C 8S", -1)] // Pair beats high card
		[TestCase("2H 2D 3C 3S 9S", "5C 5S 6H 7C 8S", 1)]  // Two pair beats pair
		[TestCase("2H 2D 3C 3S 9S", "5C 5S 5H 7C 8S", -1)] // 3 of a kind beats 2 pair
		[TestCase("2H 3C 4H 5D 6S", "5C 5S 5H 7C 8S", 1)]  // Straight beats 3 of a kind
		[TestCase("2H 3C 4H 5D 6S", "2C 5C 6C 7C 8C", -1)] // Flush beats straight
		[TestCase("2H 2D 2S 5D 5S", "2C 5C 6C 7C 8C", 1)]  // Full house beats flush
		[TestCase("2H 2D 2S 5D 5S", "6D 6H 6C 6S 8C", -1)] // Four of a kind beats full house
		[TestCase("2H 3H 4H 5H 6H", "7D 7H 7C 7S 8C", 1)]  // Straight flush beats four of a kind
		[TestCase("2H 4H 3C 7S 9S", "2D 4D 3S 7C TS", -1)] // High card vs. High Card
        [TestCase("2H 2C 2S 2D 3H", "4H 4D 4S 4C 3S", -2)] // Four of a kind vs. Four of a kind
		public void TestHandComparison(string hand1, string hand2, int expectedDiff)
		{
			var handA = new Hand(hand1);
			var handB = new Hand(hand2);
			Assert.AreEqual(handA.CompareTo(handB), expectedDiff);
		}
	}
}
