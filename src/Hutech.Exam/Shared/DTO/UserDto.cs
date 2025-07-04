using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hutech.Exam.Shared.DTO
{
    public class UserDto
    {
        public UserDto()
        {
        }

        // loại bỏ Comment, PasswordSalt, DateCreated, FailedPwdAttemptCount, FailedPwdAttemptWindowStart, FailedPwdAnswerCount
        // FailedPwdAnswerWindowStart, IsBuildInUser, Password
        public Guid MaNguoiDung { get; set; }

        public string TenDangNhap { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Ten { get; set; } = null!;

        //public string Password { get; set; } = null!;

        public int MaVaiTro { get; set; }

        public bool DaXoa { get; set; }

        public bool DaKhoa { get; set; }

        public DateTime? ThoiGianHoatDong { get; set; }

        public DateTime? ThoiGianDangNhap { get; set; }

        public DateTime? ThoiGianDoiMatKhau { get; set; }

        public DateTime? ThoiGianKhoa { get; set; }
        public virtual RoleDto MaRoleNavigation { get; set; } = null!;

        public override string ToString()
        {
            return Ten;
        }

        public UserDto(Guid userId, string loginName, string email, string name, int maRole, bool isDeleted, bool isLockedOut, DateTime? lastActivityDate, DateTime? lastLoginDate, DateTime? lastPasswordChangedDate, DateTime? lastLockoutDate, RoleDto maRoleNavigation)
        {
            MaNguoiDung = userId;
            TenDangNhap = loginName;
            Email = email;
            Ten = name;
            MaVaiTro = maRole;
            DaXoa = isDeleted;
            DaKhoa = isLockedOut;
            ThoiGianHoatDong = lastActivityDate;
            ThoiGianDangNhap = lastLoginDate;
            ThoiGianDoiMatKhau = lastPasswordChangedDate;
            ThoiGianKhoa = lastLockoutDate;
            MaRoleNavigation = maRoleNavigation;
        }

        public UserDto(UserDto other)
        {
            MaNguoiDung = other.MaNguoiDung;
            TenDangNhap = other.TenDangNhap;
            Email = other.Email;
            Ten = other.Ten;
            MaVaiTro = other.MaVaiTro;
            DaXoa = other.DaXoa;
            DaKhoa = other.DaKhoa;
            ThoiGianHoatDong = other.ThoiGianHoatDong;
            ThoiGianDangNhap = other.ThoiGianDangNhap;
            ThoiGianDoiMatKhau = other.ThoiGianDoiMatKhau;
            ThoiGianKhoa = other.ThoiGianKhoa;
            MaRoleNavigation = other.MaRoleNavigation;
        }
    }
}
