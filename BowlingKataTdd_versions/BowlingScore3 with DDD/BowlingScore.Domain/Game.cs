namespace BowlingScore.Domain;

public record Game
{
    public Game()
    {
        Frames = new List<Frame>();
    }

    public List<Frame> Frames { get; init; }

    public void AddRoll(int downPinsCount)
    {
        if (!Frames.Any() || Frames.Last().IsComplete)
            Frames.Add(new Frame(downPinsCount));
        else
            Frames.Last().SetSecondRoll(downPinsCount);
    }

    public int GetScore()
    {
        var score = 0;
        var limitFramesCountTo10 = Frames.Count > 10 ? 10 : Frames.Count;

        for (int i = 0; i < limitFramesCountTo10; i++)
        {
            score += Frames[i].ScoreWoBonus;
            score += GetStrikeBonusIfApplicable(i);
            score += GetSpareBonusIfApplicable(i);
        }
        return score;
    }

    private int GetStrikeBonusIfApplicable(int frameIndex)
    {
        if (!HasFrameAtIndex(frameIndex))
            return 0;

        var bonus = 0;
        if (Frames[frameIndex].IsStrike)
        {
            if (HasFrameAtIndex(frameIndex + 1))
            {
                bonus += Frames[frameIndex + 1].FirstRoll.DownPinsCount;

                if (!Frames[frameIndex + 1].IsStrike)
                {
                    if (Frames[frameIndex + 1].HasSecondRoll)
                        bonus += Frames[frameIndex + 1].SecondRoll!.DownPinsCount;
                }
                else
                {
                    if (HasFrameAtIndex(frameIndex + 2))
                        bonus += Frames[frameIndex + 2].FirstRoll.DownPinsCount;
                }
            }
        }
        return bonus;
    }

    private int GetSpareBonusIfApplicable(int frameIndex)
    {
        if (!HasFrameAtIndex(frameIndex))
            return 0;
                    
        if (Frames[frameIndex].IsSpare && HasFrameAtIndex(frameIndex + 1))
                return Frames[frameIndex + 1].FirstRoll.DownPinsCount;
        
        return 0;
    }   

    private bool HasFrameAtIndex(int index) =>  index < Frames.Count;
}
