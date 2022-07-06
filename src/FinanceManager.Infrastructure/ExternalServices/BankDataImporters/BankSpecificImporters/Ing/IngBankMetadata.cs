namespace FinanceManager.Infrastructure.ExternalServices.BankDataImporters.BankSpecificImporters.Ing;
public record IngBankMetadata
{
    public string? IBAN { get; set; }
    public string? AccountName { get; set; }
    public string? BankName { get; set; }
    public string? Customers { get; set; }
    public string? TimeRange { get; set; }
    public string? Balance { get; set; }
}
public enum IngMetadataKeys
{
    IBAN,
    Kontoname,
    Bank,
    Kunde,
    Zeitraum,
    Saldo
}
