



namespace BowlingScore.Application;

public class BowlingScoreService
{
    public BowlingScoreService()
    {
        _rollsHistory = new List<int>();
    }

    // Warning: stateful service. RollHistory should rather be retrived from persistance to make service stateless.
    private List<int> _rollsHistory;

    public void AddRoll(int downPinsCount)
    {
        if (downPinsCount < 0 || downPinsCount > 10)
            throw new WrongDownPinsCountException();

        _rollsHistory.Add(downPinsCount);

        if (IsStrikeOnRoll(_rollsHistory.Count - 1))
            AddRoll(0);

    }

    public int GetScore()
    {
        var score = 0;
        for (int i = 0; i < _rollsHistory.Count; i++)
        {
            score += _rollsHistory[i];
            score += GetStrikeBonusIfApplicable(i);
            score += GetSpareBonusIfApplicable(i);
        }
        return score;
    }

    private int GetStrikeBonusIfApplicable(int rollIndex)
    {
        var bonus = 0;
        if (IsStrikeOnRoll(rollIndex))
        {
            if (HasTwoNextRoll(rollIndex))
                bonus += _rollsHistory[rollIndex + 2];
            if (HasThreeNextRoll(rollIndex))
                bonus += _rollsHistory[rollIndex + 3];
        }
        return bonus;
    }

    private bool IsStrikeOnRoll(int rollIndex) => rollIndex >=0 && IsEven(rollIndex) && _rollsHistory[rollIndex] == 10;

    private bool IsStrikeOnRollFrame(int rollIndex) => IsStrikeOnRoll(rollIndex) || IsStrikeOnRoll(rollIndex -1);

    private int GetSpareBonusIfApplicable(int rollIndex)
    {
        if (!IsStrikeOnRollFrame(rollIndex))
            if (HasPreviousRoll(rollIndex) && HasNextRoll(rollIndex))
                if (CurrentRollFrameHasSpare(rollIndex))
                    return _rollsHistory[rollIndex + 1];
        return 0;
    }

    private bool CurrentRollFrameHasSpare(int i) =>  _rollsHistory[i] + _rollsHistory[i - 1] == 10 && IsOdd(i);

    private bool HasNextRoll(int i) => i + 1 < _rollsHistory.Count;
    private bool HasTwoNextRoll(int i) => i + 2 < _rollsHistory.Count;
    private bool HasThreeNextRoll(int i) => i + 3 < _rollsHistory.Count;

    private bool HasPreviousRoll(int i) => i > 0;

    private bool IsEven(int i) => i % 2 == 0; 
    private bool IsOdd(int i) => i % 2 == 1; 
}