using FluentAssertions;
using KataTddBowling.Appli;

namespace KataTddBowling.Tests;

public class BowlingScoreTests 
{
    private readonly BowlingService service; 
    public BowlingScoreTests()
    {
        service = new BowlingService();
        service.CreateNewGame();
    }

    [Fact]
    public void NewGameWoRolls_ShouldScoreZero_Test()
    {
        // Arrange
        var service = new BowlingService();
        service.CreateNewGame();

        // Act
        var score = service.GetScore();

        // Assert
        score.Should().Be(0);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(2)]
    [InlineData(9)]
    public void SingleRoll_ShouldScoreTheRollValueTest(int downPinsCount)
    {
        // Arrange
        service.AddRoll(downPinsCount);

        // Act
        var score = service.GetScore();

        // Assert
        score.Should().Be(downPinsCount);
    }

    [Theory]
    [InlineData(11)]
    [InlineData(-1)]
    public void RollWithWrongValue_ShouldThrowWrongRollValueException_Test(int downPinsCount)
    {
        // Arrange
        Action action = () => service.AddRoll(downPinsCount);

        // Assert
        action.Should().Throw<WrongRollValueException>();
    }

    [Theory]
    [InlineData(0, 0)]
    [InlineData(2, 2, 2)]
    [InlineData(5, 3, 1, 4)]
    public void MultipleRollsWoSparesNorStrikes_ShouldScoreTheRollsAddedValues_Test(params int[] downPinsCount)
    {
        // Arrange
        foreach (var pin in downPinsCount) 
            service.AddRoll(pin);

        // Act
        var score = service.GetScore();

        // Assert
        score.Should().Be(downPinsCount.Sum());
    }

    [Theory]
    [InlineData(16, 5, 5, 3)]
    [InlineData(12, 0, 10, 1)]
    public void GameWithSpare_ShouldScoreSpareBonus_Test(int awaitedScore, params int[] rollsValues)
    {
        // Arrange
        foreach (var pin in rollsValues)
            service.AddRoll(pin);

        // Act
        var score = service.GetScore();

        // Assert
        score.Should().Be(awaitedScore);
    }

}