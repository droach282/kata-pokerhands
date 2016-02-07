using System;
using System.Collections.Generic;
using System.Linq;

namespace kata_pokerhands
{
	public enum Suit
	{
		Clubs,
		Hearts,
		Diamonds,
		Spades
	}

	public enum Value
	{
		Two = 0,
		Three = 1,
		Four = 2,
		Five = 3,
		Six = 4,
		Seven = 5,
		Eight = 6,
		Nine = 7,
		Ten = 8,
		Jack = 9,
		Queen = 10,
		King = 11,
		Ace = 12
	}

	public class Card : IComparable<Card>
	{
		private static readonly Dictionary<char, Value> ValueMapping = new Dictionary<char, Value>
		{
			{'2', Value.Two},
			{'3', Value.Three},
			{'4', Value.Four},
			{'5', Value.Five},
			{'6', Value.Six},
			{'7', Value.Seven},
			{'8', Value.Eight},
			{'9', Value.Nine},
			{'T', Value.Ten},
			{'J', Value.Jack},
			{'Q', Value.Queen},
			{'K', Value.King}
		};

		private static readonly Dictionary<char, Suit> SuitMapping = new Dictionary<char, Suit>
		{
			{'H', Suit.Hearts},
			{'S', Suit.Spades},
			{'D', Suit.Diamonds},
			{'C', Suit.Clubs}
		};

		public Card(string card)
		{
			Suit = ParseSuit(card[1]);
			Value = ParseValue(card[0]);
		}

		public Suit Suit { get; set; }
		public Value Value { get; set; }

		public int CompareTo(Card other)
		{
			// if this card has a lower value than the other card, put it first.
			return Value - other.Value;
		}

		private Value ParseValue(char value)
		{
			return ValueMapping[value];
		}

		private Suit ParseSuit(char suit)
		{
			return SuitMapping[suit];
		}

		public override string ToString()
		{
			return $"{ValueMapping.Single(x => x.Value == Value).Key}{SuitMapping.Single(x => x.Value == Suit).Key}";
		}
	}
}