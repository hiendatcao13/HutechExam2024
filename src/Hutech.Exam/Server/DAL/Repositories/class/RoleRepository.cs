using System.Data;
using AutoMapper;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.Models;

namespace Hutech.Exam.Server.DAL.Repositories
{
    public class RoleRepository(IMapper mapper) : IRoleRepository
    {
        private readonly IMapper _mapper = mapper;

        public static readonly int COLUMN_LENGTH = 3;

        public RoleDto GetProperty(IDataReader dataReader, int start = 0)
        {
            Role role = new()
            {
                MaRole = dataReader.GetInt32(0 + start),
                TenRole = dataReader.GetString(1 + start),
                MoTa = dataReader.GetString(2 + start)
            };

            return _mapper.Map<RoleDto>(role);
        }
    }
}
