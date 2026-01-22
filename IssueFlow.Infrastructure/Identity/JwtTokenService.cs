using IssueFlow.Application.Auth;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace IssueFlow.Infrastructure.Identity;

public class JwtTokenService : IJwtTokenService
{
    private readonly IConfiguration _configuration;

    public JwtTokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateAccessToken(JwtUserDto jwtUserDto)
    {
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Sub, jwtUserDto.UserId),
            new Claim(JwtRegisteredClaimNames.Email, jwtUserDto.Email),
            new Claim(JwtRegisteredClaimNames.UniqueName, jwtUserDto.UserName)
        };

        foreach (var role in jwtUserDto.Roles)
            claims.Add(new Claim(ClaimTypes.Role, role));

        var secretKey = _configuration["Jwt:SecretKey"]
            ?? throw new InvalidOperationException("JWT secret key is not configured.");
        var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        var credentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

        var expiryHours = _configuration.GetValue<int>("Jwt:ExpiryHours");
        if (expiryHours <= 0)
            throw new InvalidOperationException("JWT expiry hours is either not configured or set to an invalid value.");

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(expiryHours),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
