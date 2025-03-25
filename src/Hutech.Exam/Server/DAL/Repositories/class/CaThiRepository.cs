using Hutech.Exam.Server.DAL.DataReader;
using System.Data;

namespace Hutech.Exam.Server.DAL.Repositories
{
    public class CaThiRepository : ICaThiRepository
    {
        public async Task<IDataReader> SelectBy_ma_chi_tiet_dot_thi(int ma_chi_tiet_dot_thi)
        {
            DatabaseReader sql = new("ca_thi_SelectBy_ma_chi_tiet_dot_thi");
            sql.SqlParams("@ma_chi_tiet_dot_thi", SqlDbType.Int, ma_chi_tiet_dot_thi);
            return await sql.ExecuteReaderAsync();
        }
        public async Task<IDataReader> SelectOne(int ma_ca_thi)
        {
            DatabaseReader sql = new("ca_thi_SelectOne");
            sql.SqlParams("@ma_ca_thi", SqlDbType.Int, ma_ca_thi);
            return await sql.ExecuteReaderAsync();
        }
        public async Task<IDataReader> ca_thi_GetAll()
        {
            DatabaseReader sql = new("ca_thi_GetAll");
            return await sql.ExecuteReaderAsync();
        }
        public async Task<int> ca_thi_Activate(int ma_ca_thi, bool IsActivated)
        {
            DatabaseReader sql = new("ca_thi_Activate");
            sql.SqlParams("@ma_ca_thi", SqlDbType.Int, ma_ca_thi);
            sql.SqlParams("@IsActivated", SqlDbType.Bit, IsActivated);
            return await sql.ExecuteNonQueryAsync();
        }
        public async Task<int> ca_thi_Ketthuc(int ma_ca_thi)
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
    }
}
