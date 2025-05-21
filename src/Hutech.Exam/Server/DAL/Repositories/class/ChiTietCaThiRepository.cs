using Hutech.Exam.Server.DAL.DataReader;
using System.Data;

namespace Hutech.Exam.Server.DAL.Repositories
{
    public class ChiTietCaThiRepository : IChiTietCaThiRepository
    {
        public async Task<IDataReader> SelectOne(int chi_tiet_ca_thi)
        {
            DatabaseReader sql = new("chi_tiet_ca_thi_SelectOne");
            sql.SqlParams("@ma_chi_tiet_ca_thi", SqlDbType.Int, chi_tiet_ca_thi);
            return await sql.ExecuteReaderAsync();
        }
        public async Task<IDataReader> SelectBy_ma_sinh_vien(long ma_sinh_vien)
        {
            DatabaseReader sql = new("chi_tiet_ca_thi_SelectBy_ma_sinh_vien");
            sql.SqlParams("@ma_sinh_vien", SqlDbType.BigInt, ma_sinh_vien);
            return await sql.ExecuteReaderAsync();
        }
        public async Task<IDataReader> SelectBy_ma_ca_thi(int ma_ca_thi)
        {
            DatabaseReader sql = new("chi_tiet_ca_thi_SelectBy_ma_ca_thi");
            sql.SqlParams("@ma_ca_thi", SqlDbType.Int, ma_ca_thi);
            return await sql.ExecuteReaderAsync();
        }
        public async Task<IDataReader> SelectBy_ma_ca_thi_Search_Paged(int ma_ca_thi, string keyword, int pageNumber, int pageSize)
        {
            DatabaseReader sql = new("chi_tiet_ca_thi_SelectBy_ma_ca_thi_Search_Paged");
            sql.SqlParams("@ma_ca_thi", SqlDbType.Int, ma_ca_thi);
            sql.SqlParams("@Keyword", SqlDbType.NVarChar, keyword);
            sql.SqlParams("@PageNumber", SqlDbType.Int, pageNumber);
            sql.SqlParams("@PageSize", SqlDbType.Int, pageSize);
            return await sql.ExecuteReaderAsync();
        }
        public async Task<IDataReader> SelectBy_ma_ca_thi_Paged(int ma_ca_thi, int pageNumber, int pageSize)
        {
            DatabaseReader sql = new("chi_tiet_ca_thi_SelectBy_ma_ca_thi_Paged");
            sql.SqlParams("@ma_ca_thi", SqlDbType.Int, ma_ca_thi);
            sql.SqlParams("@PageNumber", SqlDbType.Int, pageNumber);
            sql.SqlParams("@PageSize", SqlDbType.Int, pageSize);
            return await sql.ExecuteReaderAsync();
        }
        public async Task<IDataReader> SelectBy_MaCaThi_MaSinhVien(int ma_ca_thi, long ma_sinh_vien)
        {
            DatabaseReader sql = new("chi_tiet_ca_thi_SelectBy_MaCaThi_MaSinhVien");
            sql.SqlParams("@ma_ca_thi", SqlDbType.Int, ma_ca_thi);
            sql.SqlParams("@ma_sinh_vien", SqlDbType.BigInt, ma_sinh_vien);
            return await sql.ExecuteReaderAsync();
        }
        public async Task<IDataReader> SelectBy_MaSinhVienThi(long ma_sinh_vien)
        {
            DatabaseReader sql = new("chi_tiet_ca_thi_SelectBy_MaSinhVienThi");
            sql.SqlParams("@ma_sinh_vien", SqlDbType.BigInt, ma_sinh_vien);
            return await sql.ExecuteReaderAsync();
        }
        public async Task<int> UpdateBatDau(int ma_chi_tiet_ca_thi, DateTime? thoi_gian_bat_dau)
        {
            DatabaseReader sql = new("chi_tiet_ca_thi_UpdateBatDau");
            sql.SqlParams("@ma_chi_tiet_ca_thi", SqlDbType.Int, ma_chi_tiet_ca_thi);
            sql.SqlParams("@thoi_gian_bat_dau", SqlDbType.DateTime, thoi_gian_bat_dau ?? (Object)DBNull.Value);
            return await sql.ExecuteNonQueryAsync();
        }
        public async Task<int> UpdateKetThuc(int ma_chi_tiet_ca_thi, DateTime? thoi_gian_ket_thuc, double diem, int? so_cau_dung, int? tong_so_cau)
        {
            DatabaseReader sql = new("chi_tiet_ca_thi_UpdateKetThuc");
            sql.SqlParams("@ma_chi_tiet_ca_thi", SqlDbType.Int, ma_chi_tiet_ca_thi);
            sql.SqlParams("@thoi_gian_ket_thuc", SqlDbType.DateTime, thoi_gian_ket_thuc ?? (Object)DBNull.Value);
            sql.SqlParams("@diem", SqlDbType.Float, diem);
            sql.SqlParams("@so_cau_dung", SqlDbType.Int, so_cau_dung ?? (Object)DBNull.Value);
            sql.SqlParams("@tong_so_cau", SqlDbType.Int, tong_so_cau ?? (Object)DBNull.Value);
            return await sql.ExecuteNonQueryAsync();
        }

        public async Task<int> CongGio(int ma_chi_tiet_ca_thi, int gio_cong_them, DateTime? thoi_diem_cong, string? ly_do_cong)
        {
            DatabaseReader sql = new("chi_tiet_ca_thi_CongGio");
            sql.SqlParams("@ma_chi_tiet_ca_thi", SqlDbType.Int, ma_chi_tiet_ca_thi);
            sql.SqlParams("@gio_cong_them", SqlDbType.Int, gio_cong_them);
            sql.SqlParams("@thoi_diem_cong", SqlDbType.DateTime, thoi_diem_cong ?? (Object)DBNull.Value);
            sql.SqlParams("@ly_do_cong", SqlDbType.NVarChar, ly_do_cong ?? (Object)DBNull.Value);
            return await sql.ExecuteNonQueryAsync();
        }
        public async Task<object?> Insert(int ma_ca_thi, long ma_sinh_vien, long ma_de_thi, int tong_so_cau)
        {
            DatabaseReader sql = new DatabaseReader("chi_tiet_ca_thi_Insert");
            sql.SqlParams("@ma_ca_thi", SqlDbType.Int, ma_ca_thi);
            sql.SqlParams("@ma_sinh_vien", SqlDbType.BigInt, ma_sinh_vien);
            sql.SqlParams("@ma_de_thi", SqlDbType.BigInt, ma_de_thi);
            sql.SqlParams("@tong_so_cau", SqlDbType.Int, tong_so_cau);
            return await sql.ExecuteScalarAsync();
        }
        public async Task<int> Remove(int ma_chi_tiet_ca_thi)
        {
            DatabaseReader sql = new("chi_tiet_ca_thi_Remove");
            sql.SqlParams("@ma_chi_tiet_ca_thi", SqlDbType.Int, ma_chi_tiet_ca_thi);
            return await sql.ExecuteNonQueryAsync();
        }
        public async Task<int> Update(int ma_chi_tiet_ca_thi, int? ma_ca_thi, long? ma_sinh_vien, long? ma_de_thi, int? tong_so_cau)
        {
            DatabaseReader sql = new("chi_tiet_ca_thi_Update");
            sql.SqlParams("@ma_chi_tiet_ca_thi", SqlDbType.Int, ma_chi_tiet_ca_thi);
            sql.SqlParams("@ma_ca_thi", SqlDbType.Int, ma_ca_thi ?? (Object)DBNull.Value);
            sql.SqlParams("@ma_sinh_vien", SqlDbType.BigInt, ma_sinh_vien ?? (Object)DBNull.Value);
            sql.SqlParams("@ma_de_thi", SqlDbType.BigInt, ma_de_thi ?? (Object)DBNull.Value);
            sql.SqlParams("@tong_so_cau", SqlDbType.Int, tong_so_cau ?? (Object)DBNull.Value);
            return await sql.ExecuteNonQueryAsync();
        }
        public async Task<IDataReader> SelectBy_ma_ca_thi_MSSV(int ma_ca_thi)
        {
            DatabaseReader sql = new("chi_tiet_ca_thi_SelectBy_ma_ca_thi_MSSV");
            sql.SqlParams("@ma_ca_thi", SqlDbType.Int, ma_ca_thi);
            return await sql.ExecuteReaderAsync();
        }
    }
}
