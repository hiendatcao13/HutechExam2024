using Hutech.Exam.Shared.DTO;
using System.Data;

namespace Hutech.Exam.Server.DAL.Helper
{
    public static class ChiTietBaiThiHelper
    {
        public static DataTable ToDataTable(List<ChiTietBaiThiDto> chiTietBaiThiList)
        {
            var dt = new DataTable();

            dt.Columns.Add("MaChiTietCaThi", typeof(int));
            dt.Columns.Add("MaDeThi", typeof(long));
            dt.Columns.Add("MaNhom", typeof(Guid));
            dt.Columns.Add("MaCauHoi", typeof(Guid));
            dt.Columns.Add("CauTraLoi", typeof(Guid));
            dt.Columns.Add("NgayTao", typeof(DateTime));
            dt.Columns.Add("NgayCapNhat", typeof(DateTime));
            dt.Columns.Add("KetQua", typeof(bool));
            dt.Columns.Add("ThuTu", typeof(int));

            foreach (var item in chiTietBaiThiList)
            {
                dt.Rows.Add(
                    item.MaChiTietCaThi,
                    item.MaDeThi,
                    item.MaNhom,
                    item.MaCauHoi,
                    item.CauTraLoi,
                    item.NgayTao,
                    item.NgayCapNhat,
                    item.KetQua,
                    item.ThuTu
                );
            }

            return dt;
        }
    }
}
