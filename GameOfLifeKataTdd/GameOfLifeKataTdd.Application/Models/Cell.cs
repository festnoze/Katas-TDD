namespace GameOfLifeKataTdd.Application.Models;

public record Cell
{
    public Cell(bool isAlive = false)
    {
        SetIsAlive(isAlive);
    }

    private bool _isAlive;

    public bool IsAlive => _isAlive;

    public void SetIsAlive(bool isAlive)
    {
        _isAlive = isAlive;
    }
}
