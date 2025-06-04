using AutoMapper;
using Hutech.Exam.Server.DAL.Repositories;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.DTO.Page;
using Hutech.Exam.Shared.DTO.Request;
using Hutech.Exam.Shared.DTO.Request.ChiTietCaThi;
using Hutech.Exam.Shared.Models;
using System.Data;

namespace Hutech.Exam.Server.BUS
{
    public class ChiTietCaThiService(IChiTietCaThiRepository chiTietCaThiRepository, CaThiService _caThiService, ChiTietDotThiService _chiTietDotThiService, 
        LopAoService _lopAoServcie, MonHocService _monHocService, SinhVienService _sinhVienService, IMapper mapper)
    {
        private readonly IChiTietCaThiRepository _chiTietCaThiRepository = chiTietCaThiRepository;
        private readonly CaThiService _caThiService = _caThiService;
        private readonly ChiTietDotThiService _chiTietDotThiService = _chiTietDotThiService;
        private readonly LopAoService _lopAoServcie = _lopAoServcie;
        private readonly MonHocService _monHocService = _monHocService;
        private readonly SinhVienService _sinhVienService = _sinhVienService;
        private readonly IMapper _mapper = mapper;

        public static readonly int COLUMN_LENGTH = 14; // số lượng cột trong bảng ChiTietCaThi

        public ChiTietCaThiDto GetProperty(IDataReader dataReader, int start = 0)
        {
            ChiTietCaThi chiTietCaThi = new()
            {
                MaChiTietCaThi = dataReader.GetInt32(0 + start),
                MaCaThi = dataReader.IsDBNull(1 + start) ? null : dataReader.GetInt32(1 + start),
                MaSinhVien = dataReader.IsDBNull(2 + start) ? null : dataReader.GetInt64(2 + start),
                MaDeThi = dataReader.IsDBNull(3 + start) ? null : dataReader.GetInt64(3 + start),
                ThoiGianBatDau = dataReader.IsDBNull(4 + start) ? null : dataReader.GetDateTime(4 + start),
                ThoiGianKetThuc = dataReader.IsDBNull(5 + start) ? null : dataReader.GetDateTime(5 + start),
                DaThi = dataReader.GetBoolean(6 + start),
                DaHoanThanh = dataReader.GetBoolean(7 + start),
                Diem = dataReader.GetDouble(8 + start),
                TongSoCau = dataReader.IsDBNull(9 + start) ? null : dataReader.GetInt32(9 + start),
                SoCauDung = dataReader.IsDBNull(10 + start) ? null : dataReader.GetInt32(10 + start),
                GioCongThem = dataReader.GetInt32(11 + start),
                ThoiDiemCong = dataReader.IsDBNull(12 + start) ? null : dataReader.GetDateTime(12 + start),
                LyDoCong = dataReader.IsDBNull(13 + start) ? null : dataReader.GetString(13 + start)
            };
            return _mapper.Map<ChiTietCaThiDto>(chiTietCaThi);
        }
        public async Task<ChiTietCaThiDto> SelectOne(int chi_tiet_ca_thi)
        {
            ChiTietCaThiDto chiTietCaThi = new();
            using (IDataReader dataReader = await _chiTietCaThiRepository.SelectOne(chi_tiet_ca_thi))
            {
                if (dataReader.Read())
                {
                    chiTietCaThi = GetProperty(dataReader);
                    chiTietCaThi.MaSinhVienNavigation = _sinhVienService.GetProperty(dataReader, COLUMN_LENGTH);
                }
            }
            return chiTietCaThi;
        }
        public async Task<List<ChiTietCaThiDto>> SelectBy_ma_ca_thi(int ma_ca_thi)
        {
            List<ChiTietCaThiDto> result = [];
            using(IDataReader dataReader = await _chiTietCaThiRepository.SelectBy_ma_ca_thi(ma_ca_thi))
            {
                while (dataReader.Read())
                {
                    ChiTietCaThiDto chiTietCaThi = GetProperty(dataReader);
                    chiTietCaThi.MaSinhVienNavigation = _sinhVienService.GetProperty(dataReader, COLUMN_LENGTH);

                    result.Add(chiTietCaThi);
                }
            }
            return result;
        }
        public async Task<Paged<ChiTietCaThiDto>> SelectBy_MaCaThi_Paged(int ma_ca_thi, int pageNumber, int pageSize)
        {
            List<ChiTietCaThiDto> result = [];
            int tong_so_ban_ghi = 0, tong_so_trang = 0;
            using (IDataReader dataReader = await _chiTietCaThiRepository.SelectBy_ma_ca_thi_Paged(ma_ca_thi, pageNumber, pageSize))
            {
                while (dataReader.Read())
                {
                    ChiTietCaThiDto chiTietCaThi = GetProperty(dataReader);
                    chiTietCaThi.MaSinhVienNavigation = _sinhVienService.GetProperty(dataReader, COLUMN_LENGTH);
                    result.Add(chiTietCaThi);
                }

                //chuyển sang bảng thứ 2 đọc tổng số lượng bản ghi và tổng số lượng trang
                if (dataReader.NextResult())
                {
                    while (dataReader.Read())
                    {
                        tong_so_ban_ghi = dataReader.GetInt32(0);
                        tong_so_trang = dataReader.GetInt32(1);
                    }
                }
            }
            return new Paged<ChiTietCaThiDto>() { Data = result, TotalPages = tong_so_trang, TotalRecords = tong_so_ban_ghi};
        }

        public async Task<Paged<ChiTietCaThiDto>> SelectBy_MaCaThi_Search_Paged(int ma_ca_thi, string keyword, int pageNumber, int pageSize)
        {
            List<ChiTietCaThiDto> result = [];
            int tong_so_ban_ghi = 0, tong_so_trang = 0;
            using (IDataReader dataReader = await _chiTietCaThiRepository.SelectBy_ma_ca_thi_Search_Paged(ma_ca_thi, keyword, pageNumber, pageSize))
            {
                while (dataReader.Read())
                {
                    ChiTietCaThiDto chiTietCaThi = GetProperty(dataReader);
                    chiTietCaThi.MaSinhVienNavigation = _sinhVienService.GetProperty(dataReader, COLUMN_LENGTH);
                    result.Add(chiTietCaThi);
                }

                //chuyển sang bảng thứ 2 đọc tổng số lượng bản ghi và tổng số lượng trang
                if (dataReader.NextResult())
                {
                    while (dataReader.Read())
                    {
                        tong_so_ban_ghi = dataReader.GetInt32(0);
                        tong_so_trang = dataReader.GetInt32(1);
                    }
                }
            }
            return new Paged<ChiTietCaThiDto>() { Data = result, TotalPages = tong_so_trang, TotalRecords = tong_so_ban_ghi };
        }

        public async Task<ChiTietCaThiDto> SelectBy_MaCaThi_MaSinhVien(int ma_ca_thi, long ma_sinh_vien)
        {
            ChiTietCaThiDto chiTietCaThi = new();
            using(IDataReader dataReader = await _chiTietCaThiRepository.SelectBy_MaCaThi_MaSinhVien(ma_ca_thi, ma_sinh_vien))
            {
                if (dataReader.Read())
                {
                    chiTietCaThi = GetProperty(dataReader);
                }
            }
            return chiTietCaThi;
        }
        public async Task<List<ChiTietCaThiDto>> SelectBy_ma_sinh_vien(long ma_sinh_vien)
        {
            List<ChiTietCaThiDto> result = [];
            using(IDataReader dataReader = await _chiTietCaThiRepository.SelectBy_ma_sinh_vien(ma_sinh_vien))
            {
                while (dataReader.Read())
                {
                    result.Add(GetProperty(dataReader));
                }
            }
            return result;
        }
        public async Task<ChiTietCaThiDto> SelectBy_MaSinhVienThi(long ma_sinh_vien)
        {
            int ca_thi_column = CaThiService.COLUMN_LENGTH, chi_dot_thi_column = ChiTietDotThiService.COLUMN_LENGTH, lop_ao_column = LopAoService.COLUMN_LENGTH, mon_hoc_column = MonHocService.COLUMN_LENGTH;
            ChiTietCaThiDto result = new();
            using (IDataReader dataReader = await _chiTietCaThiRepository.SelectBy_MaSinhVienThi(ma_sinh_vien))
            {
                if (dataReader.Read())
                {
                    result = GetProperty(dataReader);
                    result.MaCaThiNavigation = _caThiService.GetProperty(dataReader, COLUMN_LENGTH);
                    result.MaCaThiNavigation.MaChiTietDotThiNavigation = _chiTietDotThiService.GetProperty(dataReader, COLUMN_LENGTH + ca_thi_column);
                    result.MaCaThiNavigation.MaChiTietDotThiNavigation.MaLopAoNavigation = _lopAoServcie.GetProperty(dataReader, COLUMN_LENGTH + ca_thi_column + chi_dot_thi_column);
                    result.MaCaThiNavigation.MaChiTietDotThiNavigation.MaLopAoNavigation.MaMonHocNavigation = _monHocService.GetProperty(dataReader, COLUMN_LENGTH + ca_thi_column + chi_dot_thi_column + lop_ao_column);
                    result.MaSinhVienNavigation = _sinhVienService.GetProperty(dataReader, COLUMN_LENGTH + ca_thi_column + chi_dot_thi_column + lop_ao_column + mon_hoc_column);
                }
                if (result.MaChiTietCaThi == 0) //không có dữ liệu
                    result.MaSinhVienNavigation = await _sinhVienService.SelectOne(ma_sinh_vien);
            }
            return result;
        }
        public async Task<bool> UpdateBatDau(int ma_chi_tiet_ca_thi, DateTime thoi_gian_bat_dau)
        {
            return await _chiTietCaThiRepository.UpdateBatDau(ma_chi_tiet_ca_thi, thoi_gian_bat_dau) != 0;
        }
        public async Task<bool> UpdateKetThuc(int id, ChiTietCaThiUpdateKTThiRequest chiTietCaThi)
        {
            return await _chiTietCaThiRepository.UpdateKetThuc(id, chiTietCaThi.ThoiGianKetThuc, chiTietCaThi.Diem, chiTietCaThi.SoCauDung, chiTietCaThi.TongSoCau) != 0;
        }
        public async Task<bool> CongGio(int id, ChiTietCaThiUpdateCongGioRequest chiTietCaThi)
        {
            return await _chiTietCaThiRepository.CongGio(id, chiTietCaThi.GioCongThem, chiTietCaThi.ThoiDiemCong, chiTietCaThi.LyDoCong) != 0;
        }
        public async Task<int> Insert(ChiTietCaThiCreateRequest chiTietCaThi)
        {
            return Convert.ToInt32(await _chiTietCaThiRepository.Insert(chiTietCaThi.MaCaThi, chiTietCaThi.MaSinhVien, chiTietCaThi.MaDeThi, -1) ?? -1);
        }

        public async Task<bool> ThemSVKhanCap(string ma_so_sinh_vien, int ma_ca_thi, long ma_de_thi)
        {
            return Convert.ToInt32(await _chiTietCaThiRepository.ThemSVKhanCap(ma_so_sinh_vien, ma_ca_thi, ma_de_thi)) != 0;
        }
        public async Task<int> Remove(int ma_chi_tiet_ca_thi)
        {
            return await _chiTietCaThiRepository.Remove(ma_chi_tiet_ca_thi);
        }
        public async Task<bool> Update(int id, ChiTietCaThiUpdateRequest chiTietCaThi)
        {
            return await _chiTietCaThiRepository.Update(id, chiTietCaThi.MaCaThi, chiTietCaThi.MaSinhVien, chiTietCaThi.MaDeThi, -1) != 0;
        }
    }
}
