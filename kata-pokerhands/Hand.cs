using System;
using System.Collections.Generic;
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

	public class Hand : IComparable<Hand>
	{
		public Card[] Cards { get; }
		private IEnumerable<IGrouping<Value, Card>> _groupedCards;

		public IEnumerable<IGrouping<Value, Card>> GroupedCards
		{
			get { return _groupedCards ?? (_groupedCards = Cards.GroupBy(x => x.Value)); }
		}

		public IEnumerable<Card> GetGroupOf(int count)
		{
			return GroupedCards.First(x => x.Count() == count);
		} 

		public HandRank Rank { get; private set; }

		public Hand(string hand)
		{
			var cards = hand.Split(' ');
			if (cards.Length != 5)
				throw new ArgumentOutOfRangeException(nameof(cards), "hand must contain 5 cards");

			Cards = cards.Select(x => new Card(x)).OrderBy(x => x).ToArray();

			RankHand();
		}

		public int CompareTo(Hand other)
		{
			if (Rank != other.Rank)
				return Rank - other.Rank;

			// now things get interesting.
			switch (Rank)
			{
				case HandRank.Flush:
				case HandRank.HighCard:
				case HandRank.Straight:
				case HandRank.StraightFlush:
					return Cards.Max().Value - other.Cards.Max().Value;

				case HandRank.FourOfAKind:
					// can't have a tie of hand cards (unless somebody is cheating)
					var ourFour = GetGroupOf(4).First().Value;
					var theirFour = other.GetGroupOf(4).First().Value;
					return ourFour - theirFour;

				case HandRank.FullHouse:
				case HandRank.ThreeOfAKind:
					var ourThree= GetGroupOf(3).First().Value;
					var theirThree = other.GetGroupOf(3).First().Value;
					return ourThree - theirThree;
				// higher group of three

				case HandRank.Pair:
					var ourPair = GetGroupOf(2).First().Value;
					var theirPair = other.GetGroupOf(2).First().Value;
					if (ourPair != theirPair)
						return ourPair - theirPair;

					return Cards.Max().Value - other.Cards.Max().Value;

				case HandRank.TwoPair:
					var ourPairs = Cards.GroupBy(x => x.Value).Where(x => x.Count() == 2).OrderByDescending(x => x.First().Value);
					var theirPairs = other.Cards.GroupBy(x => x.Value).Where(x => x.Count() == 2).OrderByDescending(x => x.First().Value);

					if (ourPairs.First().First().Value != theirPairs.First().First().Value)
						return ourPairs.First().First().Value - theirPairs.First().First().Value;

					if (ourPairs.Last().First().Value != theirPairs.Last().First().Value)
						return ourPairs.Last().First().Value - theirPairs.Last().First().Value;

					return Cards.Max().Value - other.Cards.Max().Value;

				default:
					return 0;
			}
		}

		public override string ToString()
		{
			// ReSharper disable once CoVariantArrayConversion
			return string.Join(" ", (object[])Cards);
		}

		private void RankHand()
		{	
			var isFlush = Cards.Select(x => x.Suit).Distinct().Count() == 1;

			var distinctCards = Cards.Select(x => x.Value).Distinct().ToArray();
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
				switch (distinctCards.Length)
				{
					case 4:
						Rank = HandRank.Pair;
						break;
					case 3:
						Rank = GroupedCards.Any(x => x.Count() == 3) ? HandRank.ThreeOfAKind : HandRank.TwoPair;
						break;
					case 2:
						Rank = GroupedCards.Any(x => x.Count() == 4) ? HandRank.FourOfAKind : HandRank.FullHouse;
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
