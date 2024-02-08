using BowlingScore.Domain;
using BowlingScore.Domain.Infrastructure;

namespace BowlingScore.Infra;
public class BowlingGameRepository : IBowlingGameRepository
{
    public async Task<Game> LoadGameAsync(Guid gameId)
    {
        throw new NotImplementedException();
    }

    public async Task SaveGameAsync(Game game)
    {
        throw new NotImplementedException();
    }
}
