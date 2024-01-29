
namespace BankAccountKataTdd.Application;

public class AccountService
{
    public int LastBankAccount { get; private set; } = 0;

    public AccountService()
    {

    }

    public int? CreateNewAccount()
    {
        LastBankAccount++;
        return LastBankAccount;
    }

    public object GetAccountBalance(int? accountId)
    {
        return 0;
    }

    public void MakeDeposit(int? accountId, int v)
    {
    }
}
