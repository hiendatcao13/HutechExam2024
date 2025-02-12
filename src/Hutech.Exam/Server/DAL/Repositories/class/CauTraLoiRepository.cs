using Hutech.Exam.Server.DAL.DataReader;
using System.Data;

namespace Hutech.Exam.Server.DAL.Repositories
{
    public class CauTraLoiRepository : ICauTraLoiRepository
    {
        public async Task<IDataReader> SelectOne(int ma_cau_tra_loi)
        {
            DatabaseReader sql = new DatabaseReader("tbl_CauHoi_SelectOne");
            sql.SqlParams("@MaCauTraLoi", SqlDbType.Int, ma_cau_tra_loi);
            return await sql.ExecuteReader();
        }
        public async Task<IDataReader> SelectBy_MaCauHoi(int ma_cau_hoi)
        {
            DatabaseReader sql = new DatabaseReader("tbl_CauTraLoi_SelectBy_MaCauHoi");
            sql.SqlParams("@MaCauHoi", SqlDbType.Int, ma_cau_hoi);
            return await sql.ExecuteReader();
        }
    }
}
