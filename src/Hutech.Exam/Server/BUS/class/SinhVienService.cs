using AutoMapper;
using Hutech.Exam.Server.DAL.Repositories;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.Models;
using System;
using System.Data;
using System.Diagnostics;

namespace Hutech.Exam.Server.BUS
{
    public class SinhVienService(ISinhVienRepository sinhVienRepository, IMapper mapper)
    {
        private readonly ISinhVienRepository _sinhVienRepository = sinhVienRepository;
        private readonly IMapper _mapper = mapper;

        public static readonly int COLUMN_LENGTH = 14; // số lượng cột trong bảng SinhVien

        public SinhVienDto GetProperty(IDataReader dataReader, int start = 0)
        {
            SinhVien sv = new()
            {
                MaSinhVien = dataReader.GetInt64(0 + start),
                HoVaTenLot = dataReader.IsDBNull(1 + start) ? null : dataReader.GetString(1 + start),
                TenSinhVien = dataReader.IsDBNull(2 + start) ? null : dataReader.GetString(2 + start),
                GioiTinh = dataReader.IsDBNull(3 + start) ? null : dataReader.GetInt16(3 + start),
                NgaySinh = dataReader.IsDBNull(4 + start) ? null : dataReader.GetDateTime(4 + start),
                MaLop = dataReader.IsDBNull(5 + start) ? null : dataReader.GetInt32(5 + start),
                DiaChi = dataReader.IsDBNull(6 + start) ? null : dataReader.GetString(6 + start),
                Email = dataReader.IsDBNull(7 + start) ? null : dataReader.GetString(7 + start),
                DienThoai = dataReader.IsDBNull(8 + start) ? null : dataReader.GetString(8 + start),
                MaSoSinhVien = dataReader.IsDBNull(9 + start) ? null : dataReader.GetString(9 + start),
                StudentId = dataReader.IsDBNull(10 + start) ? null : dataReader.GetGuid(10 + start),
                IsLoggedIn = dataReader.IsDBNull(11 + start) ? null : dataReader.GetBoolean(11 + start),
                LastLoggedIn = dataReader.IsDBNull(12 + start) ? null : dataReader.GetDateTime(12 + start),
                LastLoggedOut = dataReader.IsDBNull(13 + start) ? null : dataReader.GetDateTime(13 + start),
                Photo = null // chưa biết cách xử lí (image === byte)
            };
            return _mapper.Map<SinhVienDto>(sv);
        }
        public async Task<List<SinhVienDto>> GetAll()
        {
            List<SinhVienDto> result = new();
            using(IDataReader dataReader = await _sinhVienRepository.GetAll())
            {
                while (dataReader.Read())
                {
                    SinhVienDto sv = GetProperty(dataReader);
                    result.Add(sv);
                }
            }
            return result;
        }
        public async Task<SinhVienDto> SelectBy_ma_so_sinh_vien(string ma_so_sinh_vien)
        {
            SinhVienDto sv = new();
            using (IDataReader dataReader = await _sinhVienRepository.SelectBy_ma_so_sinh_vien(ma_so_sinh_vien))
            {
                if (dataReader.Read())
                {
                    sv = GetProperty(dataReader);
                }
            }
            return sv;
        }
        public async Task<int> Login(long ma_sinh_vien, DateTime last_log_in)
        {
            return await _sinhVienRepository.Login(ma_sinh_vien, last_log_in);
        }
        public async Task<bool> Logout(long ma_sinh_vien, DateTime last_log_out)
        {
            return await _sinhVienRepository.Logout(ma_sinh_vien, last_log_out) != 0;
            
        }
        //lấy thông tin của 1 sinh viên từ mã số sinh viên
        public async Task<SinhVienDto> SelectOne(long ma_sinh_vien)
        {
            SinhVienDto sv = new();
            using(IDataReader dataReader = await _sinhVienRepository.SelectOne(ma_sinh_vien))
            {
                if(dataReader.Read())
                {
                    sv = GetProperty(dataReader);
                }
            }
            return sv;
        }
        public async Task<long> Insert(SinhVienDto sinhVien)
        {
            return (long)(await _sinhVienRepository.Insert(sinhVien.HoVaTenLot, sinhVien.TenSinhVien, sinhVien.GioiTinh, sinhVien.NgaySinh, sinhVien.MaLop, sinhVien.DiaChi,
                sinhVien.Email, sinhVien.DienThoai, sinhVien.MaSoSinhVien, sinhVien.StudentId) ?? -1);
        }
        public async Task<int> Update(long ma_sinh_vien, string? ho_va_ten_lot, string? ten_sinh_vien, int? gioi_tinh,
            DateTime? ngay_sinh, int? ma_lop, string? dia_chi, string? email, string? dien_thoai, string? ma_so_sinh_vien)
        {
            return await _sinhVienRepository.Update(ma_sinh_vien, ho_va_ten_lot, ten_sinh_vien, gioi_tinh,
            ngay_sinh, ma_lop, dia_chi, email, dien_thoai, ma_so_sinh_vien);
        }
    }
}
