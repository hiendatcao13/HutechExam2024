using Hutech.Exam.Server.DAL.DataReader;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.Data;

namespace Hutech.Exam.Server.DAL.Repositories
{
    public class CaThiRepository : ICaThiRepository
    {
        public async Task<IDataReader> SelectBy_ma_chi_tiet_dot_thi(int ma_chi_tiet_dot_thi)
        {
            DatabaseReader sql = new("ca_thi_SelectBy_ma_chi_tiet_dot_thi_Paged");
            sql.SqlParams("@ma_chi_tiet_dot_thi", SqlDbType.Int, ma_chi_tiet_dot_thi);
            return await sql.ExecuteReaderAsync();
        }
        public async Task<IDataReader> SelectBy_ma_chi_tiet_dot_thi_Paged(int ma_chi_tiet_dot_thi, int pageNumber, int pageSize)
        {
            DatabaseReader sql = new("ca_thi_SelectBy_ma_chi_tiet_dot_thi_Paged");
            sql.SqlParams("@ma_chi_tiet_dot_thi", SqlDbType.Int, ma_chi_tiet_dot_thi);
            sql.SqlParams("@PageNumber", SqlDbType.Int, pageNumber);
            sql.SqlParams("@PageSize", SqlDbType.Int, pageSize);
            return await sql.ExecuteReaderAsync();
        }

        public async Task<IDataReader> SelectBy_ma_chi_tiet_dot_thi_Search_Paged(int ma_chi_tiet_dot_thi, string keyword, int pageNumber, int pageSize)
        {
            DatabaseReader sql = new("ca_thi_SelectBy_ma_chi_tiet_dot_thi_Search_Paged");
            sql.SqlParams("@ma_chi_tiet_dot_thi", SqlDbType.Int, ma_chi_tiet_dot_thi);
            sql.SqlParams("@Keyword", SqlDbType.NVarChar, keyword);
            sql.SqlParams("@PageNumber", SqlDbType.Int, pageNumber);
            sql.SqlParams("@PageSize", SqlDbType.Int, pageSize);
            return await sql.ExecuteReaderAsync();
        }

        public async Task<IDataReader> SelectOne(int ma_ca_thi)
        {
            DatabaseReader sql = new("ca_thi_SelectOne");
            sql.SqlParams("@ma_ca_thi", SqlDbType.Int, ma_ca_thi);
            return await sql.ExecuteReaderAsync();
        }

        public async Task<IDataReader> GetAll()
        {
            DatabaseReader sql = new("ca_thi_GetAll");
            return await sql.ExecuteReaderAsync();
        }

        public async Task<int> Activate(int ma_ca_thi, bool IsActivated)
        {
            DatabaseReader sql = new("ca_thi_Activate");
            sql.SqlParams("@ma_ca_thi", SqlDbType.Int, ma_ca_thi);
            sql.SqlParams("@IsActivated", SqlDbType.Bit, IsActivated);
            return await sql.ExecuteNonQueryAsync();
        }

        public async Task<int> HuyKichHoat(int ma_ca_thi)
        {
            DatabaseReader sql = new("ca_thi_HuyKichHoat");
            sql.SqlParams("@ma_ca_thi", SqlDbType.Int, ma_ca_thi);
            return await sql.ExecuteNonQueryAsync();
        }
        public async Task<int> Ketthuc(int ma_ca_thi)
        {
            DatabaseReader sql = new("ca_thi_Ketthuc");
            sql.SqlParams("@ma_ca_thi", SqlDbType.Int, ma_ca_thi);
            return await sql.ExecuteNonQueryAsync();
        }
        public async Task<object?> Insert(string ten_ca_thi, int ma_chi_tiet_dot_thi, DateTime thoi_gian_bat_dau, int ma_de_thi, int thoi_gian_thi)
        {
            DatabaseReader sql = new("ca_thi_Insert");
            sql.SqlParams("@ten_ca_thi", SqlDbType.NVarChar, ten_ca_thi);
            sql.SqlParams("@ma_chi_tiet_dot_thi", SqlDbType.Int, ma_chi_tiet_dot_thi);
            sql.SqlParams("@thoi_gian_bat_dau", SqlDbType.DateTime, thoi_gian_bat_dau);
            sql.SqlParams("@MaDeThi", SqlDbType.Int, ma_de_thi);
            sql.SqlParams("@ThoiGianThi", SqlDbType.Int, thoi_gian_thi);
            return await sql.ExecuteScalarAsync();
        }
        public async Task<int> Remove(int ma_ca_thi)
        {
            DatabaseReader sql = new("ca_thi_Remove");
            sql.SqlParams("@ma_ca_thi", SqlDbType.Int, ma_ca_thi);
            return await sql.ExecuteNonQueryAsync();
        }
        public async Task<int> Update(int ma_ca_thi, string ten_ca_thi, int ma_chi_tiet_dot_thi, DateTime thoi_gian_bat_dau, int ma_de_thi, int thoi_gian_thi)
        {
            DatabaseReader sql = new("ca_thi_Update");
            sql.SqlParams("@ma_ca_thi", SqlDbType.Int, ma_ca_thi);
            sql.SqlParams("@ten_ca_thi", SqlDbType.NVarChar, ten_ca_thi);
            sql.SqlParams("@ma_chi_tiet_dot_thi", SqlDbType.Int, ma_chi_tiet_dot_thi);
            sql.SqlParams("@thoi_gian_bat_dau", SqlDbType.DateTime, thoi_gian_bat_dau);
            sql.SqlParams("@MaDeThi", SqlDbType.Int, ma_de_thi);
            sql.SqlParams("@ThoiGianThi", SqlDbType.Int, thoi_gian_thi);
            return await sql.ExecuteNonQueryAsync();
        }
        public async Task<IDataReader> SelectBy_MaDotThi_MaLop_LanThi(int ma_dot_thi, int ma_lop, int lan_thi)
        {
            DatabaseReader sql = new("ca_thi_SelectBy_MaDotThi_MaLop_LanThi");
            sql.SqlParams("@ma_dot_thi", SqlDbType.Int, ma_dot_thi);
            sql.SqlParams("@ma_lop", SqlDbType.Int, ma_lop);
            sql.SqlParams("@lan_thi", SqlDbType.Int, lan_thi);
            return await sql.ExecuteReaderAsync();
        }
    }
}
