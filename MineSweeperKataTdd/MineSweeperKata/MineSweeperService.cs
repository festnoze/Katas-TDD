using MineSweeperKata.Models;
using System.Drawing;

namespace MineSweeperKata.Services;

public class MineSweeperService
{
    private Grid grid => _grid ?? throw new ArgumentNullException(nameof(grid));
    private Grid? _grid = null;
    public bool IsGameInitialized => _grid is not null;

    public Grid GetClonedGrid() => grid.GetClonedGrid();

    public void CreateNewGameGrid(int width = 9, int height = 9, List<Point>? externallyProvidedMinesPositions = null)
    {
        // Create positions for mines
        List<Point> minesPositions;
        if (externallyProvidedMinesPositions is null)
            minesPositions = GetRandomMinesPositions(width, height);
        else
            minesPositions = externallyProvidedMinesPositions;
        
        // Prepare the new grid with mines
        _grid = new Grid(width, height, minesPositions);
    }

    public virtual List<Point> GetRandomMinesPositions(int width, int height)
    {
        var rdm = new Random();
        var allGridMinesPositions = new List<Point>();
        var cellsCount = width * height;
        var minesCount = Convert.ToInt32(Math.Round(cellsCount * 0.15));
        Point currentMinePosition;

        for (int mineNbr = 0; mineNbr < minesCount; mineNbr++)
        {
            do currentMinePosition = new Point(rdm.Next(width), rdm.Next(height));
            while (currentMinePosition == new Point(0,0) || allGridMinesPositions.Contains(currentMinePosition));

            allGridMinesPositions.Add(currentMinePosition);
        }

        return allGridMinesPositions;
    }

    public GameStateEnum UnmineCell(int x, int y)
    {
        return grid.UnmineCell(x, y);
    }

    public int? CellAdjacentMinesCount(int x, int y)
    {
        return grid.CellAdjacentMinesCount(x, y);
    }

    public bool CellIsHidden(int x, int y)
    {
        return grid.CellIsHidden(x, y);
    }

    public bool CellHasMine(int x, int y)
    {
        return grid.CellHasMine(x, y);
    }

    public bool CellHasFlag(int x, int y)
    {
        return grid.CellHasFlag(x, y);
    }

    public void CellChangeFlag(int x, int y)
    {
        grid.CellChangeFlag(x, y);
    }


}
