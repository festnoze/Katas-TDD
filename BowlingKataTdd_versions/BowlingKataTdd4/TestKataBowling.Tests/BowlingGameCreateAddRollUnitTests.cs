using FluentAssertions;

namespace TestKataBowling.Tests;

public class BowlingGameCreateAddRollUnitTests : IDisposable
{
    private readonly BowlingService _bowlingService;

    public BowlingGameCreateAddRollUnitTests()
    {
        _bowlingService = new BowlingService();
    }

    public void Dispose()
    {
    }

    [Fact]
    public void CreateNewGame_ShouldSucceed_Test()
    {
        // Arrange

        // Act
        var gameId = _bowlingService.CreateGame();

        // Assert
        gameId.Should().NotBe(Guid.Empty);
    }

    [Fact]
    public void AddNewRoll_ShouldSucceed_Test()
    {
        // Arrange
        var gameId = _bowlingService.CreateGame();

        // Act
        _bowlingService.AddRoll(5);

        // Assert
    }

    [Theory]
    [InlineData(11)]
    [InlineData(-1)]
    [InlineData(-154)]
    [InlineData(11000)]
    public void AddNewRollWithInvalidPinsCount_ShouldThrowAnInvalidPinsCountException_Test(int downPinsCount)
    {
        // Arrange
        var gameId = _bowlingService.CreateGame();

        // Act
        var action = () => _bowlingService.AddRoll(downPinsCount);

        // Assert
        action.Should().Throw<InvalidPinsCountException>();
    }
}