namespace WordCloudBackend.Features.WordCounter.Extensions;

public static class WordEntryExtensions
{
    /// <summary>
    /// Orders an enumerable of WordEntry objects by their Count property.
    /// </summary>
    /// <param name="entries">The enumerable of WordEntry objects.</param>
    /// <param name="sortOrder">The sort order to apply (Ascending or Descending).</param>
    /// <returns>An ordered enumerable of WordEntry objects.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when an unsupported sortOrder is provided.</exception>
    public static IEnumerable<WordEntry> ApplySortOrder(this IEnumerable<WordEntry> entries, WordEntrySortOrder sortOrder)
    {
        return sortOrder switch
        {
            WordEntrySortOrder.CountAscending => entries.OrderBy(e => e.Count),
            WordEntrySortOrder.CountDescending => entries.OrderByDescending(e => e.Count),
            _ => throw new ArgumentOutOfRangeException(nameof(sortOrder), sortOrder, $"The sort order '{sortOrder}' is not supported.")
        };
    }
}