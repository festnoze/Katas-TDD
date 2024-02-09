using KataTddBowling.Domain.Models;

namespace KataTddBowling.Infra;

public class BowlingGameRepository : IBowlingGameRepository
{
    public BowlingGameRepository()
    { }

    public void CreateNewGame(Guid gameId)
    {
        throw new NotImplementedException();
    }

    public void SaveGame(Game game)
    {
        throw new NotImplementedException();
    }

    public Game GetGame(Guid gameId)
    {
        throw new NotImplementedException();
    }
}
