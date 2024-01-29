namespace BankAccountKataTdd.Infra.Data.Models;

public record AccountModel
{
    public Guid AccountId  {get; init; }
    public string UserName {get; init; }
    public decimal Balance { get; init; }

    public AccountModel(Guid accountId, string userName, decimal balance)
    {
        AccountId = accountId;
        UserName = userName;
        Balance = balance;
    }
}