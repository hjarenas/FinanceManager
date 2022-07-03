using Microsoft.Extensions.Logging;

namespace FinanceManager.Infrastructure.Files.Csv;
public class CsvReaderFactory : ICsvReaderFactory
{
    private readonly ILogger<CsvReader> _csvReaderLogger;

    public CsvReaderFactory(ILogger<CsvReader> csvReaderLogger) => _csvReaderLogger = csvReaderLogger;

    public ICsvReader CreateCsvReader(string filePath, string[] delimiters) =>
        new CsvReader(filePath, delimiters, _csvReaderLogger);
}
