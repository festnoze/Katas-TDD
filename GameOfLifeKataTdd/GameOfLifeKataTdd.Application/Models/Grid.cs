
using GameOfLifeKataTdd.Application.Exceptions;
using System.Drawing;
using System.Runtime.Intrinsics.Arm;

namespace GameOfLifeKataTdd.Application.Models;

public record Grid
{
    private Cell[,] _gridCells;

    private int _generationNumber = 0;
    public int GenerationNumber => _generationNumber;

    public Grid(int width, int height, params Point[]? aliveCells)
    {
        _generationNumber = 0;
        _gridCells = new Cell[width, height];

        // Instanciate each Cell object into the array (with a default value as: death)
        for (int i = 0; i < width; i++)
            for (int j = 0; j < height; j++)
                _gridCells[i, j] = new Cell(aliveCells is null ? false : aliveCells.Contains(new Point(i, j)));
    }

    public int Width => _gridCells.GetLength(0);
    public int Height => _gridCells.GetLength(1);

    public bool IsCellAlive(int cellXCoord, int cellYCoord)
    {
        CheckProvidedCellCoordonatesValidity(cellXCoord, cellYCoord);
        return _gridCells[cellXCoord, cellYCoord].IsAlive;
    }

    public void ChangeIsAliveForCell(int cellXCoord, int cellYCoord, bool isAliveNewValue)
    {
        CheckProvidedCellCoordonatesValidity(cellXCoord, cellYCoord);
        _gridCells[cellXCoord, cellYCoord].SetIsAlive(isAliveNewValue);
    }

    public Grid UpdateGame()
    {
        var newGrid = this.AsClonedGrid();
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                if (MustCellBecomeAlive(x, y))
                {
                    newGrid.ChangeIsAliveForCell(x, y, true);
                    continue;
                }

                if (MustCellStayAlive(x, y))
                    continue;

                // In others cases, alive cells become dead
                if (newGrid.IsCellAlive(x, y))
                    newGrid.ChangeIsAliveForCell(x, y, false);
            }
        }
        _gridCells = newGrid._gridCells;
        _generationNumber++;


        CopyAllCellsFromGrid(newGrid);
        return this;
    }

    private void CopyAllCellsFromGrid(Grid newGrid)
    {
        // Check that grid sizes matchs
        if (this.Width != newGrid.Width || this.Height != newGrid.Height)
            throw new ArgumentException($"Provided grid cannot be used in {nameof(CopyAllCellsFromGrid)} method, as its size doesn't match with current grid");

        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                _gridCells[x, y] = newGrid._gridCells[x, y]; // We can access private property '_gridCells' of newGrid, because we are inside the Grid record
            }
        }
    }

    public bool[,] AsClonedArray()
    {
        var clonedGridArray = new bool[Width, Height];
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                if (_gridCells[x, y].IsAlive)
                    clonedGridArray[x, y] = true;
            }
        }
        
        return clonedGridArray;
    }

    public Grid AsClonedGrid()
    {
        var clonedGrid = new Grid(Width, Height);
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                clonedGrid.SetCell(x, y, _gridCells[x, y] with { });
            }
        }
        
        return clonedGrid;
    }

    private void CheckProvidedCellCoordonatesValidity(int cellXCoord, int cellYCoord)
    {
        if (cellXCoord < 0 || cellXCoord >= Width || cellYCoord < 0 || cellYCoord >= Height)
            throw new OutOfRangeCellCoordonatesException($"Coordonates: ({cellXCoord},{cellXCoord}) doesn't corresponds to any of game of life grid's cells");
    }

    private void SetCell(int x, int y, Cell cell)
    {
        _gridCells[x, y] = cell;
    }


    private bool MustCellStayAlive(int x, int y)
    {
        if (!_gridCells[x, y].IsAlive)
            return false;

        var countAliveAdjacentCells = CountAliveAdjacentCells(x, y);
        if (countAliveAdjacentCells == 2 || countAliveAdjacentCells == 3)
            return true;

        return false;
    }

    private bool MustCellBecomeAlive(int x, int y)
    {
        if (_gridCells[x, y].IsAlive) 
            return false;

        if(CountAliveAdjacentCells(x, y) == 3)
            return true;

        return false;
    }

    private int CountAliveAdjacentCells(int x, int y, bool excludeConcernedCell = true)
    {
        var aliveCellsCounter = 0;

        var minX = x - 1;
        if (minX < 0) minX = 0;

        var maxX = x + 1;
        if (maxX >= Width) maxX = Width - 1;

        var minY = y - 1;
        if (minY < 0) minY = 0;

        var maxY = y + 1;
        if(maxY >= Height) maxY = Height - 1;

        for (int i = minX; i <= maxX; i++)
            for (int j = minY; j <= maxY; j++)
                if (!excludeConcernedCell || i != x || j != y)
                    if (_gridCells[(int)i, j].IsAlive)
                        aliveCellsCounter++;

        return aliveCellsCounter;
    }

    public void AddRandomLivingCells(int livingCellsPercentage)
    {
        var rdm = new Random();

        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                if (rdm.Next(100) < livingCellsPercentage)
                    _gridCells[x, y].SetIsAlive(true);
            }
        }
    }

    public void InsertForm(int x, int y, Grid formToInsert)
    {
        // Don't insert form if there's no room from the specified begininng position
        if (x + formToInsert.Width >= Width || y + formToInsert.Height >= Height)
            return;

        for (int i = 0; i < formToInsert.Width; i++)
            for (int j = 0; j < formToInsert.Height; j++)
                _gridCells[x + i, y + j].SetIsAlive(formToInsert._gridCells[i, j].IsAlive);
    }
}
