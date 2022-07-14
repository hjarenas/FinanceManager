namespace FinanceManager.Infrastructure.ExternalServices.BankDataImporters;
public interface ITransactionsImporterService
{
    Task ImportDataFromAllBanksAsync();
}
