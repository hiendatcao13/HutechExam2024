using Hutech.Exam.Server.DAL.DataReader;
using System.Data;

namespace Hutech.Exam.Server.DAL.Repositories
{
    public class ChiTietDotThiResposity : IChiTietDotThiResposity
    {
        public async Task<IDataReader> SelectBy_MaDotThi(int ma_dot_thi)
        {
            DatabaseReader sql = new("chi_tiet_dot_thi_SelectBy_ma_dot_thi");
            sql.SqlParams("@ma_dot_thi", SqlDbType.Int, ma_dot_thi);
            return await sql.ExecuteReaderAsync();
        }
        public async Task<IDataReader> SelectBy_MaDotThi_MaLopAo(int ma_dot_thi, int ma_lop_ao)
        {
            DatabaseReader sql = new("chi_tiet_dot_thi_SelectBy_MaDotThi_MaLopAo");
            sql.SqlParams("@ma_dot_thi", SqlDbType.Int, ma_dot_thi);
            sql.SqlParams("@ma_lop_ao", SqlDbType.Int, ma_lop_ao);
            return await sql.ExecuteReaderAsync();
        }
        public async Task<IDataReader> SelectBy_MaDotThi_MaLopAo_LanThi(int ma_dot_thi, int ma_lop_ao, string lan_thi)
        {
            DatabaseReader sql = new("chi_tiet_dot_thi_SelectBy_MaDotThi_MaLopAo_LanThi");
            sql.SqlParams("@ma_dot_thi", SqlDbType.Int, ma_dot_thi);
            sql.SqlParams("@ma_lop_ao", SqlDbType.Int, ma_lop_ao);
            sql.SqlParams("lan_thi", SqlDbType.NVarChar, lan_thi);
            return await sql.ExecuteReaderAsync();
        }
        public async Task<IDataReader> SelectOne(int ma_chi_tiet_dot_thi)
        {
            DatabaseReader sql = new("chi_tiet_dot_thi_SelectOne");
            sql.SqlParams("@ma_chi_tiet_dot_thi", SqlDbType.Int, ma_chi_tiet_dot_thi);
            return await sql.ExecuteReaderAsync();
        }
        public async Task<IDataReader> GetAll()
        {
            DatabaseReader sql = new("chi_tiet_dot_thi_GetAll");
            return await sql.ExecuteReaderAsync();
        }
        public async Task<object?> Insert(string ten_chi_tiet_dot_thi, int ma_lop_ao, int ma_dot_thi, string lan_thi)
        {
            DatabaseReader sql = new("chi_tiet_dot_thi_Insert");
            sql.SqlParams("@ten_chi_tiet_dot_thi", SqlDbType.NVarChar, ten_chi_tiet_dot_thi);
            sql.SqlParams("@ma_lop_ao", SqlDbType.Int, ma_lop_ao);
            sql.SqlParams("@ma_dot_thi", SqlDbType.Int, ma_dot_thi);
            sql.SqlParams("@lan_thi", SqlDbType.NVarChar, lan_thi);
            return await sql.ExecuteScalarAsync();
        }
        public async Task<int> Update(int ma_chi_tiet_dot_thi, string ten_chi_tiet_dot_thi, int ma_lop_ao, int ma_dot_thi, string lan_thi)
        {
            DatabaseReader sql = new("chi_tiet_dot_thi_Update");
            sql.SqlParams("@ma_chi_tiet_dot_thi", SqlDbType.Int, ma_chi_tiet_dot_thi);
            sql.SqlParams("@ten_chi_tiet_dot_thi", SqlDbType.NVarChar, ten_chi_tiet_dot_thi);
            sql.SqlParams("@ma_lop_ao", SqlDbType.Int, ma_lop_ao);
            sql.SqlParams("@ma_dot_thi", SqlDbType.Int, ma_dot_thi);
            sql.SqlParams("@lan_thi", SqlDbType.NVarChar, lan_thi);
            return await sql.ExecuteNonQueryAsync();
        }
        public async Task<int> Remove(int ma_chi_tiet_dot_thi)
        {
            DatabaseReader sql = new("chi_tiet_dot_thi_Remove");
            sql.SqlParams("@ma_chi_tiet_dot_thi", SqlDbType.Int, ma_chi_tiet_dot_thi);
            return await sql.ExecuteNonQueryAsync();
        }
    }
}
