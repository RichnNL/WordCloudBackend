namespace WordCloudBackend.Features.WordCounter.Commands;

public class GetWordEntriesByStringCommandHandler(IWordCounterService wordCounterService)
    : IRequestHandler<GetWordEntriesByStringCommand, ReadOnlyCollection<WordEntry>>
{
    public Task<ReadOnlyCollection<WordEntry>> Handle(GetWordEntriesByStringCommand request)
    {
        var wordEntries = wordCounterService.CountWords(request.Text, WordEntrySortOrder.CountDescending);
        return Task.FromResult(wordEntries.AsReadOnly());
    }
}