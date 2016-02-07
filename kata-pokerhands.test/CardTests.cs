using NUnit.Framework;

namespace kata_pokerhands.test
{
	[TestFixture]
	public class CardTests
	{
		[TestCase("2H", Value.Two, Suit.Hearts)]
		[TestCase("TC", Value.Ten, Suit.Clubs)]
		public void TestCardCtor(string input, Value value, Suit suit)
		{
			var card = new Card(input);
			Assert.AreEqual(value, card.Value);
			Assert.AreEqual(suit, card.Suit);
		}

		[TestCase("3C", "2H", 1)]
		[TestCase("2S", "3D", -1)]
		[TestCase("3H", "3C", 0)]
		public void TestCardSort(string first, string second, int expectedDifference)
		{
			var firstCard = new Card(first);
			var secondCard = new Card(second);

			var result = firstCard.CompareTo(secondCard);

			Assert.AreEqual(expectedDifference, result);
		}

		[Test]
		public void TestToString()
		{
			var cardString = "2H";
			var card = new Card(cardString);

			Assert.AreEqual(cardString, card.ToString());
		}
	}
}
