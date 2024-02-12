using BowlingKataTdd2.Application.Exceptions;

namespace BowlingKataTdd2.Application;

public class BowlingGameService
{
    public void CreateNewGame()
    {
        _pinsDownList = new();
    }

    private List<int> _pinsDownList = null!;

    private bool _isEvenRoll => _pinsDownList.Count % 2 == 0;
    private bool _isStrike(int pinsDownCount)
    {
        return _isEvenRoll && pinsDownCount == 10;
    }

    private void AddRolls(params int[] rollValues)
    {
        foreach (int rollValue in rollValues)
        {
            if (rollValue < 0 || rollValue > 10)
                throw new WrongDownPinsCountException();

            if (_pinsDownList is null)
                throw new NotInitializedGameException();

            var isStrike = _isStrike(rollValue);

            _pinsDownList.Add(rollValue);

            // Add fake roll in case of strike to complete the frame
            if (isStrike)
                _pinsDownList.Add(0);
        }
    }

    public int GetScore(params int[] rollValues)
    {
        AddRolls(rollValues);

        var bonusScore = 0;
        for (int i = 0; i < _pinsDownList.Count && i < 20; i+=2)
        {
            // Add bonus for Strike
            if (_pinsDownList[i] == 10)
            {
                if (_pinsDownList.Count - 1 >= i + 3)
                {
                    bonusScore += _pinsDownList[i + 2];
                    if (_pinsDownList[i + 2] == 10 && _pinsDownList.Count - 1 >= i + 4)
                        bonusScore += _pinsDownList[i + 4];
                    else
                        bonusScore += _pinsDownList[i + 3];
                    continue;
                }

                if (_pinsDownList.Count - 1 >= i + 2)
                {
                    bonusScore += _pinsDownList[i + 2];
                    continue;
                }
            }

            // Add bonus for Spare
            if (_pinsDownList.Count - 1 >= i + 2 && _pinsDownList[i] + _pinsDownList[i + 1] == 10)
            {
                bonusScore += _pinsDownList[i + 2];
                continue;
            }

        }

        return _pinsDownList.Take(20).Sum() + bonusScore;
    }
}