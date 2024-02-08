using BowlingScore.Domain;
using BowlingScore.Domain.Exceptions;
using BowlingScore.Domain.Infrastructure;

namespace BowlingScore.Tests.Fakes;
public class FakeBowlingGameRepository : IBowlingGameRepository
{
    // fake persistance with a static dictionary
    private static Dictionary<Guid, Game> _games = new Dictionary<Guid, Game>();

    public async Task<Game> LoadGameAsync(Guid gameId)
    {
        _games.TryGetValue(gameId, out var game);
        if (game is null) 
            throw new NotFoundGameIdentifierException();

        return game;
    }

    public async Task SaveGameAsync(Game game)
    {
        if (await DoesGameExistAsync(game.GameId))
            _games[game.GameId] = game;
        else
            _games.Add(game.GameId, game);
    }

    private async Task<bool> DoesGameExistAsync(Guid gameId)
    {
        return _games.TryGetValue(gameId, out _);
    }
}
