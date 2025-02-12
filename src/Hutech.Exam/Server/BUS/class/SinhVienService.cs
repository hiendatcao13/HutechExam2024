using AutoMapper;
using Hutech.Exam.Server.DAL.Repositories;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.Models;
using System;
using System.Data;
using System.Diagnostics;

namespace Hutech.Exam.Server.BUS
{
    public class SinhVienService
    {
        private readonly ISinhVienRepository _sinhVienRepository;
        private readonly IMapper _mapper;
        public SinhVienService(ISinhVienRepository sinhVienRepository, IMapper mapper)
        {
            _sinhVienRepository = sinhVienRepository;
            _mapper = mapper;
        }
        private SinhVienDto getProperty(IDataReader dataReader)
        {
            SinhVien sv = new()
            {
                MaSinhVien = dataReader.GetInt64(0),
                HoVaTenLot = dataReader.IsDBNull(1) ? null : dataReader.GetString(1),
                TenSinhVien = dataReader.IsDBNull(2) ? null : dataReader.GetString(2),
                GioiTinh = dataReader.IsDBNull(3) ? null : dataReader.GetInt16(3),
                NgaySinh = dataReader.IsDBNull(4) ? null : dataReader.GetDateTime(4),
                MaLop = dataReader.IsDBNull(5) ? null : dataReader.GetInt32(5),
                DiaChi = dataReader.IsDBNull(6) ? null : dataReader.GetString(6),
                Email = dataReader.IsDBNull(7) ? null : dataReader.GetString(7),
                DienThoai = dataReader.IsDBNull(8) ? null : dataReader.GetString(8),
                MaSoSinhVien = dataReader.IsDBNull(9) ? null : dataReader.GetString(9),
                StudentId = dataReader.IsDBNull(10) ? null : dataReader.GetGuid(10),
                IsLoggedIn = dataReader.IsDBNull(11) ? null : dataReader.GetBoolean(11),
                LastLoggedIn = dataReader.IsDBNull(12) ? null : dataReader.GetDateTime(12),
                LastLoggedOut = dataReader.IsDBNull(13) ? null : dataReader.GetDateTime(13),
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
                    SinhVienDto sv = getProperty(dataReader);
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
                    sv = getProperty(dataReader);
                }
            }
            return sv;
        }
        public async Task Login(long ma_sinh_vien, DateTime last_log_in)
        {
            if(!await _sinhVienRepository.Login(ma_sinh_vien, last_log_in))
            {
                throw new Exception("Can't update SinhVien Login");
            }
        }
        public async Task Logout(long ma_sinh_vien, DateTime last_log_out)
        {
            if(!await _sinhVienRepository.Logout(ma_sinh_vien, last_log_out))
            {
                throw new Exception("Can't update SinhVien Logout");
            }
        }
        //lấy thông tin của 1 sinh viên từ mã số sinh viên
        public async Task<SinhVienDto> SelectOne(long ma_sinh_vien)
        {
            SinhVienDto sv = new();
            using(IDataReader dataReader = await _sinhVienRepository.SelectOne(ma_sinh_vien))
            {
                if(dataReader.Read())
                {
                    sv = getProperty(dataReader);
                }
            }
            return sv;
        }
        public async Task<long> Insert(SinhVienDto sinhVien)
        {
            object? ma_sinh_vien = await _sinhVienRepository.Insert(sinhVien.HoVaTenLot, sinhVien.TenSinhVien, sinhVien.GioiTinh, sinhVien.NgaySinh, sinhVien.MaLop, sinhVien.DiaChi,
                sinhVien.Email, sinhVien.DienThoai, sinhVien.MaSoSinhVien, sinhVien.StudentId);
            try
            {
                return Convert.ToInt64(ma_sinh_vien);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public async Task Update(long ma_sinh_vien, string? ho_va_ten_lot, string? ten_sinh_vien, int? gioi_tinh,
            DateTime? ngay_sinh, int? ma_lop, string? dia_chi, string? email, string? dien_thoai, string? ma_so_sinh_vien)
        {
            if (!await _sinhVienRepository.Update(ma_sinh_vien, ho_va_ten_lot, ten_sinh_vien, gioi_tinh,
            ngay_sinh, ma_lop, dia_chi, email, dien_thoai, ma_so_sinh_vien))
            {
                throw new Exception("Can not update sinh vien");
            }
        }
    }
}
