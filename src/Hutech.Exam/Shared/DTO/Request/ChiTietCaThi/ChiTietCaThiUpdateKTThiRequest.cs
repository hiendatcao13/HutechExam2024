using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hutech.Exam.Shared.DTO.Request.ChiTietCaThi
{
    public class ChiTietCaThiUpdateKTThiRequest
    {
        public ChiTietCaThiUpdateKTThiRequest()
        {
        }

        public ChiTietCaThiUpdateKTThiRequest(DateTime thoiGianKetThuc, double diem, int soCauDung, int tongSoCau)
        {
            ThoiGianKetThuc = thoiGianKetThuc;
            Diem = diem;
            SoCauDung = soCauDung;
            TongSoCau = tongSoCau;
        }

        public DateTime ThoiGianKetThuc { get; set; }

        public double Diem { get; set; }

        public int SoCauDung { get; set; }

        public int TongSoCau { get; set; }
    }
}
