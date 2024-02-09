using FluentAssertions;

namespace TestKataBowling.Tests;

public class BowlingGameScoringTests : IDisposable
{
    private readonly BowlingService _bowlingService;
    private readonly Guid _gameId;

    public BowlingGameScoringTests()
    {
        _bowlingService = new BowlingService();
        _gameId = _bowlingService.CreateGame();
    }

    public void Dispose()
    {
    }

    [Fact]
    public void AnEmptyGame_ShouldScoreZero_Test()
    {
        // Act
        var score = _bowlingService.GetScore(_gameId);

        // Assert
        score.Should().Be(0);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(9)]
    [InlineData(0, 0)]
    [InlineData(1, 5)]
    [InlineData(2, 2)]
    [InlineData(9, 1)]
    public void GameWithRolls_ShouldScoreTheAddedRolls_Test(params int[] rollsPinsDownCount)
    {
        // Arrange
        foreach (var rollPinsDownCount in rollsPinsDownCount)
            _bowlingService.AddRoll(rollPinsDownCount);

        // Act
        var score = _bowlingService.GetScore(_gameId);

        // Assert
        score.Should().Be(rollsPinsDownCount.Sum());
    }
}