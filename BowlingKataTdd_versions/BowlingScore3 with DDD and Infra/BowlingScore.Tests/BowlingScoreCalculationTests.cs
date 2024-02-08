using BowlingScore.Application;
using BowlingScore.Domain.Exceptions;
using BowlingScore.Domain.Infrastructure;
using BowlingScore.Tests.Fakes;

namespace BowlingScore.Tests;

public class BowlingScoreCalculationTests
{
    public BowlingScoreCalculationTests()
    {
        gameRepository = new FakeBowlingGameRepository();
        service = new BowlingScoreService(gameRepository);
    }

    private readonly BowlingScoreService service;
    private readonly IBowlingGameRepository gameRepository;

    [Fact]
    public async Task GameWithNoRolls_ShouldScoreZero_Test()
    {
        // Arrange
        var gameId = await service.CreateNewGameAsync();

        // Act
        var score = await service.GetScoreAsync(gameId);

        // Assert
        score.Should().Be(0);
    }

    [Fact]
    public async Task AddRoll_ForAnUnexistingGame_ShouldThrowNotFoundGameIdentifierException_Test()
    {
        // Arrange
        var unexistingGameId = Guid.NewGuid();

        // Act
        var action = () => service.AddRollAsync(0, unexistingGameId);

        // Assert
        await action.Should().ThrowAsync<NotFoundGameIdentifierException>();
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
    public async Task GameWithMultipleRoll_ShouldScoreAllRollsAdded_Test(params int[] rollsDownPinsCount)
    {
        // Arrange
        var gameId = await service.CreateNewGameAsync();

        foreach (var rollDownPinsCount in rollsDownPinsCount)
            await service.AddRollAsync(rollDownPinsCount, gameId);

        // Act
        var score = await service.GetScoreAsync(gameId);

        // Assert
        score.Should().Be(rollsDownPinsCount.Sum());
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(11)]
    [InlineData(145)]
    public async Task RollWithWrongDownPinsCount_ShouldFail_Test(int downPinsCount)
    {
        // Arrange
        var gameId = await service.CreateNewGameAsync();

        // Act
        var action = () => service.AddRollAsync(downPinsCount, gameId);

        // Assert
        await action.Should().ThrowAsync<WrongDownPinsCountException>();
    }

    [Theory]
    [InlineData(10, 7, 3)] // spare on 1st frame w/o next rolls (yield no spare bonus)
    [InlineData(14, 7, 3, 2)] // spare on 1st frame w/ single next roll
    [InlineData(17, 7, 3, 2, 3)] // spare on 1st frame w/ 2 next rolls
    [InlineData(14, 0, 0, 7, 3, 2)] // spare on 2nd frame
    [InlineData(12, 0, 7, 3, 2)] // fake-spare: no bonus as the 2 rolls are on 2 frames
    [InlineData(31, 7, 3, 7, 3, 2)] // 2 spares on following frames
    [InlineData(27, 7, 3, 2, 1, 5, 5, 1)] // 2 spares on separated frames
    public async Task GameHasSpares_ScoreShouldIncludeSparesBonuses_Test(int awaitedScore, params int[] rollsDownPinsCount)
    {
        await CalculatedScoreMatchsAwaitedScoreTestAsync(awaitedScore, rollsDownPinsCount);
    }

    [Theory]
    [InlineData(10, 10)] // strike on 1st frame w/o next rolls (yield no strike bonus)
    [InlineData(14, 10, 2)] // strike on 1st frame w/ single next roll
    [InlineData(20, 10, 2, 3)] // strike on 1st frame w/ 2 next rolls
    [InlineData(17, 0, 10, 2, 3)] // fake strike: 10 pins down on 2nd roll of frame is actually a spare
    public async Task GameHasStrikes_ScoreShouldIncludeStrikeBonuses_Test(int awaitedScore, params int[] rollsDownPinsCount)
    {
        await CalculatedScoreMatchsAwaitedScoreTestAsync(awaitedScore, rollsDownPinsCount);
    }

    [Theory]
    [InlineData(0, 5, 2, 17)]
    [InlineData(10, 10, 10, 300)]
    public async Task StrikeAtFrame10_ShouldAllow2ExtraRollsForBonusOnly_Test(int pinsDownOnAllRolls, int pinsDownOn21thRoll, int pinsDownOn22thRoll, int awaitedScore)
    {
        // Arrange
        var gameId = await service.CreateNewGameAsync();

        for (int i = 0; i < 2 * 9; i++)
            await service.AddRollAsync(pinsDownOnAllRolls, gameId);
        await service.AddRollAsync(10, gameId);
        await service.AddRollAsync(pinsDownOn21thRoll, gameId);
        await service.AddRollAsync(pinsDownOn22thRoll, gameId);

        // Act
        var score = await service.GetScoreAsync(gameId);

        // Assert
        score.Should().Be(awaitedScore);
    }


    private async Task CalculatedScoreMatchsAwaitedScoreTestAsync(int awaitedScore, int[] rollsDownPinsCount)
    {
        // Arrange
        var gameId = await service.CreateNewGameAsync();

        foreach (var rollDownPinsCount in rollsDownPinsCount)
            await service.AddRollAsync(rollDownPinsCount, gameId);

        // Act
        var score = await service.GetScoreAsync(gameId);

        // Assert
        score.Should().Be(awaitedScore);
    }
}