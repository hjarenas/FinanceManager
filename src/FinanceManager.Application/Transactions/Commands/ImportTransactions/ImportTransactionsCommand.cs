using SharedKernel.Exceptions;

namespace FinanceManager.Application.Transactions.Commands.ImportTransactions;
public record ImportTransactionsCommand
{
    private string? _recipient;
    private string? _sender;
    private IEnumerable<string>? _categories;
    private string? _transactionDescription;

    public DateOnly BookingDate { get; init; }
    public string Recipient
    {
        get => _recipient ?? throw new UninitializedPropertyException();
        init => _recipient = value;
    }
    public string Sender
    {
        get => _sender ?? throw new UninitializedPropertyException();
        init => _sender = value;
    }
    public IEnumerable<string> Categories
    {
        get => _categories ?? throw new UninitializedPropertyException();
        init => _categories = value;
    }
    public string TransactionDescription
    {
        get => _transactionDescription ?? throw new UninitializedPropertyException();
        init => _transactionDescription = value;
    }
    public double TransactionAmount { get; init; }
    public bool IsReimbursable { get; init; }
    public bool IsRecurring { get; init; }
    public bool IsRequired { get; init; }
}
