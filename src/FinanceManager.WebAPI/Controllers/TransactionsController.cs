using FinanceManager.Application.Transactions;
using Microsoft.AspNetCore.Mvc;

namespace FinanceManager.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransactionsController : ControllerBase
{
    private readonly ILogger<TransactionsController> _logger;
    private readonly ITransactionsService _transactionsService;

    public TransactionsController(ILogger<TransactionsController> logger, ITransactionsService transactionsService)
    {
        _logger = logger;
        _transactionsService = transactionsService;
    }

    [HttpGet(Name = nameof(ListExpenses))]
    public IActionResult ListExpenses() =>
        Ok(_transactionsService.GetAllExpenses());

    [HttpPost]
    public async Task<IActionResult> AddExpenseAsync(
        [FromBody] CreateTransactionRequest createExpenseRequest,
        CancellationToken cancellationToken)
    {
        var expense = await _transactionsService.AddExpenseAsync(createExpenseRequest, cancellationToken);
        return CreatedAtRoute(nameof(GetExpenseById), new { expenseId = expense.Id }, expense);
    }

    [HttpGet("{expenseId}", Name = nameof(GetExpenseById))]
    public IActionResult GetExpenseById([FromRoute] Guid expenseId)
    {
        var expense = _transactionsService.GetSingleExpense(expenseId);
        return expense is not null
            ? Ok(expense)
            : NotFound();

    }
}
