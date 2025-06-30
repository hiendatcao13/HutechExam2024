using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.DTO.Page;
using Hutech.Exam.Shared.Models;
using System.Data;

namespace Hutech.Exam.Server.DAL.Repositories
{
    public interface ISinhVienRepository
    {
        SinhVienDto GetProperty(IDataReader dataReader, int start = 0);

        Task<long> Insert(string? ho_va_ten_lot, string? ten_sinh_vien, int? gioi_tinh, DateTime? ngay_sinh, int? ma_lop,
           string? dia_chi, string? email, string? dien_thoai, string? ma_so_sinh_vien, Guid? student_id);

        Task Insert_Batch(List<SinhVienDto> sinhVienDtos);

        Task<bool> Update(long ma_sinh_vien, string? ho_va_ten_lot, string? ten_sinh_vien, int? gioi_tinh,
           DateTime? ngay_sinh, int? ma_lop, string? dia_chi, string? email, string? dien_thoai, string? ma_so_sinh_vien);

        Task<bool> Remove(long ma_sinh_vien);

        Task<bool> ForceRemove(long ma_sinh_vien);

        // lấy thông tin của 1 sinh viên từ mã sinh viên
        Task<SinhVienDto> SelectOne(long ma_sinh_vien);

        // lấy mã sinh viên từ mã số sinh viên hoặc sử dụng để check xem SV có tồn tại hay không
        Task<SinhVienDto> SelectBy_ma_so_sinh_vien(string ma_so_sinh_vien);

        // lấy hết thông tin của tất cả sinh viên
        Task<List<SinhVienDto>> GetAll();

        // cập nhật thông tin sinh viên vào hệ thống gần đây nhất
        Task<bool> Login(long ma_sinh_vien, DateTime last_log_in);

        Task<bool> Logout(long ma_sinh_vien, DateTime last_log_out);

        Task<Paged<SinhVienDto>> SelectBy_ma_lop_Search_Paged(int ma_lop, string keyword, int pageNumber, int pageSize);

        Task<Paged<SinhVienDto>> SelectBy_ma_lop_Paged(int ma_lop, int pageNumber, int pageSize);

        Task<long> LoginCount();


    }
}
