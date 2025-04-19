using Hutech.Exam.Server.DAL.DataReader;
using System.Data;

namespace Hutech.Exam.Server.DAL.Repositories
{
    public class CauTraLoiRepository : ICauTraLoiRepository
    {
        public async Task<IDataReader> SelectOne(int ma_cau_tra_loi)
        {
            DatabaseReader sql = new("CauHoi_SelectOne");
            sql.SqlParams("@MaCauTraLoi", SqlDbType.Int, ma_cau_tra_loi);
            return await sql.ExecuteReaderAsync();
        }
        public async Task<IDataReader> SelectBy_MaCauHoi(int ma_cau_hoi)
        {
            DatabaseReader sql = new("CauTraLoi_SelectBy_MaCauHoi");
            sql.SqlParams("@MaCauHoi", SqlDbType.Int, ma_cau_hoi);
            return await sql.ExecuteReaderAsync();
        }
    }
}
