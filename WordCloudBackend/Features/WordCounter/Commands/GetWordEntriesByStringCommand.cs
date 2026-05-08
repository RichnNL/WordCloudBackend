namespace WordCloudBackend.Features.WordCounter.Commands;

/// <summary>
/// A command responsible for calculating the word count from a given string.
/// </summary>
public class GetWordEntriesByStringCommand : IRequest<ReadOnlyCollection<WordEntry>>
{
    public string Text { get; }

    public GetWordEntriesByStringCommand(string text)
    {
        Text = text;
    }
}