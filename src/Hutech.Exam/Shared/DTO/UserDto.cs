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
        public Guid UserId { get; set; }

        public string LoginName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Name { get; set; } = null!;

        //public string Password { get; set; } = null!;

        public int MaRole { get; set; }

        public bool IsDeleted { get; set; }

        public bool IsLockedOut { get; set; }

        public DateTime? LastActivityDate { get; set; }

        public DateTime? LastLoginDate { get; set; }

        public DateTime? LastPasswordChangedDate { get; set; }

        public DateTime? LastLockoutDate { get; set; }
        public virtual RoleDto MaRoleNavigation { get; set; } = null!;

        public override string ToString()
        {
            return Name;
        }

        public UserDto(Guid userId, string loginName, string email, string name, int maRole, bool isDeleted, bool isLockedOut, DateTime? lastActivityDate, DateTime? lastLoginDate, DateTime? lastPasswordChangedDate, DateTime? lastLockoutDate, RoleDto maRoleNavigation)
        {
            UserId = userId;
            LoginName = loginName;
            Email = email;
            Name = name;
            MaRole = maRole;
            IsDeleted = isDeleted;
            IsLockedOut = isLockedOut;
            LastActivityDate = lastActivityDate;
            LastLoginDate = lastLoginDate;
            LastPasswordChangedDate = lastPasswordChangedDate;
            LastLockoutDate = lastLockoutDate;
            MaRoleNavigation = maRoleNavigation;
        }

        public UserDto(UserDto other)
        {
            UserId = other.UserId;
            LoginName = other.LoginName;
            Email = other.Email;
            Name = other.Name;
            MaRole = other.MaRole;
            IsDeleted = other.IsDeleted;
            IsLockedOut = other.IsLockedOut;
            LastActivityDate = other.LastActivityDate;
            LastLoginDate = other.LastLoginDate;
            LastPasswordChangedDate = other.LastPasswordChangedDate;
            LastLockoutDate = other.LastLockoutDate;
            MaRoleNavigation = other.MaRoleNavigation;
        }
    }
}
