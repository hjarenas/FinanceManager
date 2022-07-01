namespace FinanceManager.Infrastructure.Files;
public interface IFileSearchService
{
    IEnumerable<string> GetMatchingFilesInDirectory(string directoryPath, string matchingPattern);
}