using BankAccountKataTdd.Application;
using FluentAssertions.Common;

namespace BankAccountKataTdd.Tests;

public class GetAccountBalanceTests
{
    [Fact]
    public void GetAccountBalance_Test()
    {
        /// Arrange
        var service = new AccountService();
        var accountId = service.CreateNewAccount();

        /// Act
        var balance = service.GetAccountBalance(accountId);

        /// Assert
        balance.Should().Be(0);
    }

}
