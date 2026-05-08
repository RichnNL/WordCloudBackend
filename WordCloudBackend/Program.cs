using WordCloudBackend.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.AddSwagger();

var app = builder.Build();

app.ConfigureSwagger();

app.UseHttpsRedirection();

var wordCountGroup = app.MapGroup("/api/wordcount")
    .WithTags("Word Counter");

wordCountGroup.MapGet("/", () => 
    {
        return Results.Ok(new[] { "Record 1", "Record 2" });
    })
    .WithSummary("Retrieves all word count records");

wordCountGroup.MapGet("/{id:int}", (int id) => Results.Ok(new { Id = id, Message = "Specific record found" }))
    .WithSummary("Retrieves a specific word count by its ID");

app.Run();