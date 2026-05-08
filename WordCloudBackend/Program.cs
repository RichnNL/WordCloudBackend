var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


// Configure the HTTP request pipeline.

app.MapOpenApi();
app.UseSwagger();
app.UseSwaggerUI();


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