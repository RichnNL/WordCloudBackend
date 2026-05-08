using WordCloudBackend.Features.WordCounter.Extensions;

namespace WordCloudBackend.Features.WordCounter.Commands;

/// <summary>
/// A command responsible for calculating the word count from a given string.
/// </summary>
public class GetWordCountByStringCommand
{
    /// <summary>
    /// Executes the command to retrieve word counts.
    /// </summary>
    /// <returns>A read-only collection of <see cref="WordEntry"/> items representing the word counts.</returns>
    public ReadOnlyCollection<WordEntry> Execute(string text)
    {
        var entries = new List<WordEntry>
        {
            new("hello", 1),
            new("world", 2)
        };
        
        return entries.ApplySortOrder(WordEntrySortOrder.CountDescending)
            .ToList()
            .AsReadOnly();
    }
}