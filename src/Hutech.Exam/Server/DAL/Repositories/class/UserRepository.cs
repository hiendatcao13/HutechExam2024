using AutoMapper;
using Hutech.Exam.Server.DAL.DataReader;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.Models;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using System.Data;

namespace Hutech.Exam.Server.DAL.Repositories
{
    public class UserRepository(IMapper mapper) : IUserRepository
    {
        private readonly IMapper _mapper = mapper;

        public UserDto GetProperty(IDataReader dataReader, int start = 0)
        {
            User user = new()
            {
                UserId = dataReader.GetGuid(0 + start),
                LoginName = dataReader.GetString(1 + start),
                Email = dataReader.GetString(2 + start),
                Name = dataReader.GetString(3 + start),
                Password = dataReader.GetString(4 + start),
                MaRole = dataReader.GetInt32(5 + start),
                DateCreated = dataReader.GetDateTime(6 + start),
                IsDeleted = dataReader.GetBoolean(7 + start),
                IsLockedOut = dataReader.GetBoolean(8 + start),
                LastActivityDate = dataReader.IsDBNull(9 + start) ? null : dataReader.GetDateTime(9 + start),
                LastLoginDate = dataReader.IsDBNull(10 + start) ? null : dataReader.GetDateTime(10 + start),
                LastPasswordChangedDate = dataReader.IsDBNull(11 + start) ? null : dataReader.GetDateTime(11 + start),
                LastLockoutDate = dataReader.IsDBNull(12 + start) ? null : dataReader.GetDateTime(12 + start),
                FailedPwdAttemptCount = dataReader.IsDBNull(13 + start) ? null : dataReader.GetInt32(13 + start),
                FailedPwdAttemptWindowStart = dataReader.IsDBNull(14 + start) ? null : dataReader.GetDateTime(14 + start),
                FailedPwdAnswerCount = dataReader.IsDBNull(15 + start) ? null : dataReader.GetInt32(15 + start),
                FailedPwdAnswerWindowStart = dataReader.IsDBNull(16 + start) ? null : dataReader.GetDateTime(16 + start),
                PasswordSalt = dataReader.IsDBNull(17 + start) ? null : dataReader.GetString(17 + start),
                Comment = dataReader.GetString(18 + start),
                IsBuildInUser = dataReader.GetBoolean(19 + start)
            };
            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> SelectOne(Guid userId)
        {
            using DatabaseReader sql = new("User_SelectOne");

            sql.SqlParams("@UserId", SqlDbType.UniqueIdentifier, userId);

            using var dataReader = await sql.ExecuteReaderAsync();
            UserDto user = new();

            if (dataReader != null && dataReader.Read())
            {
                user = GetProperty(dataReader);
            }

            return user;
        }

        public async Task<UserDto> SelectByLoginName(string loginName)
        {
            using DatabaseReader sql = new("User_SelectByLoginName");

            sql.SqlParams("@LoginName", SqlDbType.NVarChar, loginName);

            using var dataReader = await sql.ExecuteReaderAsync();
            UserDto user = new();

            if (dataReader != null && dataReader.Read())
            {
                user = GetProperty(dataReader);
            }

            return user;
        }

        public async Task<List<string>> Login(string loginName)
        {
            using DatabaseReader sql = new("User_Login");

            sql.SqlParams("@LoginName", SqlDbType.NVarChar, loginName);

            using var dataReader = await sql.ExecuteReaderAsync();
            List<string> user = [];

            if (dataReader != null && dataReader.Read())
            {
                user.Add(dataReader.GetGuid(0).ToString());
                user.Add(dataReader.GetString(1));
                user.Add(dataReader.GetString(2));
            }

            return user;
        }

        public async Task<bool> LoginSuccess(Guid userId)
        {
            using DatabaseReader sql = new("User_LoginSuccess");

            sql.SqlParams("@UserId", SqlDbType.UniqueIdentifier, userId);

            return await sql.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> LoginFail(Guid userId)
        {
            using DatabaseReader sql = new("User_LoginFail");

            sql.SqlParams("@UserId", SqlDbType.UniqueIdentifier, userId);

            return await sql.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> UpdateLastActivity(Guid userId, DateTime lastActivityDate)
        {
            using DatabaseReader sql = new("User_UpdateLastActivity");

            sql.SqlParams("@UserId", SqlDbType.UniqueIdentifier, userId);
            sql.SqlParams("@LastActivityDate", SqlDbType.DateTime, lastActivityDate);

            return await sql.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> Update(Guid userId, string? loginName, string? username, string? email, string? name, bool? isDeleted, bool? isLockedOut,
            DateTime? lastActivityDate, DateTime? lastLoginDate, DateTime? lastLockedOutDate, int? failedPwdAttemptCount,
            DateTime? failedPwdAttemptWindowStart, string? comment)
        {
            DatabaseReader sql = new("User_Update");
            sql.SqlParams("@UserId", SqlDbType.UniqueIdentifier, userId);
            sql.SqlParams("@LoginName", SqlDbType.NVarChar, (loginName == null) ? DBNull.Value : loginName);
            sql.SqlParams("@Email", SqlDbType.NVarChar, (username == null) ? DBNull.Value : username);
            sql.SqlParams("Name", SqlDbType.NVarChar, (name == null) ? DBNull.Value : name);
            sql.SqlParams("IsDeleted", SqlDbType.Bit, (isDeleted == null) ? DBNull.Value : isDeleted);
            sql.SqlParams("@IsLockedOut", SqlDbType.Bit, (isLockedOut == null) ? DBNull.Value : isLockedOut);
            sql.SqlParams("@LastActiviyDate", SqlDbType.DateTime, (lastActivityDate == null) ? DBNull.Value : lastActivityDate);
            sql.SqlParams("@LastLoginDate", SqlDbType.DateTime, (lastLoginDate == null) ? DBNull.Value : lastLoginDate);
            sql.SqlParams("@LastLockedOutDate", SqlDbType.DateTime, (lastLockedOutDate == null) ? DBNull.Value : lastLockedOutDate);
            sql.SqlParams("@FailedPwdAttemptCount", SqlDbType.Int, (failedPwdAttemptCount == null) ? DBNull.Value : failedPwdAttemptCount);
            sql.SqlParams("@FailedPwdAttemptWindowStart", SqlDbType.DateTime, (failedPwdAttemptWindowStart == null) ? DBNull.Value : failedPwdAttemptWindowStart);
            sql.SqlParams("Comment", SqlDbType.NText, (comment == null) ? DBNull.Value : comment);
            return await sql.ExecuteNonQueryAsync() > 0;
        }
    }
}
