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
    }


    [Fact]
    public void AddRollToNewGame_ShouldSucceed_Test()
    {
        // Arrange
        _service.CreateNewGame();

        // Act
        _service.AddRoll(5);

        // Assert
        // Don't throw exception
    }

    [Fact]
    public void AddRoll_WithNoPriorCallToNewGame_ShouldThrowNotInitializedGameException_Test()
    {
        // Arrange
        // No CreateNewGame method call

        // Act
        var action = () => _service.AddRoll(5);

        // Assert
        action.Should().Throw<NotInitializedGameException>();
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
        var action = () => _service.AddRoll(downPinsCount);

        // Assert
        action.Should().ThrowExactly<WrongDownPinsCountException>();
    }


    [Fact]
    public void AddSingleRollToNewGame_ScoreShouldEqualsRollValue_Test()
    {
        // Arrange

        // Act

        // Assert

    }

}