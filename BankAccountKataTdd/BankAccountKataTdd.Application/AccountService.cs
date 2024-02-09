using BankAccountKataTdd.Infra.Data;

namespace BankAccountKataTdd.Application;

public class AccountService
{
    private IAccountRepository _accountRepository;
    public AccountService(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public async Task<Guid> CreateNewAccountAsync(string userName)
    {
        return await _accountRepository.CreateNewAccountAsync(userName);
    }

    public async Task<decimal> GetAccountBalanceAsync(Guid accountId)
    {
        var bank = await _accountRepository.GetBankWithAccountIdAsync(accountId);
        return bank.GetAccountBalance(accountId);
    }

    public async Task DepositAsync(Guid accountId, decimal depositAmount)
    {
        // Hydrate bank domain model
        var bank = await _accountRepository.GetBankWithAccountIdAsync(accountId);

        // Call buisness action on model
        bank.MakeDeposit(accountId, depositAmount);

        // Persist modifications on domain model
        await _accountRepository.SetBalanceAsync(accountId, bank.GetAccountBalance(accountId));
    }

    public async Task WithdrawalAsync(Guid accountId, decimal withdrawalAmount)
    {
        // Hydrate bank domain model
        var bank = await _accountRepository.GetBankWithAccountIdAsync(accountId);

        // Call buisness action on model
        bank.MakeWithdrawal(accountId, withdrawalAmount);

        // Persist modifications on domain model
        await _accountRepository.SetBalanceAsync(accountId, bank.GetAccountBalance(accountId));
    }

    public async Task TransfertAsync(Guid account1Id, Guid account2Id, decimal fundsTransfertAmount)
    {
        // Hydrate bank domain model
        var bank = await _accountRepository.GetBankWithAccountIdAsync(account1Id, account2Id);

        // Call buisness action on model
        bank.MakeTransfert(account1Id, account2Id, fundsTransfertAmount);

        // Persist modifications on domain model
        await _accountRepository.SetBalanceAsync(account1Id, bank.GetAccountBalance(account1Id));
        await _accountRepository.SetBalanceAsync(account2Id, bank.GetAccountBalance(account2Id));
    }
}
