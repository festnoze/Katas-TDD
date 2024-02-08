using BowlingScore.Domain.Exceptions;

namespace BowlingScore.Domain;

public record Roll
{
    public int DownPinsCount { get; init; }
    public Roll(int downPinsCount)
    {
        if (downPinsCount < 0 || downPinsCount > 10)
            throw new WrongDownPinsCountException();

        DownPinsCount = downPinsCount;
    }
}
