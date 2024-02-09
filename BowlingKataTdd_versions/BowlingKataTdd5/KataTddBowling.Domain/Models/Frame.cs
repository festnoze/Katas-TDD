namespace KataTddBowling.Domain.Models;

public record Frame
{
    public Roll FirstRoll { get; init; }
    public Roll? SecondRoll { get; private set; }

    public Frame(int firstRollDownPinsCount)
    {        
        FirstRoll = new Roll(firstRollDownPinsCount);
    }

    public void SetSecondRoll(int secondRollDownPinsCount)
    {
        SecondRoll = new Roll(secondRollDownPinsCount);
    }

    public int Score => FirstRoll.DownPinsCount + (SecondRoll?.DownPinsCount ?? 0);
    public bool IsStrike => FirstRoll.DownPinsCount == 10;
    public bool IsSpare => !IsStrike && IsCompleted && Score == 10;
    public bool IsCompleted => IsStrike || SecondRoll is not null;

}
