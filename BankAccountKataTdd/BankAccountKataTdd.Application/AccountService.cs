using BankAccountKataTdd.Infra.Data.Models;
using BankAccountKataTdd.Tests;
using Studi.Api.Core.Exceptions.Guards;

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
        var accountInfos = await _accountRepository.GetInfosByAccountIdAsync(accountId);
        Guard.Against.Null(accountInfos, "Unfound account with specified id");

        return accountInfos.Balance;
    }

    public async Task MakeDepositAsync(Guid accountId, decimal depositAmount)
    {
        await CheckAccountExistanceAsync(accountId);
        CheckDepositAmountValidity(depositAmount);

        var account = await _accountRepository.GetInfosByAccountIdAsync(accountId);
        var newBalance = account!.Balance + depositAmount;

        await _accountRepository.SetBalanceAsync(accountId, newBalance);
    }

    public async Task MakeWithdrawalAsync(Guid accountId, decimal withdrawalAmount)
    {
        var account = await _accountRepository.GetInfosByAccountIdAsync(accountId);

        await CheckAccountExistanceAsync(accountId);
        CheckWithdrawalAmountValidity(withdrawalAmount);
        CheckIfWithdrawalExceedsAvailableFunds(withdrawalAmount, account!.Balance);

        var newBalance = account.Balance - withdrawalAmount;

        await _accountRepository.SetBalanceAsync(accountId, newBalance);
    }

    private async Task CheckAccountExistanceAsync(Guid accountId)
    {
        var accountInfos = await _accountRepository.GetInfosByAccountIdAsync(accountId);
        if (accountInfos is null)
            throw new NotExistingAccountException();
    }

    private void CheckDepositAmountValidity(decimal depositAmount)
    {
        // Forbid negative and zero deposits and deposit above 1M (which are special deposits)
        if (depositAmount <= 0 || depositAmount > 1000000)
            throw new InvalidDepositAmountException();
    }

    private void CheckWithdrawalAmountValidity(decimal withdrawalAmount)
    {
        // Forbid negative and zero deposits and deposit above 1M (which are special deposits)
        if (withdrawalAmount <= 0 || withdrawalAmount > 7000)
            throw new InvalidWithdrawalAmountException();
    }

    private void CheckIfWithdrawalExceedsAvailableFunds(decimal withdrawalAmount, decimal accountBalance)
    {
        if (withdrawalAmount > accountBalance) 
            throw new InsufficientFundsForWithdrawalException();
    }
}
