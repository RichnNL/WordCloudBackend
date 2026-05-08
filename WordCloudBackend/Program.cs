using WordCloudBackend.Features.WordCounter.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.AddSwagger();

builder.Services.AddScoped<IMediator, Mediator>();
builder.Services.AddWordCounterServices();

var app = builder.Build();

app.ConfigureSwagger();

app.UseHttpsRedirection();

app.MapWordCounterEndpoints();

app.Run();