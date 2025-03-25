using Hutech.Exam.Server.DAL.DataReader;
using System.Data;

namespace Hutech.Exam.Server.DAL.Repositories
{
    public class NhomCauHoiRepository : INhomCauHoiRepository
    {
        public async Task<IDataReader> SelectBy_MaDeThi(int ma_de_thi)
        {
            DatabaseReader sql = new("tbl_NhomCauHoi_SelectBy_MaDeThi");
            sql.SqlParams("@MaDeThi", SqlDbType.Int, ma_de_thi);
            return await sql.ExecuteReaderAsync();
        }
        public async Task<IDataReader> SelectOne(int ma_nhom)
        {
            DatabaseReader sql = new("tbl_NhomCauHoi_SelectOne");
            sql.SqlParams("@MaNhom", SqlDbType.Int, ma_nhom);
            return await sql.ExecuteReaderAsync();
        }
    }
}
