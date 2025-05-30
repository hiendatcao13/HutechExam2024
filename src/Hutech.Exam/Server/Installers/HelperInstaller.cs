
using Hutech.Exam.Server.DAL.Helper;
using Hutech.Exam.Server.Helper;

namespace Hutech.Exam.Server.Installers
{
    public class HelperInstaller : IInstaller
    {
        public void InstallService(IServiceCollection services, IConfiguration configuration)
        {

            services.AddSingleton<IHashIdHelper, HashIdHelper>();
        }
    }
}
