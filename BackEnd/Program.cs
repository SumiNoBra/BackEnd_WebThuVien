using API.Authentication;
using API.Services;
using Application.Interfaces;
using Application.IServices;
using BackEnd.Middleware;
using Domain.Entities;
using Infrastructure.Context;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Threading.RateLimiting;
namespace WebApplication1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
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
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    var key = Encoding.UTF8.GetBytes(builder.Configuration["SecretKey"] ?? string.Empty);
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
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
                });
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
                options.AddPolicy("User", policy => policy.RequireRole("Admin").RequireRole("User"));
            });
            builder.Services.AddSingleton<JwtTokenService>();
            builder.Services.AddDbContext<QuanlythuvienContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("QL_THUVIENV2"));
            });
            builder.Services.AddMemoryCache();
            //v1
            builder.Services.AddScoped<IGoogleAuthService, GoogleAuthService>();
            //
            //v2
            builder.Services.AddScoped<ITaiLieuRepo, TaiLieuRepo>();
            builder.Services.AddScoped<IDocGiaRepo, DocGiaRepo>();
            builder.Services.AddScoped<INhanVienRepo, NhanVienRepo>();
            builder.Services.AddScoped<ITacGia_TheLoai_NXBRepo, TacGia_TheLoai_NXB>();
            builder.Services.AddScoped<ITheBanDocRepo, TheBanDocRepo>();
            builder.Services.AddScoped<IPhieuMuonRepo, PhieuMuonRepo>();
            builder.Services.AddScoped<IDanhGiaBinhLuanRepo, DanhGiaBinhLuanRepo>();
            builder.Services.AddScoped<IPhieuPhatRepo, PhieuPhatRepo>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IDatMuonTruocRepo, DatMuonTruocRepo>();
            builder.Services.AddScoped<IXuLyGiaHanRepo, XuLyGiaHanRepo>();
            // rate limit
            builder.Services.AddRateLimiter(options =>
            {
                options.AddFixedWindowLimiter(policyName: "fixedwindow", configureOptions =>
                {
                    configureOptions.PermitLimit = 1000;
                    configureOptions.Window = TimeSpan.FromSeconds(100);
                    configureOptions.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                    configureOptions.QueueLimit = 0;
                });

                options.AddPolicy("per_ip", httpContext =>
                    RateLimitPartition.GetFixedWindowLimiter(
                        partitionKey: httpContext.User.Identity?.IsAuthenticated == true
                            ? httpContext.User.Identity.Name!
                            : httpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown",
                        factory: _ => new FixedWindowRateLimiterOptions
                        {
                            PermitLimit = 500,
                            Window = TimeSpan.FromSeconds(30),
                            QueueLimit = 0
                        }));
                options.AddPolicy("ip_login", httpcontext =>

                    RateLimitPartition.GetFixedWindowLimiter(
                        partitionKey: httpcontext.Connection.RemoteIpAddress?.ToString() ?? "unknown",
                        factory: _ => new FixedWindowRateLimiterOptions
                        {
                            PermitLimit = 5,
                            Window = TimeSpan.FromSeconds(10),
                            QueueLimit = 0
                        }));

                options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

            });




            builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();
            builder.Services.AddControllers();
            builder.Services.AddHttpClient();
            builder.Services.AddAutoMapper(typeof(MappingProfile));
            var app = builder.Build();
            app.UseRateLimiter();
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
