
using Microsoft.AspNetCore.ResponseCompression;
using System.Configuration;

namespace Hutech.Exam.Server.Installers
{
    public class SystemInstaller : IInstaller
    {
        public void InstallService(IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions();
            services.AddMemoryCache();

            services.AddControllersWithViews();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddRazorPages();
            services.AddResponseCompression(option =>
            {
                option.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
                    new[] { "application/octet-stream" 
                    });
            });
        }
    }
}
