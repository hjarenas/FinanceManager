using FinanceManager.Application.Transactions.Commands.ImportTransactions;

namespace FinanceManager.Infrastructure.ExternalServices.BankDataImporters;
public class TransactionsImporterService : ITransactionsImporterService
{
    private readonly IEnumerable<IBankTransactionsImporter> _importers;

    public TransactionsImporterService(IEnumerable<IBankTransactionsImporter> importers) => _importers = importers;
    public async Task<IEnumerable<ImportTransactionsCommand>> ImportDataFromAllBanksAsync()
    {
        var expensesFromAllBanks = new List<ImportTransactionsCommand>();
        foreach (var importer in _importers)
        {
            var expensesFromSingleBank = await importer.ImportData();
            expensesFromAllBanks.Add(expensesFromSingleBank);
        }
        return expensesFromAllBanks;
    }
}
