using Hutech.Exam.Server.DAL.DataReader;
using System.Data;

namespace Hutech.Exam.Server.DAL.Repositories
{
    public class DotThiRepository : IDotThiRepository
    {
        public async Task<IDataReader> GetAll()
        {
            DatabaseReader sql = new DatabaseReader("dot_thi_GetAll");
            return await sql.ExecuteReader();
        }
        public async Task<IDataReader> SelectOne(int ma_dot_thi)
        {
            DatabaseReader sql = new DatabaseReader("dot_thi_SelectOne");
            sql.SqlParams("@ma_dot_thi", SqlDbType.Int, ma_dot_thi);
            return await sql.ExecuteReader();
        }
    }
}
