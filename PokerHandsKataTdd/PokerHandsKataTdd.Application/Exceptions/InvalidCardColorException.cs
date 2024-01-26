namespace PokerHandsKataTdd.Application.Exceptions;

public class InvalidCardColorException : Exception
{
    public InvalidCardColorException(char providedColorChar) : base($"Invalid card color character: {providedColorChar}")
    {}    
}
