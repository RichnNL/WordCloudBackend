using WordCloudBackend.Features.WordCounter.Commands;

namespace WordCloudBackend.Extensions;

public static class WordCounterControllerExtensions
{
    /// <summary>
    /// Maps the word counter endpoints to the application.
    /// </summary>
    /// <param name="app">The web application builder.</param>
    /// <returns>The original web application builder.</returns>
    public static WebApplication MapWordCounterEndpoints(this WebApplication app)
    {
        var wordCountGroup = app.MapGroup("/api/wordcount")
            .WithTags("Word Counter");

        wordCountGroup.MapGet("/", () => 
            {
                var command = new GetWordCountByStringCommand();
                return Results.Ok(command.Execute());
            })
            .WithSummary("Retrieves all word count records");
        
        return app;
    }
}