using System;
using System.Linq;

namespace kata_pokerhands
{
	public class Hand
	{
		private readonly Card[] _cards;

		public Hand(string hand)
		{
			var cards = hand.Split(' ');
			if (cards.Length != 5)
				throw new ArgumentOutOfRangeException(nameof(cards), "hand must contain 5 cards");

			_cards = cards.Select(x => new Card(x)).OrderBy(x => x).ToArray();
		}

		public override string ToString()
		{
			// ReSharper disable once CoVariantArrayConversion
			return string.Join(" ", (object[])_cards);
		}
	}
}
