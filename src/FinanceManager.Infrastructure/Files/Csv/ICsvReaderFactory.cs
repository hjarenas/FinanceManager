namespace FinanceManager.Infrastructure.Files.Csv;
public interface ICsvReaderFactory
{
    ICsvReader CreateCsvReader(string filePath, string[] delimiters);
}