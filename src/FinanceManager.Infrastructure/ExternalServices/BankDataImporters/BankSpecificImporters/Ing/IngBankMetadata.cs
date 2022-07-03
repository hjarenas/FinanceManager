namespace FinanceManager.Infrastructure.ExternalServices.BankDataImporters.BankSpecificImporters.Ing;
public record BankMetadata
{
    public string? IBAN { get; set; }
    public string? AccountName { get; set; }
    public string? BankName { get; set; }
    public string? Customers { get; set; }
    public string? TimeRange { get; set; }
    public string? Balance { get; set; }
}