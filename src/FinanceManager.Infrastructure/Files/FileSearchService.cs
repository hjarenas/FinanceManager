using System.Text.RegularExpressions;

namespace FinanceManager.Infrastructure.Files;
public class FileSearchService : IFileSearchService
{
    public IEnumerable<string> GetMatchingFilesInDirectory(string directoryPath, string patternToMatch)
    {
         var regex = new Regex(patternToMatch);
        var files = Directory.GetFiles(directoryPath)
            .Where(fileName => regex.IsMatch(fileName));
        return files;
    }
}