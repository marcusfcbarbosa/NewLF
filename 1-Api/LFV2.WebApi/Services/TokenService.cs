using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace LFV2.WebApi.Services
{
    public class TokenService
    {
        public static string GenerateToken(string username)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Settings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]{
                    new Claim(JwtRegisteredClaimNames.UniqueName, username),
                    new Claim(JwtRegisteredClaimNames.Name, "test marquinhos")
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            var generateToken = tokenHandler.WriteToken(token);
            DecriptToken(generateToken, JwtRegisteredClaimNames.UniqueName);
            return generateToken;
        }

        public static string DecriptToken(string token, string claimType)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var test = tokenHandler.ReadToken(token) as JwtSecurityToken;

            var stringClaimValue = test.Claims.FirstOrDefault(claim => claim.Type == claimType);
            return stringClaimValue.Value;

        }
    }
}
