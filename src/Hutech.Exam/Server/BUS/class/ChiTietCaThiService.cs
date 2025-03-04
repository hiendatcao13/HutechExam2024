using AutoMapper;
using Hutech.Exam.Server.DAL.Repositories;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.Models;
using System.Data;

namespace Hutech.Exam.Server.BUS
{
    public class ChiTietCaThiService
    {
        private readonly IChiTietCaThiRepository _chiTietCaThiRepository;
        private readonly IMapper _mapper;
        public ChiTietCaThiService(IChiTietCaThiRepository chiTietCaThiRepository, IMapper mapper)
        {
            _chiTietCaThiRepository = chiTietCaThiRepository;
            _mapper = mapper;
        }
        private ChiTietCaThiDto getProperty(IDataReader dataReader)
        {
            ChiTietCaThi chiTietCaThi = new()
            {
                MaChiTietCaThi = dataReader.GetInt32(0),
                MaCaThi = dataReader.IsDBNull(1) ? null : dataReader.GetInt32(1),
                MaSinhVien = dataReader.IsDBNull(2) ? null : dataReader.GetInt64(2),
                MaDeThi = dataReader.IsDBNull(3) ? null : dataReader.GetInt64(3),
                ThoiGianBatDau = dataReader.IsDBNull(4) ? null : dataReader.GetDateTime(4),
                ThoiGianKetThuc = dataReader.IsDBNull(5) ? null : dataReader.GetDateTime(5),
                DaThi = dataReader.GetBoolean(6),
                DaHoanThanh = dataReader.GetBoolean(7),
                Diem = dataReader.GetDouble(8),
                TongSoCau = dataReader.IsDBNull(9) ? null : dataReader.GetInt32(9),
                SoCauDung = dataReader.IsDBNull(10) ? null : dataReader.GetInt32(10),
                GioCongThem = dataReader.GetInt32(11),
                ThoiDiemCong = dataReader.IsDBNull(12) ? null : dataReader.GetDateTime(12),
                LyDoCong = dataReader.IsDBNull(13) ? null : dataReader.GetString(13)
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
                    chiTietCaThi = getProperty(dataReader);
                }
            }
            return chiTietCaThi;
        }
        public async Task<List<ChiTietCaThiDto>> SelectBy_ma_ca_thi(int ma_ca_thi)
        {
            List<ChiTietCaThiDto> result = new();
            using(IDataReader dataReader = await _chiTietCaThiRepository.SelectBy_ma_ca_thi(ma_ca_thi))
            {
                while (dataReader.Read())
                {
                    result.Add(getProperty(dataReader));
                }
            }
            return result;
        }
        public async Task<ChiTietCaThiDto> SelectBy_MaCaThi_MaSinhVien(int ma_ca_thi, long ma_sinh_vien)
        {
            ChiTietCaThiDto chiTietCaThi = new();
            using(IDataReader dataReader = await _chiTietCaThiRepository.SelectBy_MaCaThi_MaSinhVien(ma_ca_thi, ma_sinh_vien))
            {
                if (dataReader.Read())
                {
                    chiTietCaThi = getProperty(dataReader);
                }
            }
            return chiTietCaThi;
        }
        public async Task<List<ChiTietCaThiDto>> SelectBy_ma_sinh_vien(long ma_sinh_vien)
        {
            List<ChiTietCaThiDto> result = new();
            using(IDataReader dataReader = await _chiTietCaThiRepository.SelectBy_ma_sinh_vien(ma_sinh_vien))
            {
                while (dataReader.Read())
                {
                    result.Add(getProperty(dataReader));
                }
            }
            return result;
        }
        public async Task<List<ChiTietCaThiDto>> SelectBy_MaSinhVienThi(long ma_sinh_vien, DateTime ngay_hien_tai)
        {
            List<ChiTietCaThiDto> result = new();
            using (IDataReader dataReader = await _chiTietCaThiRepository.SelectBy_MaSinhVienThi(ma_sinh_vien, ngay_hien_tai))
            {
                while (dataReader.Read())
                {
                    result.Add(getProperty(dataReader));
                }
            }
            return result;
        }
        public async Task<int> UpdateBatDau(ChiTietCaThiDto chiTietCaThi)
        {
            int ma_chi_tiet_ca_thi = chiTietCaThi.MaChiTietCaThi;
            DateTime? thoi_gian_bat_dau = chiTietCaThi.ThoiGianBatDau;
            return await _chiTietCaThiRepository.UpdateBatDau(ma_chi_tiet_ca_thi, thoi_gian_bat_dau);
        }
        public async Task<int> UpdateKetThuc(ChiTietCaThiDto chiTietCaThi)
        {
            //float diem, int so_cau_dung, int tong_so_cau
            int ma_chi_tiet_ca_thi = chiTietCaThi.MaChiTietCaThi;
            DateTime? thoi_gian_ket_thuc = chiTietCaThi.ThoiGianKetThuc;
            double diem = chiTietCaThi.Diem;
            int? so_cau_dung = chiTietCaThi.SoCauDung;
            int? tong_so_cau = chiTietCaThi.TongSoCau;
            return await _chiTietCaThiRepository.UpdateKetThuc(ma_chi_tiet_ca_thi, thoi_gian_ket_thuc, diem, so_cau_dung, tong_so_cau);
        }
        public async Task<int> CongGio(ChiTietCaThiDto chiTietCaThi)
        {
            int ma_chi_tiet_ca_thi = chiTietCaThi.MaChiTietCaThi;
            int gio_cong_them = chiTietCaThi.GioCongThem;
            DateTime? thoi_diem_cong = chiTietCaThi.ThoiDiemCong;
            string? ly_do_cong = chiTietCaThi.LyDoCong;
            return await _chiTietCaThiRepository.CongGio(ma_chi_tiet_ca_thi, gio_cong_them, thoi_diem_cong, ly_do_cong);
        }
        public async Task<int> Insert(int ma_ca_thi, long ma_sinh_vien, long ma_de_thi, int tong_so_cau)
        {
            return (int)(await _chiTietCaThiRepository.Insert(ma_ca_thi, ma_sinh_vien, ma_de_thi, tong_so_cau) ?? -1);
        }
        public async Task<int> Remove(int ma_chi_tiet_ca_thi)
        {
            return await _chiTietCaThiRepository.Remove(ma_chi_tiet_ca_thi);
        }
        public async Task<int> Update(int ma_chi_tiet_ca_thi, int? ma_ca_thi, long? ma_sinh_vien, long? ma_de_thi, int? tong_so_cau)
        {
            return await _chiTietCaThiRepository.Update(ma_chi_tiet_ca_thi, ma_ca_thi, ma_sinh_vien, ma_de_thi, tong_so_cau);
        }
    }
}
