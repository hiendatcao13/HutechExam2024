using AutoMapper;
using Hutech.Exam.Server.DAL.Repositories;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.DTO.Page;
using Hutech.Exam.Shared.DTO.Request.User;
using Hutech.Exam.Shared.Models;
using Microsoft.AspNetCore.Identity;
using System.Data;

namespace Hutech.Exam.Server.BUS
{
    public class UserService(IUserRepository userRepository)
    {
        #region Private Fields
        private readonly IUserRepository _userRepository = userRepository;
        #endregion

        #region Public Methods
        public async Task<UserDto> SelectOne(Guid userId)
        {
            return await _userRepository.SelectOne(userId);
        }

        public async Task<Paged<UserDto>> GetAll_Paged(int pageNumber, int pageSize)
        {
            
            return await _userRepository.GetAll_Paged(pageNumber, pageSize);
        }

        public async Task<Paged<UserDto>> GetAll_Search_Paged(string keyword, int pageNumber, int pageSize)
        {
            return await _userRepository.GetAll_Search_Paged(keyword, pageNumber, pageSize);
        }


        public async Task<Guid> Insert(UserCreateRequest user)
        {
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password, workFactor: 12);
            return await _userRepository.Insert(user);
        }

        public async Task<UserDto> SelectByLoginName(string loginName)
        {
            return await _userRepository.SelectByLoginName(loginName);
        }

        public async Task<User> Login(string loginName)
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

        public async Task<bool> Update(Guid id, UserUpdateRequest user)
        {
            return await _userRepository.Update(id, user);
        }

        public async Task<bool> UpdatePassword(Guid id, UserUpdatePasswordRequest user)
        {
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password, workFactor: 12);
            return await _userRepository.UpdatePassword(id, user);
        }

        public async Task<bool> Remove(Guid id)
        {
            return await _userRepository.Delete(id);
        }

        public async Task<bool> CheckExistName(string loginName, string email)
        {
            return await _userRepository.CheckExistName(loginName, email);
        }
        #endregion

    }
}
