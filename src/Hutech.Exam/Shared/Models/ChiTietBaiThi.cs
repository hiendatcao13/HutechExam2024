using System;
using System.Collections.Generic;

namespace Hutech.Exam.Shared.Models;

public partial class ChiTietBaiThi
{
    public long MaChiTietBaiThi { get; set; }

    public int MaChiTietCaThi { get; set; }

    public long MaDeThi { get; set; }

    public Guid MaNhom { get; set; }

    public Guid MaCauHoi { get; set; }

    public Guid? CauTraLoi { get; set; }

    public DateTime NgayTao { get; set; }

    public DateTime? NgayCapNhat { get; set; }

    public bool? KetQua { get; set; }

    public int ThuTu { get; set; }

    public virtual ChiTietCaThi MaChiTietCaThiNavigation { get; set; } = null!;
}
