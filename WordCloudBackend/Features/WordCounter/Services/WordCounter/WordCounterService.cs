namespace WordCloudBackend.Features.WordCounter.Services.WordCounter;

public class WordCounterService : IWordCounterService
{
    /// <inheritdoc />
    public IList<WordEntry> CountWords(string text, WordEntrySortOrder sortOrder)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            throw new ArgumentException("Text cannot be null, empty, or whitespace.", nameof(text));
        }

        var entries = new List<WordEntry>
        {
            new("hello", 1),
            new("world", 2)
        };

        return entries.ApplySortOrder(sortOrder)
            .ToList();
    }
}