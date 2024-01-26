using FluentAssertions;
using GameOfLifeKataTdd.Application.Services;
using System.Drawing;

namespace GameOfLifeKataTdd.Tests;
public class GridUpdationTests
{

    private GameOfLifeService service;
    public GridUpdationTests()
    {
        service = new GameOfLifeService();
    }

    [Theory]
    [InlineData(2, 2, new int[] { 1, 0, 0, 1, 1, 1 }, new int[] { 0, 0 })] // test left upper corner cell
    [InlineData(2, 3, new int[] { 1, 0, 0, 1, 1, 1 }, new int[] { 0, 0 })] // test left upper corner cell (w/ other grid size)
    [InlineData(3, 3, new int[] { 1, 0, 1, 1, 2, 1 }, new int[] { 2, 0 })] // test right upper corner cell
    [InlineData(3, 3, new int[] { 1, 2, 0, 1, 1, 1 }, new int[] { 0, 2 })] // test left bottom corner cell
    [InlineData(3, 3, new int[] { 1, 2, 2, 1, 1, 1 }, new int[] { 2, 2 })] // test right bottom corner cell
    [InlineData(3, 3, new int[] { 2, 2, 2, 1, 1, 2 }, new int[] { 1, 1 })] // test center cell
    [InlineData(3, 3, new int[] { 1, 0, 0, 1, 2, 2 }, new int[] { 1, 1 })] // test center cell (w/ other alive cells)
    public void DeadCell_BecomeAlive_IfItAsExactly3AliveNeighbors_Test(int width, int height, int[] livingCellsCoordonates, int[] awaitedNewLivingCellsCoordonates)
    {
        /// Arrange
        var grid = service.CreateNewGameOfLifeGrid(width, height, Helpers.GetCellsPositionsList(livingCellsCoordonates));
        var awaitedNewLivingCellsPositions = Helpers.GetCellsPositionsList(awaitedNewLivingCellsCoordonates);
        foreach (var cell in awaitedNewLivingCellsPositions)
            grid[cell.X, cell.Y].Should().BeFalse();

        /// Act
        var newGrid = service.UpdateGame();

        /// Assert
        foreach (var cell in awaitedNewLivingCellsPositions)
            newGrid[cell.X, cell.Y].Should().BeTrue();
    }


    [Theory]
    [InlineData(2, 2, new int[] { 0, 0, 0, 1, 1, 1 }, new int[] { 0, 0 })] // test left upper corner cell w/ 2 neighbors living cells
    [InlineData(2, 3, new int[] { 0, 0, 1, 0, 1, 1 }, new int[] { 0, 0 })] // test left upper corner cell w/ 2 neighbors living cells (w/ other grid size & living cells positions)
    [InlineData(3, 3, new int[] { 2, 0, 1, 1, 2, 1 }, new int[] { 2, 0 })] // test right upper corner cell w/ 2 neighbors living cells
    [InlineData(3, 3, new int[] { 0, 2, 0, 1, 1, 1 }, new int[] { 0, 2 })] // test left bottom corner cell w/ 2 neighbors living cells
    [InlineData(3, 3, new int[] { 2, 2, 2, 1, 1, 1 }, new int[] { 2, 2 })] // test right bottom corner cell w/ 2 neighbors living cells
    [InlineData(3, 3, new int[] { 1, 1, 2, 1, 1, 2 }, new int[] { 1, 1 })] // test center cell w/ 2 neighbors living cells
    [InlineData(3, 3, new int[] { 1, 1, 0, 1, 2, 2 }, new int[] { 1, 1 })] // test center cell w/ 2 neighbors living cells (w/ other alive cells)
    //
    [InlineData(2, 2, new int[] { 0, 0, 0, 1, 1, 1, 1, 0 }, new int[] { 0, 0 })] // test left upper corner cell w/ 3 neighbors living cells
    [InlineData(2, 3, new int[] { 0, 0, 1, 0, 1, 1, 0, 1 }, new int[] { 0, 0 })] // test left upper corner cell w/ 3 neighbors living cells (w/ other grid size & living cells positions)
    [InlineData(3, 3, new int[] { 2, 0, 1, 1, 2, 1, 1, 0 }, new int[] { 2, 0 })] // test right upper corner cell w/ 3 neighbors living cells
    [InlineData(3, 3, new int[] { 0, 2, 0, 1, 1, 1, 1, 2 }, new int[] { 0, 2 })] // test left bottom corner cell w/ 3 neighbors living cells
    [InlineData(3, 3, new int[] { 2, 2, 2, 1, 1, 1, 1, 2 }, new int[] { 2, 2 })] // test right bottom corner cell w/ 3 neighbors living cells
    [InlineData(3, 3, new int[] { 1, 1, 2, 1, 1, 2, 0, 0 }, new int[] { 1, 1 })] // test center cell w/ 3 neighbors living cells
    [InlineData(3, 3, new int[] { 1, 1, 0, 1, 2, 2, 1, 0 }, new int[] { 1, 1 })] // test center cell w/ 3 neighbors living cells (w/ other alive cells)
    public void LivingCell_StayAlive_IfItAsExactly2Or3AliveNeighbors_Test(int width, int height, int[] livingCellsCoordonates, int[] awaitedNewLivingCellsCoordonates)
    {
        /// Arrange
        var grid = service.CreateNewGameOfLifeGrid(width, height, Helpers.GetCellsPositionsList(livingCellsCoordonates));
        var awaitedNewLivingCellsPositions = Helpers.GetCellsPositionsList(awaitedNewLivingCellsCoordonates);
        foreach (var cell in awaitedNewLivingCellsPositions)
            grid[cell.X, cell.Y].Should().BeTrue();

        /// Act
        var newGrid = service.UpdateGame();

        /// Assert
        foreach (var cell in awaitedNewLivingCellsPositions)
            newGrid[cell.X, cell.Y].Should().BeTrue();
    }


    // Too overcrowded cells
    [Theory]
    [InlineData(3, 3, new int[] { 1, 1, 2, 1, 1, 2, 0, 0 , 0, 1}, new int[] { 1, 1 })] // test center cell w/ 4 neighbors living cells
    [InlineData(3, 3, new int[] { 1, 1, 0, 1, 2, 2, 1, 0, 0, 0 }, new int[] { 1, 1 })] // test center cell w/ 4 neighbors living cells (w/ other alive cells)
    [InlineData(3, 3, new int[] { 1, 1, 2, 1, 1, 2, 0, 0, 0, 1, 2, 2 }, new int[] { 1, 1 })] // test center cell w/ 5 neighbors living cells
    [InlineData(3, 3, new int[] { 1, 1, 0, 1, 2, 2, 1, 0, 0, 2, 2, 1 }, new int[] { 1, 1 })] // test center cell w/ 5 neighbors living cells (w/ other alive cells)
    [InlineData(3, 3, new int[] { 1, 1, 2, 1, 1, 2, 0, 0, 0, 1, 2, 2, 1, 0 }, new int[] { 1, 1 })] // test center cell w/ 6 neighbors living cells
    [InlineData(3, 3, new int[] { 1, 1, 2, 1, 1, 2, 0, 0, 0, 1, 2, 2, 1, 0, 2, 0 }, new int[] { 1, 1 })] // test center cell w/ 7 neighbors living cells
    [InlineData(3, 3, new int[] { 0, 0, 0, 1, 1, 0, 1, 1, 2, 0, 0, 2, 2, 1, 1, 2, 2, 2 }, new int[] { 1, 1 })] // test center cell w/ 8 neighbors living cells
    public void LivingCell_DiesOut_IfItAsMoreThan3AliveNeighbors_Test(int width, int height, int[] livingCellsCoordonates, int[] awaitedNewDeadCellsCoordonates)
    {
        Helpers.ShouldDieCellTest(service, width, height, livingCellsCoordonates, awaitedNewDeadCellsCoordonates);
    }

    // Too empty
    [InlineData(3, 3, new int[] { 1, 1, 2, 1 }, new int[] { 1, 1 })] // test center cell w/ 1 neighbor living cells
    [InlineData(3, 3, new int[] { 1, 1, 0, 1 }, new int[] { 1, 1 })] // test center cell w/ 1 neighbor living cells (w/ other alive cell)
    [InlineData(3, 3, new int[] { 1, 1 }, new int[] { 1, 1 })] // test center cell w/ 0 neighbor living cells
    public void LivingCell_DiesOut_IfItAsLessThan2AliveNeighbors_Test(int width, int height, int[] livingCellsCoordonates, int[] awaitedNewDeadCellsCoordonates)
    {
        Helpers.ShouldDieCellTest(service, width, height, livingCellsCoordonates, awaitedNewDeadCellsCoordonates);
    }
}
