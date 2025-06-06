using Hutech.Exam.Server.DAL.DataReader;
using System.Data;

namespace Hutech.Exam.Server.DAL.Repositories
{
    public class CustomRepository : ICustomRepository
    {
        public async Task<IDataReader> GetDeThi(long ma_de_hoan_vi)
        {
            DatabaseReader sql = new("Custom_GetDeThi");
            sql.SqlParams("@MaDeThiHoanVi", SqlDbType.BigInt, ma_de_hoan_vi);
            return await sql.ExecuteReaderAsync();
        }

        public async Task<IDataReader> LayMaThongTinDeThi(long ma_de_thi)
        {
            DatabaseReader sql = new("Custom_LayMaThongTinDeThi");
            sql.SqlParams("@MaDeThi", SqlDbType.BigInt, ma_de_thi);
            return await sql.ExecuteReaderAsync();
        }
    }
}
