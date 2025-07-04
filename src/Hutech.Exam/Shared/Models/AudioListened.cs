using System;
using System.Collections.Generic;

namespace Hutech.Exam.Shared.Models;

public partial class AudioListened
{
    public long MaNghe { get; set; }

    public int MaChiTietCaThi { get; set; }

    public string? TenFile { get; set; }

    public int SoLanNghe { get; set; }
}
