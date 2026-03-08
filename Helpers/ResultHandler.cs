using ImoblyAI.Api.Common;

namespace ImoblyAI.Api.Helpers;

public static class ResultHandler
{
    public static IResult Handle<T>(Result<T> result)
    {
        if (result.Success)
            return Results.Ok(result.Data);

        var statusCode = result.ErrorCode switch
        {
            ErrorCode.Validation => StatusCodes.Status400BadRequest,
            ErrorCode.InvalidCredentials => StatusCodes.Status401Unauthorized,
            ErrorCode.Unauthorized => StatusCodes.Status401Unauthorized,
            ErrorCode.Forbidden => StatusCodes.Status403Forbidden,
            ErrorCode.NotFound => StatusCodes.Status404NotFound,
            ErrorCode.Conflict => StatusCodes.Status409Conflict,
            ErrorCode.ExternalService => StatusCodes.Status502BadGateway,
            _ => StatusCodes.Status400BadRequest
        };

        var error = new ErrorResponse(
            statusCode,
            result.ErrorCode?.ToString().ToUpper() ?? "UNKNOWN_ERROR",
            result.Error ?? "Erro inesperado"
        );

        return Results.Json(new { error }, statusCode: statusCode);
    }
}