using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hutech.Exam.Shared.DTO.Request.LopAo
{
    public class LopAoCreateRequest
    {
        public LopAoCreateRequest()
        {
        }

        public LopAoCreateRequest(string tenLopAo, DateTime ngayBatDau, int maMonHoc)
        {
            TenLopAo = tenLopAo;
            NgayBatDau = ngayBatDau;
            MaMonHoc = maMonHoc;
        }

        public string TenLopAo { get; set; } = string.Empty;

        public DateTime NgayBatDau { get; set; }

        public int MaMonHoc { get; set; }
    }
}
