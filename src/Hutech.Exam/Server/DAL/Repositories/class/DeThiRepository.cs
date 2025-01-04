using Hutech.Exam.Server.DAL.DataReader;
using System.Data;

namespace Hutech.Exam.Server.DAL.Repositories
{
    public class DeThiRepository : IDeThiRepository
    {
        public IDataReader SelectOne(int ma_de_thi)
        {
            DatabaseReader sql = new DatabaseReader("tbl_DeThi_SelectOne");
            sql.SqlParams("@MaDeThi", SqlDbType.Int, ma_de_thi);
            return sql.ExcuteReader();
        }
        public IDataReader SelectBy_ma_de_hv(long ma_de_hv)
        {
            DatabaseReader sql = new DatabaseReader("tbl_DeThi_SelectBy_ma_de_hv");
            sql.SqlParams("@MaDeHV", SqlDbType.BigInt, ma_de_hv);
            return sql.ExcuteReader();
        }
    }
}
