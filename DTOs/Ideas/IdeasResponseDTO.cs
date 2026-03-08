using System.Text.Json.Serialization;

namespace ImoblyAI.Api.DTOs.Ideas;

public class IdeiasResponseDTO
{
    [JsonPropertyName("ideias")]
    public List<IdeiaDTO> Ideias { get; set; } = [];
}

public class IdeiaDTO
{
    [JsonPropertyName("titulo")]
    public string Titulo { get; set; } = "";

    [JsonPropertyName("hook")]
    public string Hook { get; set; } = "";

    [JsonPropertyName("objetivo")]
    public string Objetivo { get; set; } = "";
}