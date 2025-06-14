﻿using System;
using System.Collections.Generic;

namespace Hutech.Exam.Shared.Models;

public partial class DeThi
{
    public int MaDeThi { get; set; }

    public int MaMonHoc { get; set; }

    public string TenDeThi { get; set; } = null!;

    public DateTime NgayTao { get; set; }

    public int NguoiTao { get; set; }

    public string? GhiChu { get; set; }

    public bool LuuTam { get; set; }

    public bool DaDuyet { get; set; }

    public int? TongSoDeHoanVi { get; set; }

    public bool BoChuongPhan { get; set; }

    public virtual ICollection<DeThiHoanVi> DeThiHoanVis { get; set; } = new List<DeThiHoanVi>();

    public virtual ICollection<NhomCauHoi> NhomCauHois { get; set; } = new List<NhomCauHoi>();
}
