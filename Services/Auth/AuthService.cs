using ImoblyAI.Api.Common;
using ImoblyAI.Api.Data;
using ImoblyAI.Api.DTOs.Auth;
using ImoblyAI.Api.DTOs.User;
using ImoblyAI.Api.Models.User;
using Microsoft.EntityFrameworkCore;

namespace ImoblyAI.Api.Services.Auth;

public class AuthService
{
    private readonly TokenService _jwtService;
    private readonly AppDbContext _context;

    public AuthService(TokenService jwtService, AppDbContext context)
    {
        _jwtService = jwtService;
        _context = context;
    }

    public async Task<Result<AuthResponseDTO>> Register(RegisterDTO dto)
    {
        var existingUser = await _context.Users
            .Where(u => u.Email == dto.Email || u.Document == dto.Document)
            .Select(u => new { u.Email, u.Document })
            .FirstOrDefaultAsync();

        if (existingUser != null)
        {
            if (existingUser.Email == dto.Email)
                return Result<AuthResponseDTO>.Fail("Email já registrado.", ErrorCode.Conflict);

            if (existingUser.Document == dto.Document)
                return Result<AuthResponseDTO>.Fail("Documento já registrado.", ErrorCode.Conflict);
        }

        var user = new User
        {
            Name = dto.Name,
            Document = dto.Document,
            Email = dto.Email,
            Password = BCrypt.Net.BCrypt.HashPassword(dto.Password)
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        var token = _jwtService.GenerateToken(user);

        return Result<AuthResponseDTO>.Ok(
            new AuthResponseDTO(
                token,
                new UserResponseDTO(user.Id, user.Name, user.Email, user.CreditsRemaining, user.SubscriptionStatus,
                    user.Role)
            )
        );
    }

    public async Task<Result<AuthResponseDTO>> Login(LoginDTO dto)
    {
        User? user = await _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Email == dto.Email);

        if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.Password))
            return Result<AuthResponseDTO>.Fail(
                "Email ou senha inválidos.",
                ErrorCode.InvalidCredentials
            );

        var token = _jwtService.GenerateToken(user);

        var response = new AuthResponseDTO(
            token,
            new UserResponseDTO(
                user.Id,
                user.Name,
                user.Email,
                user.CreditsRemaining,
                user.SubscriptionStatus,
                user.Role
            )
        );

        return Result<AuthResponseDTO>.Ok(response);
    }
}