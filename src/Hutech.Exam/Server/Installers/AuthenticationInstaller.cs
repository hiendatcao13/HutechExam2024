
using Hutech.Exam.Server.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Hutech.Exam.Server.Installers
{
    public class AuthenticationInstaller : IInstaller
    {
        public void InstallService(IServiceCollection services, IConfiguration configuration)
        {
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
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(JwtAuthenticationManager.JWT_SECURITY_KEY)),
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
