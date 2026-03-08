using ImoblyAI.Api.Enums.Subscription;
using ImoblyAI.Api.Enums.User;

namespace ImoblyAI.Api.DTOs.User;

public record UserResponseDTO(
    Guid Id,
    string Name,
    string Email,
    int CreditsRemaining,
    SubscriptionStatus SubscriptionStatus,
    Roles Role
);