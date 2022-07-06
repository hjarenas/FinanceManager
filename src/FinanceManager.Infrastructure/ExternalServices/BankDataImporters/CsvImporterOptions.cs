namespace FinanceManager.Infrastructure.ExternalServices.BankDataImporters;
public record CsvImporterOptions
{
    public const string CsvImporterSectionName = "CsvImporter";
    public string? FilesLocation { get; set; }
}
