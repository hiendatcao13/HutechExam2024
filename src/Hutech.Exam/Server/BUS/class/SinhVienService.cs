using Hutech.Exam.Server.DAL.Repositories;
using Hutech.Exam.Shared.Models;
using System;
using System.Data;
using System.Diagnostics;

namespace Hutech.Exam.Server.BUS
{
    public class SinhVienService
    {
        private readonly ISinhVienRepository _sinhVienRepository;
        public SinhVienService(ISinhVienRepository sinhVienRepository)
        {
            _sinhVienRepository = sinhVienRepository;
        }
        public List<SinhVien> GetAll()
        {
            List<SinhVien> list = new List<SinhVien>();
            using(IDataReader dataReader = _sinhVienRepository.GetAll())
            {
                while (dataReader.Read())
                {
                    SinhVien sv = getProperty(dataReader);
                    list.Add(sv);
                }
            }
            return list;
        }
        private SinhVien getProperty(IDataReader dataReader)
        {
            SinhVien sv = new SinhVien();
            sv.MaSinhVien = dataReader.GetInt64(0);
            sv.HoVaTenLot = dataReader.IsDBNull(1) ? null : dataReader.GetString(1);
            sv.TenSinhVien = dataReader.IsDBNull(2) ? null : dataReader.GetString(2);
            sv.GioiTinh = dataReader.IsDBNull(3) ? null : dataReader.GetInt16(3);
            sv.NgaySinh = dataReader.IsDBNull(4) ? null : dataReader.GetDateTime(4);
            sv.MaLop = dataReader.IsDBNull(5) ? null : dataReader.GetInt32(5);
            sv.DiaChi = dataReader.IsDBNull(6) ? null : dataReader.GetString(6);
            sv.Email = dataReader.IsDBNull(7) ? null : dataReader.GetString(7);
            sv.DienThoai = dataReader.IsDBNull(8) ? null : dataReader.GetString(8);
            sv.MaSoSinhVien = dataReader.IsDBNull(9) ? null : dataReader.GetString(9);
            sv.StudentId = dataReader.IsDBNull(10) ? null : dataReader.GetGuid(10);
            sv.IsLoggedIn = dataReader.IsDBNull(11) ? null : dataReader.GetBoolean(11);
            sv.LastLoggedIn = dataReader.IsDBNull(12) ? null : dataReader.GetDateTime(12);
            sv.LastLoggedOut = dataReader.IsDBNull(13) ? null : dataReader.GetDateTime(13);
            sv.Photo = null; // chưa biết cách xử lí (image === byte)
            return sv;
        }
        public SinhVien SelectBy_ma_so_sinh_vien(string ma_so_sinh_vien)
        {
            SinhVien sv = new SinhVien();
            using (IDataReader dataReader = _sinhVienRepository.SelectBy_ma_so_sinh_vien(ma_so_sinh_vien))
            {
                if (dataReader.Read())
                {
                    sv = getProperty(dataReader);
                }
            }
            return sv;
        }
        public void Login(long ma_sinh_vien, DateTime last_log_in)
        {
            if(!_sinhVienRepository.Login(ma_sinh_vien, last_log_in))
            {
                throw new Exception("Can't update SinhVien Login");
            }
        }
        public void Logout(long ma_sinh_vien, DateTime last_log_out)
        {
            if(!_sinhVienRepository.Logout(ma_sinh_vien, last_log_out))
            {
                throw new Exception("Can't update SinhVien Logout");
            }
        }
        //lấy thông tin của 1 sinh viên từ mã số sinh viên
        public SinhVien SelectOne(long ma_sinh_vien)
        {
            SinhVien sv = new SinhVien();
            using(IDataReader dataReader = _sinhVienRepository.SelectOne(ma_sinh_vien))
            {
                if(dataReader.Read())
                {
                    sv = getProperty(dataReader);
                }
            }
            return sv;
        }
        public long Insert(SinhVien sinhVien)
        {
            Object ma_sinh_vien = _sinhVienRepository.Insert(sinhVien.HoVaTenLot, sinhVien.TenSinhVien, sinhVien.GioiTinh, sinhVien.NgaySinh, sinhVien.MaLop, sinhVien.DiaChi,
                sinhVien.Email, sinhVien.DienThoai, sinhVien.MaSoSinhVien, sinhVien.StudentId);
            try
            {
                return Convert.ToInt64(ma_sinh_vien);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public void Update(long ma_sinh_vien, string? ho_va_ten_lot, string? ten_sinh_vien, int? gioi_tinh,
            DateTime? ngay_sinh, int? ma_lop, string? dia_chi, string? email, string? dien_thoai, string? ma_so_sinh_vien)
        {
            if (!_sinhVienRepository.Update(ma_sinh_vien, ho_va_ten_lot, ten_sinh_vien, gioi_tinh,
            ngay_sinh, ma_lop, dia_chi, email, dien_thoai, ma_so_sinh_vien))
            {
                throw new Exception("Can not update sinh vien");
            }
        }
    }
}
