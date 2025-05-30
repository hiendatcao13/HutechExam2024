﻿using System;
using System.Collections.Generic;

namespace Hutech.Exam.Shared.Models;

public partial class MonHoc
{
    public int MaMonHoc { get; set; }

    public string? MaSoMonHoc { get; set; }

    public string? TenMonHoc { get; set; }

    public virtual ICollection<LopAo> LopAos { get; set; } = new List<LopAo>();
    public virtual ICollection<Clo> Clos { get; set; } = new List<Clo>();
}
