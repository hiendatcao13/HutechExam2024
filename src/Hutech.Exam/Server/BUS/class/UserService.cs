using AutoMapper;
using Hutech.Exam.Server.DAL.Repositories;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.Models;
using Microsoft.AspNetCore.Identity;
using System.Data;

namespace Hutech.Exam.Server.BUS
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        private UserDto getProperty(IDataReader dataReader)
        {
            User user = new()
            {
                UserId = dataReader.GetGuid(0),
                LoginName = dataReader.GetString(1),
                Email = dataReader.GetString(2),
                Name = dataReader.GetString(3),
                Password = dataReader.GetString(4),
                MaRole = dataReader.GetInt32(5),
                DateCreated = dataReader.GetDateTime(6),
                IsDeleted = dataReader.GetBoolean(7),
                IsLockedOut = dataReader.GetBoolean(8),
                LastActivityDate = dataReader.IsDBNull(9) ? null : dataReader.GetDateTime(9),
                LastLoginDate = dataReader.IsDBNull(10) ? null : dataReader.GetDateTime(10),
                LastPasswordChangedDate = dataReader.IsDBNull(11) ? null : dataReader.GetDateTime(11),
                LastLockoutDate = dataReader.IsDBNull(12) ? null : dataReader.GetDateTime(12),
                FailedPwdAttemptCount = dataReader.IsDBNull(13) ? null : dataReader.GetInt32(13),
                FailedPwdAttemptWindowStart = dataReader.IsDBNull(14) ? null : dataReader.GetDateTime(14),
                FailedPwdAnswerCount = dataReader.IsDBNull(15) ? null : dataReader.GetInt32(15),
                FailedPwdAnswerWindowStart = dataReader.IsDBNull(16) ? null : dataReader.GetDateTime(16),
                PasswordSalt = dataReader.IsDBNull(17) ? null : dataReader.GetString(17),
                Comment = dataReader.GetString(18),
                IsBuildInUser = dataReader.GetBoolean(19)
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
                    user = getProperty(dataReader);
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
                    user = getProperty(dataReader);
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
