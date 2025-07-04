using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hutech.Exam.Shared.Models
{
    public partial class SinhVienDuPhong
    {
        public long MaSinhVienDuPhong { get; set; }

        public long MaSinhVien { get; set; }

        public int MaLopAo { get; set; }

        public int MaCaThi { get; set; }
    }
}
