using System;
using System.Collections.Generic;

namespace Hutech.Exam.Shared.Models;

public partial class DeThiHoanVi
{
    public long MaDeHv { get; set; }

    public int MaDeThi { get; set; }

    public string? KyHieuDe { get; set; }

    public DateTime NgayTao { get; set; }

    public Guid? Guid { get; set; }

    public virtual DeThi MaDeThiNavigation { get; set; } = null!;

    public virtual ICollection<NhomCauHoiHoanVi> NhomCauHoiHoanVis { get; set; } = new List<NhomCauHoiHoanVi>();
}
