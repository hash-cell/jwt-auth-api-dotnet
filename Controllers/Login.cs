using LoginJWT.Data;
using Microsoft.AspNetCore.Mvc;
using LoginJWT.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Security.Cryptography;
using LoginJWT.Interfaces;

namespace LoginJWT.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
        public async Task<IActionResult> Register(LoginRequest model) => Ok(await _authService.Register(model));
    [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest model) => Ok(await _authService.Login(model));
    [HttpPost("refresh")]
        public async Task<IActionResult> Refresh(RefreshToken model) => Ok(await _authService.RefreshToken(model));
}
