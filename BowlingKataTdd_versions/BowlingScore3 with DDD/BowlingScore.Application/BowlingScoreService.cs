using BowlingScore.Domain;

namespace BowlingScore.Application;

public class BowlingScoreService
{
    public BowlingScoreService()
    {
        _game = new Game();
    }

    // Warning: stateful service. RollHistory should rather be retrived from persistance to make service stateless.
    private Game _game;

    public void AddRoll(int downPinsCount)
    {
        _game.AddRoll(downPinsCount);
    }

    public int GetScore()
    {
        return _game.GetScore();
    }
}