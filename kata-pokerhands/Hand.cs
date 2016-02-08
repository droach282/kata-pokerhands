using System;
using System.Linq;

namespace kata_pokerhands
{
	public enum HandRank
	{
		HighCard = 0,
		Pair = 1,
		TwoPair = 2,
		ThreeOfAKind = 3,
		Straight = 4,
		Flush = 5,
		FullHouse = 6,
		FourOfAKind = 7,
		StraightFlush = 8//,
		//RoyalFlush = 9 // eh. Really this is a straight flush with a Ace high card.
	}

	public class Hand
	{
		private readonly Card[] _cards;

		public HandRank Rank { get; private set; }

		public Hand(string hand)
		{
			var cards = hand.Split(' ');
			if (cards.Length != 5)
				throw new ArgumentOutOfRangeException(nameof(cards), "hand must contain 5 cards");

			_cards = cards.Select(x => new Card(x)).OrderBy(x => x).ToArray();

			RankHand();
		}

		public override string ToString()
		{
			// ReSharper disable once CoVariantArrayConversion
			return string.Join(" ", (object[])_cards);
		}

		private void RankHand()
		{	
			var isFlush = _cards.Select(x => x.Suit).Distinct().Count() == 1;

			var distinctCards = _cards.Select(x => x.Value).Distinct().ToArray();
			var isStraight = distinctCards.Count() == 5 && distinctCards.Max() - distinctCards.Min() == 4;

			if (isStraight && isFlush)
			{
				Rank = HandRank.StraightFlush;
			}
			else if (isStraight)
			{
				Rank = HandRank.Straight;
			}
			else if (isFlush)
			{
				Rank = HandRank.Flush;
			}
			else if (distinctCards.Length < 5)
			{
				var groupedValues = _cards.GroupBy(x => x.Value).ToList();

				switch (distinctCards.Length)
				{
					case 4:
						Rank = HandRank.Pair;
						break;
					case 3:
						Rank = groupedValues.Any(x => x.Count() == 3) ? HandRank.ThreeOfAKind : HandRank.TwoPair;
						break;
					case 2:
						Rank = groupedValues.Any(x => x.Count() == 4) ? HandRank.FourOfAKind : HandRank.FullHouse;
						break;
				}
			}
			else
			{
				// High card (sad trombone)
				Rank = HandRank.HighCard;
			}
		}
	}
}
