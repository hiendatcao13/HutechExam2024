using System;
using System.Collections.Generic;

namespace Hutech.Exam.Shared.Models;

public partial class CaThi
{
    public int MaCaThi { get; set; }

    public string? TenCaThi { get; set; }

    public int MaChiTietDotThi { get; set; }

    public DateTime ThoiGianBatDau { get; set; }

    public bool DaGanDe { get; set; }

    public bool KichHoat { get; set; }

    public DateTime? ThoiGianKichHoat { get; set; }

    public int ThoiGianThi { get; set; }

    public bool KetThuc { get; set; }

    public DateTime? ThoiDiemKetThuc { get; set; }

    public string? MatMa { get; set; }

    public bool DaDuyet { get; set; }

    public string? LichSuHoatDong { get; set; }

    public virtual ICollection<ChiTietCaThi> ChiTietCaThis { get; set; } = new List<ChiTietCaThi>();

    public virtual ChiTietDotThi MaChiTietDotThiNavigation { get; set; } = null!;
}
