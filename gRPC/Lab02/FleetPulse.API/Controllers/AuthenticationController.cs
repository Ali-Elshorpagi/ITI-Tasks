using FleetPulse.API.Models;
using FleetPulse.API.Repositories;
using FleetPulse.OrderService.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FleetPulse.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthenticationController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly IUserRepo _repository;
    public AuthenticationController(IConfiguration configuration, IUserRepo repository)
    {
        _configuration = configuration;
        _repository = repository;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto request)
    {
        if (await _repository.UserExistsAsync(request.Username))
        {
            return BadRequest("Username already exists.");
        }

        var user = new UserEntity
        {
            Username = request.Username,
            PasswordHash = request.Password,
            Role = !string.IsNullOrWhiteSpace(request.Role) ? request.Role : "User"
        };

        await _repository.RegisterAsync(user);

        return Ok("User Registered Successfully.");
    }


    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto request)
    {
        var user = await _repository.LoginAsync(request.Username);

        if (user == null)
        {
            return Unauthorized("Invalid username or password.");
        }

        if (user.PasswordHash != request.Password)
        {
            return Unauthorized("Invalid username or password.");
        }

        var token = GenerateJwtToken(
            user.Username,
            user.Role);

        return Ok(new
        {
            Token = token,
            Username = user.Username,
            Role = user.Role
        });
    }


    private string GenerateJwtToken(
    string username,
    string role)
    {
        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(
                _configuration["Jwt:Key"]!));

        var credentials = new SigningCredentials(
            key,
            SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
        new Claim(JwtRegisteredClaimNames.Sub, username),

        new Claim(ClaimTypes.Name, username),

        new Claim(ClaimTypes.Role, role),

        new Claim(
            JwtRegisteredClaimNames.Jti,
            Guid.NewGuid().ToString())
    };

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(2),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler()
            .WriteToken(token);
    }
}