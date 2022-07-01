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

        }
        return await Task.FromResult(new ImportExpensesCommand());
    }

    private void GetRawData(string fileName)
    {
        var reader = _csvReaderFactory.CreateCsvReader(fileName, new[] { CsvDelimiter });
        //skip first line
        reader.ReadFields();
        var bankMetadata = GetMetadata(reader);
        var transactionDetails = GetTransactionsInformation(reader);
    }
    private BankMetadata GetMetadata(ICsvReader reader)
    {
        var lineFields = reader.ReadFirstRowWithNColumns(2);
        do
        {

        } while((lineFields = reader.ReadFields()).Length == 2);
        return new BankMetadata();
    }

    private IEnumerable<IngTransactionDetails> GetTransactionsInformation(ICsvReader reader)
    {
        var transactionDetails = new List<IngTransactionDetails>();
        var headers = reader.ReadFirstRowWithNColumns(10);
        while(!reader.EndOfData)
        {
            var transactionDetailsLine = reader.ReadFields();
        }
        return transactionDetails;
    }

    private IEnumerable<string> GetMatchingFiles()
    {
        if (string.IsNullOrEmpty(_csvImporterOptions.FilesLocation))
        {
            throw new InvalidOperationException("Files cannot be imported if the location is not configured");
        }

        return _fileSearchService.GetMatchingFilesInDirectory(_csvImporterOptions.FilesLocation, FileNamePattern);
    }
}