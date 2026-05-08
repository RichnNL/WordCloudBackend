namespace WordCloudBackend.Features.WordCounter.Models;

/// <summary>
/// Options for configuring how text should be parsed.
/// </summary>
public class TextParserOptions
{
    /// <summary>
    /// Whether to convert all text to lower case.
    /// </summary>
    public required bool ConvertToLowerCase { get; init; }

    /// <summary>
    /// The minimum word length required to be included in the output. Default is 0 (no minimum).
    /// </summary>
    public required int MinimumWordLength { get; init; }

    /// <summary>
    /// A set of words to be ignored or removed from the text. Case-insensitive by default.
    /// </summary>
    public required HashSet<string> IgnoredWords { get; init; } 
}