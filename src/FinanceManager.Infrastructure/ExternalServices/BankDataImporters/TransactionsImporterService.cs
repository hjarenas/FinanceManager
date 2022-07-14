namespace FinanceManager.Infrastructure.ExternalServices.BankDataImporters;
public class TransactionsImporterService : ITransactionsImporterService
{
    private readonly IEnumerable<IBankTransactionsImporter> _importers;

    public TransactionsImporterService(IEnumerable<IBankTransactionsImporter> importers) => _importers = importers;
    public async Task ImportDataFromAllBanksAsync()
    {
        foreach (var importer in _importers)
        {
            await importer.ImportDataAsync();
        }
    }
}
