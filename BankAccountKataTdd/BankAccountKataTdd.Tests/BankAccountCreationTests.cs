using BankAccountKataTdd.Application;
using BankAccountKataTdd.Tests.Fakes;

namespace BankAccountKataTdd.Tests;

public class BankAccountCreationTests
{
    private FakeAccountRepository _fakeAccountRepository;
    private AccountService _service;

    public BankAccountCreationTests()
    {
        // Use fake rather than mock when possible //var fakeAccountRepository = Mock.Of<IAccountRepository>();
        _fakeAccountRepository = new FakeAccountRepository();
        _service = new AccountService(_fakeAccountRepository);
    }  

    [Theory]
    [InlineData("test account creation")]
    public async Task CreateAnAccount_ShouldSucceed_Test(string userName)
    {
        /// Act
        var accountId = await _service.CreateNewAccountAsync(userName);

        /// Assert
        _fakeAccountRepository.LastAccount.UserName.Should().Be(userName);
        _fakeAccountRepository.LastAccount.AccountId.Should().Be(accountId);
    }

    [Fact]
    public async Task CreateAccountWithExistingName_ShouldFail_Test()
    {
        /// Arrange
        await _service.CreateNewAccountAsync("test");

        /// Act
        var action = () => _service.CreateNewAccountAsync("test");

        /// Assert
        await action.Should().ThrowAsync<ExistingAccountWithSameNameException>();
    }


    [Fact]
    public async Task CreateAccountWithMissingName_ShouldFail_Test()
    {
        /// Arrange

        /// Act
        var action = () => _service.CreateNewAccountAsync(string.Empty);

        /// Assert
        await action.Should().ThrowAsync<AccountMissingUserNameException>();
    }
}
