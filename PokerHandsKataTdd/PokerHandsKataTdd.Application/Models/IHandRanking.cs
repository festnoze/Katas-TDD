using PokerHandsKataTdd.Application.Models.Enums;

namespace PokerHandsKataTdd.Application.Models;

public interface IHandRanking
{
    HandRankingTypeEnum HandRankingType { get; }
    CardValueEnum HandRankingHigherValue { get; }
    CardValueEnum? HandRankingHigherSecondValue { get; }
}