using BankAccountKataTdd.Infra.Data.Models;
using BankAccountKataTdd.Tests;

namespace BankAccountKataTdd.Infra.Data;
public class AccountRepository : IAccountRepository
{
#pragma warning disable CS1998 // Cette méthode async n'a pas d'opérateur 'await' et elle s'exécutera de façon synchrone
    public async Task<Guid> CreateNewAccountAsync(string userName)
    {
        throw new NotImplementedException();
    }

    public async Task<AccountIto?> GetInfosByAccountIdAsync(Guid accountId)
    {
        throw new NotImplementedException();
    }

    public Task SetBalanceAsync(Guid accountId, decimal newBalance)
    {
        throw new NotImplementedException();
    }
#pragma warning restore CS1998 // Cette méthode async n'a pas d'opérateur 'await' et elle s'exécutera de façon synchrone
}
