using FinanceManager.Application.Expenses;
using FinanceManager.Domain.ExpensesAggregate;
using Microsoft.AspNetCore.Mvc;

namespace FinanceManager.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExpensesController : ControllerBase
    {
        private readonly ILogger<ExpensesController> _logger;
        private readonly IExpensesService _expensesService;

        public ExpensesController(ILogger<ExpensesController> logger, IExpensesService expensesService)
        {
            _logger = logger;
            _expensesService = expensesService;
        }

        [HttpGet(Name = nameof(ListExpenses))]
        public IEnumerable<Expense> ListExpenses()
        {
            return _expensesService.GetAllExpenses();
        }

        [HttpPost]
        public IActionResult AddExpense(CreateExpenseRequest createExpenseRequest)
        {
            _expensesService.AddExpense(createExpenseRequest);
            return Accepted();
        }

        [HttpGet]
        [Route("/{expenseId}")]
        public IActionResult GetExpense([FromRoute] Guid expenseId)
        {
            var expense = _expensesService.GetSingleExpense(expenseId);
            return expense is not null 
                ? Ok(expense)
                : NotFound();

        }
    }
}