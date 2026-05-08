namespace WordCloudBackend.Features.WordCounter.Commands;

public class GetWordEntriesByStringCommandHandler(
    IWordCounterService wordCounterService,
    ITextParserService textParserService)
    : IRequestHandler<GetWordEntriesByStringCommand, ReadOnlyCollection<WordEntry>>
{
    public Task<ReadOnlyCollection<WordEntry>> Handle(GetWordEntriesByStringCommand request)
    {
        var options = new TextParserOptions
        {
            ConvertToLowerCase = true,
            IgnoredWords = new HashSet<string>(),
            MinimumWordLength = 3
        };

        var parsedText = textParserService.ParseText(request.Text, options);

        var wordEntries = wordCounterService.CountWords(parsedText, WordEntrySortOrder.CountDescending);

        return Task.FromResult(wordEntries.AsReadOnly());
    }
}