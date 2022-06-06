using FinanceManager.Domain.ExpensesAggregate;
namespace FinanceManager.Application.Expenses;
public interface IExpensesService
{
    void AddExpense(CreateExpenseRequest expense);
    IEnumerable<Expense> GetAllExpenses();
    Expense? GetSingleExpense(Guid expenseId);
}