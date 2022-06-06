using AutoMapper;
using CleanArchitecture.Application.Common.Interfaces;
using FinanceManager.Domain.ExpensesAggregate;
namespace FinanceManager.Application.Expenses;
public class ExpensesService : IExpensesService
{
    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _context;
    private IList<Expense> _expenses;
    public ExpensesService(IMapper mapper,IApplicationDbContext context)
    {
        _expenses = new List<Expense>();
        _mapper = mapper;
        _context = context;
    }

    public async Task AddExpenseAsync(CreateExpenseRequest createExpenseRequest, CancellationToken cancellationToken)
    {
        var expense = _mapper.Map<Expense>(createExpenseRequest);
        _context.Expenses.Add(expense);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public IEnumerable<Expense> GetAllExpenses()
    {
        return _context.Expenses.ToList();
    }

    public Expense? GetSingleExpense(Guid expenseId)
    {
        return _expenses.FirstOrDefault(e => e.ExpenseId == expenseId);
    }
}
