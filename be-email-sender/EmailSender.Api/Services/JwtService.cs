using EmailSender.Api.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EmailSender.Api.Services;

public class JwtService(IConfiguration configuration) : IJwtService
{
    public string GenerateToken(string appId, string appName)
    {
        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!));
        var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);


        var claims = new[]
        {
            new Claim("appId", appId),
            new Claim("appName", appName),
        };

        var token = new JwtSecurityToken(
            issuer: configuration["Jwt:Issuer"],
            audience: configuration["Jwt:Audience"],
            claims: claims,
            expires: null,
            signingCredentials: credentials
        );

        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
        
        return tokenString;
    }
}
