using Hutech.Exam.Server.DAL.Repositories;

namespace Hutech.Exam.Server.Installers
{
    public class RepositoryInstaller : IInstaller
    {
        public void InstallService(IServiceCollection services, IConfiguration configuration)
        {
            // Them cac repository vao
            services.AddScoped<IAudioListenedRepository, AudioListenedRepository>();
            services.AddScoped<ICaThiRepository, CaThiRepository>();
            services.AddScoped<IChiTietBaiThiRepository, ChiTietBaiThiRepository>();
            services.AddScoped<IChiTietCaThiRepository, ChiTietCaThiRepository>();
            services.AddScoped<IChiTietDotThiRepository, ChiTietDotThiRepository>();
            services.AddScoped<IDeThiRepository, DeThiRepository>();
            services.AddScoped<IDotThiRepository, DotThiRepository>();
            services.AddScoped<IKhoaRepository, KhoaRepository>();
            services.AddScoped<ILopAoRepository, LopAoRepository>();
            services.AddScoped<ILopRepository, LopRepository>();
            services.AddScoped<IMonHocRepository, MonHocRepository>();
            services.AddScoped<ISinhVienRepository, SinhVienRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();

        }
    }
}
