using System;
using System.Collections.Generic;

namespace Hutech.Exam.Shared.Models;

public partial class ChiTietDeThiHoanVi
{
    public long MaDeHv { get; set; }

    public int MaNhom { get; set; }

    public int MaCauHoi { get; set; }

    public int ThuTu { get; set; }

    public string? HoanViTraLoi { get; set; }

    public int? DapAn { get; set; }

    public virtual NhomCauHoiHoanVi Ma { get; set; } = null!;
}
