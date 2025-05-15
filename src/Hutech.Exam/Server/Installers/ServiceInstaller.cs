
using Hutech.Exam.Server.BUS;

namespace Hutech.Exam.Server.Installers
{
    public class ServiceInstaller : IInstaller
    {
        public void InstallService(IServiceCollection services, IConfiguration configuration)
        {
            // Them cac service vao
            services.AddScoped<AudioListenedService>();
            services.AddScoped<CaThiService>();
            services.AddScoped<CauHoiService>();
            services.AddScoped<CauTraLoiService>();
            services.AddScoped<ChiTietBaiThiService>();
            services.AddScoped<ChiTietCaThiService>();
            services.AddScoped<ChiTietDeThiHoanViService>();
            services.AddScoped<ChiTietDeThiService>();
            services.AddScoped<ChiTietDotThiService>();
            services.AddScoped<DeThiHoanViService>();
            services.AddScoped<DeThiService>();
            services.AddScoped<DotThiService>();
            services.AddScoped<KhoaService>();
            services.AddScoped<LopAoService>();
            services.AddScoped<LopService>();
            services.AddScoped<MonHocService>();
            services.AddScoped<NhomCauHoiHoanViService>();
            services.AddScoped<NhomCauHoiService>();
            services.AddScoped<SinhVienService>();
            services.AddScoped<UserService>();
            services.AddScoped<CloService>();

            // sử dụng custom lại đề
            services.AddScoped<CustomDeThiService>();


            //sử dụng RabbitMQ
            services.AddScoped<RabbitMQService>();

            //sử dụng Redis
            services.AddScoped<RedisService>();

        }
    }
}
