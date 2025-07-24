using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Token
{
    public class TokenService : ITokenService
    {
        private readonly JwtOptions jwtOptions;

        public TokenService(IOptions<JwtOptions> jwtOptions)
        {
                this.jwtOptions = jwtOptions.Value;
        }
        public string GenerateToken(string username,string role)
        {
            var claims = new[] {
         new Claim (JwtRegisteredClaimNames.Sub, username),
         new Claim (ClaimTypes.Role, role ),
         new Claim (JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())

         };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Key));
            var creds = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
             issuer: jwtOptions.Issuer,
             audience: jwtOptions.Audience,
             claims: claims,
             expires: DateTime.UtcNow.AddMinutes(jwtOptions.ExpiresInMinutes),
             signingCredentials: creds
                ); 
            return new JwtSecurityTokenHandler().WriteToken(token);


        }
    }
}
