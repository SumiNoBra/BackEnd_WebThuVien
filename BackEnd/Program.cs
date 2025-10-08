using Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using BackEnd.EF_Contexts;
using BackEnd.Repositories;
namespace WebApplication1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            // Add cors policy
            builder.Services.AddCors(option =>
            {
                option.AddPolicy("CORS", options =>
                {
                    options
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
                });
            });
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
                options.AddPolicy("User", policy => policy.RequireRole("Admin").RequireRole("User"));
            });
            builder.Services.AddXAuthentication(builder.Configuration);
            builder.Services.AddDbContext<QlThuvienContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("QL_THUVIEN"));
            });
            builder.Services.AddSingleton<GenerateJwtToken>();
            builder.Services.AddScoped<ISachRepository, SachRepository>();
            builder.Services.AddScoped<INguoiDungRepository, NguoiDungRepository>();
            builder.Services.AddControllers();
            builder.Services.AddHttpClient();
            builder.Services.AddAutoMapper(typeof(MappingProfile));
            var app = builder.Build();
            //app.UseHttpsRedirection();
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }
            app.UseCors("CORS");
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();

        }
    }
}
