using Microsoft.AspNetCore.Authentication;

namespace Authorization
{
    public static class XClientSourceAuthenticationExtensions
    {
        public static AuthenticationBuilder AddXAuthentication(
            this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddAuthentication("X-Client-Source")
                .AddScheme<XAuthenticationHandlerOptions, XAuthenticationHandler>(
                    "X-Client-Source", options =>
                    {
                        options.IssuerSigningKey = configuration["SecretKey"]!;
                    });
        }
    }

}
