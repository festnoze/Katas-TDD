using BowlingScore.Domain;
using BowlingScore.Domain.Infrastructure;

namespace BowlingScore.Application;

public class BowlingScoreService
{
    public BowlingScoreService(IBowlingGameRepository bowlingGameRepository)
    {
        _gameRepository = bowlingGameRepository;
    }

    private readonly IBowlingGameRepository _gameRepository;

    public async Task<Guid> CreateNewGameAsync()
    { 
        var game = new Game();
        await _gameRepository.SaveGameAsync(game);

        return game.GameId;
    }

    public async Task AddRollAsync(int downPinsCount, Guid gameId)
    {
        var game = await _gameRepository.LoadGameAsync(gameId);
        if (game is null) throw new ArgumentNullException(nameof(AddRollAsync));

        game.AddRoll(downPinsCount);

        await _gameRepository.SaveGameAsync(game);
    }

    public async Task<int> GetScoreAsync(Guid gameId)
    {
        var game = await _gameRepository.LoadGameAsync(gameId);
        if (game is null) throw new ArgumentNullException(nameof(AddRollAsync));

        return game.GetScore();
    }
}