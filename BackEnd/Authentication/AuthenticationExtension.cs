using Microsoft.AspNetCore.Authentication;

namespace API.Authentication
{
    public static class CustomJwtAuthenticationExtensions
    {
        public static AuthenticationBuilder AddCustomJwtAuthentication(
            this IServiceCollection services, IConfiguration configuration)
        {
            const string schemeName = CustomJwtAuthenticationOptions.SchemeName;

            return services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = schemeName;
                options.DefaultChallengeScheme = schemeName;
            })
            .AddScheme<CustomJwtAuthenticationOptions, CustomJwtAuthenticationHandler>(
                schemeName, options =>
                {
                    options.IssuerSigningKey = configuration["SecretKey"]
                                               ?? throw new InvalidOperationException("SecretKey is missing in configuration.");
                });
        }
    }

}
