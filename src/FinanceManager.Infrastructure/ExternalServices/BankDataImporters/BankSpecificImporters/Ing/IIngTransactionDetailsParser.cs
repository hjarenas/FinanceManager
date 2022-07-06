using FinanceManager.Infrastructure.Files.Csv;

namespace FinanceManager.Infrastructure.ExternalServices.BankDataImporters.BankSpecificImporters.Ing;
public interface IIngTransactionDetailsParser
{
    IEnumerable<IngTransactionDetails> ParseTransactionDetails(ICsvReader reader);
}
