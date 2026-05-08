namespace WordCloudBackend.Features.WordCounter.Services.WordCounter;

public interface IWordCounterService
{
    /// <summary>
    /// Counts the occurrences of each word in the provided text and returns them sorted.
    /// </summary>
    /// <param name="text">The text to analyse.</param>
    /// <param name="sortOrder">The sort order to apply to the results.</param>
    /// <returns>A list of <see cref="WordEntry"/> objects representing the word counts.</returns>
    /// <exception cref="ArgumentException">Thrown when the provided text is null, empty, or consists only of white-space characters.</exception>
    IList<WordEntry> CountWords(string text, WordEntrySortOrder sortOrder);
}