using BankAccountKataTdd.Application;
using BankAccountKataTdd.Tests.Fakes;

namespace BankAccountKataTdd.Tests;

public class FundTransfertTests
{
    private FakeAccountRepository _fakeAccountRepository;
    private AccountService _service;

    public FundTransfertTests()
    {
        // Use fake rather than mock when possible //var fakeAccountRepository = Mock.Of<IAccountRepository>();
        _fakeAccountRepository = new FakeAccountRepository();
        _service = new AccountService(_fakeAccountRepository);
    }


    [Theory]
    [InlineData(100, 0, 40)]
    public async Task FundsTransfert_ShouldSucceed_Test(decimal originAccountBalance, decimal destAccountBalance, decimal fundsTransfertAmount)
    {
        /// Arrange
        var account1Id = await _service.CreateNewAccountAsync("user1");
        var account2Id = await _service.CreateNewAccountAsync("user2");
        await _fakeAccountRepository.SetBalanceAsync(account1Id, originAccountBalance);
        await _fakeAccountRepository.SetBalanceAsync(account1Id, destAccountBalance);

        /// Act
        await _service.MakeTransfertAsync(account1Id, account2Id, fundsTransfertAmount);

        /// Assert
        var bank = await _fakeAccountRepository.GetBankWithAccountIdAsync(account1Id, account2Id);
        bank.GetAccountBalance(account1Id).Should().Be(originAccountBalance - fundsTransfertAmount);
        bank.GetAccountBalance(account2Id).Should().Be(destAccountBalance + fundsTransfertAmount);
    }

}
