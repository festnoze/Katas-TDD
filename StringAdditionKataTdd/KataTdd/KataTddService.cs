using System;
using System.Globalization;

namespace KataTdd;

public class KataTddService : IKataTddService
{
    public KataTddService()
    {}

    public decimal Add(string values)
    {
        var result = 0m;
        var splitedValues = SplitBySpace(values);
        var splitedDecimals = ConvertToDecimalArray(splitedValues);

        foreach (var value in splitedDecimals)
        {
            result += Convert.ToDecimal(value);
        }
        return result;
    }

    private static string[] SplitBySpace(string values)
    {
        var result = values.Split(' ').ToList();
        for(var i = result.Count - 1; i >= 0; i--) 
        {
            if (string.IsNullOrWhiteSpace(result[i]))
                result.RemoveAt(i);
        }

        return result.ToArray();
    }

    private static decimal[] ConvertToDecimalArray(string[] values)
    {
        var decimals = new List<decimal>();
        var frenchFormatProvider = new NumberFormatInfo { NumberDecimalSeparator = ",", NumberGroupSeparator = "." };
        var usaFormatProvider = new NumberFormatInfo { NumberDecimalSeparator = ".", NumberGroupSeparator = "," };

        foreach (var value in values)
        {
            if (IsFormatCorrect(value, frenchFormatProvider) && decimal.TryParse(value, NumberStyles.Number, frenchFormatProvider, out decimal result))
            {
                decimals.Add(result);
            }
            // Try default formats upon failure
            else if (IsFormatCorrect(value, usaFormatProvider) && decimal.TryParse(value, NumberStyles.Number, usaFormatProvider, out result))
            {
                decimals.Add(result);
            }
            else if (decimal.TryParse(value, out result))
            {
                decimals.Add(result);
            }
            else
                throw new ArgumentException($"Unhandled format for provided number: {value}");
        }

        return decimals.ToArray();
    }

    private static bool IsFormatCorrect(string value, NumberFormatInfo formatProvider)
    {
        // Check that there's no more than one decimal separator
        if (value.Count(c => c.ToString() == formatProvider.NumberDecimalSeparator) > 1)
            return false;

        // Extract the integer part and verify group separators
        var integerPart = value.Split(formatProvider.NumberDecimalSeparator[0])[0];
        var groups = integerPart.Split(formatProvider.NumberGroupSeparator[0]);

        // Verify that each group contains three digits, except the first one
        var anyGroupExceptFirstNotContaining3Digits = groups.Skip(1).Any(gr => gr.Length != 3);
        return !anyGroupExceptFirstNotContaining3Digits;
    }
}
