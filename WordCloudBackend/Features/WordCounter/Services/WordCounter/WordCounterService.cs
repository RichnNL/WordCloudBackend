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

        var words = text.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        var wordCounts = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

        foreach (var word in words)
        {
            if (wordCounts.TryGetValue(word, out var count))
            {
                wordCounts[word] = count + 1;
            }
            else
            {
                wordCounts[word] = 1;
            }
        }

        var entries = wordCounts.Select(kvp => new WordEntry(kvp.Key, kvp.Value));

        return entries.ApplySortOrder(sortOrder).ToList();
    }
}