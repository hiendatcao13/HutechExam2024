using Hutech.Exam.Server.DAL.DataReader;
using System.Data;

namespace Hutech.Exam.Server.DAL.Repositories
{
    public interface IUserRepository
    {
        public Task<IDataReader> SelectOne(Guid userId);
        public Task<IDataReader> SelectByLoginName(string loginName);
        public Task<IDataReader> Login(string loginName);
        public Task<int> LoginSuccess(Guid userId);
        public Task<int> LoginFail(Guid userId);
        public Task<int> UpdateLastActivity(Guid userId, DateTime lastActivityDate);
        public Task<bool> Update(Guid userId, string? loginName, string? username, string? email, string? name, bool? isDeleted, bool? isLockedOut,
            DateTime? lastActivityDate, DateTime? lastLoginDate, DateTime? lastLockedOutDate, int? failedPwdAttemptCount,
            DateTime? failedPwdAttemptWindowStart, string? comment);
    }
}
