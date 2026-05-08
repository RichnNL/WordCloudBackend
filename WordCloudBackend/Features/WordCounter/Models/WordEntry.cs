namespace WordCloudBackend.Features.WordCounter.Models;

/// <summary>
/// Represents a word and its frequency count in a text.
/// </summary>
/// <param name="Word">The extracted word.</param>
/// <param name="Count">The number of times the word appears.</param>
public record WordEntry([Required, MinLength(1)] string Word, [Range(1, int.MaxValue)] int Count);