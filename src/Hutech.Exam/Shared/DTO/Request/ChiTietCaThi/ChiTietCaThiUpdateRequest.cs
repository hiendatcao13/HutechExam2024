using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hutech.Exam.Shared.DTO.Request.ChiTietCaThi
{
    public class ChiTietCaThiUpdateRequest
    {
        public ChiTietCaThiUpdateRequest()
        {
        }

        public ChiTietCaThiUpdateRequest(int maCaThi, long maSinhVien, long maDeThi)
        {
            MaCaThi = maCaThi;
            MaSinhVien = maSinhVien;
            MaDeThi = maDeThi;
        }

        public int MaCaThi { get; set; }

        public long MaSinhVien { get; set; }

        public long MaDeThi { get; set; }
    }
}
