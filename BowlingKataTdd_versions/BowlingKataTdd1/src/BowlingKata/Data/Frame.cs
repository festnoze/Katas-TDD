namespace BowlingKata.Data;

public record Frame
{
    private Roll _firstRoll;

    private Roll? _secondRoll;
    public Frame(int firstRollScore)
    {
        _firstRoll = new Roll(firstRollScore);
        _secondRoll = null;
    }

    public void SetSecondRoll(int secondRollScore) 
    {
        _secondRoll = new Roll(secondRollScore);
    }

    public bool IsFrameCompleted => IsStrike || _secondRoll is not null;
    public bool IsStrike => _firstRoll.RollResult == 10;
    public bool IsSpare => _secondRoll is not null && _firstRoll.RollResult + _secondRoll!.RollResult == 10;

    public int FirstRollScore => _firstRoll.RollResult;
    public int SecondRollScore => _secondRoll?.RollResult ?? 0;
    public int FrameScore => FirstRollScore + SecondRollScore;
}
