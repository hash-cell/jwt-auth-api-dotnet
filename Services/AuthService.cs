using LoginJWT.Data;
using LoginJWT.Models;
using Microsoft.EntityFrameworkCore;
using LoginJWT.Interfaces;

namespace LoginJWT.Services;

public class AuthService : IAuthService
{
    private readonly AppDbContext _context;
    private readonly ITokenService _tokenService;
    private readonly IConfiguration _configuration;

    public AuthService(AppDbContext context, ITokenService tokenService, IConfiguration configuration)
    {
        _context = context;
        _tokenService = tokenService;
        _configuration = configuration;
    }

    public async Task<string> Register(LoginRequest model)
    {
        if (model is null || !IsValidEmail(model.Email) || string.IsNullOrEmpty(model.Password))
            throw new Exception("Email ou senha inválidos.");

        bool exists = await _context.Logins.AnyAsync(u => u.Email == model.Email);
        if (exists)
            throw new Exception("Este email já está sendo usado.");

        var pepper = _configuration["Pepper"];

        var newUser = new Login
        {
            Email = model.Email,
            Password = BCrypt.Net.BCrypt.HashPassword(model.Password + pepper)
        };

        _context.Logins.Add(newUser);
        await _context.SaveChangesAsync();

        return "Usuário registrado com sucesso!";
    }

    public async Task<object> Login(LoginRequest model)
    {
        if (model is null || !IsValidEmail(model.Email) || string.IsNullOrEmpty(model.Password))
            throw new Exception("Email ou senha incorretos.");

        var user = await _context.Logins.FirstOrDefaultAsync(u => u.Email == model.Email);
        if (user is null)
            throw new Exception("Email ou senha incorretos.");

        var pepper = _configuration["Pepper"];
        if (!BCrypt.Net.BCrypt.Verify(model.Password + pepper, user.Password))
            throw new Exception("Email ou senha incorretos.");

        var token = _tokenService.GenerateAccessToken(user);
        var refreshTokenValue = _tokenService.GenerateRefreshToken();

        var refreshToken = new RefreshToken
        {
            UserId = user.Id.ToString(),
            Token = refreshTokenValue,
            Expires = DateTime.UtcNow.AddDays(7)
        };

        _context.RefreshTokens.Add(refreshToken);
        await _context.SaveChangesAsync();

        return new
        {
            token,
            refreshToken = refreshTokenValue
        };
    }

    public async Task<object> RefreshToken(RefreshToken model)
    {
        if (model is null || string.IsNullOrEmpty(model.UserId) || string.IsNullOrEmpty(model.Token))
            throw new Exception("Requisição inválida");

        var storedToken = await _context.RefreshTokens
            .FirstOrDefaultAsync(t => t.Token == model.Token && t.UserId == model.UserId);

        if (storedToken == null)
            throw new Exception("Refresh Token inválido.");

        if (storedToken.IsExpired)
        {
            _context.RefreshTokens.Remove(storedToken);
            await _context.SaveChangesAsync();
            throw new Exception("Refresh Token expirado.");
        }

        var user = await _context.Logins.FindAsync(int.Parse(storedToken.UserId));
        if (user == null)
            throw new Exception("Usuário não encontrado.");

        var newAccessToken = _tokenService.GenerateAccessToken(user);
        var newRefreshTokenValue = _tokenService.GenerateRefreshToken();

        _context.RefreshTokens.Remove(storedToken);
        _context.RefreshTokens.Add(new RefreshToken
        {
            UserId = user.Id.ToString(),
            Token = newRefreshTokenValue,
            Expires = DateTime.UtcNow.AddDays(7)
        });

        await _context.SaveChangesAsync();

        return new
        {
            token = newAccessToken,
            refreshToken = newRefreshTokenValue
        };
    }

    private static bool IsValidEmail(string email)
    {
        try
        {
            new System.Net.Mail.MailAddress(email);
            return true;
        }
        catch
        {
            return false;
        }
    }
}