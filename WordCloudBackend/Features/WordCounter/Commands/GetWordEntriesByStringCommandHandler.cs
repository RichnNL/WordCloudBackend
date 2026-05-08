using WordCloudBackend.Features.WordCounter.Extensions;

namespace WordCloudBackend.Features.WordCounter.Commands;

public class GetWordEntriesByStringCommandHandler : IRequestHandler<GetWordEntriesByStringCommand, ReadOnlyCollection<WordEntry>>
{
    public Task<ReadOnlyCollection<WordEntry>> Handle(GetWordEntriesByStringCommand request)
    {
        var entries = new List<WordEntry>
        {
            new("hello", 1),
            new("world", 2)
        };
        
        var result = entries.ApplySortOrder(WordEntrySortOrder.CountDescending)
            .ToList()
            .AsReadOnly();
            
        return Task.FromResult(result);
    }
}