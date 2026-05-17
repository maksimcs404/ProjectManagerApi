using Microsoft.IdentityModel.Tokens;
using ProjectManager.Core.Models.Domain;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProjectManager.Application.Services
{
    public static class JwtService
    {
        public static string GenerateToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("mysupersecret_secretsecretsecretkey!123"));


            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim("UserName", user.UserName),
                new Claim("CreatedAt", user.CreatedAt?.ToString("o") ?? string.Empty)
            };

            var token = new JwtSecurityToken(
                issuer: "ProjectManagerApi",
                audience: "ProjectManagerClient",
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(30),
                signingCredentials : new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
                );

            var tokenHandler = new JwtSecurityTokenHandler();
            string jwtToken = tokenHandler.WriteToken(token);
            return jwtToken;
        }
    }
}
