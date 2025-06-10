using AutoMapper;
using Hutech.Exam.Server.DAL.Repositories;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.Models;
using Microsoft.AspNetCore.Identity;
using System.Data;

namespace Hutech.Exam.Server.BUS
{
    public class UserService(IUserRepository userRepository)
    {
        private readonly IUserRepository _userRepository = userRepository;

        public async Task<UserDto> SelectOne(Guid userId)
        {
            return await _userRepository.SelectOne(userId);
        }

        public async Task<UserDto> SelectByLoginName(string loginName)
        {
            return await _userRepository.SelectByLoginName(loginName);
        }

        public async Task<List<string>> Login(string loginName)
        {
            return await _userRepository.Login(loginName);
        }

        public async Task<bool> LoginSuccess(Guid userId)
        {
            return await _userRepository.LoginSuccess(userId);
        }

        public async Task<bool> LoginFail(Guid userId)
        {
            return await _userRepository.LoginFail(userId);
        }

        public async Task<bool> UpdateLastActivity(Guid userId, DateTime lastActivityDate)
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
