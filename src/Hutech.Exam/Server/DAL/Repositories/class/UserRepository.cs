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

        public static readonly int COLUMN_LENGTH = 17;

        public User GetProperty(IDataReader dataReader, int start = 0)
        {
            User user = new()
            {
                MaNguoiDung = dataReader.GetGuid(0 + start),
                TenDangNhap = dataReader.GetString(1 + start),
                Email = dataReader.GetString(2 + start),
                Ten = dataReader.GetString(3 + start),
                MatKhau = dataReader.GetString(4 + start),
                MaVaiTro = dataReader.GetInt32(5 + start),
                NgayTao = dataReader.GetDateTime(6 + start),
                DaXoa = dataReader.GetBoolean(7 + start),
                DaKhoa = dataReader.GetBoolean(8 + start),
                ThoiGianHoatDong = dataReader.IsDBNull(9 + start) ? null : dataReader.GetDateTime(9 + start),
                ThoiGianDangXuat = dataReader.IsDBNull(10 + start) ? null : dataReader.GetDateTime(10 + start),
                ThoiGianDoiMatKhau = dataReader.IsDBNull(11 + start) ? null : dataReader.GetDateTime(11 + start),
                ThoiGianKhoa = dataReader.IsDBNull(12 + start) ? null : dataReader.GetDateTime(12 + start),
                SoLanDangNhapSai = dataReader.IsDBNull(13 + start) ? null : dataReader.GetInt32(13 + start),
                ThoiGianDangNhapSai = dataReader.IsDBNull(14 + start) ? null : dataReader.GetDateTime(14 + start),
                GhiChu = dataReader.IsDBNull(15 + start) ? null : dataReader.GetString(15 + start),
                LaNguoiDungHeThong = dataReader.GetBoolean(16 + start)
            };
            return user;
        }

        public async Task<Paged<UserDto>> GetAll_Paged(int pageNumber, int pageSize)
        {
            using DatabaseReader sql = new("NguoiDung_GetAll_Paged");

            sql.SqlParams("@PageNumber", SqlDbType.Int, pageNumber);
            sql.SqlParams("@PageSize", SqlDbType.Int, pageSize);

            using var dataReader = await sql.ExecuteReaderAsync();
            List<UserDto> result = [];
            int tong_so_ban_ghi = 0, tong_so_trang = 0;

            while (await dataReader!.ReadAsync())
            {
                UserDto user = _mapper.Map<UserDto>(GetProperty(dataReader));
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

        public async Task<Paged<UserDto>> GetAll_GiamThi_Paged(int pageNumber, int pageSize)
        {
            using DatabaseReader sql = new("NguoiDung_GetAll_GiamThi_Paged");

            sql.SqlParams("@PageNumber", SqlDbType.Int, pageNumber);
            sql.SqlParams("@PageSize", SqlDbType.Int, pageSize);

            using var dataReader = await sql.ExecuteReaderAsync();
            List<UserDto> result = [];
            int tong_so_ban_ghi = 0, tong_so_trang = 0;

            while (await dataReader!.ReadAsync())
            {
                UserDto user = _mapper.Map<UserDto>(GetProperty(dataReader));
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
            using DatabaseReader sql = new("NguoiDung_GetAll_Search_Paged");

            sql.SqlParams("@Keyword", SqlDbType.NVarChar, keyword);
            sql.SqlParams("@PageNumber", SqlDbType.Int, pageNumber);
            sql.SqlParams("@PageSize", SqlDbType.Int, pageSize);

            using var dataReader = await sql.ExecuteReaderAsync();
            List<UserDto> result = [];
            int tong_so_ban_ghi = 0, tong_so_trang = 0;

            while (await dataReader!.ReadAsync())
            {
                UserDto user = _mapper.Map<UserDto>(GetProperty(dataReader));
                user.MaRoleNavigation = _roleRepository.GetProperty(dataReader, COLUMN_LENGTH);
                result.Add(user);
            }
            //chuyển sang bảng thứ 2 đọc tổng số lượng bản ghi và tổng số lượng trang
            if (dataReader != null && dataReader.NextResult())
            {
                while (await dataReader.ReadAsync())
                {
                    tong_so_ban_ghi = dataReader.GetInt32(0);
                    tong_so_trang = dataReader.GetInt32(1);
                }
            }

            return new Paged<UserDto> { Data = result, TotalPages = tong_so_trang, TotalRecords = tong_so_ban_ghi };

        }

        public async Task<Paged<UserDto>> GetAll_Search_GiamThi_Paged(string keyword, int pageNumber, int pageSize)
        {
            using DatabaseReader sql = new("NguoiDung_GetAll_Search_GiamThi_Paged");

            sql.SqlParams("@Keyword", SqlDbType.NVarChar, keyword);
            sql.SqlParams("@PageNumber", SqlDbType.Int, pageNumber);
            sql.SqlParams("@PageSize", SqlDbType.Int, pageSize);

            using var dataReader = await sql.ExecuteReaderAsync();
            List<UserDto> result = [];
            int tong_so_ban_ghi = 0, tong_so_trang = 0;

            while (await dataReader!.ReadAsync())
            {
                UserDto user = _mapper.Map<UserDto>(GetProperty(dataReader));
                user.MaRoleNavigation = _roleRepository.GetProperty(dataReader, COLUMN_LENGTH);
                result.Add(user);
            }
            //chuyển sang bảng thứ 2 đọc tổng số lượng bản ghi và tổng số lượng trang
            if (dataReader != null && dataReader.NextResult())
            {
                while (await dataReader.ReadAsync())
                {
                    tong_so_ban_ghi = dataReader.GetInt32(0);
                    tong_so_trang = dataReader.GetInt32(1);
                }
            }

            return new Paged<UserDto> { Data = result, TotalPages = tong_so_trang, TotalRecords = tong_so_ban_ghi };

        }

        public async Task<UserDto> SelectOne(Guid userId)
        {
            using DatabaseReader sql = new("NguoiDung_SelectOne");

            sql.SqlParams("@MaNguoiDung", SqlDbType.UniqueIdentifier, userId);

            using var dataReader = await sql.ExecuteReaderAsync();
            UserDto user = new();

            if (await dataReader!.ReadAsync())
            {
                user = _mapper.Map<UserDto>(GetProperty(dataReader));
                user.MaRoleNavigation = _roleRepository.GetProperty(dataReader, COLUMN_LENGTH);
            }

            return user;
        }

        public async Task<Guid> Insert(UserCreateRequest user)
        {
            using DatabaseReader sql = new("NguoiDung_Insert");

            sql.SqlParams("@MaVaiTro", SqlDbType.Int, user.MaRole);
            sql.SqlParams("@TenDangNhap", SqlDbType.NVarChar, user.LoginName);
            sql.SqlParams("@Email", SqlDbType.NVarChar, user.Email);
            sql.SqlParams("@Ten", SqlDbType.NVarChar, user.Name);
            sql.SqlParams("@MatKhau", SqlDbType.NVarChar, user.Password);
            sql.SqlParams("@NgayTao", SqlDbType.DateTime, user.DateCreated);
            sql.SqlParams("@GhiChu", SqlDbType.NVarChar, user.Comment);

            return (Guid)(await sql.ExecuteScalarAsync() ?? new Guid());
        }

        public async Task<UserDto> SelectByLoginName(string loginName)
        {
            using DatabaseReader sql = new("NguoiDung_SelectBy_TenDangNhap");

            sql.SqlParams("@TenDangNhap", SqlDbType.NVarChar, loginName);

            using var dataReader = await sql.ExecuteReaderAsync();
            UserDto user = new();

            if (await dataReader!.ReadAsync())
            {
                user = _mapper.Map<UserDto>(GetProperty(dataReader));
            }

            return user;
        }

        public async Task<User> Login(string loginName)
        {
            using DatabaseReader sql = new("NguoiDung_Login");

            sql.SqlParams("@TenDangNhap", SqlDbType.NVarChar, loginName);

            using var dataReader = await sql.ExecuteReaderAsync();
            User user = new();

            if (await dataReader!.ReadAsync())
            {
                user = GetProperty(dataReader);
                user.MaRoleNavigation = _mapper.Map<Role>(_roleRepository.GetProperty(dataReader, COLUMN_LENGTH));
            }

            return user;
        }

        public async Task<bool> LoginSuccess(Guid userId)
        {
            using DatabaseReader sql = new("NguoiDung_LoginSuccess");

            sql.SqlParams("@MaNguoiDung", SqlDbType.UniqueIdentifier, userId);

            return await sql.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> LoginFail(Guid userId)
        {
            using DatabaseReader sql = new("NguoiDung_LoginFail");

            sql.SqlParams("@MaNguoiDung", SqlDbType.UniqueIdentifier, userId);

            return await sql.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> UpdateLastActivity(Guid userId, DateTime lastActivityDate)
        {
            using DatabaseReader sql = new("NguoiDung_UpdateLastActivity");

            sql.SqlParams("@MaNguoiDung", SqlDbType.UniqueIdentifier, userId);
            sql.SqlParams("@ThoiGianHoatDong", SqlDbType.DateTime, lastActivityDate);

            return await sql.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> Update(Guid id, UserUpdateRequest user)
        {
            using DatabaseReader sql = new("NguoiDung_Update");
            sql.SqlParams("@MaNguoiDung", SqlDbType.UniqueIdentifier, id);
            sql.SqlParams("MaVaiTro", SqlDbType.Int, user.MaRole);
            sql.SqlParams("@TenDangNhap", SqlDbType.NVarChar, user.LoginName);
            sql.SqlParams("@Email", SqlDbType.NVarChar, user.Email);
            sql.SqlParams("@Ten", SqlDbType.NVarChar, user.Name);
            sql.SqlParams("@DaXoa", SqlDbType.Bit, user.IsDeleted);
            sql.SqlParams("@DaKhoa", SqlDbType.Bit, user.IsLockedOut);
            sql.SqlParams("@GhiChu", SqlDbType.NVarChar, user.Comment);

            return await sql.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> UpdatePassword(Guid id, UserUpdatePasswordRequest user)
        {
            using DatabaseReader sql = new("NguoiDung_UpdateMatKhau");

            sql.SqlParams("@MaNguoiDung", SqlDbType.UniqueIdentifier, id);
            sql.SqlParams("@MatKhau", SqlDbType.NVarChar, user.Password);
            sql.SqlParams("@ThoiGianDoiMatKhau", SqlDbType.DateTime, user.LastPasswordChangedDate);

            return await sql.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> Delete(Guid id)
        {
            using DatabaseReader sql = new("NguoiDung_Delete");

            sql.SqlParams("@MaNguoiDung", SqlDbType.UniqueIdentifier, id);

            return await sql.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> CheckExistName(string loginName, string email)
        {
            using DatabaseReader sql = new("NguoiDung_CheckTen");

            sql.SqlParams("@TenDangNhap", SqlDbType.NVarChar, loginName);
            sql.SqlParams("@Email", SqlDbType.NVarChar, email);

            return Convert.ToInt32(await sql.ExecuteScalarAsync()) == 1;
        }
    }
}
