using BowlingKataTdd2.Application;
using FluentAssertions;

namespace BowlingKataTdd2.Tests;

public class GameScoreCalculationTests
{
    private BowlingGameService _service;

    public GameScoreCalculationTests()
    {
        _service = new BowlingGameService();
        _service.CreateNewGame();
    }

    [Fact]
    public void GameWithNoRolls_ShouldHaveScoreValueZero_Test()
    {
        // Act
        var score = _service.GetScore();

        // Assert
        score.Should().Be(0);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(5)]
    [InlineData(10)]
    [InlineData(0, 0)]
    [InlineData(1, 1)]
    [InlineData(5, 4)]
    [InlineData(7, 3)] //Spare (w/o further rolls)
    [InlineData(1, 1, 1)]
    [InlineData(1, 1, 1, 1)]
    [InlineData(1, 1, 1, 1, 1)]
    [InlineData(1, 1, 1, 1, 1, 1)]
    [InlineData(1, 1, 1, 1, 1, 1, 1)]
    [InlineData(1, 1, 1, 1, 1, 1, 1, 1)]
    [InlineData(1, 1, 1, 1, 1, 1, 1, 1, 1)]
    [InlineData(1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1)] // 20 rolls (full-game)
    public void SingleRoll_ShouldScoreTheFirstRoll_Test(params int[] downPinsCountOnRolls)
    {
        TestScoreCalculationAfterRolls(downPinsCountOnRolls);
    }

    [Theory]
    [InlineData(12, 5, 5, 1)] // spare on first frame
    [InlineData(26, 7, 3, 8)] // spare on first frame
    [InlineData(26, 7, 3, 8, 0)] // spare on first frame
    [InlineData(26, 0, 0, 7, 3, 8)] // spare on second frame
    [InlineData(26, 0, 0, 0, 0, 7, 3, 8)] // spare on third frame
    [InlineData(13, 0, 7, 3, 3)] // fake-spare on two frames should not yield bonus
    public void SingleSpare_ScoreShouldHaveSpareBonus_Test(int awaitedScore, params int[] downPinsCountOnRolls)
    {
        // Arrange
        foreach (var downPinsCountOnRoll in downPinsCountOnRolls)
        {
            _service.GetScore(downPinsCountOnRoll);
        }

        // Act
        var score = _service.GetScore();

        // Assert
        score.Should().Be(awaitedScore);
    }

    [Theory]
    [InlineData(18, 10, 1, 3)] // strike on first frame
    [InlineData(26, 0, 0, 10, 3, 5)] // strike on second frame
    [InlineData(18, 0, 0, 0, 0, 10, 3, 1)] // strike on third frame
    [InlineData(10, 0, 0, 10)] // strike on second frame with no subsequent rolls (yield no bonus and no errors)
    [InlineData(16, 0, 0, 10, 3)] // strike on second frame with only 1 subsequent roll (yield last roll bonus and no errors)
    [InlineData(21, 0, 10, 3, 5)] // fake-strike on second frame's roll should not yield strike bonus but spare bonus
    public void SingleStrike_ShouldScoreStrikeBonus_Test(int awaitedScore, params int[] downPinsCountOnRolls)
    {
        // Arrange
        foreach (var downPinsCountOnRoll in downPinsCountOnRolls)
        {
            _service.GetScore(downPinsCountOnRoll);
        }

        // Act
        var score = _service.GetScore();

        // Assert
        score.Should().Be(awaitedScore);
    }

    private void TestScoreCalculationAfterRolls(params int[] downPinsCountOnRolls)
    {
        // Arrange
        foreach (var downPinsCountOnRoll in downPinsCountOnRolls)
        {
            _service.GetScore(downPinsCountOnRoll);
        }

        // Act
        var score = _service.GetScore();

        // Assert
        score.Should().Be(downPinsCountOnRolls.Sum());
    }

    [Theory]
    [InlineData(1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 10, 1, 3)] // 20 rolls (full-game)
    public void FinalStrikeRoll_ShouldScoreOk_Test(params int[] downPinsCountOnRolls)
    {
        TestScoreCalculationAfterRolls(downPinsCountOnRolls);
    }
}
