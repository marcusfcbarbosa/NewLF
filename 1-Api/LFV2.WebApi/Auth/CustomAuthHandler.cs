using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace LFV2.WebApi.Auth
{
    public class CustomAuthHandler
    {
        public bool IsValidToken(string jwtToken, string issuer, string audience, string metadataAddress)
        {
            return ValidateToken(jwtToken, null, null, null);
        }

        private bool ValidateToken(string jwtToken, string issuer, string audience, ICollection<SecurityKey> signingKeys)
        {
            try
            {
                var key = Encoding.ASCII.GetBytes(Settings.Secret);
                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                };
                ISecurityTokenValidator tokenValidator = new JwtSecurityTokenHandler();
                var claim = tokenValidator.ValidateToken(jwtToken, validationParameters, out
                    var _);
                var scope = claim.FindFirst(c => c.Type == JwtRegisteredClaimNames.Name);
                if (scope == null) throw new Exception("404 - Authorization failed - Invalid Scope");
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("404 - Authorization failed", ex);
            }
        }
    }
}
