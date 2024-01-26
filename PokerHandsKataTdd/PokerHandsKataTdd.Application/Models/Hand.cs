using PokerHandsKataTdd.Application.Exceptions;
using PokerHandsKataTdd.Application.Models.Enums;
using System.Collections.ObjectModel;
using System.Drawing;
namespace PokerHandsKataTdd.Application.Models;

public class Hand
{
    private List<Card> cards { get; }
    public ReadOnlyCollection<Card> Cards => cards.AsReadOnly();

    public Hand(string handCardsString)
    {
        var cardsStrings = handCardsString.Split(' ');

        // A valid hand should contains 7 cards
        if (cardsStrings.Length != 7)
            throw new WrongCardsCountException();

        var createdCards = Card.CreateMany(cardsStrings).ToList();

        if (createdCards.GroupBy(card => card).Any(gr => gr.Count() > 1))
            throw new DuplicateCardInHandException();

        cards = createdCards;
    }

    public HandRanking EvaluateRanking()
    {
        var groupedByValueCards = cards.GroupBy(c => c.cardValue.Value).ToList();
        var groupedByColorCards = cards.GroupBy(c => c.cardColor.Color).ToList();
        var setsCount = groupedByValueCards.Count(gr => gr.Count() == 3);
        var hasThreeOfAKind = setsCount > 0;
        var hasFourOfAKind = groupedByValueCards.Any(gr => gr.Count() == 4);
        var pairsCount = groupedByValueCards.Count(gr => gr.Count() == 2);
        var hasPairs = pairsCount > 0;
        var hasSinglePair = pairsCount == 1;
        var hasTwoPairs = pairsCount >= 2;
        var hasFullHouse = (hasThreeOfAKind && hasPairs) || setsCount == 2;
        var hasFlush = groupedByColorCards.Any(gr => gr.Count() >= 5);
        var hasStraight = GetHigherValueAndCardsCountOfLongestConsecutiveValues(cards.Select(c => c.cardValue.Value).ToList()).CardsCount >= 5;
        var color = groupedByColorCards.Where(gr => gr.Count() >= 5).Select(gr => gr.Key).FirstOrDefault();
        var coloredCardsValues = cards.Where(c => c.cardColor.Color == color).Select(c => c.cardValue.Value).ToList();
        var longestConsecutiveValuesOfColor = GetHigherValueAndCardsCountOfLongestConsecutiveValues(coloredCardsValues);
        var hasStraightFlush = longestConsecutiveValuesOfColor.CardsCount >= 5;

        if (hasStraightFlush)
        {
            return new HandRanking(HandRankingTypeEnum.StraightFlush, (CardValueEnum)longestConsecutiveValuesOfColor.HighestValue);
        }

        if (hasFourOfAKind) 
        {
            var fourOfAKindValue = groupedByValueCards.Where(gr => gr.Count() == 4).Select(gr => gr.Key).First();
            return new HandRanking(HandRankingTypeEnum.FourOfAKind, fourOfAKindValue);
        }
        
        if (hasFullHouse)
        {
            var highestSetValue = groupedByValueCards.Where(gr => gr.Count() == 3).OrderByDescending(gr => gr.Key).Select(gr => gr.Key).First();
            var highestPairValue = groupedByValueCards.Where(gr => gr.Count() == 2).OrderByDescending(gr => gr.Key).Select(gr => gr.Key).FirstOrDefault();
            
            // Case of 2 sets are identified
            if (!hasPairs)
                highestPairValue = groupedByValueCards.Where(gr => gr.Count() == 3).OrderByDescending(gr => gr.Key).Select(gr => gr.Key).Skip(1).First();
            
            return new HandRanking(HandRankingTypeEnum.FullHouse, highestSetValue, highestPairValue);
        }

        if (hasFlush)
        {
            return new HandRanking(HandRankingTypeEnum.Flush, coloredCardsValues.Max());
        }

        if (hasStraight)
        {
            var highestStraightValue = GetHigherValueAndCardsCountOfLongestConsecutiveValues(cards.Select(c => c.cardValue.Value).ToList()).HighestValue;
            return new HandRanking(HandRankingTypeEnum.Straight, (CardValueEnum)highestStraightValue);
        }

        if (hasThreeOfAKind)
        {
            var highestSetValue = groupedByValueCards.Where(gr => gr.Count() == 3).OrderByDescending(gr => gr.Key).Select(gr => gr.Key).First();
            return new HandRanking(HandRankingTypeEnum.ThreeOfAKind, highestSetValue);
        }

        if (hasTwoPairs)
        {
            var pairsValues = groupedByValueCards.Where(gr => gr.Count() == 2).OrderByDescending(gr => gr.Key).Select(gr => gr.Key).ToList();
            return new HandRanking(HandRankingTypeEnum.TwoPairs, pairsValues[0], pairsValues[1]);
        }

        if (hasSinglePair)
            return new HandRanking(HandRankingTypeEnum.Pair, groupedByValueCards.Single(gr => gr.Count() == 2).Key);

        // High card
        return new HandRanking(
                HandRankingTypeEnum.HighCard, 
                cards.First(card => (int)card.cardValue.Value == cards.Max(c => (int)c.cardValue.Value)).cardValue.Value);
    }

    public HandRankingComparaisonEnum ResultVersusHand(Hand handToCompareWith)
    {
        return this.EvaluateRanking().CompareRankingPropertiesValues(handToCompareWith.EvaluateRanking());
    }

    private (int HighestValue, int CardsCount) GetHigherValueAndCardsCountOfLongestConsecutiveValues(List<CardValueEnum> cardValues)
    {
        var cardValuesASInt = cardValues.Distinct().OrderBy(v => v).Select(v => (int)v).ToList();
        var higherValueOfLongestConsecutiveValues = 0;

        // Add low Ace to Handle case of wheel straight: A-2-3-4-5
        if (cardValuesASInt.Contains((int)CardValueEnum.Ace))
            cardValuesASInt = new List<int> { 1 }.Union(cardValuesASInt).ToList();

        var cardsCountOfLongestConsecutiveValues = 0;
        var currentConsecutiveValuesCount = 1;
        for (int i = 1; i < cardValuesASInt.Count; i++)
        {
            if (cardValuesASInt[i] != cardValuesASInt[i - 1] + 1)
            {
                currentConsecutiveValuesCount = 1;
                continue;
            }

            // If is consecutive value
            currentConsecutiveValuesCount++;
            if (currentConsecutiveValuesCount > cardsCountOfLongestConsecutiveValues)
            {
                cardsCountOfLongestConsecutiveValues = currentConsecutiveValuesCount;
                higherValueOfLongestConsecutiveValues = cardValuesASInt[i];
            }
        }

        if (currentConsecutiveValuesCount > cardsCountOfLongestConsecutiveValues)
            cardsCountOfLongestConsecutiveValues = currentConsecutiveValuesCount;

        return (higherValueOfLongestConsecutiveValues, cardsCountOfLongestConsecutiveValues);
    }
}
