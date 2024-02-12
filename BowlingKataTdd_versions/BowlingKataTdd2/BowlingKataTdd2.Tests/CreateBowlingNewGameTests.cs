using BowlingKataTdd2.Application;
using FluentAssertions;

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


    [Fact]
    public void AddRoll_WithNoPriorCallToNewGame_ShouldThrowNotInitializedGameException_Test()
    {
        // Arrange
        var service = new BowlingGameService();
        // No CreateNewGame method call

        // Act
        var action = () => service.GetScore(5);

        // Assert
        action.Should().Throw<NotInitializedGameException>();
    }
}