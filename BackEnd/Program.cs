using Microsoft.EntityFrameworkCore;
using BackEnd.Middleware;
using Domain.Entities;
using Application.Interfaces;
using Infrastructure.Repositories;
using Application.IServices;
using API.Services;
using API.Authentication;
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
            builder.Services.AddCustomJwtAuthentication(builder.Configuration);
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
                options.AddPolicy("User", policy => policy.RequireRole("Admin").RequireRole("User"));
            });
            builder.Services.AddSingleton<JwtTokenService>();
            builder.Services.AddDbContext<QlThuvienContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("QL_THUVIEN"));
            });
            builder.Services.AddScoped<ISachRepository, SachRepository>();
            builder.Services.AddScoped<INguoiDungRepository, NguoiDungRepository>();
            builder.Services.AddScoped<IYeuCauMuonRepository,YeuCauMuonRepository>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IGoogleAuthService, GoogleAuthService>();
            builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();
            builder.Services.AddControllers();
            builder.Services.AddHttpClient();
            builder.Services.AddAutoMapper(typeof(MappingProfile));
            var app = builder.Build();
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }
            app.UseCors("CORS");
            app.UseMiddleware<GlobalExceptionHandlerMiddleware>();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();

        }
    }
}
