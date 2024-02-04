using BowlingKata.Services;

namespace BowlingKata.Tests;

public class BowlingScoreTests
{
    private BowlingService _bowlingService;

    public BowlingScoreTests()
    {
        _bowlingService = new BowlingService();    
    }

    [Theory]
    [InlineData(0, 0)]
    [InlineData(1, 20)]
    [InlineData(2, 40)]
    [InlineData(3, 60)]
    public void AllRollSamePinsDownGameTest(int pinsDownCount, int awaitedScore)
    {
        /// Arrange
        _bowlingService.NewGame();

        /// Act
        for (int i = 0; i < 20; i++)
        {
            _bowlingService.Roll(pinsDownCount);
        }

        /// Assert
        Assert.Equal(awaitedScore, _bowlingService.FinalScore());
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
        _bowlingService.NewGame();

        /// Act
        _bowlingService.Roll(firstPinsDownCount);
        _bowlingService.Roll(secondPinsDownCount);
        _bowlingService.Roll(thirdPinsDownCount);

        for (int i = 3; i < 20; i++)
        {
            _bowlingService.Roll(0);
        }

        /// Assert
        Assert.Equal(awaitedScore, _bowlingService.FinalScore());
    }

    [Theory]
    [InlineData(6, 3, 28)]
    [InlineData(7, 0, 24)]
    public void SingleStrikeTest(int firstFollowingPinsDownCount, int secondFollowingPinsDownCount, int awaitedScore)
    {
        /// Arrange
        _bowlingService.NewGame();

        /// Act
        _bowlingService.Roll(10);
        _bowlingService.Roll(firstFollowingPinsDownCount);
        _bowlingService.Roll(secondFollowingPinsDownCount);

        for (int i = 4; i < 20; i++)
        {
            _bowlingService.Roll(0);
        }

        /// Assert
        Assert.Equal(awaitedScore, _bowlingService.FinalScore());
    }

    [Theory]
    [InlineData(6, 3, 25)]
    [InlineData(0, 7, 17)]
    public void SpareOnSecondFrameRollIsntAStrikeTest(int firstFollowingPinsDownCount, int secondFollowingPinsDownCount, int awaitedScore)
    {
        /// Arrange
        _bowlingService.NewGame();

        /// Act
        _bowlingService.Roll(0);
        _bowlingService.Roll(10);
        _bowlingService.Roll(firstFollowingPinsDownCount);
        _bowlingService.Roll(secondFollowingPinsDownCount);

        for (int i = 4; i < 20; i++)
        {
            _bowlingService.Roll(0);
        }

        /// Assert
        Assert.Equal(awaitedScore, _bowlingService.FinalScore());
    }

    [Fact]
    public void SpareAfterStrikeTest()
    {
        /// Arrange
        _bowlingService.NewGame();

        /// Act
        _bowlingService.Roll(10);
        _bowlingService.Roll(7);
        _bowlingService.Roll(3);
        _bowlingService.Roll(4);

        for (int i = 5; i < 20; i++)
        {
            _bowlingService.Roll(0);
        }

        /// Assert
        Assert.Equal(38, _bowlingService.FinalScore());
    }

    [Theory]
    [InlineData(0, 20, 0)] // All zero
    [InlineData(2, 20, 40)] // All roll downs two pins
    [InlineData(5, 21, 150)] // All spares
    [InlineData(10, 12, 300)] // All strikes
    public void AllRollsWithSameResultsTest(int pinsDown, int rolls, int expectedScore)
    {
        /// Arrange
        _bowlingService.NewGame();

        /// Act
        for (int i = 0; i < rolls; i++)
        {
            _bowlingService.Roll(pinsDown);
        }
        var score = _bowlingService.FinalScore();

        /// Assert
        Assert.Equal(expectedScore, score);
    }


    [Theory]
    [InlineData(null, 0)]
    [InlineData(null, 0, 0)]
    [InlineData(46, 0, 1, 2, 3, 4, 5, 0, 1, 2, 3, 4, 5, 0, 1, 2, 3, 4, 5, 0, 1 )]
    [InlineData(61, 0, 1, 2, 3, 4, 5, 0, 1, 2, 3, 4, 5, 0, 1, 2, 3, 4, 5, 10, 3, 3 )]
    public void HasGameEndedTest(int? awaitedFinalScore, params int[] rollResult)
    {
        /// Arrange
        _bowlingService.NewGame();

        /// Act
        for (int i = 0; i < rollResult.Length; i++)
            _bowlingService.Roll(rollResult[i]);

        /// Assert
        if (awaitedFinalScore != null)
        {
            Assert.Equal(awaitedFinalScore, _bowlingService.FinalScore());
            return;
        }

        try
        {
            Assert.Equal(awaitedFinalScore, _bowlingService.FinalScore());
        }
        catch (Exception ex)
        {
            Assert.Null(awaitedFinalScore);
            Assert.Equal("Game hasn't ended, therefore FinalScore cannot be computed", ex.Message);
        }
    }

    [Theory]
    [InlineData(0, 20, false)]
    [InlineData(2, 20, false)]
    [InlineData(3, 21, true)]
    [InlineData(5, 21, false)] // All spares
    [InlineData(5, 22, true)] // All spares
    [InlineData(10, 12, false)] // All strikes
    [InlineData(10, 13, true)] // All strikes
    public void AddRollWhileGameHasEndedTest(int pinsDown, int rolls, bool awaitedFailure)
    {
        /// Arrange
        _bowlingService.NewGame();

        /// Act
        try
        {
            for (int i = 0; i < rolls; i++)
            {
                _bowlingService.Roll(pinsDown);
            }
        
        /// Assert
            Assert.False(awaitedFailure);
        }
        catch (Exception ex)
        {
            Assert.True(awaitedFailure);
            Assert.Equal("Game has already ended, therefore another roll score cannot be added to the score sheet", ex.Message);
        }
    }
}