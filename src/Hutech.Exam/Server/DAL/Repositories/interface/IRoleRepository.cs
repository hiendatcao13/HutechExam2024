using Hutech.Exam.Shared.DTO;
using System.Data;

namespace Hutech.Exam.Server.DAL.Repositories
{
    public interface IRoleRepository
    {
        RoleDto GetProperty(IDataReader dataReader, int start = 0);
    }
}
