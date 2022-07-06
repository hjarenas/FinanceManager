using FinanceManager.Application.Expenses.Commands.ImportExpenses;
using FinanceManager.Infrastructure.Files;
using FinanceManager.Infrastructure.Files.Csv;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace FinanceManager.Infrastructure.ExternalServices.BankDataImporters.BankSpecificImporters.Ing;
public class IngTransactionsImporter : IBankTransactionsImporter
{
    private const string FileNamePattern = @"Umsatzanzeige_([A-Z0-9]+)_([0-9]{8})\.csv";
    private const string CsvDelimiter = ";";
    private readonly CsvImporterOptions _csvImporterOptions;
    private readonly IFileSearchService _fileSearchService;
    private readonly ICsvReaderFactory _csvReaderFactory;
    private readonly IIngMetadataParser _ingMetadataParser;
    private readonly IIngTransactionDetailsParser _ingTransactionDetailsParser;
    private readonly ISender _mediator;
    private readonly ILogger<IngTransactionsImporter> _logger;

    public IngTransactionsImporter(
        IOptions<CsvImporterOptions> csvImporterOptions,
        IFileSearchService fileSearchService,
        ICsvReaderFactory csvReaderFactory,
        IIngMetadataParser ingMetadataParser,
        IIngTransactionDetailsParser ingTransactionDetailsParser,
        ISender mediator,
        ILogger<IngTransactionsImporter> logger)
    {
        _csvImporterOptions = csvImporterOptions.Value;
        _fileSearchService = fileSearchService;
        _csvReaderFactory = csvReaderFactory;
        _ingMetadataParser = ingMetadataParser;
        _ingTransactionDetailsParser = ingTransactionDetailsParser;
        _mediator = mediator;
        _logger = logger;
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

        _ingMetadataParser.ParseMetadata(reader);
        var transactionDetails = _ingTransactionDetailsParser.ParseTransactionDetails(reader);
        _logger.LogInformation("Transaction details {TransactionDetails}", transactionDetails);
    }

    private IEnumerable<string> GetMatchingFiles() =>
        string.IsNullOrEmpty(_csvImporterOptions.FilesLocation)
            ? throw new InvalidOperationException("Files cannot be imported if the location is not configured")
            : _fileSearchService.GetMatchingFilesInDirectory(_csvImporterOptions.FilesLocation, FileNamePattern);
}
