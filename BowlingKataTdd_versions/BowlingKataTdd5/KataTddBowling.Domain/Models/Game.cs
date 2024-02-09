using System;
using System.Security.AccessControl;
using static System.Formats.Asn1.AsnWriter;

namespace KataTddBowling.Domain.Models;

public record Game
{
    public Guid Id { get; private set; }
    List<Frame> _frames;

    public Game(Guid? gameId = null)
    {
        CreateNewGame(gameId);
    }

    public void CreateNewGame(Guid? gameId = null)
    {
        Id = gameId ?? Guid.NewGuid();
        _frames = new List<Frame>();
    }

    public void AddRoll(int downPinsCount)
    {
        if (!_frames.Any() || _frames.Last().IsCompleted)
            _frames.Add(new Frame(downPinsCount));
        else
            _frames.Last().SetSecondRoll(downPinsCount);
    }

    public int GetScore()
    {
        var score = 0;
        for (int i = 0; i < _frames.Count && i < 10; i++)
        {
            score += _frames[i].Score;

            if (i + 1 < _frames.Count)
            {
                if (_frames[i].IsStrike)
                {
                    score += _frames[i + 1].Score;
                    if (_frames[i + 1].IsStrike && i + 2 < _frames.Count)
                        score += _frames[i + 2].FirstRoll.DownPinsCount;
                }

                if (_frames[i].IsSpare)
                    score += _frames[i + 1].FirstRoll.DownPinsCount;
            }
        } 
        return score;
    }
}
