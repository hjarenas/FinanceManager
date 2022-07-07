using FinanceManager.Domain.TransactionsAggregate;

namespace FinanceManager.Application.Transactions;
public interface ITransactionsService
{
    Task<Transaction> AddExpenseAsync(CreateTransactionRequest createExpenseRequest, CancellationToken cancellationToken);
    IEnumerable<Transaction> GetAllExpenses();
    Transaction? GetSingleExpense(Guid expenseId);
}
