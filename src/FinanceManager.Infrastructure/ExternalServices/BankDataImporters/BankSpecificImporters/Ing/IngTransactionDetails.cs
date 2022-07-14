using System.Text.RegularExpressions;
using FinanceManager.Application.BankAccounts.Commands.ImportBankAccounts;
using FinanceManager.Application.Transactions.Commands.ImportTransactions;
using FinanceManager.Domain.TransactionsAggregate;

namespace FinanceManager.Infrastructure.ExternalServices.BankDataImporters.BankSpecificImporters.Ing;
public record IngTransactionDetails
{
    private readonly Regex _categoriesRegex = new("#category:([a-zA-Z0-9]+)", RegexOptions.Compiled);
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

    public ImportTransactionCommand ToImportTransactionCommand(IngBankMetadata metadata) =>
        new(
            GetBankAccountInformation(metadata),
            BookingDate,
            ThirdParty,
            Description,
            BookingType,
            Amount > 0 ? Direction.Incoming : Direction.Outgoing,
            Math.Abs(Amount),
            false,
            GetCategories()
        );
    private static ImportBankAccountCommand GetBankAccountInformation(IngBankMetadata metadata)
    {
        ArgumentNullException.ThrowIfNull(metadata.IBAN);
        ArgumentNullException.ThrowIfNull(metadata.BankName);
        return new(metadata.IBAN, metadata.BankName, GetBankAccountHolders(metadata));
    }
    private IEnumerable<string> GetCategories()
    {
        var categories = new List<string>();
        var categoryMatches = _categoriesRegex.Matches(Notes);
        foreach (var matchObject in categoryMatches)
        {
            var match = (matchObject as Match)!;
            var group = match.Groups[1];
            categories.Add(group.Value);
        }
        return categories;
    }

    private static IEnumerable<CreateAccountHolderDto> GetBankAccountHolders(IngBankMetadata metadata)
    {
        var accountHolders = new List<CreateAccountHolderDto>();
        var customers = metadata.Customers?.Split(',');
        if (customers is null)
        {
            return accountHolders;
        }

        foreach (var customer in customers)
        {
            var names = customer.Trim().Split(' ');
            accountHolders.Add(new(names.First(), names.Last()));
        }
        return accountHolders;
    }

    public static string[] CsvFileHeaders { get; } = {
        "Buchung",
        "Valuta",
        "Auftraggeber/Empfänger",
        "Buchungstext",
        "Notiz",
        "Verwendungszweck",
        "Saldo",
        "Währung",
        "Betrag",
        "Währung"
    };
}
