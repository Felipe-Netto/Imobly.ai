using ImoblyAI.Api.DTOs.Auth;
using ImoblyAI.Api.Helpers;
using ImoblyAI.Api.Services.Auth;

namespace ImoblyAI.Api.Routes.Auth;

public static class AuthenticationRoute
{
    public static void MapAuthenticationRoutes(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/auth");

        group.MapPost("/register", async (RegisterDTO register, AuthService authService) =>
        {
            var result = await authService.Register(register);
            return ResultHandler.Handle(result);
        });

        group.MapPost("/login", async (LoginDTO login, AuthService authService) =>
        {
            var result = await authService.Login(login);
            return ResultHandler.Handle(result);
        });
    }
}