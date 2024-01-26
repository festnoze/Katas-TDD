using FluentAssertions;
using MineSweeperKata.Models;
using MineSweeperKata.Services;
using System.Drawing;

namespace MineSweeperKata.Tests;

public class UnmineCellTests
{
    public UnmineCellTests()
    {
        _mineSweeperService = new MineSweeperService();    
    }

    private MineSweeperService _mineSweeperService;


    [Fact]
    public void RevealingCell_OnGripWithoutMines_ShouldRevealAllCells_Test()
    {
        /// Arrange
        _mineSweeperService.CreateNewGameGrid(8, 8, new List<Point>());

        /// Act
        _mineSweeperService.UnmineCell(0, 0);
        var gridCellsArray = _mineSweeperService.GetClonedGrid().ClonedCellsArray;

        /// Assert
        foreach ( var cell in gridCellsArray ) 
        {
            cell.IsHidden.Should().BeFalse();
        }
    }

    [Fact]
    public void Unmining_ACellWithMine_EndsGame_Test()
    {
        /// Arrange
        _mineSweeperService.CreateNewGameGrid(8, 8);

        /// Act
        GameStateEnum result = GameStateEnum.Ongoing;
        Point minePosition = default(Point)!;
        foreach (var cell in  _mineSweeperService.GetClonedGrid().ClonedCellsArray)
        {
            if (cell.HasMine)
            {
                minePosition = cell.Position;
                break;
            }
        }

        result = _mineSweeperService.UnmineCell(minePosition.X, minePosition.Y);

        /// Assert
        result.Should().Be(GameStateEnum.Lose);
    }

    [Fact]
    public void Unmining_AnOutOfGridCell_ShouldThrowAnArgumentException_Test()
    {
        /// Arrange
        _mineSweeperService.CreateNewGameGrid(8, 8);

        /// Act
        var action = () => _mineSweeperService.UnmineCell(8, 2);

        /// Assert
        action.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void PerformAction_OnAnUninitializedGame_ShouldThrowANullArgumentException_Test()
    {
        /// Arrange
        // Forgot to call 'CreateNewGameGrid' method, which create a new game, and the associated grid

        /// Act
        var action = () => _mineSweeperService.UnmineCell(2, 2);

        /// Assert
        action.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void UnminingCellWithoutMine_ShouldRevealAdjacentCellsMinesCounter_Test()
    {
        /// Arrange
        _mineSweeperService.CreateNewGameGrid(8, 8);

        /// Act
        GameStateEnum result = GameStateEnum.Ongoing;

        Point noMinePosition = default(Point)!;
        var gridArray = _mineSweeperService.GetClonedGrid().ClonedCellsArray;
        foreach (var cell in gridArray)
        {
            if (!cell.HasMine && cell.IsHidden && cell.Position.X > 1 && cell.Position.Y > 1)
            {
                noMinePosition = cell.Position;
                break;
            }
        }
        result = _mineSweeperService.UnmineCell(noMinePosition.X, noMinePosition.Y);
        var newGridArray = _mineSweeperService.GetClonedGrid().ClonedCellsArray;

        /// Assert
        result.Should().Be(GameStateEnum.Ongoing);

        for (int x = noMinePosition.X - 1; x <= noMinePosition.X + 1; x++)
        {
            for (int y = noMinePosition.Y - 1; y <= noMinePosition.Y + 1; y++)
            {
                if (newGridArray[x, y].HasMine)
                {

                    newGridArray[x, y].AdjacentMinesCount.Should().BeNull();
                    newGridArray[x, y].IsHidden.Should().BeTrue();
                }
                else
                {
                    newGridArray[x, y].AdjacentMinesCount.Should().NotBeNull();
                    newGridArray[x, y].IsHidden.Should().BeFalse();
                }
            }
        }
    }

    [Fact]
    public void MinesCounterOnAdjcentCellsWork_WithOneMine_OnCellRevealing_Test()
    {
        /// Arrange
        var minesPosition = new List<Point> { new Point(2, 1) };
        _mineSweeperService.CreateNewGameGrid(3, 3, minesPosition);

        /// Act
        _mineSweeperService.UnmineCell(0, 0);
        var modifiedGridCellArray = _mineSweeperService.GetClonedGrid().ClonedCellsArray;
        var modifiedGridIntArray = _mineSweeperService.GetClonedGrid().AsIntArray;

        /// Assert
        modifiedGridCellArray[0, 0].IsHidden.Should().BeFalse();
        modifiedGridCellArray[0, 0].AdjacentMinesCount.Should().Be(0);

        modifiedGridCellArray[0, 1].IsHidden.Should().BeFalse();
        modifiedGridCellArray[0, 1].AdjacentMinesCount.Should().Be(0);

        modifiedGridCellArray[0, 2].IsHidden.Should().BeFalse();
        modifiedGridCellArray[0, 2].AdjacentMinesCount.Should().Be(0);

        modifiedGridCellArray[1, 0].IsHidden.Should().BeFalse();
        modifiedGridCellArray[1, 0].AdjacentMinesCount.Should().Be(1);

        modifiedGridCellArray[1, 1].IsHidden.Should().BeFalse();
        modifiedGridCellArray[1, 1].AdjacentMinesCount.Should().Be(1);

        modifiedGridCellArray[1, 2].IsHidden.Should().BeFalse();
        modifiedGridCellArray[1, 2].AdjacentMinesCount.Should().Be(1);

        modifiedGridCellArray[2, 0].IsHidden.Should().BeTrue();
        modifiedGridCellArray[2, 0].AdjacentMinesCount.Should().BeNull();

        modifiedGridCellArray[2, 1].IsHidden.Should().BeTrue();
        modifiedGridCellArray[2, 1].AdjacentMinesCount.Should().BeNull();

        modifiedGridCellArray[2, 2].IsHidden.Should().BeTrue();
        modifiedGridCellArray[2, 2].AdjacentMinesCount.Should().BeNull();

        modifiedGridIntArray[0, 0].Should().Be(0);
        modifiedGridIntArray[0, 1].Should().Be(0);
        modifiedGridIntArray[0, 2].Should().Be(0);
        modifiedGridIntArray[1, 0].Should().Be(1);
        modifiedGridIntArray[1, 1].Should().Be(1);
        modifiedGridIntArray[1, 2].Should().Be(1);
        modifiedGridIntArray[2, 0].Should().BeNull();
        modifiedGridIntArray[2, 1].Should().BeNull();
        modifiedGridIntArray[2, 2].Should().BeNull();
    }


    [Fact]
    public void RevealAdjacentMine_LetMinedCellHidden_Test()
    {
        /// Arrange
        var minesPosition = new List<Point> { new Point(1, 1) };
        _mineSweeperService.CreateNewGameGrid(2, 2, minesPosition);

        /// Act
        _mineSweeperService.UnmineCell(0, 0);
        var modifiedGridCellArray = _mineSweeperService.GetClonedGrid().ClonedCellsArray;

        /// Assert
        modifiedGridCellArray[0, 0].IsHidden.Should().BeFalse();
        modifiedGridCellArray[0, 0].AdjacentMinesCount.Should().Be(1);
        modifiedGridCellArray[0, 1].IsHidden.Should().BeFalse();
        modifiedGridCellArray[0, 1].AdjacentMinesCount.Should().Be(1);
        modifiedGridCellArray[1, 0].IsHidden.Should().BeFalse();
        modifiedGridCellArray[1, 0].AdjacentMinesCount.Should().Be(1);
        modifiedGridCellArray[1, 1].IsHidden.Should().BeTrue();
        modifiedGridCellArray[1, 1].AdjacentMinesCount.Should().BeNull();
    }

    [Fact]
    public void RevealAlreadyRevealedCell_MakeNoChanges_Test()
    {
        /// Arrange
        var minesPosition = new List<Point> { new Point(1, 1) };
        _mineSweeperService.CreateNewGameGrid(2, 2, minesPosition);
        _mineSweeperService.UnmineCell(0, 0);
        var previousGridState = _mineSweeperService.GetClonedGrid();

        /// Act
        _mineSweeperService.UnmineCell(0, 1);
        var newGridState = _mineSweeperService.GetClonedGrid();

        /// Assert
        newGridState.IsSameGridAs(previousGridState).Should().BeTrue();
    }

    [Fact]
    public void MinesCounterOnAdjcentCellsWork_WithThreeMine_OnCellRevealing_Test()
    {
        /// Arrange
        var minesPositions = new List<Point>
                                {
                                    new Point(1, 0),
                                    new Point(0, 1),
                                    new Point(1, 1),
                                    new Point(2, 1),
                                    new Point(1, 2),
                                };
        _mineSweeperService.CreateNewGameGrid(3, 3, minesPositions);

        /// Act
        _mineSweeperService.UnmineCell(0, 0);

        /// Assert
        var modifiedGridCellArray = _mineSweeperService.GetClonedGrid().ClonedCellsArray;
        var modifiedGridIntArray = _mineSweeperService.GetClonedGrid().AsIntArray;

        modifiedGridCellArray[0,0].IsHidden.Should().BeFalse();
        modifiedGridCellArray[0,0].AdjacentMinesCount.Should().Be(3);
        modifiedGridCellArray[0,1].IsHidden.Should().BeTrue();
        modifiedGridCellArray[0,1].AdjacentMinesCount.Should().BeNull();
        modifiedGridCellArray[1,0].IsHidden.Should().BeTrue();
        modifiedGridCellArray[1,0].AdjacentMinesCount.Should().BeNull();
        modifiedGridCellArray[1,1].IsHidden.Should().BeTrue();
        modifiedGridCellArray[1,1].AdjacentMinesCount.Should().BeNull();
        modifiedGridCellArray[2,2].IsHidden.Should().BeTrue();
        modifiedGridCellArray[2,2].AdjacentMinesCount.Should().BeNull();

        modifiedGridIntArray[0,0].Should().Be(3);
        modifiedGridIntArray[0,1].Should().BeNull();
        modifiedGridIntArray[1,0].Should().BeNull();
        modifiedGridIntArray[1,1].Should().BeNull();
        modifiedGridIntArray[2,2].Should().BeNull();
    }

    [Fact]
    public void FlaggedCell_RemainsFlagged_WhenUnmineCellAdjcentCells_Test()
    {
        /// Arrange
        var minesPositions = new List<Point> { new Point(1, 0) };
        _mineSweeperService.CreateNewGameGrid(3, 3, minesPositions);
        _mineSweeperService.CellChangeFlag(1, 0);

        /// Act
        _mineSweeperService.UnmineCell(0, 0);

        /// Assert
        var modifiedGridCellArray = _mineSweeperService.GetClonedGrid().ClonedCellsArray;
        modifiedGridCellArray[1, 0].HasFlag.Should().BeTrue();
    }
}