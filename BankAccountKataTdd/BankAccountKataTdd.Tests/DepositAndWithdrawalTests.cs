using BankAccountKataTdd.Application;

namespace BankAccountKataTdd.Tests;

public class DepositAndWithdrawalTests
{

    [Fact]
    public void MakeDeposit_IntoAnAccount_ShouldSucceedTest()
    {
        /// Arrange
        var service = new AccountService();
        var accountId = service.CreateNewAccount();

        /// Act
        service.MakeDeposit(accountId, 10);

        /// Assert
        var balance = service.GetAccountBalance(accountId);
        balance.Should().Be(10);
    }
}
