using AutoMapper;
using FinanceManager.Domain.ExpensesAggregate;
namespace FinanceManager.Application.Expenses;
public class ExpensesService : IExpensesService
{
    private readonly IMapper _mapper;
    private IList<Expense> _expenses;
    public ExpensesService(IMapper mapper)
    {
        _expenses = new List<Expense>();
        _mapper = mapper;
    }
    public void AddExpense(CreateExpenseRequest createExpenseRequest)
    {
        var expense = _mapper.Map<Expense>(createExpenseRequest);
        _expenses.Add(expense);
    }

    public IEnumerable<Expense> GetAllExpenses()
    {
        return _expenses;
    }

    public Expense? GetSingleExpense(Guid expenseId)
    {
        return _expenses.FirstOrDefault(e => e.ExpenseId == expenseId);
    }
}
