using FinanceManager.Application.Expenses.Commands.ImportExpenses;

namespace FinanceManager.Infrastructure.ExternalServices.BankDataImporters;
public class TransactionsImporterService : ITransactionsImporterService
{
    private readonly IEnumerable<IBankTransactionsImporter> _importers;

    public TransactionsImporterService(IEnumerable<IBankTransactionsImporter> importers)
    {
        _importers = importers;
    }
    public async Task<IEnumerable<ImportExpensesCommand>> ImportDataFromAllBanks()
    {
        var expensesFromAllBanks = new List<ImportExpensesCommand>();
        foreach(var importer in _importers)
        {
            var expensesFromSingleBank = await importer.ImportData();
            expensesFromAllBanks.Add(expensesFromSingleBank);
        }
        return expensesFromAllBanks;
    }
}