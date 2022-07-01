using FinanceManager.Application.Expenses.Commands.ImportExpenses;

namespace FinanceManager.Infrastructure.ExternalServices.BankDataImporters;

public interface IBankTransactionsImporter
{
    Task<ImportExpensesCommand> ImportData();
}