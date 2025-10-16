using Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using BackEnd.EF_Contexts;
using BackEnd.Repositories;
using BackEnd.Middleware;
using BackEnd.Interfaces;
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
            builder.Services.AddSingleton<GenerateJwtToken>();
            builder.Services.AddDbContext<QlThuvienContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("QL_THUVIEN"));
            });
            builder.Services.AddScoped<ISachRepository, SachRepository>();
            builder.Services.AddScoped<INguoiDungRepository, NguoiDungRepository>();
            builder.Services.AddScoped<IYeuCauMuonRepository,YeuCauMuonRepository>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
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
