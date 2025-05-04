
using Hutech.Exam.Server.DAL.Repositories;
using Hutech.Exam.Shared.DTO.Custom;

namespace Hutech.Exam.Server.Installers
{
    public class RepositoryInstaller : IInstaller
    {
        public void InstallService(IServiceCollection services, IConfiguration configuration)
        {
            // Them cac repository vao
            services.AddScoped<IAudioListenedRepository, AudioListenedRepository>();
            services.AddScoped<ICaThiRepository, CaThiRepository>();
            services.AddScoped<ICauHoiRepository, CauHoiRepository>();
            services.AddScoped<ICauTraLoiRepository, CauTraLoiRepository>();
            services.AddScoped<IChiTietBaiThiRepository, ChiTietBaiThiRepository>();
            services.AddScoped<IChiTietCaThiRepository, ChiTietCaThiRepository>();
            services.AddScoped<IChiTietDeThiHoanViRepository, ChiTietDeThiHoanViRepository>();
            services.AddScoped<IChiTietDeThiRepository, ChiTietDeThiRepository>();
            services.AddScoped<IChiTietDotThiResposity, ChiTietDotThiResposity>();
            services.AddScoped<IDeThiHoanViRepository, DeThiHoanViRepository>();
            services.AddScoped<IDeThiRepository, DeThiRepository>();
            services.AddScoped<IDotThiRepository, DotThiRepository>();
            services.AddScoped<IKhoaRepository, KhoaRepository>();
            services.AddScoped<ILopAoRepository, LopAoRepository>();
            services.AddScoped<ILopRepository, LopRepository>();
            services.AddScoped<IMonHocRepository, MonHocRepository>();
            services.AddScoped<INhomCauHoiHoanViRepository, NhomCauHoiHoanViRepository>();
            services.AddScoped<INhomCauHoiRepository, NhomCauHoiRepository>();
            services.AddScoped<ISinhVienRepository, SinhVienRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ICloRepository, CloRepository>();
            services.AddScoped<ICustomRepository, CustomRepository>();
            services.AddScoped<CustomDeThi>();
        }
    }
}
