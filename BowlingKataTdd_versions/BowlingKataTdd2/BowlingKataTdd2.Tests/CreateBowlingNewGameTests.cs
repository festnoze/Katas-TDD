using BowlingKataTdd2.Application;

namespace BowlingKataTdd2.Tests;

public class CreateBowlingNewGameTests
{
    [Fact]
    public void CreateNewGame_ShouldSucced_Test()
    {
        // Arrange
        var service = new BowlingGameService();

        // Act
        service.CreateNewGame();

        // Assert
        // Don't throw exception
    }
}