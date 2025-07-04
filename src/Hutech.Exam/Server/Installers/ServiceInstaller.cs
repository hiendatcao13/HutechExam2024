
using Hutech.Exam.Server.BUS;
using Hutech.Exam.Server.BUS.RabbitServices;

namespace Hutech.Exam.Server.Installers
{
    public class ServiceInstaller : IInstaller
    {
        public void InstallService(IServiceCollection services, IConfiguration configuration)
        {
            // Them cac service vao
            services.AddScoped<AudioListenedService>();
            services.AddScoped<CaThiService>();
            services.AddScoped<ChiTietBaiThiService>();
            services.AddScoped<ChiTietCaThiService>();
            services.AddScoped<ChiTietDotThiService>();
            services.AddScoped<DeThiService>();
            services.AddScoped<DotThiService>();
            services.AddScoped<KhoaService>();
            services.AddScoped<LopAoService>();
            services.AddScoped<LopService>();
            services.AddScoped<MonHocService>();
            services.AddScoped<SinhVienService>();
            services.AddScoped<UserService>();

            services.AddScoped<BcryptService>();

            // sử dụng custom lại đề
            services.AddScoped<SubmitService>();
            services.AddScoped<SelectAnswerService>();
            services.AddScoped<ExamRecoveryService>();
            services.AddScoped<RedisService>();
            services.AddScoped<SystemService>();

        }
    }
}
