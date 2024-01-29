using BankAccountKataTdd.Application;
using BankAccountKataTdd.Tests.Fakes;
using FluentAssertions.Common;

namespace BankAccountKataTdd.Tests;

public class GetAccountBalanceTests
{
    private FakeAccountRepository _fakeAccountRepository;
    private AccountService _service;

    public GetAccountBalanceTests()
    {
        // Use fake rather than mock when possible //var fakeAccountRepository = Mock.Of<IAccountRepository>();
        _fakeAccountRepository = new FakeAccountRepository();
        _service = new AccountService(_fakeAccountRepository);
    }

    [Fact]
    public async Task CreatedAccount_GetBalance_Test()
    {
        /// Arrange
        var accountId = await _service.CreateNewAccountAsync("test");

        /// Act
        var balance = await _service.GetAccountBalanceAsync(accountId);

        /// Assert
        balance.Should().Be(0);
    }

}
