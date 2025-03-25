using Hutech.Exam.Server.DAL.DataReader;
using System.Data;

namespace Hutech.Exam.Server.DAL.Repositories
{
    public class LopAoRepository : ILopAoRepository
    {
        public async Task<IDataReader> SelectOne(int ma_lop_ao)
        {
            DatabaseReader sql = new("lop_ao_SelectOne");
            sql.SqlParams("@ma_lop_ao", SqlDbType.Int, ma_lop_ao);
            return await sql.ExecuteReaderAsync();
        }
        public async Task<IDataReader> SelectBy_ma_mon_hoc(int ma_mon_hoc)
        {
            DatabaseReader sql = new("lop_ao_SelectBy_ma_mon_hoc");
            sql.SqlParams("@ma_mon_hoc", SqlDbType.Int, ma_mon_hoc);
            return await sql.ExecuteReaderAsync();
        }
        public async Task<IDataReader> GetAll()
        {
            DatabaseReader sql = new("lop_ao_GetAll");
            return await sql.ExecuteReaderAsync();
        }
        public async Task<object?> Insert(string ten_lop_ao, DateTime ngay_bat_dau, int ma_mon_hoc)
        {
            DatabaseReader sql = new("lop_ao_Insert");
            sql.SqlParams("@ten_lop_ao", SqlDbType.NVarChar, ten_lop_ao);
            sql.SqlParams("@ngay_bat_dau", SqlDbType.DateTime, ngay_bat_dau);
            sql.SqlParams("@ma_mon_hoc", SqlDbType.Int, ma_mon_hoc);
            return await sql.ExecuteScalarAsync();
        }
        public async Task<int> Update(int ma_lop_ao, string ten_lop_ao, DateTime ngay_bat_dau, int ma_mon_hoc)
        {
            DatabaseReader sql = new("lop_ao_Update");
            sql.SqlParams("@ma_lop_ao", SqlDbType.Int, ma_lop_ao);
            sql.SqlParams("@ten_lop_ao", SqlDbType.NVarChar, ten_lop_ao);
            sql.SqlParams("@ngay_bat_dau", SqlDbType.DateTime, ngay_bat_dau);
            sql.SqlParams("@ma_mon_hoc", SqlDbType.Int, ma_mon_hoc);
            return await sql.ExecuteNonQueryAsync();
        }
        public async Task<int> Remove(int ma_lop_ao)
        {
            DatabaseReader sql = new("lop_ao_Remove");
            sql.SqlParams("@ma_lop_ao", SqlDbType.Int, ma_lop_ao);
            return await sql.ExecuteNonQueryAsync();
        }
    }
}
   