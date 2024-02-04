using FluentAssertions;

namespace RomanNumerals.Tests;

public class RomanNumbersTests
{
    [Theory]
    [InlineData(1, "I")]
    [InlineData(2, "II")]
    [InlineData(3, "III")]
    [InlineData(4, "IV")]
    [InlineData(5, "V")]
    [InlineData(6, "VI")]
    [InlineData(7, "VII")]
    [InlineData(8, "VIII")]
    [InlineData(9, "IX")]
    public void SingleDigitNumber_TranslateToRoman_ShouldSucceed_Test(int numberToTranslate, string awaitedRomanTranslation)
    {
        TranslateToRomanNumberTest(numberToTranslate, awaitedRomanTranslation);
    }

    [Theory]
    [InlineData(10, "X")]
    [InlineData(20, "XX")]
    [InlineData(30, "XXX")]
    [InlineData(40, "XL")]
    [InlineData(50, "L")]
    [InlineData(60, "LX")]
    [InlineData(70, "LXX")]
    [InlineData(80, "LXXX")]
    [InlineData(90, "XC")]
    public void TwoDigitsNumberWoUnits_TranslateToRoman_ShouldSucceed_Test(int numberToTranslate, string awaitedRomanTranslation)
    {
        TranslateToRomanNumberTest(numberToTranslate, awaitedRomanTranslation);
    }

    [Theory]
    [InlineData(11, "XI")]
    [InlineData(22, "XXII")]
    [InlineData(33, "XXXIII")]
    [InlineData(44, "XLIV")]
    [InlineData(55, "LV")]
    [InlineData(66, "LXVI")]
    [InlineData(77, "LXXVII")]
    [InlineData(88, "LXXXVIII")]
    [InlineData(99, "XCIX")]
    public void TwoDigitsNumberWithUnits_TranslateToRoman_ShouldSucceed_Test(int numberToTranslate, string awaitedRomanTranslation)
    {
        TranslateToRomanNumberTest(numberToTranslate, awaitedRomanTranslation);
    }

    [Theory]
    [InlineData(100, "C")]
    [InlineData(200, "CC")]
    [InlineData(300, "CCC")]
    [InlineData(400, "CD")]
    [InlineData(500, "D")]
    [InlineData(600, "DC")]
    [InlineData(700, "DCC")]
    [InlineData(800, "DCCC")]
    [InlineData(900, "CM")]
    public void ThreeDigitsNumberWoDecimalNorUnits_TranslateToRoman_ShouldSucceed_Test(int numberToTranslate, string awaitedRomanTranslation)
    {
        TranslateToRomanNumberTest(numberToTranslate, awaitedRomanTranslation);
    }

    [Theory]
    [InlineData(111, "CXI")]
    [InlineData(222, "CCXXII")]
    [InlineData(333, "CCCXXXIII")]
    [InlineData(444, "CDXLIV")]
    [InlineData(555, "DLV")]
    [InlineData(666, "DCLXVI")]
    [InlineData(777, "DCCLXXVII")]
    [InlineData(888, "DCCCLXXXVIII")]
    [InlineData(999, "CMXCIX")]
    public void ThreeDigitsNumberWithDecimalAndUnits_TranslateToRoman_ShouldSucceed_Test(int numberToTranslate, string awaitedRomanTranslation)
    {
        TranslateToRomanNumberTest(numberToTranslate, awaitedRomanTranslation);
    }

    [Theory]
    [InlineData(1000, "M")]
    [InlineData(2000, "MM")]
    [InlineData(3000, "MMM")]
    [InlineData(4000, "MVM")]
    [InlineData(5000, "VM")]
    [InlineData(6000, "VMM")]
    [InlineData(7000, "VMMM")]
    [InlineData(8000, "VMMMM")]
    [InlineData(9000, "MXM")]
    public void FourDigitsNumberWoHunderedsDecimalsNorUnits_TranslateToRoman_ShouldSucceed_Test(int numberToTranslate, string awaitedRomanTranslation)
    {
        TranslateToRomanNumberTest(numberToTranslate, awaitedRomanTranslation);
    }

    [Theory]
    [InlineData(10000, "XM")]
    [InlineData(11000, "XMM")]
    [InlineData(50000, "LM")]
    public void FiveDigitsNumberWoHunderedsDecimalsNorUnits_TranslateToRoman_ShouldSucceed_Test(int numberToTranslate, string awaitedRomanTranslation)
    {
        TranslateToRomanNumberTest(numberToTranslate, awaitedRomanTranslation);
    }


    [Theory]
    [InlineData(100000, "CM")]
    [InlineData(500000, "DM")]
    public void SixDigitsNumberWoHunderedsDecimalsNorUnits_TranslateToRoman_ShouldSucceed_Test(int numberToTranslate, string awaitedRomanTranslation)
    {
        TranslateToRomanNumberTest(numberToTranslate, awaitedRomanTranslation);
    }

    [Theory]
    [InlineData(1000000)]
    public void SevenDigitsNumber_TranslateToRoman_ShouldThrowArgumentOutOfRangeException_Test(int numberToTranslate)
    {
        // Arrange
        var service = new RomanService();

        // Act
        var action = () => service.TranslateToRoman(numberToTranslate);

        // Assert
        action.Should().Throw<ArgumentOutOfRangeException>();
    }

    private static void TranslateToRomanNumberTest(int numberToTranslate, string awaitedRomanTranslation)
    {

        // Arrange
        var service = new RomanService();

        // Act
        var result = service.TranslateToRoman(numberToTranslate);

        // Assert
        result.Should().Be(awaitedRomanTranslation);
    }
}