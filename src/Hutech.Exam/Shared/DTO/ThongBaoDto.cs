﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hutech.Exam.Shared.DTO
{
    public partial class ThongBaoDto
    {
        public ThongBaoDto()
        {
        }

        public int MaThongBao { get; set; }

        public Guid UserId { get; set; }

        public DateTime NgayTao { get; set; }

        public string NoiDung { get; set; } = null!;

        public ThongBaoDto(int maThongBao, Guid userId, DateTime ngayTao, string noiDung)
        {
            MaThongBao = maThongBao;
            UserId = userId;
            NgayTao = ngayTao;
            NoiDung = noiDung;
        }

        public ThongBaoDto(ThongBaoDto other)
        {
            MaThongBao = other.MaThongBao;
            UserId = other.UserId;
            NgayTao = other.NgayTao;
            NoiDung = other.NoiDung;
        }
    }
}
