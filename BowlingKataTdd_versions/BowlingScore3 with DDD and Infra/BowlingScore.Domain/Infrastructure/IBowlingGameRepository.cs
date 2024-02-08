namespace BowlingScore.Domain.Infrastructure;

public interface IBowlingGameRepository
{
    Task<Game> LoadGameAsync(Guid gameId);

    Task SaveGameAsync(Game game);
}
