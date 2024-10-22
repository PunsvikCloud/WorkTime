var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/", () => "WorkTime is running")
    .WithOpenApi();

app.MapPost("/clock-in", () => "Clocked in")
    .WithOpenApi();

app.MapPost("/clock-out", () => "Clocked out")
    .WithOpenApi();

app.Run();

public partial class Program { }
