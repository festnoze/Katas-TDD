using BankAccountKataTdd.Infra.Data.Exceptions;
using Studi.Api.Core.Exceptions.Guards;
using System.Collections.ObjectModel;

namespace BankAccountKataTdd.Infra.Data.Models;

public record BankModel
{
    public Guid BankId  {get; private set; }
    public string BankName {get; private set; }

    private List<AccountModel> _accounts = new List<AccountModel>();
    public ReadOnlyCollection<AccountModel> Accounts => _accounts.AsReadOnly();

    public BankModel(Guid bankId, string bankName, IEnumerable<AccountModel> accounts)
    {
        BankId = bankId;
        BankName = bankName;
        _accounts.AddRange(accounts);
    }
    
    public BankModel(string bankName)
    {
        BankId = Guid.NewGuid();
        BankName = bankName;
    }

    public Guid CreateNewAccount(string userName)
    {
        if (string.IsNullOrWhiteSpace(userName))
            throw new AccountMissingUserNameException();

        if (_accounts.Any(x => x.UserName == userName))
            throw new ExistingAccountWithSameNameException();

        var account = AccountModel.CreateNew(userName);
        _accounts.Add(account);
        return account.AccountId;
    }

    public decimal GetAccountBalance(Guid accountId)
    {
        CheckAccountExistance(accountId);

        return _accounts.Single(x => x.AccountId == accountId).Balance;
    }

    public void SetAccountBalance(Guid accountId, decimal newBalance)
    {
        CheckAccountExistance(accountId);

        var accountIndex = _accounts.FindIndex(x => x.AccountId == accountId);
        var modifiedAccount = _accounts[accountIndex].GetClonedAccountWithNewBalance(newBalance);
        _accounts[accountIndex] = modifiedAccount;
    }

    public void MakeDeposit(Guid accountId, decimal depositAmount)
    {
        CheckAccountExistance(accountId);

        var account = _accounts.Single(x => x.AccountId == accountId);
        account!.MakeDeposit(depositAmount);
    }

    public void MakeWithdrawal(Guid accountId, decimal withdrawalAmount)
    {
        CheckAccountExistance(accountId);

        var account = _accounts.Single(x => x.AccountId == accountId);
        account!.MakeWithdrawal(withdrawalAmount);
    }

    public void MakeTransfert(Guid account1Id, Guid account2Id, decimal fundsTransfertAmount)
    {
        CheckAccountExistance(account1Id, account2Id);
    }

    public void CheckAccountExistance(params Guid[] accountsId)
    {
        var noAccountToCheck = !accountsId.Any();
        var missingAccount = accountsId.Any(accId => !_accounts.Any(x => x.AccountId == accId));
        if (noAccountToCheck || missingAccount)
                throw new NotExistingAccountException();
    }
}