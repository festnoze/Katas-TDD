using BankAccountKataTdd.Application;
using BankAccountKataTdd.Infra.Data.Exceptions;
using BankAccountKataTdd.Tests.Fakes;

namespace BankAccountKataTdd.Tests;

public class WithdrawalTests
{
    private FakeAccountRepository _fakeAccountRepository;
    private AccountService _service;

    public WithdrawalTests()
    {
        // Use fake rather than mock when possible //var fakeAccountRepository = Mock.Of<IAccountRepository>();
        _fakeAccountRepository = new FakeAccountRepository();
        _service = new AccountService(_fakeAccountRepository);
    }

    [Theory]
    [InlineData("test int deposit", 10)]
    [InlineData("test big int deposit", 6010)]
    [InlineData("test decimal deposit", 150.10)]
    [InlineData("test small decimal deposit", 0.01)]
    [InlineData("test 3 digits decimal deposit", 1870.012)]
    [InlineData("test special chars $¨^?%", 50)]
    public async Task MakeWithdrawal_FromAnAccount_ShouldSucceed_Test(string userName, decimal withdrawalAmount)
    {
        /// Arrange
        var accountId = await _service.CreateNewAccountAsync(userName);
        await _fakeAccountRepository.SetBalanceAsync(accountId, withdrawalAmount);

        /// Act
        await _service.WithdrawalAsync(accountId, withdrawalAmount);

        /// Assert
        _fakeAccountRepository.LastAccount.UserName.Should().Be(userName);
        _fakeAccountRepository.LastAccount.Balance.Should().Be(0);
        accountId.Should().Be(_fakeAccountRepository.LastAccount.AccountId);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-0.01)]
    [InlineData(-100000)]
    public async Task MakeWithdrawal_OfAnInvalidAmount_ShouldFail_Test(decimal invalidDepositAmount)
    {
        /// Arrange
        var accountId = await _service.CreateNewAccountAsync("test user 232");

        /// Act
        var action = () => _service.DepositAsync(accountId, invalidDepositAmount);

        /// Assert
        await action.Should().ThrowAsync<InvalidDepositAmountException>();
    }

    [Theory]
    [InlineData(0, 0.01)]
    [InlineData(10, 10.01)]
    [InlineData(123.32, 1000.01)]
    public async Task MakeWithdrawal_OfAnAmountExceedingBalance_ShouldFail_Test(decimal accountBalance, decimal withdrawalAmount)
    {
        /// Arrange
        var userName = "test 322";
        var accountId = await _service.CreateNewAccountAsync(userName);
        await _fakeAccountRepository.SetBalanceAsync(accountId, accountBalance);

        /// Act
        var action = () => _service.WithdrawalAsync(accountId, withdrawalAmount);

        /// Assert
        await action.Should().ThrowAsync<InsufficientFundsForWithdrawalException>();
    }

    [Fact]
    public async Task MakeWithdrawal_FromANotExistingAccount_ShouldFail_Test()
    {
        /// Arrange
        var accountId = Guid.NewGuid();

        /// Act
        var action = () => _service.DepositAsync(accountId, 10);

        /// Assert
        await action.Should().ThrowAsync<NotExistingAccountException>();
    }
}
