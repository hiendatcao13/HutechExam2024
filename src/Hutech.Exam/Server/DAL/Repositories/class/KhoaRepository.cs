using Hutech.Exam.Server.DAL.DataReader;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;

namespace Hutech.Exam.Server.DAL.Repositories
{
    public class KhoaRepository : IKhoaRepository
    {
        public async Task<IDataReader> GetAll()
        {
            DatabaseReader sql = new DatabaseReader("khoa_GetAll");
            return await sql.ExecuteReaderAsync();
        }
    }
}
