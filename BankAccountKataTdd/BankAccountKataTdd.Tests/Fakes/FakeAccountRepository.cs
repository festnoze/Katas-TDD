using BankAccountKataTdd.Infra.Data.Models;
using Studi.Api.Core.Exceptions.Guards;

namespace BankAccountKataTdd.Tests.Fakes;

public class FakeAccountRepository : IAccountRepository
{
    public List<AccountIto> Accounts = new List<AccountIto>();
    public AccountIto LastAccount => Accounts.Last();

    public Task<Guid> CreateNewAccountAsync(string userName)
    {
        if (string.IsNullOrWhiteSpace(userName))
            throw new AccountMissingUserNameException();

        if (Accounts.Any(x => x.UserName == userName))
            throw new ExistingAccountWithSameNameException();
        
        var accountId = Guid.NewGuid();
        Accounts.Add(new AccountIto(accountId, userName, 0));

        
        return Task.FromResult(Accounts.Last().AccountId);
    }

    public Task<AccountIto?> GetInfosByAccountIdAsync(Guid accountId)
    {
        return Task.FromResult(Accounts.FirstOrDefault(x => x.AccountId == accountId));
    }

    public Task SetBalanceAsync(Guid accountId, decimal newBalance)
    {
        var accountIndex = Accounts.FindIndex(x => x.AccountId == accountId);
        if (accountIndex != -1)
            Accounts[accountIndex] = Accounts[accountIndex] with { Balance = newBalance };

        return Task.CompletedTask;
    }
}
