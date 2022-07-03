using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic.FileIO;

namespace FinanceManager.Infrastructure.Files.Csv;
public class CsvReader : TextFieldParser, ICsvReader
{
    private readonly string _filePath;
    private readonly ILogger<CsvReader> _logger;

    public CsvReader(string filePath, string[] delimiters, ILogger<CsvReader> logger)
        : base(filePath)
    {
        Delimiters = delimiters;
        _filePath = filePath;
        _logger = logger;
    }

    public string[] ReadColumns() => ReadFields() ?? Array.Empty<string>();

    public string[] ReadFirstRowWithNColumns(int numberOfColumns)
    {
        while (!EndOfData)
        {
            var fields = ReadColumns();
            if (fields.Length == numberOfColumns)
            {
                return fields;
            }
        }

        _logger.LogError(
            "The loaded {File} does not have a line with {NumberOfColumns} columns",
             _filePath,
             numberOfColumns);
        throw new FileLoadException($"The loaded file does not have a line with '{numberOfColumns}' columns");
    }
}
