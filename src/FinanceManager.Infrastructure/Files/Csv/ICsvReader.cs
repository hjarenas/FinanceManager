namespace FinanceManager.Infrastructure.Files.Csv;
public interface ICsvReader
{
    bool EndOfData { get; }
    string[] ReadColumns();
    string[] ReadFirstRowWithNColumns(int numberOfColumns);
}
