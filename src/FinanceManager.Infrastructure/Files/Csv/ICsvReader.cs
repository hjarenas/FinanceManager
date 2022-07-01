namespace FinanceManager.Infrastructure.Files.Csv;
public interface ICsvReader
{
    bool EndOfData { get; }
    string[] ReadFields();
    string[] ReadFirstRowWithNColumns(int numberOfColumns);
}