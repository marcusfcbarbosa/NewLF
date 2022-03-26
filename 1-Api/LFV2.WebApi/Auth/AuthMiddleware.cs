using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace LFV2.WebApi.Auth
{
    public class AuthMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;

        public AuthMiddleware(RequestDelegate next,IConfiguration configuration)
        {
            _configuration = configuration;
            _next = next;
        }
        public async Task Invoke(HttpContext httpContext)
        {
            if (httpContext.Request.Path.StartsWithSegments(PathString.FromUriComponent("/api")))
            {
                try
                {
                    var path = httpContext.Request.Path;
                    string token = string.Empty;
                    string issuer = _configuration["JwtToken:Issuer"]; //Get issuer value from your configuration
                    string audience = _configuration["JwtToken:Audience"]; //Get audience value from your configuration
                    string metaDataAddress = issuer + "/.well-known/oauth-authorization-server";
                    CustomAuthHandler authHandler = new CustomAuthHandler();
                    var header = httpContext.Request.Headers["Authorization"];
                    if (header.Count == 0) throw new Exception("Authorization header is empty");
                    string[] tokenValue = Convert.ToString(header).Trim().Split(" ");
                    if (tokenValue.Length > 1) token = tokenValue[1];
                    else throw new Exception("Authorization token is empty");
                    if (authHandler.IsValidToken(token, issuer, audience, metaDataAddress)) await _next(httpContext);
                }
                catch (Exception)
                {
                    httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    HttpResponseWritingExtensions.WriteAsync(httpContext.Response, "{\"message\": \"Unauthorized\"}").Wait();
                }
            }

            await _next(httpContext);

        }
    }
}
