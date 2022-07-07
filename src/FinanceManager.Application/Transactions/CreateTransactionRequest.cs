using FinanceManager.Application.Common.Mappings;
using FinanceManager.Domain.AccountsAggregate;
using FinanceManager.Domain.TransactionsAggregate;

namespace FinanceManager.Application.Transactions;
public record CreateTransactionRequest : IMapTo<Transaction>
{
    private Category? _category;
    private BankAccount? _bankAccount;
    public DateOnly BookingDate { get; set; }
    public string Recipient { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string IntendedUse { get; set; } = string.Empty;
    public Direction Direction { get; set; }
    public decimal AmountInEur { get; set; }
    public Category Category
    {
        set => _category = value;
        get => _category ?? throw new InvalidOperationException($"Uninitialized property: {nameof(Category)}");
    }
    public BankAccount BankAccount
    {
        set => _bankAccount = value;
        get => _bankAccount ?? throw new InvalidOperationException($"Uninitialized property: {nameof(BankAccount)}");
    }
}
