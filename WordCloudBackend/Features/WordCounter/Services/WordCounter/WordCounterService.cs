namespace WordCloudBackend.Features.WordCounter.Services.WordCounter;

public class WordCounterService : IWordCounterService
{
    public IList<WordEntry> CountWords(string text, WordEntrySortOrder sortOrder)
    {
        var entries = new List<WordEntry>
        {
            new("hello", 1),
            new("world", 2)
        };

        return entries.ApplySortOrder(sortOrder)
            .ToList();
    }
}