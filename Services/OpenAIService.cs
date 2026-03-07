using System.Net.Http.Headers;
using System.Text.Json;

namespace ImoblyAI.Api.Services;

public class OpenAIService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public OpenAIService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
    }

    public async Task<string> GerarIdeiasAsync(string descricaoImovel)
    {
        var apiKey = _configuration["OpenAI:ApiKey"];
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
        Console.WriteLine($"DEBUG: A chave começa com: {apiKey?.Substring(0, 10)}...");

        var systemPrompt = _configuration["OpenAI:SystemPrompt"];

        var requestBody = new
        {
            model = "gpt-4o-mini",
            messages = new[]
            {
                new { role = "system", content = systemPrompt },
                new { role = "user", content = descricaoImovel }
            },
            response_format = new { type = "json_object" }
        };

        var response = await _httpClient.PostAsJsonAsync("https://api.openai.com/v1/chat/completions", requestBody);
        return await response.Content.ReadAsStringAsync();
    }
}