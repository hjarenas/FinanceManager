using AutoMapper;
using FinanceManager.Application.Common.Interfaces;
using FinanceManager.Domain.TransactionsAggregate;

namespace FinanceManager.Application.Transactions;
public class TransactionsService : ITransactionsService
{
    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _context;
    public TransactionsService(IMapper mapper, IApplicationDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<Transaction> AddExpenseAsync(
        CreateTransactionRequest createExpenseRequest,
        CancellationToken cancellationToken)
    {
        var expense = _mapper.Map<Transaction>(createExpenseRequest);
        _context.Transactions.Add(expense);
        await _context.SaveChangesAsync(cancellationToken);
        return expense;
    }

    public IEnumerable<Transaction> GetAllExpenses() => _context.Transactions.ToList();

    public Transaction? GetSingleExpense(Guid expenseId) =>
        _context.Transactions.FirstOrDefault(e => e.Id == expenseId);
}
