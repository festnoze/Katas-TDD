using BankAccountKataTdd.Application;

namespace BankAccountKataTdd.Tests;

public class BankAccountCreationTests
{
    [Fact]
    public void CreateAnAccount_ShouldSucceed_Test()
    {
        /// Arrange
        var service = new AccountService();

        /// Act
        var accountId = service.CreateNewAccount();

        /// Assert
        accountId.Should().NotBeNull();
        accountId.Should().Be(1);
    }

    [Fact]
    public void CreateASecondAccount_ShouldSucceed_Test()
    {
        /// Arrange
        var service = new AccountService();
        service.CreateNewAccount();

        /// Act
        var accountId = service.CreateNewAccount();

        /// Assert
        accountId.Should().NotBeNull();
        accountId.Should().Be(2);
        service.LastBankAccount.Should().Be(2);
    }
}
