
using Hutech.Exam.Server.Authentication;
using Hutech.Exam.Server.Configurations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Hutech.Exam.Server.Installers
{
    public class AuthenticationInstaller : IInstaller
    {
        public void InstallService(IServiceCollection services, IConfiguration configuration)
        {
            // Đăng ký cấu hình JWT từ appsettings.json
            var jwtConfig = configuration.GetSection("JwtConfiguration").Get<JwtConfiguration>();

            services.Configure<JwtConfiguration>(configuration.GetSection("JwtConfiguration"));

            services.AddScoped<JwtAuthenticationManager>();


            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.RequireHttpsMetadata = false;
                o.SaveToken = true;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtConfig?.SecurityKey ?? "")),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };

                // Cấu hình để SignalR nhận JWT từ query string khi kết nối WebSocket
                o.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Query["access_token"];
                        var path = context.HttpContext.Request.Path;

                        if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/MainHub"))
                        {
                            context.Token = accessToken;
                        }
                        return Task.CompletedTask;
                    }
                };
            });
        }
    }
}
