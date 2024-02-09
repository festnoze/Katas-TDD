using FluentAssertions;

namespace TestKataBowling.Tests;

public class BowlingGameScoringWithSpareOrStrikeTests : IDisposable
{
    private readonly BowlingService _bowlingService;
    private readonly Guid _gameId;

    public BowlingGameScoringWithSpareOrStrikeTests()
    {
        _bowlingService = new BowlingService();
        _gameId = _bowlingService.CreateGame();
    }

    public void Dispose()
    {
    }

    [Theory]
    [InlineData(0, 10, 2)]
    [InlineData(9, 1, 3)]
    public void GameWithSingleSpare_ShouldScoreSpareBonus_Test(params int[] rollsPinsDownCount)
    {
        // Arrange
        rollsPinsDownCount.Length.Should().Be(3);
        foreach (var rollPinsDownCount in rollsPinsDownCount)
            _bowlingService.AddRoll(rollPinsDownCount);

        // Act
        var score = _bowlingService.GetScore(_gameId);

        // Assert
        score.Should().Be(rollsPinsDownCount.Sum() + rollsPinsDownCount.Last());
    }


    [Theory]
    [InlineData(12, 10, 1)]
    [InlineData(18, 10, 1, 3)]
    [InlineData(34, 10, 10, 2)]
    public void GameWithSingleStrike_ShouldScoreSpareBonus_Test(int awaitedScore, params int[] rollsPinsDownCount)
    {
        // Arrange
        foreach (var rollPinsDownCount in rollsPinsDownCount)
            _bowlingService.AddRoll(rollPinsDownCount);

        // Act
        var score = _bowlingService.GetScore(_gameId);

        // Assert
        score.Should().Be(awaitedScore);
    }
}