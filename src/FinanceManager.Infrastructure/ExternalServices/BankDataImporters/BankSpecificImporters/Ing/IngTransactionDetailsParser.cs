using System.Globalization;
using FinanceManager.Infrastructure.Files.Csv;

namespace FinanceManager.Infrastructure.ExternalServices.BankDataImporters.BankSpecificImporters.Ing;
public class IngTransactionDetailsParser : IIngTransactionDetailsParser
{
    private const string DateFormat = "dd.MM.yyyy";
    public IEnumerable<IngTransactionDetails> ParseTransactionDetails(ICsvReader reader)
    {
        var transactionDetails = new List<IngTransactionDetails>();
        ValidateTransactionDetailsHeaders(reader);
        do
        {
            var lineFields = reader.ReadColumns();
            transactionDetails.Add(ParseTransactionDetailLine(lineFields));
        } while (!reader.EndOfData);
        return transactionDetails;
    }
    private static IngTransactionDetails ParseTransactionDetailLine(IReadOnlyList<string> lineFields)
    {
        var transactionDetails = new IngTransactionDetails();
        for (var i = 0; i < lineFields.Count; i++)
        {
            transactionDetails = ParseLineIntoObject(i, lineFields[i], transactionDetails);
        }
        return transactionDetails;
    }
    private static IngTransactionDetails ParseLineIntoObject(
        int columnIndex,
        string stringValue,
        IngTransactionDetails currentObject) =>
        columnIndex switch
        {
            0 => currentObject with { BookingDate = DateOnly.ParseExact(stringValue, DateFormat) },
            1 => currentObject with { ValueDate = DateOnly.ParseExact(stringValue, DateFormat) },
            2 => currentObject with { ThirdParty = stringValue },
            3 => currentObject with { BookingType = stringValue },
            4 => currentObject with { Notes = stringValue },
            5 => currentObject with { Description = stringValue },
            6 => currentObject with { RemainingBalance = ParseDoubleFromString(stringValue) },
            7 => currentObject with { RemainingBalanceCurrency = stringValue },
            8 => currentObject with { Amount = ParseDoubleFromString(stringValue) },
            9 => currentObject with { AmountCurrency = stringValue },
            _ => throw new ArgumentException(
                    message: "Invalid Index Value: " + columnIndex,
                    paramName: nameof(columnIndex))
        };
    private static double ParseDoubleFromString(ReadOnlySpan<char> stringValue) =>
        double.TryParse(stringValue, NumberStyles.Any, new CultureInfo("de-DE"), out var result)
            ? result
            : 0d;
    private static IEnumerable<string> ValidateTransactionDetailsHeaders(ICsvReader reader)
    {
        var lineFields = reader.ReadColumns();
        while (!reader.EndOfData && lineFields.Any() && lineFields.First() != "Buchung")
        {
            lineFields = reader.ReadColumns();
        }
        if (reader.EndOfData)
        {
            throw new InvalidOperationException("The file did not contain the expected transaction details headers");
        }
        else if (lineFields.Length != IngTransactionDetails.CsvFileHeaders.Length)
        {
            throw new InvalidOperationException("The Transaction Detail Headers has changed!");
        }
        for (var i = 0; i < lineFields.Length; i++)
        {
            if (lineFields[i] != IngTransactionDetails.CsvFileHeaders[i])
            {
                throw new InvalidOperationException("The Transaction Detail Headers has changed!");
            }
        }
        return lineFields;
    }
}
