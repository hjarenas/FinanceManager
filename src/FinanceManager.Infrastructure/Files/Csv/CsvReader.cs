using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic.FileIO;

namespace FinanceManager.Infrastructure.Files.Csv;
public class CsvReader : ICsvReader
{
    private TextFieldParser _reader;
    private string _filePath;
    private readonly ILogger<CsvReader> _logger;

    public CsvReader(string filePath, string[] delimiters, ILogger<CsvReader> logger)
    {
        _reader = new TextFieldParser(filePath);
        _reader.Delimiters = delimiters;
        _filePath = filePath;
        _logger = logger;
    }
    public bool EndOfData => _reader.EndOfData;

    public string[] ReadFields()
    {
        return _reader.ReadFields() ?? new string[] { };
    }

    public string[] ReadFirstRowWithNColumns(int numberOfColumns)
    {
        while (!_reader.EndOfData)
        {
            var fields = _reader.ReadFields();
            if (fields?.Length == numberOfColumns)
            {
                return fields;
            }
        }

        _logger.LogError(
            "The loaded {file} does not have a line with {numberOfColumns} columns",
             _filePath,
             numberOfColumns);
        throw new FileLoadException($"The loaded file does not have a line with '{numberOfColumns}' columns");
    }
}
