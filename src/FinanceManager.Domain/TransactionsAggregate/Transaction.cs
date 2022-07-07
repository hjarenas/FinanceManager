using FinanceManager.Domain.AccountsAggregate;
using SharedKernel.Exceptions;

namespace FinanceManager.Domain.TransactionsAggregate;
public class Transaction
{
    private string? _recipient;
    private string? _description;
    private string? _intendedUse;
    private Category? _category;
    private BankAccount? _bankAccount;
    public Guid? TransactionId { get; init; }
    public DateOnly BookingDate { get; set; }
    public string Recipient
    {
        get => _recipient ?? throw new UninitializedPropertyException();
        set => _recipient = value;
    }
    public string Description
    {
        get => _description ?? throw new UninitializedPropertyException();
        set => _description = value;
    }
    /// <summary>
    /// Verwendungszweck
    /// </summary>
    public string IntendedUse
    {
        get => _intendedUse ?? throw new UninitializedPropertyException();
        set => _intendedUse = value;
    }
    public Direction Direction { get; set; }
    public decimal AmountInEur { get; set; }
    public bool Reimbursable { get; set; }
    public bool IsRecurring { get; set; }
    public Category Category
    {
        get => _category ?? throw new UninitializedPropertyException();
        set => _category = value;
    }

    public BankAccount BankAccount
    {
        get => _bankAccount ?? throw new UninitializedPropertyException();
        set => _bankAccount = value;
    }
}
