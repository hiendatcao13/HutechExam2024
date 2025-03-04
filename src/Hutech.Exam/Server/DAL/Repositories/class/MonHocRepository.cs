using Hutech.Exam.Server.DAL.DataReader;
using System.Data;

namespace Hutech.Exam.Server.DAL.Repositories
{
    public class MonHocRepository : IMonHocRepository
    {
        public async Task<IDataReader> SelectOne(int ma_mon_hoc)
        {
            DatabaseReader sql = new DatabaseReader("mon_hoc_SelectOne");
            sql.SqlParams("@ma_mon_hoc", SqlDbType.Int, ma_mon_hoc);
            return await sql.ExecuteReaderAsync();
        }
        public async Task<IDataReader> GetAll()
        {
            DatabaseReader sql = new DatabaseReader("mon_hoc_GetAll");
            return await sql.ExecuteReaderAsync();
        }
        public async Task<object?> Insert(string ma_so_mon_hoc, string ten_mon_hoc)
        {
            DatabaseReader sql = new DatabaseReader("mon_hoc_Insert");
            sql.SqlParams("@ma_so_mon_hoc", SqlDbType.NVarChar, ma_so_mon_hoc);
            sql.SqlParams("@ten_mon_hoc", SqlDbType.NVarChar, ten_mon_hoc);
            return await sql.ExecuteScalarAsync();
        }
        public async Task<int> Update(int ma_mon_hoc, string ma_so_mon_hoc, string ten_mon_hoc)
        {
            DatabaseReader sql = new DatabaseReader("mon_hoc_Update");
            sql.SqlParams("@ma_mon_hoc", SqlDbType.Int, ma_mon_hoc);
            sql.SqlParams("@ma_so_mon_hoc", SqlDbType.NVarChar, ma_so_mon_hoc);
            sql.SqlParams("@ten_mon_hoc", SqlDbType.NVarChar, ten_mon_hoc);
            return await sql.ExecuteNonQueryAsync();
        }
        public async Task<int> Remove(int ma_mon_hoc)
        {
            DatabaseReader sql = new DatabaseReader("mon_hoc_Remove");
            sql.SqlParams("@ma_mon_hoc", SqlDbType.Int, ma_mon_hoc);
            return await sql.ExecuteNonQueryAsync();
        }
    }
}
