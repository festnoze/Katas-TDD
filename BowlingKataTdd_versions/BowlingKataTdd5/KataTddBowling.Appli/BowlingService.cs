using KataTddBowling.Domain.Models;
using KataTddBowling.Infra;

namespace KataTddBowling.Appli;

public class BowlingService
{
    private readonly IBowlingGameRepository _repository;
    public BowlingService(IBowlingGameRepository bowlingGameRepository)
    {
        _repository = bowlingGameRepository;
    }


    public Guid CreateNewGame()
    {
        var game = new Game();
        _repository.CreateNewGame(game.Id);

        return game.Id;
    }

    public void AddRoll(Guid gameId, int downPinsCount)
    {
        var game = _repository.GetGame(gameId);
        
        game.AddRoll(downPinsCount);

        _repository.SaveGame(game);
    }

    public int GetScore(Guid gameId)
    {
        var game = _repository.GetGame(gameId);
        return game.GetScore();
    }
}