
using AspNetCoreRateLimit;
using Hutech.Exam.Server.BUS;

namespace Hutech.Exam.Server.Installers
{
    public class RateLimitingAPIInstaller : IInstaller
    {
        public void InstallService(IServiceCollection services, IConfiguration configuration)
        {
            services.AddInMemoryRateLimiting();
            //load general configuration from appsettings.json
            services.Configure<ClientRateLimitOptions>(configuration.GetSection("ClientRateLimiting"));
            //load client rules from appsettings.json
            services.Configure<ClientRateLimitPolicies>(configuration.GetSection("ClientRateLimitPolicies"));

            services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();


        }
    }
}
