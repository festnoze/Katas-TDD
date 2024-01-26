namespace BowlingKata.Data;

public record BowlingGameScores
{
    public BowlingGameScores()
    {
       Clear();
    }

    private List<Frame> _gameRollsHistory = new List<Frame>();

    public void AddRollScore(int rollScore) 
    {
        if (HasGameEnded())
            throw new InvalidOperationException($"Game has already ended, therefore another roll score cannot be added to the score sheet");

        if (!_gameRollsHistory.Any() || _gameRollsHistory.Last().IsFrameCompleted)
            _gameRollsHistory.Add(new Frame(rollScore));
        else
            _gameRollsHistory.Last().SetSecondRoll(rollScore);
    }

    public void Clear()
    {
        _gameRollsHistory = new List<Frame>();
    }

    public bool HasGameEnded()
    {
        if (_gameRollsHistory.Count < 10) 
            return false;
        
        if (!_gameRollsHistory[9].IsFrameCompleted) 
            return false;

        if (_gameRollsHistory[9].IsSpare)
        {
            if (_gameRollsHistory.Count != 11 || _gameRollsHistory[10].IsFrameCompleted)
                return false;
        }

        if (_gameRollsHistory[9].IsStrike)
        {
            if (_gameRollsHistory.Count == 11 && _gameRollsHistory[10].RollsCount == 2)
                    return true;

            if (_gameRollsHistory.Count == 12 && _gameRollsHistory[10].IsStrike)
                    return true;

            return false;
        }

        return true;
    }

    public int FinalScore()
    {
        // Check that the game has really ended
        if (!HasGameEnded())
            throw new InvalidOperationException($"Game hasn't ended, therefore {nameof(FinalScore)} cannot be computed");

        return PartialScore();
    }

    public int PartialScore()
    {
        var score = 0;
        for (int frameindex = 0; frameindex < _gameRollsHistory.Count(); frameindex++)
        {
            // Stop if it's the last frame
            if (frameindex + 1 > 10)
                continue;

            score += _gameRollsHistory[frameindex].FrameScore;

            // Check it's a 'strike'
            if (_gameRollsHistory[frameindex].IsStrike)
            {
                if (_gameRollsHistory[frameindex + 1].IsStrike && frameindex + 2 < _gameRollsHistory.Count())
                    score += _gameRollsHistory[frameindex + 1].FirstRollScore + _gameRollsHistory[frameindex + 2].FirstRollScore;
                else
                    score += _gameRollsHistory[frameindex + 1].FrameScore;
                continue;
            }

            //  Check it's a 'spare'
            if (_gameRollsHistory[frameindex].IsSpare)
            {
                score += _gameRollsHistory[frameindex + 1].FirstRollScore;
                continue;
            }
        }

        return score;
    }
}
