using PokerHandsKataTdd.Application.Exceptions;
using PokerHandsKataTdd.Application.Models.Enums;

namespace PokerHandsKataTdd.Application.Models;

public record CardValue
{
    public CardValueEnum Value { get; }

    public string StringValue => Value.ToString();

    public CardValue(CardValueEnum value)
    {
        Value = value;
    }

    public CardValue(char charValue)
    {
        if (!Enum.IsDefined(typeof(CardCharValueEnum), (int)charValue))
            throw new InvalidCardValueException(charValue);

        // Map the char to CardValueEnum
        var charEnumValue = (CardCharValueEnum)charValue;
        Value = CharValueEnumToValueEnum(charEnumValue);
    }

    private CardValueEnum CharValueEnumToValueEnum(CardCharValueEnum charEnum)
    {
        // Assuming names in both enums match exactly
        return (CardValueEnum)Enum.Parse(typeof(CardValueEnum), charEnum.ToString());
    }

    private CardCharValueEnum ValueEnumToCharValueEnum(CardValueEnum valueEnum)
    {
        // Assuming the names in both enums match exactly
        return (CardCharValueEnum)Enum.Parse(typeof(CardCharValueEnum), valueEnum.ToString());
    }
}

