using FluentAssertions;
using PokerHandsKataTdd.Application.Exceptions;
using PokerHandsKataTdd.Application.Models;

namespace PokerHandsKataTdd.Tests;

public class HandCreationTests
{
    [Theory]
    [InlineData("4S 6D 9C KH 7D 2S 3S")]// with colors convention: letters
    [InlineData("4♠ 6♦ 9♣ K♥ 7♦ 2♠ 3♠")] // with colors convention: symbols
    [InlineData("4♠ 6D 9♣ KH 7♦ 2♠ 3S")] // with mixed colors convention: symbols and letters
    public void CreateValidHand_ShouldSucceed_Test(string handCards)
    {
        /// Act
        var hand = new Hand(handCards);

        /// Assert
        var cardsAsStrings = hand.Cards.Select(c => c.ToString());
        cardsAsStrings.Should().HaveCount(7);
        cardsAsStrings.Should().Contain("Four of Spade");
        cardsAsStrings.Should().Contain("Six of Diamond");
        cardsAsStrings.Should().Contain("Nine of Club");
        cardsAsStrings.Should().Contain("King of Heart");
        cardsAsStrings.Should().Contain("Seven of Diamond");
        cardsAsStrings.Should().Contain("Two of Spade");
        cardsAsStrings.Should().Contain("Three of Spade");
    }


    [Theory]
    [InlineData("2S 2S 4J 6D 7D 9C KD")] // Wrong color
    [InlineData("2S 2S 4D 6D 7d 9C KD")] // Wrong color (case sensitivity)
    public void CreateInvalidHand_WithInvalidCardColor_ShouldFailed_Test(string handCards)
    {
        /// Arrange
        var createHandAction = () => new Hand(handCards);

        /// Act & Assert
        createHandAction.Should().Throw<InvalidCardColorException>();
    }

    [Theory]
    [InlineData("KD 1S 4S 6D 7D 9C KD")] // Wrong value: 1
    [InlineData("4S KD 0S 4S 7D 9C 4S")] // Wrong value: 0
    [InlineData("4S KD DS 4S 7D 9C 4S")] // Wrong value: D
    public void CreateInvalidHand_WithInvalidCardValue_ShouldFailed_Test(string handCards)
    {
        /// Arrange
        var createHandAction = () => new Hand(handCards);

        /// Act & Assert
        createHandAction.Should().Throw<InvalidCardValueException>();
    }

    [Theory]
    [InlineData("1L KD 0$ 4S 7D 9C 4S")] // Wrong value & color
    [InlineData("00 $£ DK KD 7D 9C 4S")] // Wrong value & color
    public void CreateInvalidHand_WithInvalidCardValueAndColor_ShouldFailed_Test(string handCards)
    {
        /// Arrange
        var createHandAction = () => new Hand(handCards);

        /// Act & Assert
        createHandAction.Should()
            .Throw<Exception>().Where(e =>
                e is InvalidCardColorException ||
                e is InvalidCardValueException);
    }

    [Theory]
    [InlineData("2S KD 5 4S 7D 9C 4S")] // Wrong card with 1 char
    [InlineData("2S KD 5C 10S 7D 9C 4S")] // Wrong card with 3 chars
    [InlineData("2S KD 5C SDS 7D 9C 4S")] // Wrong card with 3 chars
    public void CreateInvalidHand_WithInvalidCardDescriptionSize_ShouldFailed_Test(string handCards)
    {
        /// Arrange
        var createHandAction = () => new Hand(handCards);

        /// Act & Assert
        createHandAction.Should().Throw<InvalidCardDescriptionSizeException>();
    }

    [Theory]
    [InlineData("2S")] //under-sized hand
    [InlineData("2S 9C")] //under-sized hand
    [InlineData("2S 3S 4S")] //under-sized hand
    [InlineData("2S 3S 4S 6D")] //under-sized hand
    [InlineData("2S 3S 4S 6D 7D")] //under-sized hand
    [InlineData("2S 3S 4S 6D 7D 9C")] //under-sized hand
    [InlineData("2S 3S 4S 6D 7D 9C TC QD")] //over-sized hand
    [InlineData("2S 3S 4S 6D 7D 9C TC QD KC")] //over-sized hand
    [InlineData("2S 3S 4S 6D 7D 9C TC QD KC AH")] //over-sized hand
    public void CreateInvalidHand_WithWrongCardsNumber_ShouldFailed_Test(string handCards)
    {
        /// Arrange
        var createHandAction = () => new Hand(handCards);

        /// Act & Assert
        createHandAction.Should().Throw<WrongCardsCountException>();
    }

    [Theory]
    [InlineData("2S 2S 4S 6D 7D 9C KD")]
    [InlineData("KD 2S 4S 6D 7D 9C KD")]
    [InlineData("4S KD 2S 4S 7D 9C 4S")]
    public void CreateInvalidHand_WithDuplicateCards_ShouldFailed_Test(string handCards)
    {
        /// Arrange
        var createHandAction = () => new Hand(handCards);

        /// Act & Assert
        createHandAction.Should().Throw<DuplicateCardInHandException>();
    }
}