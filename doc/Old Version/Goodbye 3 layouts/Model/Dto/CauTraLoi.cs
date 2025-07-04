using System;
using System.Collections.Generic;

namespace Hutech.Exam.Shared.Models;

public partial class CauTraLoi
{
    public int MaCauTraLoi { get; set; }

    public int MaCauHoi { get; set; }

    public int ThuTu { get; set; }

    public string? NoiDung { get; set; }

    public bool LaDapAn { get; set; }

    public bool HoanVi { get; set; }

    public virtual CauHoi MaCauHoiNavigation { get; set; } = null!;
}
