namespace FinanceManager.Infrastructure.ExternalServices.BankDataImporters.BankSpecificImporters.Ing;
public record IngTransactionDetails
{
    public DateOnly BookingDate { get; set; }
    public DateOnly ValueData { get; set; }
    public string ThirdParty { get; set; } = string.Empty;
    public string BookingType { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public double RemainingBalance { get; set; }
    public string RemainingBalanceCurrency { get; set; } = string.Empty;
    public double Amount { get; set; }
    public string AmountCurrency { get; set; } = string.Empty;
}
