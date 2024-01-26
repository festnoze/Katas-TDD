using System.Drawing;

namespace MineSweeperKata.Models;

public record Cell
{
    public Cell(int cellX, int cellY, bool hasMine = false)
    {
        Position = new Point(cellX, cellY);
        if (hasMine) 
            _hasMine = true;
    }

    // Public props
    public Point Position { get; set; }
    public bool HasMine => _hasMine;
    public bool IsHidden => AdjacentMinesCount is null;
    

    public bool HasFlag => _hasFlag;
    public int? AdjacentMinesCount => _adjacentMinesCount;

    // Public methods

    public void ChangeCellFlag()
    {
        _hasFlag = !_hasFlag;
    }

    public void RevealCell(int adjacentMines)
    {
        if (AdjacentMinesCount is not null)
            throw new InvalidOperationException($"Unneeded recalculation of an already known AdjacentMinesCount");

        _adjacentMinesCount = adjacentMines;
    }

    // Private fields
    private int? _adjacentMinesCount = null;
    private bool _hasMine = false;
    private bool _hasFlag = false;
}
