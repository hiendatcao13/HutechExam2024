﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hutech.Exam.Shared.Models
{
    public partial class ThongBao
    {
        public int MaThongBao { get; set; }

        public Guid UserId { get; set; }

        public DateTime NgayTao { get; set; }

        public string NoiDung { get; set; } = null!;
    }
}
