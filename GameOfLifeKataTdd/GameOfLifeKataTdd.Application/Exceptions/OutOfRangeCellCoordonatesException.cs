namespace GameOfLifeKataTdd.Application.Exceptions;

public class OutOfRangeCellCoordonatesException : Exception
{
    public OutOfRangeCellCoordonatesException(string message) : base(message)
    {        
    }
}
