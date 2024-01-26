using PokerHandsKataTdd.Application.Exceptions;

namespace PokerHandsKataTdd.Application.Models;

public record Card
{
    public CardValue cardValue { get; }
    public CardColor cardColor { get; }

    public Card(string cardValueAndColor)
    {
        if (cardValueAndColor.Length != 2)
            throw new InvalidCardDescriptionSizeException(cardValueAndColor.Length);

        cardValue = new CardValue(cardValueAndColor[0]);
        cardColor = new CardColor(cardValueAndColor[1]);        
    }

    public static Card[] CreateMany(string[] cardsValuesAndColors)
    {
        var result = new Card[cardsValuesAndColors.Length];

        for (int i = 0; i < cardsValuesAndColors.Length; i++)
        {
            result[i] = new Card(cardsValuesAndColors[i]);
        }

        return result;
    }

    public override string ToString()
    { 
        return $"{cardValue.StringValue} of {cardColor.StringColor}"; 
    }
}
