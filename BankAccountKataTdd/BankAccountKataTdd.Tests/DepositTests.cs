using BankAccountKataTdd.Application;
using BankAccountKataTdd.Infra.Data.Exceptions;
using BankAccountKataTdd.Tests.Fakes;

namespace BankAccountKataTdd.Tests;

public class DepositTests
{
    private FakeAccountRepository _fakeAccountRepository;
    private AccountService _service;

    public DepositTests()
    {
        // Use fake rather than mock when possible //var fakeAccountRepository = Mock.Of<IAccountRepository>();
        _fakeAccountRepository = new FakeAccountRepository();
        _service = new AccountService(_fakeAccountRepository);
    }

    [Theory]
    [InlineData("test int deposit", 10)]
    [InlineData("test big int deposit", 150010)]
    [InlineData("test decimal deposit", 150.10)]
    [InlineData("test small decimal deposit", 0.01)]
    [InlineData("test 3 digits decimal deposit", 1870.012)]
    [InlineData("test special chars $¨^?%", 50)]
    public async Task MakeDeposit_IntoAnAccount_ShouldSucceed_Test(string userName, decimal depositValue)
    {
        /// Arrange
        var accountId = await _service.CreateNewAccountAsync(userName);
        _fakeAccountRepository.LastAccount.Balance.Should().Be(0);

        /// Act
        await _service.DepositAsync(accountId, depositValue);

        /// Assert
        _fakeAccountRepository.LastAccount.UserName.Should().Be(userName);
        _fakeAccountRepository.LastAccount.Balance.Should().Be(depositValue);
        accountId.Should().Be(_fakeAccountRepository.LastAccount.AccountId);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-0.01)]
    [InlineData(-100000)]
    [InlineData(1000001)]
    [InlineData(10000000)]
    public async Task MakeADeposit_OfAnInvalidAmount_ShouldFail_Test(decimal invalidDepositAmount)
    {
        /// Arrange
        var accountId = await _service.CreateNewAccountAsync("test user 232");

        /// Act
        var action = () => _service.DepositAsync(accountId, invalidDepositAmount);

        /// Assert
        await action.Should().ThrowAsync<InvalidDepositAmountException>();
    }


    [Fact]
    public async Task MakeDeposit_IntoANotExistingAccount_ShouldFail_Test()
    {
        /// Arrange
        var accountId = Guid.NewGuid(); // Inexisting account id

        /// Act
        var action = () => _service.DepositAsync(accountId, 10);

        /// Assert
        await action.Should().ThrowAsync<NotExistingAccountException>();
    }
}
