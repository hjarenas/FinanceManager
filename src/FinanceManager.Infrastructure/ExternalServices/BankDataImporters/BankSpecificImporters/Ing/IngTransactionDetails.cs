namespace FinanceManager.Infrastructure.ExternalServices.BankDataImporters.BankSpecificImporters.Ing;
public record IngTransactionDetails
{
    public DateOnly BookingDate { get; init; }
    public DateOnly ValueDate { get; init; }
    public string ThirdParty { get; init; } = string.Empty;
    public string BookingType { get; init; } = string.Empty;
    public string Notes { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public double RemainingBalance { get; init; }
    public string RemainingBalanceCurrency { get; init; } = string.Empty;
    public double Amount { get; init; }
    public string AmountCurrency { get; init; } = string.Empty;
    public static string[] CsvFileHeaders { get; } = {
        "Buchung",
        "Valuta",
        "Auftraggeber/Empf�nger",
        "Buchungstext",
        "Notiz",
        "Verwendungszweck",
        "Saldo",
        "W�hrung",
        "Betrag",
        "W�hrung"
    };
}
