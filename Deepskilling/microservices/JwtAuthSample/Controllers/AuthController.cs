using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using JwtAuthSample.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace JwtAuthSample.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _configuration;

    public AuthController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginModel model)
    {
        if (!IsValidUser(model))
        {
            return Unauthorized();
        }

        var token = GenerateJwtToken(model.Username, isAdmin: model.Username.Equals("admin", StringComparison.OrdinalIgnoreCase));
        return Ok(new { Token = token });
    }

    private static bool IsValidUser(LoginModel model)
    {
        return (model.Username, model.Password) switch
        {
            ("admin", "admin123") => true,
            ("user", "user123") => true,
            _ => false
        };
    }

    private string GenerateJwtToken(string username, bool isAdmin)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, username)
        };

        if (isAdmin)
        {
            claims.Add(new Claim(ClaimTypes.Role, "Admin"));
        }

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var durationInMinutes = int.Parse(_configuration["Jwt:DurationInMinutes"] ?? "60");

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(durationInMinutes),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
