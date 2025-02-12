using Hutech.Exam.Server.DAL.DataReader;
using System.Data;

namespace Hutech.Exam.Server.DAL.Repositories
{
    public class LopAoRepository : ILopAoRepository
    {
        public async Task<IDataReader> SelectOne(int ma_lop_ao)
        {
            DatabaseReader sql = new DatabaseReader("lop_ao_SelectOne");
            sql.SqlParams("@ma_lop_ao", SqlDbType.Int, ma_lop_ao);
            return await sql.ExecuteReader();
        }
        public async Task<IDataReader> SelectBy_ma_mon_hoc(int ma_mon_hoc)
        {
            DatabaseReader sql = new DatabaseReader("lop_ao_SelectBy_ma_mon_hoc");
            sql.SqlParams("@ma_mon_hoc", SqlDbType.Int, ma_mon_hoc);
            return await sql.ExecuteReader();
        }
    }
}
   