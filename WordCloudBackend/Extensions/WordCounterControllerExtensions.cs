using Microsoft.AspNetCore.Mvc;

namespace WordCloudBackend.Extensions;

public static class WordCounterControllerExtensions
{
    /// <summary>
    /// Maps the main word counter group and registers its child endpoints.
    /// </summary>
    /// <param name="app">The web application builder.</param>
    /// <returns>The original web application builder.</returns>
    public static WebApplication MapWordCounterEndpoints(this WebApplication app)
    {
        var wordCountGroup = app.MapGroup("/api/wordcount")
            .WithTags("Word Counter");

        wordCountGroup.MapWordCountByTextInPayloadEndpoint();
        wordCountGroup.MapWordCountByTextInRouteEndpoint();
        
        return app;
    }

    /// <summary>
    /// Maps the POST endpoint for analysing large text payloads.
    /// </summary>
    private static RouteGroupBuilder MapWordCountByTextInPayloadEndpoint(this RouteGroupBuilder group)
    {
        group.MapPost("/", async ([FromBody] string text, [FromServices] IMediator mediator) => 
            {
                var command = new GetWordEntriesByStringCommand(text);
                var sortedWordCounts = await mediator.Send(command);
                
                return Results.Ok(sortedWordCounts);
            })
            .WithName("CountWordsInPayload")
            .WithSummary("Analyses a large text payload")
            .WithDescription("Accepts a text payload in the HTTP body and returns an array of words sorted by their frequency of occurrence. Best for articles or long paragraphs.\n\n" +
                             "By default, the following parsing rules apply:\n" +
                             "- Words must be at least 3 letters long to be counted.\n" +
                             "- Words containing numbers are ignored.\n" +
                             "- Special characters (punctuation) are removed.\n" +
                             "- Text is converted to lowercase (e.g. 'Hello' and 'hello' are counted together).")
            .Produces<IEnumerable<WordEntry>>()
            .Produces(StatusCodes.Status500InternalServerError);

        return group;
    }

    /// <summary>
    /// Maps the GET endpoint for analysing short text strings via the URL route.
    /// </summary>
    private static RouteGroupBuilder MapWordCountByTextInRouteEndpoint(this RouteGroupBuilder group)
    {
        group.MapGet("/{text}", async ([FromRoute] string text, [FromServices] IMediator mediator) => 
            {
                var command = new GetWordEntriesByStringCommand(text);
                var sortedWordCounts = await mediator.Send(command);
                
                return Results.Ok(sortedWordCounts);
            })
            .WithName("CountWordsInRoute")
            .WithSummary("Analyses a short text string from the URL")
            .WithDescription("Extracts the text directly from the URL route and returns an array of words sorted by frequency. Note: Browsers enforce URL length limits, so only use this for short sentences.\n\n" +
                             "By default, the following parsing rules apply:\n" +
                             "- Words must be at least 3 letters long to be counted.\n" +
                             "- Words containing numbers are ignored.\n" +
                             "- Special characters (punctuation) are removed.\n" +
                             "- Text is converted to lowercase (e.g. 'Hello' and 'hello' are counted together).")
            .Produces<IEnumerable<WordEntry>>()
            .Produces(StatusCodes.Status500InternalServerError);
        
        return group;
    }
}