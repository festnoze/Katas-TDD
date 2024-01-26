using FluentAssertions;
using MineSweeperKata.Models;
using MineSweeperKata.Services;
using System.Drawing;

namespace MineSweeperKata.Tests;

public class CreateNewGameTests
{
    public CreateNewGameTests()
    {
        _mineSweeperService = new MineSweeperService();    
    }

    private MineSweeperService _mineSweeperService;


    [Theory]
    [InlineData(0, 0)]
    [InlineData(1, 1)]
    [InlineData(2, 3)]
    [InlineData(8, 8)]
    [InlineData(20, 20)]
    public void NewGameGrid_HasRequestedSize_Test(int Width, int height)
    {
        /// Act
         _mineSweeperService.CreateNewGameGrid(Width, height);

        /// Assert
        var grid = _mineSweeperService.GetClonedGrid();
        grid.Width.Should().Be(Width);
        grid.Height.Should().Be(height);
    }

    [Theory]
    [InlineData(0, 0, 0)]
    [InlineData(1, 1, 0)]
    [InlineData(3, 3, 1)]
    [InlineData(8, 8, 10)]
    [InlineData(9, 9, 12)]
    [InlineData(12, 5, 9)]
    [InlineData(10, 10, 15)]
    [InlineData(20, 20, 60)]
    public void NewGameGrid_HasRequestedMinesCount_Test(int Width, int height, int awaitedMinesCount)
    {
        /// Act
        _mineSweeperService.CreateNewGameGrid(Width, height);

        /// Assert
        var grid = _mineSweeperService.GetClonedGrid();
        var minesCount = 0;
        foreach (var cell in grid.ClonedCellsArray)
        {
            if (cell.HasMine)
                minesCount++;
        }

        minesCount.Should().Be(awaitedMinesCount);
    }

    [Theory]
    [InlineData(0, 0)]
    [InlineData(1, 1)]
    [InlineData(3, 3)]
    [InlineData(8, 8)]
    [InlineData(12, 5)]
    [InlineData(10, 10)]
    [InlineData(20, 20)]
    public void NewGameGrid_HasAllCellsHidden_Test(int Width, int height)
    {
        /// Act
        _mineSweeperService.CreateNewGameGrid(Width, height);

        /// Assert
        foreach (var cell in _mineSweeperService.GetClonedGrid().ClonedCellsArray)
        {
            cell.IsHidden.Should().BeTrue();
        }
    }
}