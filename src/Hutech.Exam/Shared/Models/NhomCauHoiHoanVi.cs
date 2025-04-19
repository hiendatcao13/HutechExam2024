using System;
using System.Collections.Generic;

namespace Hutech.Exam.Shared.Models;

public partial class NhomCauHoiHoanVi
{
    public long MaDeHv { get; set; }

    public int MaNhom { get; set; }

    public int ThuTu { get; set; }

    public virtual DeThiHoanVi MaDeHvNavigation { get; set; } = null!;

    public virtual ICollection<ChiTietDeThiHoanVi> ChiTietDeThiHoanVis { get; set; } = new List<ChiTietDeThiHoanVi>();
}
