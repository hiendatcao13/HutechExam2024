using Hutech.Exam.Shared.Models;
using System.Data;

namespace Hutech.Exam.Server.DAL.Repositories
{
    public interface ISinhVienRepository
    {
        public Task<object?> Insert(string? ho_va_ten_lot, string? ten_sinh_vien, int? gioi_tinh, DateTime? ngay_sinh, int? ma_lop,
            string? dia_chi, string? email, string? dien_thoai, string? ma_so_sinh_vien, Guid? student_id);
        public Task<int> Update(long ma_sinh_vien, string? ho_va_ten_lot, string? ten_sinh_vien, int? gioi_tinh,
            DateTime? ngay_sinh, int? ma_lop, string? dia_chi, string? email, string? dien_thoai, string? ma_so_sinh_vien);
        public Task<int> Remove(long ma_sinh_vien);
        // lấy thông tin của 1 sinh viên từ mã sinh viên
        public Task<IDataReader> SelectOne(long ma_sinh_vien);
        // lấy mã sinh viên từ mã số sinh viên hoặc sử dụng để check xem SV có tồn tại hay không
        public Task<IDataReader> SelectBy_ma_so_sinh_vien(string ma_so_sinh_vien);
        // lấy hết thông tin của tất cả sinh viên
        public Task<IDataReader> GetAll();
        // cập nhật thông tin sinh viên vào hệ thống gần đây nhất
        public Task<int> Login(long ma_sinh_vien, DateTime last_log_in);
        public Task<int> Logout(long ma_sinh_vien, DateTime last_log_out);
    }
}
