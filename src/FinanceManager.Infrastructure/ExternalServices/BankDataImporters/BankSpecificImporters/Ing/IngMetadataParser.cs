using FinanceManager.Infrastructure.Files.Csv;

namespace FinanceManager.Infrastructure.ExternalServices.BankDataImporters.BankSpecificImporters.Ing;
public class IngMetadataParser : IIngMetadataParser
{
    public IngBankMetadata ParseMetadata(ICsvReader reader)
    {
        var lineFields = GetFirstMetadataRow(reader);
        var bankMetadata = new IngBankMetadata();

        while (!reader.EndOfData && LineContainsMetadata(lineFields))
        {
            bankMetadata = ParseLineIntoObject(lineFields, bankMetadata);
            lineFields = reader.ReadColumns();
        }
        return bankMetadata;
    }

    private static IngBankMetadata ParseLineIntoObject(IEnumerable<string> lineFields, IngBankMetadata sourceMetadata)
    {
        var keyString = lineFields.First();
        var value = lineFields.ElementAt(1);
        #pragma warning disable IDE0055
        return !Enum.TryParse(typeof(IngMetadataKeys), keyString, out var key)
            ? throw new ArgumentException(message: "Invalid enum value", paramName: nameof(lineFields))
            : key switch
            {
                IngMetadataKeys.IBAN        => sourceMetadata with { IBAN = ParseIban(value) },
                IngMetadataKeys.Bank        => sourceMetadata with { BankName = value },
                IngMetadataKeys.Kontoname   => sourceMetadata with { AccountName = value },
                IngMetadataKeys.Kunde       => sourceMetadata with { Customers = value },
                IngMetadataKeys.Saldo       => sourceMetadata with { Balance = value },
                IngMetadataKeys.Zeitraum    => sourceMetadata with { TimeRange = value },
                _                           => throw new ArgumentException(
                                                message: "Invalid enum value",
                                                paramName: nameof(lineFields))
            };
        #pragma warning restore IDE0055
    }

    private static string ParseIban(string iban) => iban.Replace(" ", "");

    private static IEnumerable<string> GetFirstMetadataRow(ICsvReader reader)
    {
        var lineFields = reader.ReadColumns();
        while (!reader.EndOfData && !LineContainsMetadata(lineFields))
        {
            lineFields = reader.ReadColumns();
        }

        return
            !reader.EndOfData
            ? lineFields
            : throw new InvalidOperationException("The file did not contain the expected metadata");
    }

    private static bool LineContainsMetadata(IEnumerable<string> lineFields) =>
        lineFields is not null
        && lineFields.Count() >= 2
        && Enum.IsDefined(typeof(IngMetadataKeys), lineFields.First());
}
