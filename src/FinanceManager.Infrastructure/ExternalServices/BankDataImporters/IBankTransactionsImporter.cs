namespace FinanceManager.Infrastructure.ExternalServices.BankDataImporters;

public interface IBankTransactionsImporter
{
    Task ImportDataAsync();
}
