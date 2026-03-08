using ImoblyAI.Api.DTOs.User;

namespace ImoblyAI.Api.DTOs.Auth;

public record AuthResponseDTO(string Token, UserResponseDTO User);