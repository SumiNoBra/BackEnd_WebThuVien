using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
namespace Authorization
{


    public class XAuthenticationHandlerOptions : AuthenticationSchemeOptions
    {
        public string IssuerSigningKey { get; set; } = string.Empty;
    }
    /// //////////////////////////////////////////////////////////////////////////

    public class XAuthenticationHandler : AuthenticationHandler<XAuthenticationHandlerOptions>
    {
        public XAuthenticationHandler(
            IOptionsMonitor<XAuthenticationHandlerOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock
        ) : base(options, logger, encoder, clock)
        {
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var tokenSource = Context.Request.Headers["Token"];
            if (tokenSource.Count == 0)
            {
                return Task.FromResult(AuthenticateResult.Fail("Missing Token header"));
            }
            var tokenSourceValue = tokenSource.FirstOrDefault();

            if (!string.IsNullOrEmpty(tokenSourceValue) 
                && verifyClient( tokenSourceValue,out var token, out var principal))
            {
                var ticket = new AuthenticationTicket(principal, Scheme.Name);
                return Task.FromResult(AuthenticateResult.Success(ticket));
            }
            return Task.FromResult(AuthenticateResult.Fail("Invalid Token"));
        }
        private bool verifyClient(string tokenValue, out SecurityToken? token, out ClaimsPrincipal? principal)
        {
            var key = Encoding.ASCII.GetBytes(Options.IssuerSigningKey);
            var handler = new JwtSecurityTokenHandler();
            var TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,// kiem tra nguon goc cua token:"https://your-auth-server.com"
                ValidateAudience = false,// kiem tra token nay dung cho ung dung nao :"my-client-app"
                ValidateLifetime = true,// kiem tra token con han khong
                LifetimeValidator = (notBefore, expires, token, param) =>
                {
                    return expires > DateTime.UtcNow;
                },
                ValidateIssuerSigningKey = true,// co kiem tra chu ky khong
                IssuerSigningKey = new SymmetricSecurityKey(key),// chia khoa de xac thuc chu ky
                RequireSignedTokens = true, // token bat buoc phai co chu ky
                RequireExpirationTime = true // token bat buoc phai co thoi gian het han

            };
            try
            {
                principal = handler.ValidateToken(tokenValue, TokenValidationParameters, out token);
                return true;
            }
            catch (Exception ex)
            {
                principal = null;
                token = null;
                return false;
            }
        }
    }
}
