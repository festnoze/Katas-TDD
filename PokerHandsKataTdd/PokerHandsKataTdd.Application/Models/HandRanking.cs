using PokerHandsKataTdd.Application.Models.Enums;

namespace PokerHandsKataTdd.Application.Models;

public record HandRanking : IHandRanking
{
    public HandRanking(HandRankingTypeEnum handRankingType, CardValueEnum handRankingHigherValue, CardValueEnum? handRankingHigherSecondValue = null)
    {
        HandRankingType = handRankingType;
        HandRankingHigherValue = handRankingHigherValue;
        HandRankingHigherSecondValue = handRankingHigherSecondValue;
    }

    public HandRanking(string handRankingString)
    {
        var handRankingProps = handRankingString.Split(' ');

        if (handRankingProps.Length != 2 && handRankingProps.Length != 3)
            throw new ArgumentException($"'{handRankingString}' isn't a valid string to create a {nameof(HandRanking)}");

        HandRankingType = Enum.Parse<HandRankingTypeEnum>(handRankingProps[0]);
        HandRankingHigherValue = Enum.Parse<CardValueEnum>(handRankingProps[1]);

        if (handRankingProps.Length == 3)
            HandRankingHigherSecondValue = Enum.Parse<CardValueEnum>(handRankingProps[2]);
    }

    public HandRankingTypeEnum HandRankingType { get; }
    public CardValueEnum HandRankingHigherValue { get; }
    public CardValueEnum? HandRankingHigherSecondValue { get; }

    // Version using reflexion :
    //private static readonly List<string> rankingPropertiesOrder = new List<string>
    //        {
    //            nameof(HandRankingType),
    //            nameof(HandRankingHigherValue),
    //            nameof(HandRankingHigherSecondValue)
    //        };

    //public HandRankingComparaisonEnum CompareRankingPropertiesValues(IHandRanking handToCompareTo, string? propertyName = null)
    //{
    //    if (propertyName is null)
    //        propertyName = rankingPropertiesOrder.First();

    //    var propInfo = typeof(IHandRanking).GetProperty(propertyName);

    //    var ownPropertyValue = (int?)propInfo?.GetValue(this) ?? -1;
    //    var otherHandPropertyValue = (int?)propInfo?.GetValue(handToCompareTo) ?? -1;

    //    if (ownPropertyValue > otherHandPropertyValue)
    //        return HandRankingComparaisonEnum.Win;

    //    if (ownPropertyValue < otherHandPropertyValue)
    //        return HandRankingComparaisonEnum.Lose;

    //    if (rankingPropertiesOrder.Last() == propertyName)
    //        return HandRankingComparaisonEnum.Tie;

    //    var nextPropertyToCompare = rankingPropertiesOrder[rankingPropertiesOrder.IndexOf(propertyName) + 1];
    //    return CompareRankingPropertiesValues(handToCompareTo, nextPropertyToCompare);
    //}

    public HandRankingComparaisonEnum CompareRankingPropertiesValues(IHandRanking handToCompareTo)
    {
        // Compare all relevant properties at once using tuples
        var ownProperties = (HandRankingType:               (int)this.HandRankingType,
                             HandRankingHigherValue:        (int)this.HandRankingHigherValue,
                             HandRankingHigherSecondValue:  (int?)this.HandRankingHigherSecondValue);

        var otherHandProperties = (HandRankingType:             (int)handToCompareTo.HandRankingType,
                                   HandRankingHigherValue:      (int)handToCompareTo.HandRankingHigherValue,
                                   HandRankingHigherSecondValue:(int?)handToCompareTo.HandRankingHigherSecondValue);

        if (ownProperties.CompareTo(otherHandProperties) > 0)
            return HandRankingComparaisonEnum.Win;
        else if (ownProperties.CompareTo(otherHandProperties) < 0)
            return HandRankingComparaisonEnum.Lose;

        return HandRankingComparaisonEnum.Tie;
    }

}
