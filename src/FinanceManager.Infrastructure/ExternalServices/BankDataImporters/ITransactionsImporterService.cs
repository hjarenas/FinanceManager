using FinanceManager.Application.Transactions.Commands.ImportTransactions;

namespace FinanceManager.Infrastructure.ExternalServices.BankDataImporters;
public interface ITransactionsImporterService
{
    Task<IEnumerable<ImportTransactionsCommand>> ImportDataFromAllBanksAsync();
}
