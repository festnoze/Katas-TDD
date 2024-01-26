using FluentAssertions.Common;
using FluentAssertions;
using System.Drawing;
using GameOfLifeKataTdd.Application.Services;

namespace GameOfLifeKataTdd.Tests;
public static class Helpers
{
    public static void GenericCreateNewGameOfLifeTest(GameOfLifeService service, int width, int height, Type? awaitedExceptionType = null, params int[]? livingCellsCoordonates)
    {
        /// Arrange
        bool[,]? newGrid = null;
        List<Point> livingCellsPositions = GetCellsPositionsList(livingCellsCoordonates);

        var createNewGameAction = () => service.CreateNewGameOfLifeGrid(width, height, livingCellsPositions);

        /// Act
        if (awaitedExceptionType is null)
            newGrid = createNewGameAction();

        /// Assert
        if (awaitedExceptionType is not null)
            createNewGameAction.Should().Throw<Exception>().Which.Should().BeOfType(awaitedExceptionType);
        else
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (livingCellsPositions.Contains(new Point(x, y)))
                        newGrid![x, y].Should().BeTrue();
                    else
                        newGrid![x, y].Should().BeFalse();
                }
            }
            return;
        }
    }

    public static List<Point> GetCellsPositionsList(int[]? livingCellsCoordonates)
    {
        var livingCellsPositions = new List<Point>();
        if (livingCellsCoordonates is not null)
        {
            if (livingCellsCoordonates.Length % 2 != 0)
                throw new ArgumentException("Le nombre de coordonnées doit être pair pour représenter des couples de coordonnées");

            for (int index = 0; index < livingCellsCoordonates.Length; index += 2)
            {
                var newPosition = new Point(livingCellsCoordonates[index], livingCellsCoordonates[index + 1]);
                if (livingCellsPositions.Contains(newPosition))
                    throw new ArgumentException($"Already existing position ({newPosition.X},{newPosition.Y}) cannot be added twice to the positions' list");

                livingCellsPositions.Add(newPosition);
            }
        }

        return livingCellsPositions;
    }

    public static void ShouldDieCellTest(GameOfLifeService service, int width, int height, int[] livingCellsCoordonates, int[] awaitedNewDeadCellsCoordonates)
    {
        /// Arrange
        var grid = service.CreateNewGameOfLifeGrid(width, height, Helpers.GetCellsPositionsList(livingCellsCoordonates));
        var awaitedNewLivingCellsPositions = Helpers.GetCellsPositionsList(awaitedNewDeadCellsCoordonates);
        foreach (var cell in awaitedNewLivingCellsPositions)
            grid[cell.X, cell.Y].Should().BeTrue();

        /// Act
        var newGrid = service.UpdateGame();

        /// Assert
        foreach (var cell in awaitedNewLivingCellsPositions)
            newGrid[cell.X, cell.Y].Should().BeFalse();
    }
}