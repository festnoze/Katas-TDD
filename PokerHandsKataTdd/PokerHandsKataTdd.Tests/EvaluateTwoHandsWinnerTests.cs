using FluentAssertions;
using PokerHandsKataTdd.Application.Models;
using PokerHandsKataTdd.Application.Models.Enums;

namespace PokerHandsKataTdd.Tests;

public class EvaluateTwoHandsWinnerTests
{
    [Theory]
    [InlineData("2S 3S 4S 2D 3D 7S 5S", "2S TS JS KD QC 9D 5C", "Win")] // Flush Seven vs. Straight King
    [InlineData("T♣ T♠ T♥ J♠ Q♠ 8♠ 3♠", "9H 8H 4H KD 7D 5H 2H", "Win")] // Flush Queen vs. Flush Nine (with differents colors convention: symbol and letter)
    [InlineData("2C 2D 9D 3D JD 8D AH", "2S TS JS KD QC AD 5C", "Win")] // Flush Jack vs. Straight Ace
    [InlineData("2S TS JS KD QC AD 5C", "2C 2D 9D 3D JD 8D AH", "Lose")] // Straight Ace vs. Ace Flush Jack (inversed)
    [InlineData("AH 3S 4C 2S 7S 5S 8S", "9S KS 4S KD 7D 9C KH", "Lose")] // Flush Eight vs. FullHouse King Nine
    public void CompareHandsRanking_WithFlush_Test(string handCards1, string handCards2, string awaitedHandRankingString)
    {
        CompareHandsRankingTest(handCards1, handCards2, Enum.Parse<HandRankingComparaisonEnum>(awaitedHandRankingString));
    }

    [Theory]
    [InlineData("2S 3S 4S 2D QD 2C 2H", "2S 3S 4S 2D 3D 7S 5S", "Win")] // FourOfAKind Two vs. Flush Seven
    [InlineData("3C 3S 4S 2D 3D 6C 3H", "3C 3S 4S 2D 9D 6C 3H", "Win")] // FourOfAKind Three vs. ThreeOfAKind Three
    [InlineData("TS 3S 5S TD 5D TC TH", "AS 3S 4S AD 3D AC 5H", "Win")] // FourOfAKind Ten vs. FullHouse Ace Three
    public void CompareHandsRanking_WithFourOfAKind_Test(string handCards1, string handCards2, string awaitedHandRankingString)
    {
        CompareHandsRankingTest(handCards1, handCards2, Enum.Parse<HandRankingComparaisonEnum>(awaitedHandRankingString));
    }

    [Theory]
    [InlineData("2S TD JD KD QD 9D 5C", "2S TS JS KS QS AS 5C", "Lose")] // StraightFlush King vs. StraightFlush Ace
    [InlineData("2S TD JD KD QD AD 5C", "6C TC TH JC AC 8C 3C", "Win")] // StraightFlush King vs. Flush ACE
    [InlineData("2S TD JD KD QD AD 5C", "TS 3S 5S TD 5D TC TH", "Win")] // StraightFlush King vs. FourOfAKind Ten
    [InlineData("2S TD JD KD QD AD 5C", "3S TS JS KS QS AS 6C", "Tie")] // StraightFlush Ace vs. StraightFlush ACE
    [InlineData("AS 2S 3S 4S 5S 7D 7C", "2S 3S 4S 5S 6S 7C TC", "Lose")] // wheel straight flush with low Ace vs. 6-high straight flush 
    public void CompareHandsRanking_WithStraightFlush_Test(string handCards1, string handCards2, string awaitedHandRankingString)
    {
        CompareHandsRankingTest(handCards1, handCards2, Enum.Parse<HandRankingComparaisonEnum>(awaitedHandRankingString));
    }

    private static void CompareHandsRankingTest(string handCards1, string handCards2, HandRankingComparaisonEnum awaitedResult)
    {
        /// Arrange
        var hand1 = new Hand(handCards1);
        var hand2 = new Hand(handCards2);

        /// Act
        var result = hand1.ResultVersusHand(hand2);

        /// Assert
        result.Should().Be(awaitedResult);
    }
}