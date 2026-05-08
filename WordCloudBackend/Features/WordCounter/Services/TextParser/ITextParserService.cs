namespace WordCloudBackend.Features.WordCounter.Services.TextParser;

public interface ITextParserService
{
    /// <summary>
    /// Processes and cleans the input text to extract valid alphabetical words. 
    /// This method automatically trims the text, normalises whitespace, discards words 
    /// containing digits or symbols, and scrubs punctuation from surviving words.
    /// Additional rules (such as casing, minimum length, and ignore lists) are applied 
    /// based on the provided <paramref name="options"/>.
    /// </summary>
    /// <param name="text">The raw string to process. If null or entirely whitespace, an empty string is returned.</param>
    /// <param name="options">The configuration rules to apply during parsing.</param>
    /// <returns>A clean, single-space-separated string containing the final processed words.</returns>
    string ParseText(string text, TextParserOptions options);
}