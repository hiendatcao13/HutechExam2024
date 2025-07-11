using System;
using System.Collections.Generic;

namespace Hutech.Exam.Shared.Models;

public partial class User
{
    public Guid MaNguoiDung { get; set; }

    public string TenDangNhap { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Ten { get; set; } = null!;

    public string MatKhau { get; set; } = null!;

    public int MaVaiTro { get; set; }

    public DateTime NgayTao { get; set; }

    public bool DaXoa { get; set; }

    public bool DaKhoa { get; set; }

    public DateTime? ThoiGianHoatDong { get; set; }

    public DateTime? ThoiGianDangNhap { get; set; }

    public DateTime? ThoiGianDoiMatKhau { get; set; }

    public DateTime? ThoiGianKhoa { get; set; }

    public int? SoLanDangNhapSai { get; set; }

    public DateTime? ThoiGianDangNhapSai { get; set; }

    public string? GhiChu { get; set; }

    public bool LaNguoiDungHeThong { get; set; }

    public virtual Role MaRoleNavigation { get; set; } = null!;
}
