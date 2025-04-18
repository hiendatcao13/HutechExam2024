﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hutech.Exam.Shared.DTO
{
    public class UserDto
    {
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

    }
}
