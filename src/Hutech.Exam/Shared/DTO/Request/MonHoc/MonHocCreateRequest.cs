using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hutech.Exam.Shared.DTO.Request.MonHoc
{
    public class MonHocCreateRequest
    {
        public MonHocCreateRequest()
        {
        }

        public MonHocCreateRequest(string maSoMonHoc, string tenMonHoc)
        {
            MaSoMonHoc = maSoMonHoc;
            TenMonHoc = tenMonHoc;
        }

        public string MaSoMonHoc { get; set; } = string.Empty;

        public string TenMonHoc { get; set; } = string.Empty;
    }
}
