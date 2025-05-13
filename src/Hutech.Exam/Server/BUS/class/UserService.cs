using AutoMapper;
using Hutech.Exam.Server.DAL.Repositories;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.Models;
using Microsoft.AspNetCore.Identity;
using System.Data;

namespace Hutech.Exam.Server.BUS
{
    public class UserService(IUserRepository userRepository, IMapper mapper)
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IMapper _mapper = mapper;

        public UserDto GetProperty(IDataReader dataReader, int start = 0)
        {
            User user = new()
            {
                UserId = dataReader.GetGuid(0 + start),
                LoginName = dataReader.GetString(1 + start),
                Email = dataReader.GetString(2  + start),
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
            UserDto user = new();
            using (IDataReader dataReader = await _userRepository.SelectOne(userId))
            {
                if (dataReader.Read())
                {
                    user = GetProperty(dataReader);
                }
            }
            return user;
        }
        public async Task<UserDto> SelectByLoginName(string loginName)
        {
            UserDto user = new();
            using (IDataReader dataReader = await _userRepository.SelectByLoginName(loginName))
            {
                if (dataReader.Read())
                {
                    user = GetProperty(dataReader);
                }
            }
            return user;
        }
        public async Task<List<string>> Login(string loginName)
        {
            List<string> user = [];
            using(IDataReader dataReader = await _userRepository.Login(loginName))
            {
                if (dataReader.Read())
                {
                    user.Add(dataReader.GetGuid(0).ToString());
                    user.Add(dataReader.GetString(1));
                    user.Add(dataReader.GetString(2));
                }
            }
            return user;
        }
        public async Task<int> LoginSuccess(Guid userId)
        {
            return await _userRepository.LoginSuccess(userId);
        }
        public async Task<int> LoginFail(Guid userId)
        {
            return await _userRepository.LoginFail(userId);
        }
        public async Task<int> UpdateLastActivity(Guid userId, DateTime lastActivityDate)
        {
            return await _userRepository.UpdateLastActivity(userId, lastActivityDate);
        }
        public async Task<bool> Update(Guid userId, string? loginName, string? username, string? email, string? name, bool? isDeleted, bool? isLockedOut,
            DateTime? lastActivityDate, DateTime? lastLoginDate, DateTime? lastLockedOutDate, int? failedPwdAttemptCount,
            DateTime? failedPwdAttemptWindowStart, string? comment)
        {
            return await _userRepository.Update(userId, loginName, username, email, name, isDeleted, isLockedOut, lastActivityDate, lastLoginDate, lastLockedOutDate, failedPwdAttemptCount, failedPwdAttemptWindowStart, comment);
        }
    }
}
