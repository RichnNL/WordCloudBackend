namespace WordCloudBackend.Features.WordCounter.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddWordCounterServices(this IServiceCollection services)
    {
        services.AddScoped<IRequestHandler<GetWordEntriesByStringCommand, ReadOnlyCollection<WordEntry>>, GetWordEntriesByStringCommandHandler>();
        
        return services;
    }
}