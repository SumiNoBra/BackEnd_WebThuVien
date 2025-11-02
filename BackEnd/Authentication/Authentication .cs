using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
namespace API.Authentication
{


    public class CustomJwtAuthenticationOptions : AuthenticationSchemeOptions
    {
        public const string SchemeName = "Custom-Token-Auth";
        public string IssuerSigningKey { get; set; } = string.Empty;
    }

    public class CustomJwtAuthenticationHandler : AuthenticationHandler<CustomJwtAuthenticationOptions>
    {
        private readonly JwtSecurityTokenHandler _tokenHandler = new JwtSecurityTokenHandler();

        public CustomJwtAuthenticationHandler(
            IOptionsMonitor<CustomJwtAuthenticationOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder
        ) : base(options, logger, encoder) { }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.TryGetValue("Token", out var tokenSource))
            {
                return Task.FromResult(AuthenticateResult.Fail("Missing Token header"));
            }

            var tokenValue = tokenSource.FirstOrDefault();
            if (string.IsNullOrEmpty(tokenValue))
            {
                return Task.FromResult(AuthenticateResult.Fail("Empty Token value"));
            }

            if (VerifyToken(tokenValue, out var principal))
            {
                var ticket = new AuthenticationTicket(principal!, Scheme.Name);
                return Task.FromResult(AuthenticateResult.Success(ticket));
            }

            return Task.FromResult(AuthenticateResult.Fail("Invalid Token"));
        }

        private bool VerifyToken(string tokenValue, out ClaimsPrincipal? principal)
        {
            SecurityToken? validatedToken;
            principal = null;

            if (string.IsNullOrEmpty(Options.IssuerSigningKey))
            {
                Logger.LogError("IssuerSigningKey is not configured.");
                return false;
            }

            var key = Encoding.ASCII.GetBytes(Options.IssuerSigningKey);

            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,

                ValidateLifetime = true,
                RequireExpirationTime = true, 

                ValidateIssuerSigningKey = true,
                RequireSignedTokens = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ClockSkew = TimeSpan.Zero 
            };

            try
            {
                principal = _tokenHandler.ValidateToken(tokenValue, validationParameters, out validatedToken);
                return true;
            }
            catch (SecurityTokenValidationException ex)
            {
                Logger.LogWarning(ex, "Token validation failed.");
                return false;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "An unexpected error occurred during token validation.");
                return false;
            }
        }
    }
}
