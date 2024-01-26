namespace PokerHandsKataTdd.Application.Exceptions;

public class InvalidCardValueException : Exception
{
    public InvalidCardValueException(char providedValueChar) : base($"Invalid card value character: {providedValueChar}")
    { }
}
