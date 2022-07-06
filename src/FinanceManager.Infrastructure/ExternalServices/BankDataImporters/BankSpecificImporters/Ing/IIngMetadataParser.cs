using FinanceManager.Infrastructure.Files.Csv;

namespace FinanceManager.Infrastructure.ExternalServices.BankDataImporters.BankSpecificImporters.Ing;
public interface IIngMetadataParser
{
    IngBankMetadata ParseMetadata(ICsvReader reader);
}
