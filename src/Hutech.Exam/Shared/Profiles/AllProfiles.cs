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

            CreateMap<CauHoi, CauHoiDto>();
            CreateMap<CauHoiDto, CauHoi>();

            CreateMap<Role, RoleDto>();
            CreateMap<RoleDto, Role>();

            CreateMap<LoaiCauHoi,  LoaiCauHoiDto>();
            CreateMap<LoaiCauHoiDto, LoaiCauHoi>();

            CreateMap<LoiDeThi, LoiDeThiDto>();
            CreateMap<LoiDeThiDto, LoiDeThi>();

            CreateMap<SinhVienDuPhong, SinhVienDuPhongDto>();
            CreateMap<SinhVienDuPhongDto, SinhVienDuPhong>();

            CreateMap<ThongBao, ThongBaoDto>();
            CreateMap<ThongBaoDto, ThongBao>();

            CreateMap<CauTraLoi, CauTraLoiDto>();
            CreateMap<CauTraLoiDto, CauTraLoi>();

            CreateMap<ChiTietBaiThi, ChiTietBaiThiDto>();
            CreateMap<ChiTietBaiThiDto, ChiTietBaiThi>();

            CreateMap<ChiTietCaThi, ChiTietCaThiDto>();
            CreateMap<ChiTietCaThiDto, ChiTietCaThi>();

            CreateMap<ChiTietDeThi,  ChiTietDeThiDto>();
            CreateMap<ChiTietDeThiDto, ChiTietDeThi>();

            CreateMap<ChiTietDeThiHoanVi, ChiTietDeThiHoanViDto>();
            CreateMap<ChiTietDeThiHoanViDto, ChiTietDeThiHoanVi>();

            CreateMap<ChiTietDotThi, ChiTietDotThiDto>();
            CreateMap<ChiTietDotThiDto, ChiTietDotThi>();

            CreateMap<DeThi, DeThiDto>();
            CreateMap<DeThiDto, DeThi>();

            CreateMap<DeThiHoanVi, DeThiHoanViDto>();
            CreateMap<DeThiHoanViDto, DeThiHoanVi>();

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

            CreateMap<NhomCauHoi, NhomCauHoiDto>();
            CreateMap<NhomCauHoiHoanViDto, NhomCauHoi>();

            CreateMap<NhomCauHoiHoanVi, NhomCauHoiHoanViDto>();
            CreateMap<NhomCauHoiHoanViDto, NhomCauHoiHoanVi>();

            CreateMap<SinhVien, SinhVienDto>();
            CreateMap<SinhVienDto, SinhVien>();

            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();

            CreateMap<Clo, CloDto>();
            CreateMap<CloDto, Clo>();

            // ánh xạ giữa CustomChiTietBaiThi và ChiTietBaiThiDto (không có CustomDeThi)
            CreateMap<ChiTietBaiThiRequest, ChiTietBaiThiDto>();
            CreateMap<ChiTietBaiThiDto, ChiTietBaiThiRequest>();

            CreateMap<CustomNhomCauHoi, NhomCauHoiDto>();
            CreateMap<NhomCauHoiDto, CustomNhomCauHoi>();

            // ánh xạ Request vs Dto ---------------------------------------------------------------------
            CreateMap<CauHoiRequest, CauHoiDto>();
            CreateMap<CauHoiDto, CauHoiRequest>();
        }
    }
}
