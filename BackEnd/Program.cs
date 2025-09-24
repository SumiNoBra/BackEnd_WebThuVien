using MainAPI.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
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
                options.AddPolicy("Policy1", policy => policy.RequireRole("Role1").RequireRole("Role2"));
                options.AddPolicy("Policy2", policy => policy.RequireClaim("Role", "User", "Admin"));
            });
            builder.Services.AddScoped<IClientSourceAuthentication, ClientSourceAuthentication>();
            builder.Services.AddScoped<ISinhVienRepository, SinhVienReponsitory>();
            builder.Services.AddScoped<IAuthenRepo, AuthenRepo>();
            builder.Services.AddSingleton<IConversation, Conversation>();
            builder.Services.AddDatabase(builder.Configuration);
            builder.Services.AddAutoMapper(typeof(MappingProfile));
            builder.Services.AddControllers();
            builder.Services.AddHttpClient();
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
