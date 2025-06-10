using AutoMapper;
using Hutech.Exam.Server.DAL.Repositories;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.DTO.Page;
using Hutech.Exam.Shared.DTO.Request.SinhVien;

namespace Hutech.Exam.Server.BUS
{
    public class SinhVienService(ISinhVienRepository sinhVienRepository)
    {
        private readonly ISinhVienRepository _sinhVienRepository = sinhVienRepository;

        public async Task<List<SinhVienDto>> GetAll()
        {
            return await _sinhVienRepository.GetAll();
        }

        public async Task<SinhVienDto> SelectBy_ma_so_sinh_vien(string ma_so_sinh_vien)
        {
            return await _sinhVienRepository.SelectBy_ma_so_sinh_vien(ma_so_sinh_vien);
        }

        public async Task<bool> Login(long ma_sinh_vien, DateTime last_log_in)
        {
            return await _sinhVienRepository.Login(ma_sinh_vien, last_log_in);
        }

        public async Task<bool> Logout(long ma_sinh_vien, DateTime last_log_out)
        {
            return await _sinhVienRepository.Logout(ma_sinh_vien, last_log_out);
            
        }
        //lấy thông tin của 1 sinh viên từ mã số sinh viên
        public async Task<SinhVienDto> SelectOne(long ma_sinh_vien)
        {
            return await _sinhVienRepository.SelectOne(ma_sinh_vien);
        }

        public async Task<long> Insert(SinhVienCreateRequest sinhVien)
        {
            return await _sinhVienRepository.Insert(sinhVien.HoVaTenLot, sinhVien.TenSinhVien, sinhVien.GioiTinh, sinhVien.NgaySinh, sinhVien.MaLop, sinhVien.DiaChi,
                sinhVien.Email, sinhVien.DienThoai, sinhVien.MaSoSinhVien, sinhVien.StudentId);
        }

        public async Task<bool> Update(long id, SinhVienUpdateRequest sinhVien)
        {
            return await _sinhVienRepository.Update(id, sinhVien.HoVaTenLot, sinhVien.TenSinhVien, sinhVien.GioiTinh,
            sinhVien.NgaySinh, sinhVien.MaLop, sinhVien.DiaChi, sinhVien.Email, sinhVien.DienThoai, sinhVien.MaSoSinhVien);
        }

        public async Task<bool> Remove(long ma_sinh_vien)
        {
            return await _sinhVienRepository.Remove(ma_sinh_vien);
        }

        public async Task<bool> ForceRemove(long ma_sinh_vien)
        {
            return await _sinhVienRepository.ForceRemove(ma_sinh_vien);
        }

        public async Task<Paged<SinhVienDto>> SelectBy_ma_lop_Paged(int ma_lop, int pageNumber, int pageSize)
        {
            return await _sinhVienRepository.SelectBy_ma_lop_Paged(ma_lop, pageNumber, pageSize);
        }

        public async Task<Paged<SinhVienDto>> SelectBy_ma_lop_Search_Paged(int ma_lop, string keyword, int pageNumber, int pageSize)
        {
            return await _sinhVienRepository.SelectBy_ma_lop_Search_Paged(ma_lop, keyword, pageNumber, pageSize);
        }
    }
}
