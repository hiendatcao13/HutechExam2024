
using Hutech.Exam.Server.DAL.Helper;
using Microsoft.AspNetCore.SignalR;

namespace Hutech.Exam.Server.Installers
{
    public class SignalRInstaller : IInstaller
    {
        public void InstallService(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSignalR()
                .AddMessagePackProtocol();
        }
    }
}
