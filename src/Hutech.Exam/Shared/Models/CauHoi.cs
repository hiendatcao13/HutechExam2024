using System;
using System.Collections.Generic;

namespace Hutech.Exam.Shared.Models;

public partial class CauHoi
{
    public int MaCauHoi { get; set; }

    public int MaNhom { get; set; }

    public int MaClo { get; set; }

    public string? TieuDe { get; set; }

    public int KieuNoiDung { get; set; }

    public string? NoiDung { get; set; }

    public string? GhiChu { get; set; }

    public bool? HoanVi { get; set; }

    public virtual ICollection<CauTraLoi> CauTraLois { get; set; } = new List<CauTraLoi>();

    public virtual ICollection<ChiTietDeThiHoanVi> ChiTietDeThiHoanVis { get; set; } = new List<ChiTietDeThiHoanVi>();

    public virtual Clo MaCloNavigation { get; set; } = null!;

    public virtual NhomCauHoi MaNhomNavigation { get; set; } = null!;
}
