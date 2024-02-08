using BowlingScore.Domain.Exceptions;

namespace BowlingScore.Domain;

public record Frame
{
    public Frame(int firstRollDownPinsCount)
    {
        FirstRoll = new Roll(firstRollDownPinsCount);
    }

    public Roll FirstRoll { get; init; }
    public Roll? SecondRoll { get; private set; }

    public void SetSecondRoll(int secondRollDownPinsCount)
    {
        if (HasSecondRoll)
            throw new FrameSecondRollAlreadySettedException();

        SecondRoll = new Roll(secondRollDownPinsCount);
    }

    public bool IsStrike => FirstRoll.DownPinsCount == 10;
    public bool IsSpare => !IsStrike && ScoreWoBonus == 10;
    public bool IsComplete => IsStrike || SecondRoll is not null;
    public int ScoreWoBonus => FirstRoll.DownPinsCount + (SecondRoll?.DownPinsCount ?? 0);
    public bool HasSecondRoll => SecondRoll is not null;
}
