using FluentAssertions;

namespace TestKataBowling.Tests;

public class BowlingGameScoringOnLastFrameTests 
{
    private readonly BowlingService _bowlingService;
    private readonly Guid _gameId;

    public BowlingGameScoringOnLastFrameTests()
    {
        _bowlingService = new BowlingService();
        _gameId = _bowlingService.CreateGame();
    }

    [Theory]
    [InlineData(false, 8)] // Game not ended if last frame not completed
    [InlineData(true, 0, 8)] // Game ended if last frame completed w/ no spare nor strike 
    [InlineData(true, 1, 3)] // Game ended if last frame completed w/ no spare nor strike 
    [InlineData(false, 0, 10)] // Game not ended if last frame completed w/ spare and w/o extra roll
    [InlineData(true, 0, 10, 2)] // Game ended if last frame completed w/ spare and 1 extra roll
    [InlineData(false, 10, 3)] // Game not ended if last frame completed w/ strike and 1 exta roll
    [InlineData(true, 10, 1, 3)] // Game ended if last frame completed w/ strike and 2 exta roll
    public void LastFrameWithExtraRolls_ShouldHasAwaitedEndsGameStatus_Test(bool awaitedEndGameStatus, params int[] rollsPinsDownCount)
    {
        // Arrange
        // Fulfill the first 9 frames (18 rolls)
        for (int i = 0; i < 18; i++)
            _bowlingService.AddRoll(0);

        foreach (var rollPinsDownCount in rollsPinsDownCount)
            _bowlingService.AddRoll(rollPinsDownCount);

        // Act
        var hasGameEnded = _bowlingService.HasGameEnded(_gameId);

        // Assert
        hasGameEnded.Should().Be(awaitedEndGameStatus);
    }

    [Theory]
    [InlineData(true, 0, 8, 2)] // Game ended if last frame completed w/ no spare nor strike 
    [InlineData(true, 0, 10, 2, 3)] // Game ended if last frame completed w/ spare and 1 extra roll
    [InlineData(true, 10, 1, 3, 4)] // Game ended if last frame completed w/ strike and 2 exta roll
    public void AddNewRoll_ToAnEndedGame_ShouldThrowRollAddedUponEndedGameException_Test(bool awaitedEndGameStatus, params int[] rollsPinsDownCount)
    {
        // Arrange
        // Fulfill the first 9 frames (18 rolls)
        for (int i = 0; i < 18; i++)
            _bowlingService.AddRoll(0);

        foreach (var rollPinsDownCount in rollsPinsDownCount)
            _bowlingService.AddRoll(rollPinsDownCount);

        // Check that game is actually ended
        var hasGameEnded = _bowlingService.HasGameEnded(_gameId);
        hasGameEnded.Should().BeTrue();

        // Act
        Action addRoll = () => _bowlingService.AddRoll(0);

        // Assert
        addRoll.Should().Throw<RollAddedUponEndedGameException>();

    }


}