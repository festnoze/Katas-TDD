using BankAccountKataTdd.Infra.Data;
using BankAccountKataTdd.Infra.Data.Models;

namespace BankAccountKataTdd.Tests.Fakes;

public class FakeAccountRepository : IAccountRepository
{
    public BankModel Bank = new BankModel("test bank");
    public AccountModel LastAccount => Bank.Accounts.Last();

    public Task<Guid> CreateNewAccountAsync(string userName)
    {        
        Bank.CreateNewAccount(userName);
        
        return Task.FromResult(Bank.Accounts.Last().AccountId);
    }

    public Task<BankModel> GetBankWithAccountIdAsync(params Guid[] accountsIds)
    {
        Bank.CheckAccountExistance(accountsIds);
        var selectedAccountsCloned = 
                Bank.Accounts
                .Where(acc => accountsIds.Contains(acc.AccountId))
                .Select(acc => acc with { });

        var bankWithSpecifiedAccounts =  new BankModel(
                                                Bank.BankId, 
                                                Bank.BankName, 
                                                selectedAccountsCloned);

        return Task.FromResult(bankWithSpecifiedAccounts);
    }

    public Task SetBalanceAsync(Guid accountId, decimal newBalance)
    {
        Bank.SetAccountBalance(accountId, newBalance);
        return Task.CompletedTask;
    }
}
