namespace WordCloudBackend.Features.WordCounter.Services.TextParser;

public class TextParserService : ITextParserService
{
    /// <inheritdoc />
    public string ParseText(string text, TextParserOptions options)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            return string.Empty;
        }

        // Replace 's or 'S with "s" to preserve the plural/possessive meaning as a single word 
        // e.g. "market's" -> "markets"
        text = text.Replace("'s", "s", StringComparison.OrdinalIgnoreCase);

        text = text.Trim();

        if (options.ConvertToLowerCase)
        {
            text = text.ToLowerInvariant();
        }
        
        var wordsEnumerable = ExtractPureWords(text);

        if (options.MinimumWordLength > 0)
        {
            wordsEnumerable = wordsEnumerable.Where(w => w.Length >= options.MinimumWordLength);
        }

        if (options.IgnoredWords.Count > 0)
        {
            wordsEnumerable = wordsEnumerable.Where(w => !options.IgnoredWords.Contains(w));
        }

        text = string.Join(" ", wordsEnumerable);
        
        return text;
    }
    
    /// <summary>
    /// Splits the text into individual words and filters out any corrupted words.
    /// A word is completely discarded if it contains any digits or symbols (e.g., "hello2" or "hel$oo").
    /// Words containing standard punctuation (like "hello," or "world.") safely pass through.
    /// </summary>
    /// <param name="text">The string to split and evaluate.</param>
    /// <returns>An enumerable sequence containing valid words.</returns>
    private static IEnumerable<string> ExtractPureWords(string text)
    {
        var wordsEnumerable = text.Split(Array.Empty<char>(), StringSplitOptions.RemoveEmptyEntries).AsEnumerable();

        wordsEnumerable = wordsEnumerable.Where(w => !w.Any(c => char.IsDigit(c) || char.IsSymbol(c)));

        wordsEnumerable = wordsEnumerable.Select(w => new string(w.Where(char.IsLetter).ToArray()));

        return wordsEnumerable.Where(w => w.Length > 0);
    }

}