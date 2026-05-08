namespace WordCloudBackend.Features.WordCounter.Services.WordCounter;

public interface IWordCounterService
{
    IList<WordEntry> CountWords(string text, WordEntrySortOrder sortOrder);
}