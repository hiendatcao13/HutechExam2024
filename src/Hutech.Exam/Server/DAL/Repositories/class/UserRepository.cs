﻿using Hutech.Exam.Server.DAL.DataReader;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using System.Data;

namespace Hutech.Exam.Server.DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        public async Task<IDataReader> SelectOne(Guid userId)
        {
            DatabaseReader sql = new("User_SelectOne");
            sql.SqlParams("@UserId", SqlDbType.UniqueIdentifier, userId);
            return await sql.ExecuteReaderAsync();
        }
        public async Task<IDataReader> SelectByLoginName(string loginName)
        {
            DatabaseReader sql = new("User_SelectByLoginName");
            sql.SqlParams("@LoginName", SqlDbType.NVarChar, loginName);
            return await sql.ExecuteReaderAsync();
        }
        public async Task<IDataReader> Login(string loginName)
        {
            DatabaseReader sql = new("User_Login");
            sql.SqlParams("@LoginName", SqlDbType.NVarChar, loginName);
            return await sql.ExecuteReaderAsync();
        }
        public async Task<int> LoginSuccess(Guid userId)
        {
            DatabaseReader sql = new("User_LoginSuccess");
            sql.SqlParams("@UserId", SqlDbType.UniqueIdentifier, userId);
            return await sql.ExecuteNonQueryAsync();
        }
        public async Task<int> LoginFail(Guid userId)
        {
            DatabaseReader sql = new("User_LoginFail");
            sql.SqlParams("@UserId", SqlDbType.UniqueIdentifier, userId);
            return await sql.ExecuteNonQueryAsync();
        }
        public async Task<int> UpdateLastActivity(Guid userId, DateTime lastActivityDate)
        {
            DatabaseReader sql = new("User_UpdateLastActivity");
            sql.SqlParams("@UserId", SqlDbType.UniqueIdentifier, userId);
            sql.SqlParams("@LastActivityDate", SqlDbType.DateTime, lastActivityDate);
            return await sql.ExecuteNonQueryAsync();
        }
        public async Task<bool> Update(Guid userId,string? loginName, string? username, string? email, string? name, bool? isDeleted, bool? isLockedOut, 
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
            return await sql.ExecuteNonQueryAsync() != 0;
        }
    }
}
