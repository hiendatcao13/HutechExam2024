using Hutech.Exam.Shared.DTO;
using System.Data;

namespace Hutech.Exam.Server.DAL.Helper
{
    public static class ChiTietBaiThiHelper
    {
        public static DataTable ToDataTable(List<ChiTietBaiThiDto> chiTietBaiThiList)
        {
            var dt = new DataTable();

            dt.Columns.Add("ma_chi_tiet_ca_thi", typeof(int));
            dt.Columns.Add("MaDeHV", typeof(long));
            dt.Columns.Add("MaNhom", typeof(int));
            dt.Columns.Add("MaCauHoi", typeof(int));
            dt.Columns.Add("MaCLO", typeof(int));
            dt.Columns.Add("CauTraLoi", typeof(int));
            dt.Columns.Add("NgayTao", typeof(DateTime));
            dt.Columns.Add("NgayCapNhat", typeof(DateTime));
            dt.Columns.Add("KetQua", typeof(bool));
            dt.Columns.Add("ThuTu", typeof(int));

            foreach (var item in chiTietBaiThiList)
            {
                dt.Rows.Add(
                    item.MaChiTietCaThi,
                    item.MaDeHv,
                    item.MaNhom,
                    item.MaCauHoi,
                    item.MaClo,
                    item.CauTraLoi,
                    DateTime.Now,
                    DateTime.Now,
                    item.KetQua,
                    item.ThuTu
                );
            }

            return dt;
        }
    }
}
