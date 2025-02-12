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
                DateCreated = dataReader.GetDateTime(5),
                IsDeleted = dataReader.GetBoolean(6),
                IsLockedOut = dataReader.GetBoolean(7),
                LastActivityDate = dataReader.IsDBNull(8) ? null : dataReader.GetDateTime(8),
                LastLoginDate = dataReader.IsDBNull(9) ? null : dataReader.GetDateTime(9),
                LastPasswordChangedDate = dataReader.IsDBNull(10) ? null : dataReader.GetDateTime(10),
                LastLockoutDate = dataReader.IsDBNull(11) ? null : dataReader.GetDateTime(11),
                FailedPwdAttemptCount = dataReader.IsDBNull(12) ? null : dataReader.GetInt32(12),
                FailedPwdAttemptWindowStart = dataReader.IsDBNull(13) ? null : dataReader.GetDateTime(13),
                FailedPwdAnswerCount = dataReader.IsDBNull(14) ? null : dataReader.GetInt32(14),
                FailedPwdAnswerWindowStart = dataReader.IsDBNull(15) ? null : dataReader.GetDateTime(15),
                PasswordSalt = dataReader.IsDBNull(16) ? null : dataReader.GetString(16),
                Comment = dataReader.GetString(17),
                IsBuildInUser = dataReader.GetBoolean(18)
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
            List<string> user = new();
            using(IDataReader dataReader = await _userRepository.Login(loginName))
            {
                if (dataReader.Read())
                {
                    user.Add(dataReader.GetString(0));
                    user.Add(dataReader.GetString(1));
                }
            }
            return user;
        }
        public async Task<bool> Update(Guid userId, string? loginName, string? username, string? email, string? name, bool? isDeleted, bool? isLockedOut,
            DateTime? lastActivityDate, DateTime? lastLoginDate, DateTime? lastLockedOutDate, int? failedPwdAttemptCount,
            DateTime? failedPwdAttemptWindowStart, string? comment)
        {
            return await _userRepository.Update(userId, loginName, username, email, name, isDeleted, isLockedOut, lastActivityDate, lastLoginDate, lastLockedOutDate, failedPwdAttemptCount, failedPwdAttemptWindowStart, comment);
        }
    }
}
