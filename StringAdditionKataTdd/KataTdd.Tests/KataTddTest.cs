namespace KataTdd.Tests;

public class KataTddTest
{
    [Theory]
    [InlineData("", 0)]
    [InlineData(" ", 0)]
    public void WhileInputNoStrings_Returns_Zero(string operands, decimal awaitedOutput)
    {
        var service = new KataTddService();
        var result = service.Add(operands);

        Assert.Equal(awaitedOutput, result);
    }

    [Theory]
    [InlineData("1", 1)]
    [InlineData("5", 5)]
    [InlineData("-7", -7)]
    [InlineData("1,3", 1.3)]
    [InlineData("-741,0", -741)]
    public void WhileInputSingleNumberString_Returns_NumberAsString(string operands, decimal awaitedOutput)
    {
        var service = new KataTddService();
        var result = service.Add(string.Join(' ', operands));

        Assert.Equal(awaitedOutput, result);
    }

    [Theory]
    [InlineData("1 1", 2)]
    [InlineData("7 -5", 2)]
    [InlineData("-8 -3", -11)]
    [InlineData("1,5 1", 2.5)]
    [InlineData("1,5 1,2", 2.7)]
    [InlineData("-1,5 3,2", 1.7)]
    public void WhileInputTwoNumbersStrings_Returns_AdditionOfTwoNumbers(string operands, decimal awaitedOutput)
    {
        var service = new KataTddService();
        var result = service.Add(operands);

        Assert.Equal(awaitedOutput, result);
    }

    [Theory]
    [InlineData("-1.000 100", -900)]
    [InlineData("721.000 100", 721100)]
    [InlineData("1.010,50 12.500,21", 13510.71)]
    public void WhileInputTwoStrings_WithGroupSeparator_Returns_Addition(string operands, decimal awaitedOutput)
    {
        var service = new KataTddService();
        var result = service.Add(operands);

        Assert.Equal(awaitedOutput, result);
    }

    [Theory]
    [InlineData("1 1 1", 3)]
    [InlineData("1 1 1 1", 4)]
    [InlineData("7 -5 5 -7", 0)]
    [InlineData("1.010,50 12.500,21 1,5", 13512.21)]
    public void WhileInputThreeOrMoreStrings_Returns_Addition(string operands, decimal awaitedOutput)
    {
        var service = new KataTddService();
        var result = service.Add(operands);

        Assert.Equal(awaitedOutput, result);
    }

    [Theory]
    [InlineData("-1.00 100", 99)]
    [InlineData("1,000.56", 1000.56)]
    [InlineData("1.01 1,21", 2.22)]
    public void WhileInputTwoStrings_WithUsDecimalAndGroupSeparators_Returns_Addition(string operands, decimal awaitedOutput)
    {
        var service = new KataTddService();
        var result = service.Add(operands);

        Assert.Equal(awaitedOutput, result);
    }
}