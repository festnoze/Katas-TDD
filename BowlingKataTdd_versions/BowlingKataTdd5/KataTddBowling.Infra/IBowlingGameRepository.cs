using KataTddBowling.Domain.Models;

namespace KataTddBowling.Infra;
public interface IBowlingGameRepository
{
    void CreateNewGame(Guid gameId);
    Game GetGame(Guid gameId);
    void SaveGame(Game game);
}