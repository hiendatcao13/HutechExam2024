using System.Data;

namespace Hutech.Exam.Server.DAL.Repositories
{
    public interface IChiTietCaThiRepository
    {
        public Task<IDataReader> SelectOne(int chi_tiet_ca_thi);
        public Task<IDataReader> SelectBy_ma_sinh_vien(long ma_sinh_vien);
        public Task<IDataReader> SelectBy_ma_ca_thi(int ma_ca_thi);
        public Task<IDataReader> SelectBy_MaCaThi_MaSinhVien(int ma_ca_thi, long ma_sinh_vien);
        public Task<IDataReader> SelectBy_MaSinhVienThi(long ma_sinh_vien);
        public Task<int> UpdateBatDau(int ma_chi_tiet_ca_thi, DateTime? thoi_gian_bat_dau);
        public Task<int> UpdateKetThuc(int ma_chi_tiet_ca_thi, DateTime? thoi_gian_ket_thuc, double diem, int? so_cau_dung, int? tong_so_cau);
        public Task<int> CongGio(int ma_chi_tiet_ca_thi, int gio_cong_them, DateTime? thoi_diem_cong, string? ly_do_cong);
        public Task<object?> Insert(int ma_ca_thi, long ma_sinh_vien, long ma_de_thi, int tong_so_cau);
        public Task<int> Remove(int ma_chi_tiet_ca_thi);
        public Task<int> Update(int ma_chi_tiet_ca_thi, int? ma_ca_thi, long? ma_sinh_vien, long? ma_de_thi, int? tong_so_cau);
        public Task<IDataReader> SelectBy_ma_ca_thi_MSSV(int ma_ca_thi);
    }
}
