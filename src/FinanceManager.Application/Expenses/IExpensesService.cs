using FinanceManager.Domain.ExpensesAggregate;
namespace FinanceManager.Application.Expenses;
public interface IExpensesService
{
    Task<Expense> AddExpenseAsync(CreateExpenseRequest createExpenseRequest, CancellationToken cancellationToken);
    IEnumerable<Expense> GetAllExpenses();
    Expense? GetSingleExpense(Guid expenseId);
}
