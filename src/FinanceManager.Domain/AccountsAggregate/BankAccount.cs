using FinanceManager.Domain.Common;
using SharedKernel.Exceptions;

namespace FinanceManager.Domain.AccountsAggregate;
public class BankAccount : BaseEntity
{
    private string? _bankName;
    private string? _iban;
    private List<AccountHolder>? _accountHolders;
    // private List<Transaction>? _transactions;

    public string BankName
    {
        get => _bankName ?? throw new UninitializedPropertyException();
        set => _bankName = value;
    }
    public string Iban
    {
        get => _iban ?? throw new UninitializedPropertyException();
        set => _iban = value;
    }
    public List<AccountHolder> AccountHolders
    {
        get => _accountHolders ?? throw new UninitializedPropertyException();
        set => _accountHolders = value;
    }

    // public List<Transaction> Transactions
    // {
    //     get => _transactions ?? throw new UninitializedPropertyException();
    //     set => _transactions = value;
    // }
}
