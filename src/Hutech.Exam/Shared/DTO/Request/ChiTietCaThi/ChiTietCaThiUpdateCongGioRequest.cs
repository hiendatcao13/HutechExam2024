using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hutech.Exam.Shared.DTO.Request.ChiTietCaThi
{
    public class ChiTietCaThiUpdateCongGioRequest
    {
        public ChiTietCaThiUpdateCongGioRequest()
        {
        }

        public ChiTietCaThiUpdateCongGioRequest(int gioCongThem, DateTime thoiDiemCong, string lyDoCong)
        {
            GioCongThem = gioCongThem;
            ThoiDiemCong = thoiDiemCong;
            LyDoCong = lyDoCong;
        }

        public int GioCongThem { get; set; }

        public DateTime ThoiDiemCong { get; set; }

        public string LyDoCong { get; set; } = string.Empty;


    }
}
