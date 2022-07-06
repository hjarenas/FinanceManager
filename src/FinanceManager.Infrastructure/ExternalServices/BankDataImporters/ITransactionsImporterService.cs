using FinanceManager.Application.Expenses.Commands.ImportExpenses;

namespace FinanceManager.Infrastructure.ExternalServices.BankDataImporters;
public interface ITransactionsImporterService
{
    Task<IEnumerable<ImportExpensesCommand>> ImportDataFromAllBanksAsync();
}
