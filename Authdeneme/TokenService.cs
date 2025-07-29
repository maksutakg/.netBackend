using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Authdeneme
{
    public class TokenService : ITokenService
    {
        private readonly JwtSettings jwtSettings;

        public TokenService(IOptions<JwtSettings> jwtSettings)
        {
            this.jwtSettings = jwtSettings.Value;
        }

        public string GenerateToken(string name, string role,int id)
        {
            var claims = new[]
            {
             new Claim (ClaimTypes.Role , role),
             new Claim (ClaimTypes.NameIdentifier , id.ToString()),
             new Claim("UserName", name) 
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key));
            var creds = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: jwtSettings.İssuer,
                audience: jwtSettings.Audience,
                claims: claims,
                expires: DateTime.Now.AddDays(jwtSettings.Expire),
                signingCredentials: creds
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
