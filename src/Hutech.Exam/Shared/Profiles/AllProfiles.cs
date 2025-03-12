using AutoMapper;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.DTO.Custom;
using Hutech.Exam.Shared.Models;

namespace Hutech.Exam.Shared.Profiles
{
    public class AllProfiles : Profile
    {
        public AllProfiles()
        {
            CreateMap<TblAudioListened, AudioListenedDto>();
            CreateMap<AudioListenedDto, TblAudioListened>();

            CreateMap<CaThi, CaThiDto>();
            CreateMap<CaThiDto, CaThi>();

            CreateMap<TblCauHoi, CauHoiDto>();
            CreateMap<CauHoiDto, TblCauHoi>();

            CreateMap<TblCauHoiMa, CauHoiMaDto>();
            CreateMap<CauHoiMaDto, TblCauHoiMa>();

            CreateMap<TblCauTraLoi, CauTraLoiDto>();
            CreateMap<CauTraLoiDto, TblCauTraLoi>();

            CreateMap<ChiTietBaiThi, ChiTietBaiThiDto>();
            CreateMap<ChiTietBaiThiDto, ChiTietBaiThi>();

            CreateMap<ChiTietCaThi, ChiTietCaThiDto>();
            CreateMap<ChiTietCaThiDto, ChiTietCaThi>();

            CreateMap<TblChiTietCauHoiMa, ChiTietCauHoiMaDto>();
            CreateMap<ChiTietCauHoiMaDto, TblChiTietCauHoiMa>();

            CreateMap<TblChiTietDeThi,  ChiTietDeThiDto>();
            CreateMap<ChiTietDeThiDto, TblChiTietDeThi>();

            CreateMap<TblChiTietDeThiHoanVi, ChiTietDeThiHoanViDto>();
            CreateMap<ChiTietDeThiHoanViDto, TblChiTietDeThiHoanVi>();

            CreateMap<ChiTietDotThi, ChiTietDotThiDto>();
            CreateMap<ChiTietDotThiDto, ChiTietDotThi>();

            CreateMap<TblDanhmucCaThiTrongNgay, DanhMucCaThiTrongNgayDto>();
            CreateMap<DanhMucCaThiTrongNgayDto, TblDanhmucCaThiTrongNgay>();

            CreateMap<TblDeThi, DeThiDto>();
            CreateMap<DeThiDto, TblDeThi>();

            CreateMap<TblDeThiHoanVi, DeThiHoanViDto>();
            CreateMap<DeThiHoanViDto, TblDeThiHoanVi>();

            CreateMap<DotThi, DotThiDto>();
            CreateMap<DotThiDto, DotThi>();

            CreateMap<Khoa, KhoaDto>();
            CreateMap<KhoaDto, Khoa>();

            CreateMap<LopAo, LopAoDto>();
            CreateMap<LopAoDto, LopAo>();

            CreateMap<Lop, LopDto>();
            CreateMap<LopDto, Lop>();

            CreateMap<Menu, MenuDto>();
            CreateMap<MenuDto, Menu>();

            CreateMap<MonHoc, MonHocDto>();
            CreateMap<MonHocDto, MonHoc>();

            CreateMap<TblNhomCauHoi, NhomCauHoiDto>();
            CreateMap<NhomCauHoiHoanViDto, TblNhomCauHoi>();

            CreateMap<TblNhomCauHoiHoanVi, NhomCauHoiHoanViDto>();
            CreateMap<NhomCauHoiHoanViDto, TblNhomCauHoiHoanVi>();

            CreateMap<SinhVien, SinhVienDto>();
            CreateMap<SinhVienDto, SinhVien>();

            CreateMap<SinhVienLopAo, SinhVienLopAoDto>();
            CreateMap<SinhVienLopAoDto, SinhVienLopAo>();

            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();

            // ánh xạ giữa CustomChiTietBaiThi và ChiTietBaiThiDto (không có CustomDeThi)
            CreateMap<CustomChiTietBaiThi, ChiTietBaiThiDto>();
            CreateMap<ChiTietBaiThiDto, CustomChiTietBaiThi>();
        }
    }
}
