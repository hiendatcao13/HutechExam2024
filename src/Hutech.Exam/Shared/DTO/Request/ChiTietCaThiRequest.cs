﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hutech.Exam.Shared.DTO.Request
{
    public class ChiTietCaThiRequest
    {
        public int MaChiTietCaThi { get; set; }

        public int? MaCaThi { get; set; }

        public long? MaSinhVien { get; set; }

        public long? MaDeThi { get; set; }

        public DateTime? ThoiGianBatDau { get; set; }

        public DateTime? ThoiGianKetThuc { get; set; }

        public bool DaThi { get; set; }

        public bool DaHoanThanh { get; set; }

        public double Diem { get; set; }

        public int? TongSoCau { get; set; }

        public int? SoCauDung { get; set; }

        public int GioCongThem { get; set; }

        public DateTime? ThoiDiemCong { get; set; }

        public string? LyDoCong { get; set; }
    }
}
