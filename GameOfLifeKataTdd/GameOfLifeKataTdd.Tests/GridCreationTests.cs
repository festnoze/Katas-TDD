using FluentAssertions;
using GameOfLifeKataTdd.Application.Exceptions;
using GameOfLifeKataTdd.Application.Services;
using System.Drawing;

namespace GameOfLifeKataTdd.Tests;

public class GridCreationTests
{
    private GameOfLifeService service;
    public GridCreationTests()
    {
        service = new GameOfLifeService();
    }

    [Theory]
    [InlineData(5, 5)]
    [InlineData(2, 7)]
    [InlineData(12, 5)]
    [InlineData(0, 0)]
    public void AskingToCreateAnEmptyGrid_ShouldReturnNewEmptyGridOfSpecifiedSize_Test(int width, int height)
    {
        Helpers.GenericCreateNewGameOfLifeTest(service, width, height);
    }

    [Theory]
    [InlineData(3, 3, 0, 0, 1, 1, 2, 0)]
    [InlineData(8, 5, 7, 4, 1, 1, 4, 2, 6, 2, 2, 4)]
    public void AskingToCreateAGridWithSpecifiedLivingCells_ShouldWorks_Test(int width, int height, params int[]? livingCellsCoordonates)
    {
        Helpers.GenericCreateNewGameOfLifeTest(service, width, height, null, livingCellsCoordonates);
    }

    [Theory]
    [InlineData(3, 3, 0, 0, 1, 1, 3, 0)]
    [InlineData(8, 5, 7, 4, 7, 5)]
    public void AskingToCreateAGridWithSpecifiedLivingCellsWithWrongCoordonates_ShouldFails_Test(int width, int height, params int[]? livingCellsCoordonates)
    {
        Helpers.GenericCreateNewGameOfLifeTest(service, width, height, typeof(OutOfRangeCellCoordonatesException), livingCellsCoordonates);
    }
}