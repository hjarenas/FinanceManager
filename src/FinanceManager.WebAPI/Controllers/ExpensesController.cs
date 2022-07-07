using FinanceManager.Application.Transactions;
using Microsoft.AspNetCore.Mvc;

namespace FinanceManager.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ExpensesController : ControllerBase
{
    private readonly ILogger<ExpensesController> _logger;
    private readonly ITransactionsService _expensesService;

    public ExpensesController(ILogger<ExpensesController> logger, ITransactionsService expensesService)
    {
        _logger = logger;
        _expensesService = expensesService;
    }

    [HttpGet(Name = nameof(ListExpenses))]
    public IActionResult ListExpenses() =>
        Ok(_expensesService.GetAllExpenses());

    [HttpPost]
    public async Task<IActionResult> AddExpenseAsync(
        [FromBody] CreateTransactionRequest createExpenseRequest,
        CancellationToken cancellationToken)
    {
        var expense = await _expensesService.AddExpenseAsync(createExpenseRequest, cancellationToken);
        return CreatedAtRoute(nameof(GetExpenseById), new { expenseId = expense.ExpenseId }, expense);
    }

    [HttpGet("{expenseId}", Name = nameof(GetExpenseById))]
    public IActionResult GetExpenseById([FromRoute] Guid expenseId)
    {
        var expense = _expensesService.GetSingleExpense(expenseId);
        return expense is not null
            ? Ok(expense)
            : NotFound();

    }
}
