
using Hutech.Exam.Server.BUS.RabbitServices;
using Hutech.Exam.Server.Configurations;

namespace Hutech.Exam.Server.Installers
{
    public class QueueInstaller : IInstaller
    {
        public void InstallService(IServiceCollection services, IConfiguration configuration)
        {
            //cấu hình RabbitMQ trong appsettings.json
            services.Configure<RabbitMQConfiguration>(configuration.GetSection("RabbitMQConfiguration"));

            //sử dụng RabbitMQ
            services.AddScoped<AnswerQueueService>();
            services.AddScoped<SubmitQueueService>();
        }
    }
}
