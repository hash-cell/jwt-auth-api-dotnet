namespace LoginJWT.Interfaces;

using LoginJWT.Models;

public interface IAuthService
{
    Task<string> Register(LoginRequest model);
    Task<object> Login(LoginRequest model);
    Task<object> RefreshToken(RefreshToken model);
}