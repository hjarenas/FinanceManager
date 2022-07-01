namespace FinanceManager.Infrastructure.ExternalServices.BankDataImporters;
public record CsvImporterOptions
{
    public string? FilesLocation { get; set; }
}