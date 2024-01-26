namespace PokerHandsKataTdd.Application.Exceptions;

public class InvalidCardDescriptionSizeException : Exception
{
    public InvalidCardDescriptionSizeException(int actualSize) : base($"Error: Card description should contains 2 char, you provided {actualSize}")
    {}
}
