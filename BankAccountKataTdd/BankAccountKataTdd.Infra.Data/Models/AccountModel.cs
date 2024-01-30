using BankAccountKataTdd.Infra.Data.Exceptions;
using System.Security.Principal;

namespace BankAccountKataTdd.Infra.Data.Models;

public record AccountModel
{
    public Guid AccountId  {get; private set; }
    public string UserName {get; private set; }
    public decimal Balance { get; private set; }

    public static AccountModel CreateExisting(Guid accountId, string userName, decimal balance)
    {
        return new AccountModel(accountId, userName, balance);
    }

    public static AccountModel CreateNew(string userName)
    {
        return new AccountModel(userName);
    }

    private AccountModel(Guid accountId, string userName, decimal balance)
    {
        AccountId = accountId;
        UserName = userName;
        Balance = balance;
    }

    private AccountModel(string userName)
    {
        AccountId = Guid.NewGuid();
        UserName = userName;
        Balance = 0;
    }

    public void MakeDeposit(decimal depositAmount)
    {
        CheckDepositAmountValidity(depositAmount);

        Balance += depositAmount;
    }

    public void MakeWithdrawal(decimal withdrawalAmount)
    {
        CheckWithdrawalAmountValidity(withdrawalAmount);
        CheckIfWithdrawalExceedsAvailableFunds(withdrawalAmount, Balance);

        Balance -= withdrawalAmount;
    }

    public AccountModel GetClonedAccountWithNewBalance(decimal newBalance)
    {
        return this with { Balance = newBalance };
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