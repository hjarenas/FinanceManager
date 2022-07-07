namespace FinanceManager.Domain.AccountsAggregate;
public class BankAccount
{
    public BankAccount(string bankName, string isin)
        => (BankName, Isin) = (bankName, isin);
    public string BankName { get; set; }
    public string Isin { get; set; }
}
