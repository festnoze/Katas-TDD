using BowlingScore.Application;

namespace BowlingScore.Tests;

public class BowlingScoreCalculationTests
{
    public BowlingScoreCalculationTests()
    {
        service = new BowlingScoreService();
    }

    private readonly BowlingScoreService service;

    [Fact]
    public void GameWithNoRolls_ShouldScoreZero_Test()
    {
        // Act
        var score = service.GetScore();

        // Assert
        score.Should().Be(0);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(3)]
    [InlineData(7)]
    [InlineData(9)]
    [InlineData(0, 0)]
    [InlineData(1, 2)]
    [InlineData(5, 3)]
    [InlineData(9, 0)]
    [InlineData(5, 3, 1)]
    [InlineData(5, 3, 1, 2)]
    [InlineData(5, 3, 1, 2, 1, 7, 2, 6, 8, 0, 5, 4, 7, 0)]
    public void GameWithMultipleRoll_ShouldScoreAllRollsAdded_Test(params int[] rollsDownPinsCount)
    {
        // Arrange
        foreach (var rollDownPinsCount in rollsDownPinsCount)
            service.AddRoll(rollDownPinsCount);

        // Act
        var score = service.GetScore();

        // Assert
        score.Should().Be(rollsDownPinsCount.Sum());
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(11)]
    [InlineData(145)]
    public void RollWithWrongDownPinsCount_ShouldFail_Test(int downPinsCount)
    {
        // Act
        var action = () => service.AddRoll(downPinsCount);

        // Assert
        action.Should().Throw<WrongDownPinsCountException>();
    }

    [Theory]
    [InlineData(10, 7, 3)] // spare on 1st frame w/o next rolls (yield no spare bonus)
    [InlineData(14, 7, 3, 2)] // spare on 1st frame w/ single next roll
    [InlineData(17, 7, 3, 2, 3)] // spare on 1st frame w/ 2 next rolls
    [InlineData(14, 0, 0, 7, 3, 2)] // spare on 2nd frame
    [InlineData(12, 0, 7, 3, 2)] // fake-spare: no bonus as the 2 rolls are on 2 frames
    [InlineData(31, 7, 3, 7, 3, 2)] // 2 spares on following frames
    [InlineData(27, 7, 3, 2, 1, 5, 5, 1)] // 2 spares on separated frames
    public void GameHasSpares_ScoreShouldIncludeSparesBonuses_Test(int awaitedScore, params int[] rollsDownPinsCount)
    {
        CalculatedScoreMatchsAwaitedScoreTest(awaitedScore, rollsDownPinsCount);
    }

    [Theory]
    [InlineData(10, 10)] // strike on 1st frame w/o next rolls (yield no strike bonus)
    [InlineData(14, 10, 2)] // strike on 1st frame w/ single next roll
    [InlineData(20, 10, 2, 3)] // strike on 1st frame w/ 2 next rolls
    [InlineData(17, 0, 10, 2, 3)] // fake strike: 10 pins down on 2nd roll of frame is actually a spare
    public void GameHasStrikes_ScoreShouldIncludeStrikeBonuses_Test(int awaitedScore, params int[] rollsDownPinsCount)
    {
        CalculatedScoreMatchsAwaitedScoreTest(awaitedScore, rollsDownPinsCount);
    }

    private void CalculatedScoreMatchsAwaitedScoreTest(int awaitedScore, int[] rollsDownPinsCount)
    {

        // Arrange
        foreach (var rollDownPinsCount in rollsDownPinsCount)
            service.AddRoll(rollDownPinsCount);

        // Act
        var score = service.GetScore();

        // Assert
        score.Should().Be(awaitedScore);
    }
}