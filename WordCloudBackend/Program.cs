var builder = WebApplication.CreateBuilder(args);

builder.AddSwagger();

var app = builder.Build();

app.ConfigureSwagger();

app.UseHttpsRedirection();

app.MapWordCounterEndpoints();

app.Run();