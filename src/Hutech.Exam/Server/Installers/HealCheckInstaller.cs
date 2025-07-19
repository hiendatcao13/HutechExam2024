
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;

namespace Hutech.Exam.Server.Installers
{
    public class HealCheckInstaller : IInstaller
    {
        public void InstallService(IServiceCollection services, IConfiguration configuration)
        {
            var rabbitSection = configuration.GetSection("RabbitMQConfiguration");


            var factory = new ConnectionFactory()
            {
                HostName = rabbitSection["HostName"]!,
                UserName = rabbitSection["Username"]!,
                Password = rabbitSection["Password"]!
            };

            services.AddHealthChecks()
                .AddSqlServer(configuration.GetConnectionString("DefaultConnection")!, name: "SQL Server")
                .AddRedis(configuration.GetSection("RedisConfiguration:ConnectionString").Value!, name: "Redis")
                .AddRabbitMQ(sp =>
                {
                    return factory.CreateConnectionAsync();
                }, name: "RabbitMQ")
                .AddProcessAllocatedMemoryHealthCheck(1024 * 1024 * 1024, name:"Bộ nhớ cấp phát (.NET)") // 1GB
                .AddPrivateMemoryHealthCheck(512 * 1024 * 1024, name: "Bộ nhớ riêng"); // 512MB

            //services.AddHealthChecksUI(setupSettings: setup =>
            //{
            //    setup.SetEvaluationTimeInSeconds(10); // Kiểm tra mỗi 10 giây
            //    setup.AddHealthCheckEndpoint("Basic Health", "/health");
            //}).AddInMemoryStorage();

        }
    }
}
