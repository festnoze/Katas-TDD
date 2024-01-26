using PokerHandsKataTdd.Application.Exceptions;
using PokerHandsKataTdd.Application.Models.Enums;

namespace PokerHandsKataTdd.Application.Models;

public record CardColor
{
    public CardColorEnum Color { get; }
    public string StringColor => Color.ToString();

    public CardColor(CardColorEnum color)
    {
        Color = color;
    }

    public CardColor(char charColor)
    {
        if (Enum.IsDefined(typeof(CardColorEnum), (int)charColor))
            Color = (CardColorEnum)charColor;
        else if (Enum.IsDefined(typeof(CardColorSymbolsEnum), (int)charColor))
            Color = Enum.Parse<CardColorEnum>(((CardColorSymbolsEnum)charColor).ToString());
        else
            throw new InvalidCardColorException(charColor);

    }
}

