using System.Data;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.DTO.Page;
using Hutech.Exam.Shared.DTO.Request.ChiTietCaThi;

namespace Hutech.Exam.Server.DAL.Repositories
{
    public interface IChiTietCaThiRepository
    {
        ChiTietCaThiDto GetProperty(IDataReader dataReader, int start = 0);

        Task<ChiTietCaThiDto> SelectOne(int chi_tiet_ca_thi);

        Task<List<ChiTietCaThiDto>> SelectBy_ma_sinh_vien(long ma_sinh_vien);

        Task<List<ChiTietCaThiDto>> SelectBy_ma_ca_thi(int ma_ca_thi);

        Task<Paged<ChiTietCaThiDto>> SelectBy_ma_ca_thi_Search_Paged(int ma_ca_thi, string keyword, int pageNumber, int pageSize);

        Task<Paged<ChiTietCaThiDto>> SelectBy_ma_ca_thi_Paged(int ma_ca_thi, int pageNumber, int pageSize);

        Task<ChiTietCaThiDto> SelectBy_MaCaThi_MaSinhVien(int ma_ca_thi, long ma_sinh_vien);

        Task<ChiTietCaThiDto> SelectBy_MaSinhVienThi(long ma_sinh_vien);

        Task<bool> UpdateBatDau(int ma_chi_tiet_ca_thi, DateTime? thoi_gian_bat_dau);

        Task<bool> UpdateKetThuc(int ma_chi_tiet_ca_thi, DateTime? thoi_gian_ket_thuc, double diem, int? so_cau_dung, int? tong_so_cau);

        Task<bool> CongGio(int ma_chi_tiet_ca_thi, int gio_cong_them, DateTime? thoi_diem_cong, string? ly_do_cong);

        Task<int> Insert(int ma_ca_thi, long ma_sinh_vien, long ma_de_thi, int tong_so_cau);

        Task Insert_Batch(List<ChiTietCaThiCreateBatchRequest> chiTietCaThis);

        Task<int> ThemSVKhanCap(string ma_so_sinh_vien, int ma_ca_thi, long ma_de_thi);

        Task<bool> Remove(int ma_chi_tiet_ca_thi);

        Task<bool> Update(int ma_chi_tiet_ca_thi, int? ma_ca_thi, long? ma_sinh_vien, long? ma_de_thi, int? tong_so_cau);
    }
}
