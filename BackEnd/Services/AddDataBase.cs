using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<QL_SinhVienContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("QL_SV")));
        return services;
    }
}
