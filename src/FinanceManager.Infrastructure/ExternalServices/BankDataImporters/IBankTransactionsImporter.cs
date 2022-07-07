using FinanceManager.Application.Transactions.Commands.ImportTransactions;

namespace FinanceManager.Infrastructure.ExternalServices.BankDataImporters;

public interface IBankTransactionsImporter
{
    Task<ImportTransactionsCommand> ImportData();
}
