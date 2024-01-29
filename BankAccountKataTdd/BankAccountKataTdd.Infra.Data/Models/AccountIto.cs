namespace BankAccountKataTdd.Infra.Data.Models;

public record AccountIto
{
    public Guid AccountId  {get; init; }
    public string UserName {get; init; }
    public decimal Balance { get; init; }

    public AccountIto(Guid accountId, string userName, decimal balance)
    {
        AccountId = accountId;
        UserName = userName;
        Balance = balance;
    }
}