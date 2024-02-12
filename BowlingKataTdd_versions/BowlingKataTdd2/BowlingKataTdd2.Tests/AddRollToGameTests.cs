using BowlingKataTdd2.Application;
using BowlingKataTdd2.Application.Exceptions;
using FluentAssertions;

namespace BowlingKataTdd2.Tests;

public class AddRollToGameTests
{
    private BowlingGameService _service;

    public AddRollToGameTests()
    {
        _service = new BowlingGameService();
        _service.CreateNewGame();
    }

    [Theory]
    [InlineData(11)]
    [InlineData(15)]
    [InlineData(-1)]
    [InlineData(-5)]
    public void AddSingleRoll_WithWrongDownPinsCount_ShouldThrowWrongDownPinsCountException_Test(int downPinsCount)
    {
        // Arrange
        _service.CreateNewGame();

        // Act
        var action = () => _service.GetScore(downPinsCount);

        // Assert
        action.Should().ThrowExactly<WrongDownPinsCountException>();
    }

    [Theory]
    [InlineData(0, 0)]
    [InlineData(1, 20)]
    [InlineData(2, 40)]
    [InlineData(3, 60)]
    public void AllRollSamePinsDownGameTest(int pinsDownCount, int awaitedScore)
    {
        /// Act
        for (int i = 0; i < 20; i++)
        {
            _service.GetScore(pinsDownCount);
        }

        /// Assert
        Assert.Equal(awaitedScore, _service.GetScore());
    }

    [Theory]
    [InlineData(6, 4, 3, 16)]
    [InlineData(7, 3, 4, 18)]
    [InlineData(5, 5, 5, 20)]
    [InlineData(9, 1, 0, 10)]
    public void SingleSpareTest(int firstPinsDownCount, int secondPinsDownCount, int thirdPinsDownCount, int awaitedScore)
    {
        // Input data validation
        Assert.Equal(10, firstPinsDownCount + secondPinsDownCount); // Check that it's actually a spare

        /// Arrange

        /// Act
        _service.GetScore(firstPinsDownCount);
        _service.GetScore(secondPinsDownCount);
        _service.GetScore(thirdPinsDownCount);

        for (int i = 3; i < 20; i++)
        {
            _service.GetScore(0);
        }

        /// Assert
        Assert.Equal(awaitedScore, _service.GetScore());
    }

    [Theory]
    [InlineData(6, 3, 28)]
    [InlineData(7, 0, 24)]
    public void SingleStrikeTest(int firstFollowingPinsDownCount, int secondFollowingPinsDownCount, int awaitedScore)
    {
        /// Arrange
        _service.GetScore(10);
        _service.GetScore(firstFollowingPinsDownCount);
        _service.GetScore(secondFollowingPinsDownCount);

        for (int i = 4; i < 20; i++)
        {
            _service.GetScore(0);
        }

        /// Assert
        Assert.Equal(awaitedScore, _service.GetScore());
    }

    [Theory]
    [InlineData(6, 3, 25)]
    [InlineData(0, 7, 17)]
    public void SpareOnSecondFrameRollIsntAStrikeTest(int firstFollowingPinsDownCount, int secondFollowingPinsDownCount, int awaitedScore)
    {
        /// Arrange
        _service.GetScore(0);
        _service.GetScore(10);
        _service.GetScore(firstFollowingPinsDownCount);
        _service.GetScore(secondFollowingPinsDownCount);

        for (int i = 4; i < 20; i++)
        {
            _service.GetScore(0);
        }

        /// Assert
        Assert.Equal(awaitedScore, _service.GetScore());
    }

    [Fact]
    public void SpareAfterStrikeTest()
    {
        /// Arrange
        _service.GetScore(10);
        _service.GetScore(7);
        _service.GetScore(3);
        _service.GetScore(4);

        for (int i = 5; i < 20; i++)
        {
            _service.GetScore(0);
        }

        /// Assert
        Assert.Equal(38, _service.GetScore());
    }

    [Theory]
    [InlineData(0, 20, 0)] // All zero
    [InlineData(2, 20, 40)] // All roll downs two pins
    [InlineData(5, 21, 150)] // All spares
    [InlineData(10, 12, 300)] // All strikes
    public void AllRollsWithSameResultsTest(int pinsDown, int rolls, int expectedScore)
    {
        /// Arrange
        for (int i = 0; i < rolls; i++)
        {
            _service.GetScore(pinsDown);
        }
        var score = _service.GetScore();

        /// Assert
        Assert.Equal(expectedScore, score);
    }

}