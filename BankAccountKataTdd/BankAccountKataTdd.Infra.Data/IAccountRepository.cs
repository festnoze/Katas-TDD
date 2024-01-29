using BankAccountKataTdd.Infra.Data.Models;

namespace BankAccountKataTdd.Tests;

public interface IAccountRepository
{
    /// <summary>
    /// Create a new account
    /// </summary>
    /// <param name="userName"></param>
    /// <param name="initialDeposit"></param>
    /// <returns>the created account Id</returns>
    Task<Guid> CreateNewAccountAsync(string userName);
    
    /// <summary>
    /// Get account infos by account id
    /// </summary>
    /// <param name="accountId"></param>
    /// <returns>the account infos ITO</returns>
    Task<AccountIto?> GetInfosByAccountIdAsync(Guid accountId);
    
    /// <summary>
    /// Set account updated balance
    /// </summary>
    /// <param name="accountId"></param>
    /// <param name="newBalance"></param>
    /// <returns></returns>
    Task SetBalanceAsync(Guid accountId, decimal newBalance);
}