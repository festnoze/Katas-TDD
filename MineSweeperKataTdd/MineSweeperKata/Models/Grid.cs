using System.Diagnostics.Metrics;
using System.Drawing;

namespace MineSweeperKata.Models;

public record Grid
{
    public Grid(int width, int height, List<Point> minesPositions)
    {
        Cells = new List<Cell>();
        for (int x = 0; x < width; x++)
            for (int y = 0; y < height; y++)
            {
                var hasMine = minesPositions.Any(mp => mp.X == x && mp.Y == y);
                Cells.Add(new Cell(x, y, hasMine));
            }
    }

    public Grid(Grid gridToClone)
    {
        Cells = new List<Cell>();
        gridToClone.Cells.ForEach(c => Cells.Add(c with { }));
    }

    public Grid GetClonedGrid()
    {
        return new Grid(this);

    }

    public bool CellIsHidden(int x, int y)
    {
        return Cells.Single(c => c.Position == new Point(x, y)).IsHidden;
    }

    public bool CellHasMine(int x, int y)
    {
        return Cells.Single(c => c.Position == new Point(x, y)).HasMine;
    }

    public bool CellHasFlag(int x, int y)
    {
        return Cells.Single(c => c.Position == new Point(x, y)).HasFlag;
    }

    public void CellChangeFlag(int x, int y)
    {
        Cells.Single(c => c.Position == new Point(x, y)).ChangeCellFlag();
    }

    public int? CellAdjacentMinesCount(int x, int y)
    {
        return Cells.Single(c => c.Position == new Point(x, y)).AdjacentMinesCount;
    }

    private List<Cell> Cells { get; set; }
    public int Width => Cells is null || !Cells.Any() ? 0 : Cells.Max(c => c.Position.X + 1);
    public int Height => Cells is null || !Cells.Any() ? 0 : Cells.Max(c => c.Position.Y + 1);

    public Cell[,] ClonedCellsArray
    {
        get
        {
            var clonedCellsArray = new Cell[Width, Height];
            foreach (var cell in GetClonedGrid().Cells)
                clonedCellsArray[cell.Position.X, cell.Position.Y] = cell;
            return clonedCellsArray;
        }
    }

    /// <summary>
    /// Return an array containing int?, which means:
    /// - 0 no adjacent mines
    /// - 1-8 nbr of adjacent mines
    /// - null hidden cell
    /// </summary>
    public int?[,] AsIntArray
    {
        get
        {
            var grid = new int?[Width, Height];
            foreach (var cell in Cells)
                grid[cell.Position.X, cell.Position.Y] = cell.AdjacentMinesCount;
            return grid;
        }
    }

    private delegate void CellAction(int x, int y);

    private void ActionOnAllAdjacentCells(CellAction methodToInvoke, int cellX, int cellY, bool minedCellsOnly = false)
    {
        for (int x = cellX - 1; x <= cellX + 1; x++)
            for (int y = cellY - 1; y <= cellY + 1; y++)
                if (x >= 0 && x < Width && y >= 0 && y < Height)
                    if (x != cellX || y != cellY)
                        if (!minedCellsOnly || Cells.Single(c => c.Position == new Point(x, y)).HasMine)
                            methodToInvoke(x, y);
    }
    
    public int GetCountOfAdjacentCellsWithMines(int cellX, int cellY)
    {
        int counter = 0;
        void IncrementCounter(int cellX, int cellY) { counter++; }

        ActionOnAllAdjacentCells(IncrementCounter, cellX, cellY, true);

        return counter;
    }

    public GameStateEnum UnmineCell(int cellX, int cellY, bool loseIfMineRevealed = true)
    {
        ValidateGridCoordonates(cellX, cellY);

        var cellToUnmine = Cells.Single(c => c.Position == new Point(cellX, cellY));

        if (cellToUnmine.HasMine)
        {
            if (loseIfMineRevealed)
                return GameStateEnum.Lose;
            else
                return GameStateEnum.Ongoing;
        }
        RevealCell(cellX, cellY);
        ActionOnAllAdjacentCells(RevealCell, cellX, cellY);

        if (!Cells.Any(c => c.IsHidden && !c.HasMine))
            return GameStateEnum.Won;

        return GameStateEnum.Ongoing;
    }

    private void ValidateGridCoordonates(int cellX, int cellY)
    {
        if (cellX < 0 || cellX > Width - 1)
            throw new ArgumentException($"Provided {nameof(cellX)} value: {cellX} while calling method: {nameof(UnmineCell)} isn't within the grid range");
        if (cellY < 0 || cellY > Height - 1)
            throw new ArgumentException($"Provided {nameof(cellY)} value: {cellY} while calling method: {nameof(UnmineCell)} isn't within the grid range");
    }

    private void RevealCell(int x, int y)
    {
        var cellToReveal = Cells.Single(c => c.Position == new Point(x, y));
        if (cellToReveal.AdjacentMinesCount.HasValue) return;
        if (cellToReveal.HasMine) return;
     
        var adjacentCellsWithMinesCount = GetCountOfAdjacentCellsWithMines(x, y);
        cellToReveal.RevealCell(adjacentCellsWithMinesCount);
        if (adjacentCellsWithMinesCount == 0)
        {
            UnmineCell(x, y, false);
        }
    }

    // Allow to compare two grids by comparing each of their cells' states
    public bool IsSameGridAs(Grid otherGrid)
    {
        return Enumerable.SequenceEqual(Cells, otherGrid.Cells);
    }
}
