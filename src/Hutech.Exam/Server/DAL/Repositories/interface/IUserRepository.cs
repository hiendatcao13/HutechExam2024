using Hutech.Exam.Server.DAL.DataReader;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.DTO.Page;
using Hutech.Exam.Shared.DTO.Request.User;
using System.Data;

namespace Hutech.Exam.Server.DAL.Repositories
{
    public interface IUserRepository
    {
        UserDto GetProperty(IDataReader dataReader, int start = 0);

        Task<Paged<UserDto>> GetAll_Paged(int pageNumber, int pageSize);

        Task<Paged<UserDto>> GetAll_Search_Paged(string keyword, int pageNumber, int pageSize);

        Task<Guid> Insert(UserCreateRequest user);

        Task<UserDto> SelectOne(Guid userId);

        Task<UserDto> SelectByLoginName(string loginName);

        Task<List<string>> Login(string loginName);

        Task<bool> LoginSuccess(Guid userId);

        Task<bool> LoginFail(Guid userId);

        Task<bool> UpdateLastActivity(Guid userId, DateTime lastActivityDate);

        Task<bool> UpdatePassword(Guid id, UserUpdatePasswordRequest user);

        Task<bool> Update(Guid id, UserUpdateRequest user);

        Task<bool> Delete(Guid id);

        Task<bool> CheckExistName(string loginName, string email);
    }
}
