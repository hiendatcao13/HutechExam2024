using AutoMapper;
using Hutech.Exam.Server.DAL.DataReader;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.DTO.Page;
using Hutech.Exam.Shared.DTO.Request.User;
using Hutech.Exam.Shared.Models;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using System.Data;

namespace Hutech.Exam.Server.DAL.Repositories
{
    public class UserRepository(IMapper mapper, IRoleRepository roleRepository) : IUserRepository
    {
        private readonly IMapper _mapper = mapper;
        private readonly IRoleRepository _roleRepository = roleRepository;

        public static readonly int COLUMN_LENGTH = 20;

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
                Comment = dataReader.IsDBNull(18 + start) ? null : dataReader.GetString(18 + start),
                IsBuildInUser = dataReader.GetBoolean(19 + start)
            };
            return _mapper.Map<UserDto>(user);
        }

        public async Task<Paged<UserDto>> GetAll_Paged(int pageNumber, int pageSize)
        {
            using DatabaseReader sql = new("User_GetAll_Paged");

            sql.SqlParams("@PageNumber", SqlDbType.Int, pageNumber);
            sql.SqlParams("@PageSize", SqlDbType.Int, pageSize);

            using var dataReader = await sql.ExecuteReaderAsync();
            List<UserDto> result = [];
            int tong_so_ban_ghi = 0, tong_so_trang = 0;

            while (dataReader != null && dataReader.Read())
            {
                UserDto user = GetProperty(dataReader);
                user.MaRoleNavigation = _roleRepository.GetProperty(dataReader, COLUMN_LENGTH);
                result.Add(user);
            }
            //chuyển sang bảng thứ 2 đọc tổng số lượng bản ghi và tổng số lượng trang
            if (dataReader != null && dataReader.NextResult())
            {
                while (dataReader.Read())
                {
                    tong_so_ban_ghi = dataReader.GetInt32(0);
                    tong_so_trang = dataReader.GetInt32(1);
                }
            }

            return new Paged<UserDto> { Data = result, TotalPages = tong_so_trang, TotalRecords = tong_so_ban_ghi };

        }

        public async Task<Paged<UserDto>> GetAll_Search_Paged(string keyword, int pageNumber, int pageSize)
        {
            using DatabaseReader sql = new("User_GetAll_Search_Paged");

            sql.SqlParams("@Keyword", SqlDbType.NVarChar, keyword);
            sql.SqlParams("@PageNumber", SqlDbType.Int, pageNumber);
            sql.SqlParams("@PageSize", SqlDbType.Int, pageSize);

            using var dataReader = await sql.ExecuteReaderAsync();
            List<UserDto> result = [];
            int tong_so_ban_ghi = 0, tong_so_trang = 0;

            while (dataReader != null && dataReader.Read())
            {
                UserDto user = GetProperty(dataReader);
                user.MaRoleNavigation = _roleRepository.GetProperty(dataReader, COLUMN_LENGTH);
                result.Add(user);
            }
            //chuyển sang bảng thứ 2 đọc tổng số lượng bản ghi và tổng số lượng trang
            if (dataReader != null && dataReader.NextResult())
            {
                while (dataReader.Read())
                {
                    tong_so_ban_ghi = dataReader.GetInt32(0);
                    tong_so_trang = dataReader.GetInt32(1);
                }
            }

            return new Paged<UserDto> { Data = result, TotalPages = tong_so_trang, TotalRecords = tong_so_ban_ghi };

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

        public async Task<Guid> Insert(UserCreateRequest user)
        {
            using DatabaseReader sql = new("User_Insert");

            sql.SqlParams("@MaRole", SqlDbType.Int, user.MaRole);
            sql.SqlParams("@LoginName", SqlDbType.NVarChar, user.LoginName);
            sql.SqlParams("@Email", SqlDbType.NVarChar, user.Email);
            sql.SqlParams("@Name", SqlDbType.NVarChar, user.Name);
            sql.SqlParams("@Password", SqlDbType.NVarChar, user.Password);
            sql.SqlParams("@DateCreated", SqlDbType.DateTime, user.DateCreated);
            sql.SqlParams("@Comment", SqlDbType.NVarChar, user.Comment);

            return (Guid)(await sql.ExecuteScalarAsync() ?? new Guid());
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

        public async Task<bool> Update(Guid id, UserUpdateRequest user)
        {
            using DatabaseReader sql = new("User_Update");
            sql.SqlParams("@UserId", SqlDbType.UniqueIdentifier, id);
            sql.SqlParams("MaRole", SqlDbType.Int, user.MaRole);
            sql.SqlParams("@LoginName", SqlDbType.NVarChar, user.LoginName);
            sql.SqlParams("@Email", SqlDbType.NVarChar, user.Email);
            sql.SqlParams("Name", SqlDbType.NVarChar, user.Name);
            sql.SqlParams("IsDeleted", SqlDbType.Bit, user.IsDeleted);
            sql.SqlParams("@IsLockedOut", SqlDbType.Bit, user.IsLockedOut);
            sql.SqlParams("Comment", SqlDbType.NVarChar, user.Comment);

            return await sql.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> UpdatePassword(Guid id, UserUpdatePasswordRequest user)
        {
            using DatabaseReader sql = new("User_UpdatePassword");

            sql.SqlParams("@UserId", SqlDbType.UniqueIdentifier, id);
            sql.SqlParams("@Password", SqlDbType.NVarChar, user.Password);
            sql.SqlParams("@LastPasswordChangedDate", SqlDbType.DateTime, user.LastPasswordChangedDate);

            return await sql.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> Delete(Guid id)
        {
            using DatabaseReader sql = new("User_Delete");

            sql.SqlParams("@UserId", SqlDbType.UniqueIdentifier, id);

            return await sql.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> CheckExistName(string loginName, string email)
        {
            using DatabaseReader sql = new("User_CheckName");

            sql.SqlParams("@LoginName", SqlDbType.NVarChar, loginName);
            sql.SqlParams("@Email", SqlDbType.NVarChar, email);

            return Convert.ToInt32(await sql.ExecuteScalarAsync()) == 1;
        }
    }
}
