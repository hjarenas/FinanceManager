using FinanceManager.Application.Expenses.Commands.ImportExpenses;
using FinanceManager.Infrastructure.Files;
using FinanceManager.Infrastructure.Files.Csv;
using MediatR;
using Microsoft.Extensions.Options;

namespace FinanceManager.Infrastructure.ExternalServices.BankDataImporters.BankSpecificImporters.Ing;
public class IngTransactionsImporter : IBankTransactionsImporter
{
    private const string FileNamePattern = @"Umsatzanzeige_([A-Z0-9]+)_([0-9]{8})\.csv";
    private const string CsvDelimiter = ";";
    private readonly CsvImporterOptions _csvImporterOptions;
    private readonly IFileSearchService _fileSearchService;
    private readonly ICsvReaderFactory _csvReaderFactory;
    private readonly ISender _mediator;

    public IngTransactionsImporter(
        IOptions<CsvImporterOptions> csvImporterOptions,
        IFileSearchService fileSearchService,
        ICsvReaderFactory csvReaderFactory,
        ISender mediator)
    {
        _csvImporterOptions = csvImporterOptions.Value;
        _fileSearchService = fileSearchService;
        _csvReaderFactory = csvReaderFactory;
        _mediator = mediator;
    }


    public async Task<ImportExpensesCommand> ImportData()
    {
        var filesToImport = GetMatchingFiles();
        foreach (var fileName in filesToImport)
        {
            GetRawData(fileName);
        }
        return await Task.FromResult(new ImportExpensesCommand());
    }

    private void GetRawData(string fileName)
    {
        var reader = _csvReaderFactory.CreateCsvReader(fileName, new[] { CsvDelimiter });
        //skip first line
        reader.ReadColumns();

        _ = GetMetadata(reader);

        _ = GetTransactionsInformation(reader);
    }
    private static BankMetadata GetMetadata(ICsvReader reader)
    {
        _ = reader.ReadFirstRowWithNColumns(2);

        do
        {

        } while ((_ = reader.ReadColumns()).Length == 2);
        return new BankMetadata();
    }

    private static IEnumerable<IngTransactionDetails> GetTransactionsInformation(ICsvReader reader)
    {
        var transactionDetails = new List<IngTransactionDetails>();
        var headers = reader.ReadFirstRowWithNColumns(10);
        if (!headers.Any())
        {
            throw new InvalidOperationException("The file has no lines with the expected number of columns");
        }
        while (!reader.EndOfData)
        {
            var transactionDetailsLine = reader.ReadColumns();
            transactionDetails.Add(new IngTransactionDetails
            {
                Notes = transactionDetailsLine?.FirstOrDefault() ?? ""
            });
        }
        return transactionDetails;
    }

    private IEnumerable<string> GetMatchingFiles() =>
        string.IsNullOrEmpty(_csvImporterOptions.FilesLocation)
            ? throw new InvalidOperationException("Files cannot be imported if the location is not configured")
            : _fileSearchService.GetMatchingFilesInDirectory(_csvImporterOptions.FilesLocation, FileNamePattern);
}
