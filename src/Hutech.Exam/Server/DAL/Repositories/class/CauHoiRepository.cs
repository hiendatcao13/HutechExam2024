using Hutech.Exam.Server.DAL.DataReader;
using System.Data;

namespace Hutech.Exam.Server.DAL.Repositories
{
    public class CauHoiRepository : ICauHoiRepository
    {
        public async Task<IDataReader> SelectOne(int ma_cau_hoi)
        {
            DatabaseReader sql = new("CauHoi_SelectOne");
            sql.SqlParams("@MaCauHoi", SqlDbType.Int, ma_cau_hoi);
            return await sql.ExecuteReaderAsync();
        }
        public async Task<IDataReader> SelectDapAn(int ma_cau_hoi)
        {
            DatabaseReader sql = new("CauHoi_SelectDapAn");
            sql.SqlParams("@MaCauHoi", SqlDbType.Int, ma_cau_hoi);
            return await sql.ExecuteReaderAsync();
        }
    }
}
