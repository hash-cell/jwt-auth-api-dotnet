namespace LoginJWT.Interfaces;

using LoginJWT.Models;

public interface ITokenService
{
    string GenerateAccessToken(Login user);
    string GenerateRefreshToken();
}