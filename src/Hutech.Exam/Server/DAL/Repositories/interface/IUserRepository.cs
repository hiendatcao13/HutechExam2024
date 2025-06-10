using Hutech.Exam.Server.DAL.DataReader;
using Hutech.Exam.Shared.DTO;
using System.Data;

namespace Hutech.Exam.Server.DAL.Repositories
{
    public interface IUserRepository
    {
        UserDto GetProperty(IDataReader dataReader, int start = 0);

        public Task<UserDto> SelectOne(Guid userId);

        public Task<UserDto> SelectByLoginName(string loginName);

        public Task<List<string>> Login(string loginName);

        public Task<bool> LoginSuccess(Guid userId);

        public Task<bool> LoginFail(Guid userId);

        public Task<bool> UpdateLastActivity(Guid userId, DateTime lastActivityDate);

        public Task<bool> Update(Guid userId, string? loginName, string? username, string? email, string? name, bool? isDeleted, bool? isLockedOut,
            DateTime? lastActivityDate, DateTime? lastLoginDate, DateTime? lastLockedOutDate, int? failedPwdAttemptCount,
            DateTime? failedPwdAttemptWindowStart, string? comment);
    }
}
