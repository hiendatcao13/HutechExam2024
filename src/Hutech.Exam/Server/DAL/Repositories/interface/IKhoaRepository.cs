using System.Data;

namespace Hutech.Exam.Server.DAL.Repositories
{
    public interface IKhoaRepository
    {
        public IDataReader GetAll();
    }
}
