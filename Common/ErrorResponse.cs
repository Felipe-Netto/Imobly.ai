namespace ImoblyAI.Api.Common;

public record ErrorResponse(
    int Status,
    string Code,
    string Message
);