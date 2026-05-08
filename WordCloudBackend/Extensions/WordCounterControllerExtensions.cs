using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using WordCloudBackend.Features.WordCounter.Commands;

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
        group.MapPost("/", ([FromBody] string text) => 
            {
                var command = new GetWordCountByStringCommand();
                var sortedWordCounts = command.Execute(text);
                
                return Results.Ok(sortedWordCounts);
            })
            .WithName("CountWordsInPayload")
            .WithSummary("Analyses a large text payload")
            .WithDescription("Accepts a text payload in the HTTP body and returns an array of words sorted by their frequency of occurrence. Best for articles or long paragraphs.");

        return group;
    }

    /// <summary>
    /// Maps the GET endpoint for analysing short text strings via the URL route.
    /// </summary>
    private static RouteGroupBuilder MapWordCountByTextInRouteEndpoint(this RouteGroupBuilder group)
    {
        group.MapGet("/{text}", ([FromRoute] string text) => 
            {
                var command = new GetWordCountByStringCommand();
                var sortedWordCounts = command.Execute(text);
                
                return Results.Ok(sortedWordCounts);
            })
            .WithName("CountWordsInRoute")
            .WithSummary("Analyses a short text string from the URL")
            .WithDescription("Extracts the text directly from the URL route and returns an array of words sorted by frequency. Note: Browsers enforce URL length limits, so only use this for short sentences.");
        
        return group;
    }
}