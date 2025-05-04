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
    }
}
