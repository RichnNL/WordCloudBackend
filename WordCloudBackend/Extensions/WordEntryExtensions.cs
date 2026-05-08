using WordCloudBackend.Features.WordCounter.Models;

namespace WordCloudBackend.Extensions;

public static class WordEntryExtensions
{
    /// <summary>
    /// Orders an enumerable of WordEntry objects by their Count in the specified order.
    /// </summary>
    /// <param name="entries">The enumerable of WordEntry objects.</param>
    /// <param name="sortOrder">The sort order to apply (Ascending or Descending).</param>
    /// <returns>An ordered enumerable of WordEntry objects.</returns>
    public static IEnumerable<WordEntry> OrderByCount(this IEnumerable<WordEntry> entries, WordEntrySortOrder sortOrder)
    {
        return sortOrder switch
        {
            WordEntrySortOrder.CountAscending => entries.OrderBy(e => e.Count),
            WordEntrySortOrder.CountDescending => entries.OrderByDescending(e => e.Count),
            _ => entries.OrderByDescending(e => e.Count)
        };
    }
}