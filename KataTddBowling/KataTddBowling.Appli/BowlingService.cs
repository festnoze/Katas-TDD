namespace KataTddBowling.Appli;

public class BowlingService
{
    public BowlingService()
    {
    }

    List<int> rollsValues = new List<int>();

    public void AddRoll(int downPinsCount)
    {
        if (downPinsCount < 0 || downPinsCount > 10)
        {
            throw new WrongRollValueException();
        }
        rollsValues.Add(downPinsCount);
    }

    public Guid CreateNewGame()
    {
        rollsValues = new List<int>();
        return Guid.NewGuid();
    }

    public int GetScore()
    {
        var score = 0;

        for (int i = 0; i < rollsValues.Count; i++)
        {
            if (IsSpare(i))
                score += GetSpareBonus(i);

            score += rollsValues[i];
        }

        return score;
    }

    private int GetSpareBonus(int i)
    {
        if (i + 1 > rollsValues.Count)
            return 0;
        return rollsValues[i + 1];
    }

    private bool IsSpare(int i)
    {
        if (!IsOdd(i)) 
            return false;

        return rollsValues[i] + rollsValues[i - 1] == 10;
    }

    private Predicate<int> IsOdd = i => i % 2 == 1;
}