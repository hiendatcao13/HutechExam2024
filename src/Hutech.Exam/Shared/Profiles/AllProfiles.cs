using AutoMapper;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.DTO.Custom;
using Hutech.Exam.Shared.DTO.Request;
using Hutech.Exam.Shared.Models;

namespace Hutech.Exam.Shared.Profiles
{
    public class AllProfiles : Profile
    {
        public AllProfiles()
        {
            CreateMap<AudioListened, AudioListenedDto>();
            CreateMap<AudioListenedDto, AudioListened>();

            CreateMap<CaThi, CaThiDto>();
            CreateMap<CaThiDto, CaThi>();

            CreateMap<Role, RoleDto>();
            CreateMap<RoleDto, Role>();

            CreateMap<ChiTietBaiThi, ChiTietBaiThiDto>();
            CreateMap<ChiTietBaiThiDto, ChiTietBaiThi>();

            CreateMap<ChiTietCaThi, ChiTietCaThiDto>();
            CreateMap<ChiTietCaThiDto, ChiTietCaThi>();

            CreateMap<ChiTietDotThi, ChiTietDotThiDto>();
            CreateMap<ChiTietDotThiDto, ChiTietDotThi>();

            CreateMap<DeThi, DeThiDto>();
            CreateMap<DeThiDto, DeThi>();

            CreateMap<DotThi, DotThiDto>();
            CreateMap<DotThiDto, DotThi>();

            CreateMap<Khoa, KhoaDto>();
            CreateMap<KhoaDto, Khoa>();

            CreateMap<LopAo, LopAoDto>();
            CreateMap<LopAoDto, LopAo>();

            CreateMap<Lop, LopDto>();
            CreateMap<LopDto, Lop>();

            CreateMap<MonHoc, MonHocDto>();
            CreateMap<MonHocDto, MonHoc>();

            CreateMap<SinhVien, SinhVienDto>();
            CreateMap<SinhVienDto, SinhVien>();

            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();

            // ánh xạ giữa CustomChiTietBaiThi và ChiTietBaiThiDto (không có CustomDeThi)
            CreateMap<ChiTietBaiThiRequest, ChiTietBaiThiDto>();
            CreateMap<ChiTietBaiThiDto, ChiTietBaiThiRequest>();

            // ánh xạ Request vs Dto ---------------------------------------------------------------------

            CreateMap<ChiTietCaThiRequest, ChiTietCaThiDto>();
            CreateMap<ChiTietCaThiDto, ChiTietCaThiRequest>();

        }
    }
}
