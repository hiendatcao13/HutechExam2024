using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hutech.Exam.Shared.DTO.Request.NhomCauHoi
{
    public class NhomCauHoiCreateRequest
    {
        public NhomCauHoiCreateRequest()
        {
        }

        public NhomCauHoiCreateRequest(int maDeThi, string tenNhom, int kieuNoiDung, string noiDung, int soCauHoi, bool hoanVi, int thuTu, int maNhomCha, int soCauLay, bool laCauHoiNhom)
        {
            MaDeThi = maDeThi;
            TenNhom = tenNhom;
            KieuNoiDung = kieuNoiDung;
            NoiDung = noiDung;
            SoCauHoi = soCauHoi;
            HoanVi = hoanVi;
            ThuTu = thuTu;
            MaNhomCha = maNhomCha;
            SoCauLay = soCauLay;
            LaCauHoiNhom = laCauHoiNhom;
        }

        public int MaDeThi { get; set; }

        public string TenNhom { get; set; } = null!;

        public int KieuNoiDung { get; set; }

        public string NoiDung { get; set; } = string.Empty;

        public int SoCauHoi { get; set; }

        public bool HoanVi { get; set; }

        public int ThuTu { get; set; }

        public int MaNhomCha { get; set; }

        public int SoCauLay { get; set; }

        public bool LaCauHoiNhom { get; set; }
    }
}
