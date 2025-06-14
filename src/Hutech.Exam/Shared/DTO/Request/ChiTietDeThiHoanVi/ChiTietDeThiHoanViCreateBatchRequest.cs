using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hutech.Exam.Shared.DTO.Request.ChiTietDeThiHoanVi
{
    public class ChiTietDeThiHoanViCreateBatchRequest
    {
        public int MaNhom { get; set; }

        public int ThuTuNhom { get; set; }

        public int MaCauHoi { get; set; }

        public int ThuTuCauHoi { get; set; }

        public string? HoanViTraLoi { get; set; }

        public int DapAn { get; set; }

        public ChiTietDeThiHoanViCreateBatchRequest(int maNhom, int thuTuNhom, int maCauHoi, int thuTuCauHoi, string? hoanViTraLoi, int dapAn)
        {
            MaNhom = maNhom;
            ThuTuNhom = thuTuNhom;
            MaCauHoi = maCauHoi;
            ThuTuCauHoi = thuTuCauHoi;
            HoanViTraLoi = hoanViTraLoi;
            DapAn = dapAn;
        }
    }
}
