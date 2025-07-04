using System;
using System.Collections.Generic;

namespace Hutech.Exam.Shared.Models;

public partial class DeThi
{
    public int MaDeThi { get; set; }

    public int MaMonHoc { get; set; }

    public string TenDeThi { get; set; } = null!;

    public Guid? Guid { get; set; }

    public string KyHieuDe { get; set; } = string.Empty;

    public DateTime NgayTao { get; set; }

}
