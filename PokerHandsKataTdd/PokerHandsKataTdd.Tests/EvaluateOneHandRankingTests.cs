using FluentAssertions;
using PokerHandsKataTdd.Application.Models;

namespace PokerHandsKataTdd.Tests;

public class EvaluateOneHandRankingTests
{
    [Theory]
    [InlineData("2S 3S 4S 9D 8D 7C 5H", "HighCard Nine")]
    [InlineData("6S 9S 2S 7D 5D TC 3H", "HighCard Ten")]
    [InlineData("TS 3S 4S 7D 8D 6C 2H", "HighCard Ten")]
    [InlineData("2S 3S 4S 6D 7D 9C KH", "HighCard King")]
    [InlineData("TS 3S 4S AD 5D 6C 9H", "HighCard Ace")]
    public void CalculateHandRanking_HighCard_Test(string handCards, string awaitedHandRankingString)
    {
        CalculateHandRankingTest(handCards, awaitedHandRankingString);
    }

    [Theory]
    [InlineData("2S 3S 4S 2D 5D 7C 8H", "Pair Two")]
    [InlineData("2S 3S 4S 8D 3D 6C 7H", "Pair Three")]
    [InlineData("TS 3S 4S 2D JD KC 4H", "Pair Four")]
    [InlineData("TS 3S 4S TD 5D 6C 9H", "Pair Ten")]
    [InlineData("2S KS 4S 6D 7D 9C KH", "Pair King")]
    [InlineData("TS 3S 4S 9D AD 6C AH", "Pair Ace")]
    public void CalculateHandRanking_OnePair_Test(string handCards, string awaitedHandRankingString)
    {
        CalculateHandRankingTest(handCards, awaitedHandRankingString);
    }

    [Theory]
    [InlineData("2S 3S 4S 2D 3D 9C 5H", "TwoPairs Three Two")]
    [InlineData("2S 3S 4S 5D 3D 7C 5H", "TwoPairs Five Three")]
    [InlineData("TS 3S 4S TD 5D 6C 4H", "TwoPairs Ten Four")]
    [InlineData("TS 3S 4S TD 5D AC AH", "TwoPairs Ace Ten")]
    [InlineData("2S KS 4S 2D 7D 9C KH", "TwoPairs King Two")]
    [InlineData("KS KD 4S AD 3D 6C AH", "TwoPairs Ace King")]
    [InlineData("2S 3S 4S 2D 3D 4C 5H", "TwoPairs Four Three")] // 3 pairs case: should take the 2 highest pairs
    public void CalculateHandRanking_TwoPair_Test(string handCards, string awaitedHandRankingString)
    {
        CalculateHandRankingTest(handCards, awaitedHandRankingString);
    }

    [Theory]
    [InlineData("2S 3S 4S 2D QD 2C 5H", "ThreeOfAKind Two")]
    [InlineData("AS 3S 4S 2D 3D 6C 3H", "ThreeOfAKind Three")]
    [InlineData("TS 3S 4S 4D 5D 6C 4H", "ThreeOfAKind Four")]
    [InlineData("TS 3S 4S TD 5D TC AH", "ThreeOfAKind Ten")]
    [InlineData("2S KS 4S KD 7D 9C KH", "ThreeOfAKind King")]
    public void CalculateHandRanking_ThreeOfAKind_Test(string handCards, string awaitedHandRankingString)
    {
        CalculateHandRankingTest(handCards, awaitedHandRankingString);
    }

    [Theory]
    [InlineData("2S TS JS KD QC 9D 5C", "Straight King")]
    [InlineData("2S TS JS KD QC AD 5C", "Straight Ace")]
    [InlineData("2S 3D 4S 5C 6S 7S 3C", "Straight Seven")] // straight with 6 cards: should take higher flush card
    [InlineData("AS 2C 3D 4H 5S 7D 7C", "Straight Five")] // wheel straight with low Ace
    public void CalculateHandRanking_Straight_Test(string handCards, string awaitedHandRankingString)
    {
        CalculateHandRankingTest(handCards, awaitedHandRankingString);
    }

    [Theory]
    [InlineData("2S 3S 4S 2D 3D 7S 5S", "Flush Seven")]
    [InlineData("9H 8H 4H KD 7D 5H 2H", "Flush Nine")]
    [InlineData("T♣ T♠ T♥ J♠ Q♠ 8♠ 3♠", "Flush Queen")]
    [InlineData("2C 2D 9D 3D JD 8D AH", "Flush Jack")]
    [InlineData("AH 3S 4C 2S 7S 5S 8S", "Flush Eight")] // straight and flush not overlaping (straight + flush != straight-flush)
    public void CalculateHandRanking_Flush_Test(string handCards, string awaitedHandRankingString)
    {
        CalculateHandRankingTest(handCards, awaitedHandRankingString);
    }

    [Theory]
    [InlineData("2S 3S 4S 2D 3D 2C 5H", "FullHouse Two Three")]
    [InlineData("AS 3S 4S 2D 3D AC 3H", "FullHouse Three Ace")]
    [InlineData("9S KS 4S KD 7D 9C KH", "FullHouse King Nine")]
    [InlineData("TC TS TH AS AD AH 3D", "FullHouse Ace Ten")] // 2 sets case: should take the highest full
    [InlineData("2C 2S 3D 3S AS AD AH", "FullHouse Ace Three")] // set + 2 pairs case: should take the highest full
    public void CalculateHandRanking_FullHouse_Test(string handCards, string awaitedHandRankingString)
    {
        CalculateHandRankingTest(handCards, awaitedHandRankingString);
    }

    [Theory]
    [InlineData("2S 3S 4S 2D QD 2C 2H", "FourOfAKind Two")]
    [InlineData("3C 3S 4S 2D 3D 6C 3H", "FourOfAKind Three")]
    [InlineData("TS 3S 5S TD 5D TC TH", "FourOfAKind Ten")]
    public void CalculateHandRanking_FourOfAKind_Test(string handCards, string awaitedHandRankingString)
    {
        CalculateHandRankingTest(handCards, awaitedHandRankingString);
    }

    [Theory]
    [InlineData("2S TD JD KD QD 9D 5C", "StraightFlush King")]
    [InlineData("2S TD JD KD QD AD 5C", "StraightFlush Ace")]
    [InlineData("2S 3D 4S 5S 6S 7S 3S", "StraightFlush Seven")] // straightflush with 6 cards: should take higher straightflush card
    [InlineData("AS 2S 3S 4S 5S 7D 7C", "StraightFlush Five")] // wheel straight flush with low Ace
    public void CalculateHandRanking_StraightFlush_Test(string handCards, string awaitedHandRankingString)
    {
        CalculateHandRankingTest(handCards, awaitedHandRankingString);
    }

    private static void CalculateHandRankingTest(string handCards, string awaitedHandRankingString)
    {
        /// Arrange
        var hand = new Hand(handCards);

        /// Act
        var result = hand.EvaluateRanking();

        /// Assert
        result.Should().Be(new HandRanking(awaitedHandRankingString));
    }
}