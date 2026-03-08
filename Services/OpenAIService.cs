using System.Net.Http.Headers;
using System.Text.Json;
using ImoblyAI.Api.Common;
using ImoblyAI.Api.DTOs.Ideas;
using ImoblyAI.Api.DTOs.OpenAIDTO;

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

    public async Task<Result<IdeiasResponseDTO>> GerarIdeiasAsync(string descricaoImovel)
    {
        var apiKey = _configuration["OpenAI:ApiKey"];

        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", apiKey);

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

        var response = await _httpClient.PostAsJsonAsync(
            "https://api.openai.com/v1/chat/completions",
            requestBody
        );

        if (!response.IsSuccessStatusCode)
            return Result<IdeiasResponseDTO>.Fail(
                "Erro ao gerar ideias.",
                ErrorCode.ExternalService
            );

        var openAiResponse = await response.Content.ReadFromJsonAsync<OpenAIResponse>();

        var content = openAiResponse?.Choices.FirstOrDefault()?.Message.Content;

        if (string.IsNullOrWhiteSpace(content))
            return Result<IdeiasResponseDTO>.Fail(
                "Resposta inválida da IA.",
                ErrorCode.ExternalService
            );

        var ideias = JsonSerializer.Deserialize<IdeiasResponseDTO>(content);

        if (ideias == null)
            return Result<IdeiasResponseDTO>.Fail(
                "Erro ao interpretar resposta da IA.",
                ErrorCode.ExternalService
            );

        return Result<IdeiasResponseDTO>.Ok(ideias);
    }
}