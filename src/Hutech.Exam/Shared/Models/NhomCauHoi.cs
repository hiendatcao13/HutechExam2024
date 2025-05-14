using System;
using System.Collections.Generic;

namespace Hutech.Exam.Shared.Models;

public partial class NhomCauHoi
{
    public int MaNhom { get; set; }

    public int MaDeThi { get; set; }

    public string TenNhom { get; set; } = null!;

    public int KieuNoiDung { get; set; }

    public string? NoiDung { get; set; }

    public int SoCauHoi { get; set; }

    public bool HoanVi { get; set; }

    public int ThuTu { get; set; }

    public int MaNhomCha { get; set; }

    public int SoCauLay { get; set; }

    public bool? LaCauHoiNhom { get; set; }

    public virtual DeThi MaDeThiNavigation { get; set; } = null!;
    public virtual ICollection<CauHoi> CauHois { get; set; } = new List<CauHoi>();
}
