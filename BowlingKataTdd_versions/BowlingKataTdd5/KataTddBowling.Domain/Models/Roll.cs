using KataTddBowling.Domain.Exceptions;

namespace KataTddBowling.Domain.Models;

public record Roll
{
    public int DownPinsCount { get; init; }

    public Roll(int downPinsCount)
    {
        if (downPinsCount < 0 || downPinsCount > 10)
            throw new WrongRollValueException();

        DownPinsCount = downPinsCount;
    }
}
