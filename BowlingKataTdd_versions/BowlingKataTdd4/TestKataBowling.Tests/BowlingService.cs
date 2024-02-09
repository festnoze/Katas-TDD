namespace TestKataBowling.Tests;

internal class BowlingService
{
    public BowlingService()
    {
    }

    private List<int> rollsScores = new List<int>();

    internal void AddRoll(int downPinsCount)
    {
        if (downPinsCount < 0 || downPinsCount > 10)
        {
            throw new InvalidPinsCountException();
        }

        rollsScores.Add(downPinsCount);

        if (IsStrike(rollsScores.Count - 1))
            rollsScores.Add(0);
    }

    internal Guid CreateGame()
    {
        rollsScores.Clear();
        return Guid.NewGuid();
    }

    internal int GetScore(Guid gameId)
    {
        var score = 0;

        for (int i = 0; i < rollsScores.Count; i++)
        {
            score += rollsScores[i];

            if (IsStrike(i))
            {
                score += GetStrikeBonus(i);
            }

            if (IsSpare(i))
            {
                score += GetSpareBonus(i);
            }
        }
        return score;

    }

    internal bool HasGameEnded(Guid gameId)
    {
        if (rollsScores.Count > 20)
            return true;

        return false;
    }

    private int GetStrikeBonus(int i)
    {
        var bonus = 0;

        if (!IsEven(i)) 
            return bonus;

        if (rollsScores.Count > i + 2)
            bonus += rollsScores[i + 2];

        if (rollsScores.Count > i + 3)
            bonus += rollsScores[i + 3];

        return bonus;
    }

    private bool IsStrike(int i)
    {
        return IsEven(i) && rollsScores.Count > i && rollsScores[i] == 10;
    }

    private bool IsSpare(int rollIndex)
    {
        return IsOdd(rollIndex) && !IsStrike(rollIndex - 1) && rollsScores[rollIndex - 1] + rollsScores[rollIndex] == 10;
    }

    private Predicate<int> IsEven => (rollIndex) => rollIndex % 2 == 0;
    private Predicate<int> IsOdd => (rollIndex) => rollIndex % 2 == 1;

    private int GetSpareBonus(int rollIndex)
    {
        if (rollIndex + 1 < rollsScores.Count)
            return rollsScores[rollIndex + 1];

        return 0;
    }

}