using System;
using System.Collections.Generic;

namespace Hutech.Exam.Shared.Models;

public partial class AudioListened
{
    public long ListenId { get; set; }

    public int MaChiTietCaThi { get; set; }

    public string FileName { get; set; } = null!;

    public int ListenedCount { get; set; }
}
