using ImoblyAI.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddHttpClient<OpenAIService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapPost("/api/ideias", async (string descricao, OpenAIService openAi) =>
{
    var resultado = await openAi.GerarIdeiasAsync(descricao);
    return Results.Content(resultado, "application/json");
});

app.UseHttpsRedirection();

app.Run();

