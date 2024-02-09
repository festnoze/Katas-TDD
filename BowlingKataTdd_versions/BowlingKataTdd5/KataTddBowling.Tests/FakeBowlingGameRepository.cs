using KataTddBowling.Domain.Models;

namespace KataTddBowling.Infra;

public class FakeBowlingGameRepository : IBowlingGameRepository
{
    private Dictionary<Guid, Game> _games;
    public FakeBowlingGameRepository()
    {
        _games = new Dictionary<Guid, Game>();
    }

    public void CreateNewGame(Guid gameId)
    {
        _games.Add(gameId, new Game(gameId));
    }

    public void SaveGame(Game game)
    {
        if (!DoesGameExists(game.Id))
            throw new ArgumentException($"Unable to find game with id: {game.Id}");

        _games[game.Id] = game;
    }

    public Game GetGame(Guid gameId)
    {
        if (!DoesGameExists(gameId))
            throw new ArgumentException($"Unable to find game with id: {gameId}");

        _games.TryGetValue(gameId, out var game);

        return game!;
    }

    private bool DoesGameExists(Guid gameId)
    {
        return _games.TryGetValue(gameId, out _);
    }
}
