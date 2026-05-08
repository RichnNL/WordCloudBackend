using System.Collections.ObjectModel;
using WordCloudBackend.Features.WordCounter.Extensions;
using WordCloudBackend.Features.WordCounter.Models;
using WordCloudBackend.Features.WordCounter.Models.Enums;

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