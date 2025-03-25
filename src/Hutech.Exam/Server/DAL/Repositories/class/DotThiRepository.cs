using Hutech.Exam.Server.DAL.DataReader;
using System.Data;

namespace Hutech.Exam.Server.DAL.Repositories
{
    public class DotThiRepository : IDotThiRepository
    {
        public async Task<IDataReader> GetAll()
        {
            DatabaseReader sql = new("dot_thi_GetAll");
            return await sql.ExecuteReaderAsync();
        }
        public async Task<IDataReader> SelectOne(int ma_dot_thi)
        {
            DatabaseReader sql = new("dot_thi_SelectOne");
            sql.SqlParams("@ma_dot_thi", SqlDbType.Int, ma_dot_thi);
            return await sql.ExecuteReaderAsync();
        }
        public async Task<object?> Insert(string ten_dot_thi, DateTime thoi_gian_bat_dau, DateTime thoi_gian_ket_thuc, int nam_hoc)
        {
            DatabaseReader sql = new("dot_thi_Insert");
            sql.SqlParams("@ten_dot_thi", SqlDbType.NVarChar, ten_dot_thi);
            sql.SqlParams("@thoi_gian_bat_dau", SqlDbType.DateTime, thoi_gian_bat_dau);
            sql.SqlParams("@thoi_gian_ket_thuc", SqlDbType.DateTime, thoi_gian_ket_thuc);
            sql.SqlParams("@NamHoc", SqlDbType.Int, nam_hoc);
            return await sql.ExecuteScalarAsync();
        }
        public async Task<int> Update(int ma_dot_thi, string ten_dot_thi, DateTime thoi_gian_bat_dau, DateTime thoi_gian_ket_thuc, int nam_hoc)
        {
            DatabaseReader sql = new("dot_thi_Update");
            sql.SqlParams("@ma_dot_thi", SqlDbType.Int, ma_dot_thi);
            sql.SqlParams("@ten_dot_thi", SqlDbType.NVarChar, ten_dot_thi);
            sql.SqlParams("@thoi_gian_bat_dau", SqlDbType.DateTime, thoi_gian_bat_dau);
            sql.SqlParams("@thoi_gian_ket_thuc", SqlDbType.DateTime, thoi_gian_ket_thuc);
            sql.SqlParams("@NamHoc", SqlDbType.Int, nam_hoc);
            return await sql.ExecuteNonQueryAsync();
        }
        public async Task<int> Remove(int ma_dot_thi)
        {
            DatabaseReader sql = new("dot_thi_Remove");
            sql.SqlParams("@ma_dot_thi", SqlDbType.Int, ma_dot_thi);
            return await sql.ExecuteNonQueryAsync();
        }
    }
}
