using FluentAssertions.Common;
using KataTddBowling.Appli;
using KataTddBowling.Domain.Exceptions;
using KataTddBowling.Infra;

namespace KataTddBowling.Tests;

public class BowlingTests : IDisposable
{
    private Guid gameId;
    public BowlingTests()
    {
        _service = new BowlingService(new FakeBowlingGameRepository());
        gameId = _service.CreateNewGame();
    }

    private readonly BowlingService _service;

    public void Dispose()
    {
    }

    [Fact]
    public void EmptyGame_ShouldScoreZero_Test()
    {
        // Arrange

        // Act
        var score = _service.GetScore(gameId);

        // Assert
        score.Should().Be(0);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(5)]
    [InlineData(8)]
    [InlineData(9)]
    public void SingleRollGame_ShouldScoreRollValue_Test(int downPinsCount)
    {
        // Arrange
        _service.AddRoll(gameId, downPinsCount);

        // Act
        var score = _service.GetScore(gameId);

        // Assert
        score.Should().Be(downPinsCount);
    }

    [Theory]
    [InlineData(11)]
    [InlineData(-1)]
    public void RollWithWrongValue_ShouldThrowWrongRollValueException_Test(int downPinsCount)
    {
        // Act
        Action addRoll = () => _service.AddRoll(gameId, downPinsCount);

        // Assert
        addRoll.Should().Throw<WrongRollValueException>();
    }

    [Theory]
    [InlineData(0, 0)]
    [InlineData(1, 2)]
    [InlineData(5,4,2,1,5)]
    [InlineData(8,0,1,2,5,1,3)]
    [InlineData(0,9,0,9,1,4,5,1,2,3,1)]
    public void MultipleRollsGame_ShouldScoreAllRollsAddedValues_Test(params int[] rollsDownPinsCount)
    {
        // Arrange
        foreach (var rollDownPinsCount in rollsDownPinsCount)
            _service.AddRoll(gameId, rollDownPinsCount);

        // Act
        var score = _service.GetScore(gameId);

        // Assert
        score.Should().Be(rollsDownPinsCount.Sum());
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

    [Theory]
    [InlineData(0, 5, 2, 17)]
    [InlineData(10, 10, 10, 300)]
    public void StrikeAtFrame10_ShouldAllow2ExtraRollsForBonusOnly_Test(int pinsDownOnAllRolls, int pinsDownOn21thRoll, int pinsDownOn22thRoll, int awaitedScore)
    {
        // Arrange
        var gameId = _service.CreateNewGame();

        for (int i = 0; i < 2 * 9; i++)
            _service.AddRoll(gameId, pinsDownOnAllRolls);
        _service.AddRoll(gameId, 10);
        _service.AddRoll(gameId, pinsDownOn21thRoll);
        _service.AddRoll(gameId, pinsDownOn22thRoll);

        // Act
        var score = _service.GetScore(gameId);

        // Assert
        score.Should().Be(awaitedScore);
    }


    private void CalculatedScoreMatchsAwaitedScoreTest(int awaitedScore, int[] rollsDownPinsCount)
    {
        // Arrange
        var gameId = _service.CreateNewGame();

        foreach (var rollDownPinsCount in rollsDownPinsCount)
            _service.AddRoll(gameId, rollDownPinsCount);

        // Act
        var score = _service.GetScore(gameId);

        // Assert
        score.Should().Be(awaitedScore);
    }
}