
namespace RomanNumerals.Tests;

public class RomanService
{
    public RomanService()
    {
    }

    private RomanNum[] romanCharArray = (RomanNum[])Enum.GetValues(typeof(RomanNum));

    public string TranslateToRoman(int number)
    {
        var romanTranslation = string.Empty;
        var numberDigits = SplitIntoDigits(number);
        var numberDigitsIndex = numberDigits.Count - 1;
        if (numberDigits.Count >= 7)
            throw new ArgumentOutOfRangeException(nameof(numberDigits));

        var firstRomanIndexForCurrentDigit = 2 * (numberDigitsIndex);
        var numberWoSubsecantDigits = numberDigits.First() * Math.Pow(10, numberDigitsIndex);
        if (numberWoSubsecantDigits < (int)romanCharArray[firstRomanIndexForCurrentDigit + 1])
        {
            if (numberWoSubsecantDigits < (int)romanCharArray[firstRomanIndexForCurrentDigit + 1] - 1 * Math.Pow(10, numberDigitsIndex))
            {
                romanTranslation += AddRomanCharSecifiedCount(numberDigits.First(), romanCharArray[firstRomanIndexForCurrentDigit]);
            }
            else
            {
                romanTranslation += romanCharArray[firstRomanIndexForCurrentDigit];
                romanTranslation += romanCharArray[firstRomanIndexForCurrentDigit + 1];
            }
        }
        else if (numberWoSubsecantDigits < (int)romanCharArray[firstRomanIndexForCurrentDigit + 1])
            romanTranslation += romanCharArray[firstRomanIndexForCurrentDigit + 1];
        else
        {
            if (numberWoSubsecantDigits < (int)romanCharArray[firstRomanIndexForCurrentDigit + 1] + 4 * Math.Pow(10, numberDigitsIndex))
            {
                romanTranslation += romanCharArray[firstRomanIndexForCurrentDigit + 1];
                var charToAdd = numberDigits.First() - 5;
                romanTranslation += AddRomanCharSecifiedCount(charToAdd, romanCharArray[firstRomanIndexForCurrentDigit]);
            }
            else
            {
                romanTranslation += romanCharArray[firstRomanIndexForCurrentDigit];
                romanTranslation += romanCharArray[firstRomanIndexForCurrentDigit + 2];
            }
        }

        if (numberDigitsIndex != 0)
            romanTranslation += TranslateToRoman(Convert.ToInt32(number.ToString().Substring(1)));

        return romanTranslation;
    }

    private static string AddRomanCharSecifiedCount(int count, RomanNum romanCharToAdd)
    {
        var result = string.Empty;
        for (int i = 0; i < count; i++)
            result += romanCharToAdd;

        return result;
    }

    private static List<int> SplitIntoDigits(int number)
    {
        List<int> digits = new List<int>();

        foreach (char digit in number.ToString())
        {
            digits.Add(int.Parse(digit.ToString()));
        }

        return digits;
    }
}