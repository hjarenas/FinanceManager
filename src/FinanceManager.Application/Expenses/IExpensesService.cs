using FinanceManager.Domain.ExpensesAggregate;
namespace FinanceManager.Application.Expenses;
public interface IExpensesService
{
    Task AddExpenseAsync(CreateExpenseRequest expense, CancellationToken cancellationToken);
    IEnumerable<Expense> GetAllExpenses();
    Expense? GetSingleExpense(Guid expenseId);
}