using Hutech.Exam.Server.DAL.DataReader;
using System.Data;

namespace Hutech.Exam.Server.DAL.Repositories
{
    public class DeThiRepository : IDeThiRepository
    {
        public async Task<IDataReader> SelectOne(int ma_de_thi)
        {
            DatabaseReader sql = new DatabaseReader("tbl_DeThi_SelectOne");
            sql.SqlParams("@MaDeThi", SqlDbType.Int, ma_de_thi);
            return await sql.ExecuteReader();
        }
        public async Task<IDataReader> SelectBy_ma_de_hv(long ma_de_hv)
        {
            DatabaseReader sql = new DatabaseReader("tbl_DeThi_SelectBy_ma_de_hv");
            sql.SqlParams("@MaDeHV", SqlDbType.BigInt, ma_de_hv);
            return await sql.ExecuteReader();
        }
    }
}
