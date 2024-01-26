using GameOfLifeKataTdd.Application.Exceptions;
using GameOfLifeKataTdd.Application.Models;
using System.Drawing;

namespace GameOfLifeKataTdd.Application.Services;

public class GameOfLifeService
{
    public GameOfLifeService()
    { }

    private Grid? _gameGrid;

    public bool[,] CreateNewGameOfLifeGrid(int width, int height, int aliveCellsPercentage)
    {
        var newGrid = new Grid(width, height);
        newGrid.AddRandomLivingCells(aliveCellsPercentage);
        _gameGrid = newGrid;

        return _gameGrid.AsClonedArray();
    }

    public bool[,] CreateNewGameOfLifeGrid(int width, int height, IEnumerable<Point>? livingCellsPositions = null)
    {
        var newGrid = new Grid(width, height);

        if (livingCellsPositions is not null)
        {
            foreach (var livingCell in livingCellsPositions)
            {
                newGrid.ChangeIsAliveForCell(livingCell.X, livingCell.Y, true);
            }
        }
        else
        {
            newGrid.AddRandomLivingCells(20);
        }
        _gameGrid = newGrid;

        return _gameGrid.AsClonedArray();
    }

    public bool[,] UpdateGame()
    {
        if (_gameGrid is null)
            return new bool[0,0];

        var newGrid = _gameGrid!.UpdateGame();
        _gameGrid = newGrid;

        return _gameGrid!.AsClonedArray();
    }

    public bool IsCellAlive(int x, int y)
    {
        return _gameGrid?.IsCellAlive(x, y) ?? false;
    }

    public int GetGenerationNumber()
    {
        return _gameGrid?.GenerationNumber ?? -1;
    }

    public void InsertForm(int x, int y, Grid formToInsert)
    {
        if (_gameGrid is null) return;

        _gameGrid.InsertForm(x, y, formToInsert);
    }
}
