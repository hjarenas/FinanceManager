using FinanceManager.Domain.AccountsAggregate;
using FinanceManager.Domain.Common;
using SharedKernel.Exceptions;

namespace FinanceManager.Domain.TransactionsAggregate;
public class Transaction : BaseEntity
{
    private string? _thirdParty;
    private string? _description;
    private string? _intendedUse;
    private IEnumerable<Category>? _categories;
    private BankAccount? _bankAccount;
    public DateOnly BookingDate { get; set; }
    public string ThirdParty
    {
        get => _thirdParty ?? throw new UninitializedPropertyException();
        set => _thirdParty = value;
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
    public IEnumerable<Category> Categories
    {
        get => _categories ?? throw new UninitializedPropertyException();
        set => _categories = value;
    }

    public BankAccount BankAccount
    {
        get => _bankAccount ?? throw new UninitializedPropertyException();
        set => _bankAccount = value;
    }

    public Guid BankAccountId { get; set; }
}
