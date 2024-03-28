using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MyStocks.Application.Abstractions;
using MyStocks.Domain.Users;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MyStocks.Infrastructure.Authentication;

public class JWTProvider : IJWTProvider
{
    private readonly IJWTConfig _jwtConfig;
    public JWTProvider(IJWTConfig jwtConfig)
    {
        _jwtConfig = jwtConfig;
    }
    public string GenerateToken(User user)
    {
        var key = _jwtConfig.Key;
        var secretKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key));

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = GenerateClaims(user),
            Issuer = _jwtConfig.Issuer,
            Audience = _jwtConfig.Audience,
            Expires = DateTime.UtcNow.AddHours(_jwtConfig.ExpiresInHours),
            SigningCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256Signature)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }

    private ClaimsIdentity GenerateClaims(User user)
    {
        var claims = new ClaimsIdentity();
        claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));

        return claims;
    }
}
