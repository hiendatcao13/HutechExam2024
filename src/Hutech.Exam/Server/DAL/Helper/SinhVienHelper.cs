using System.Data;
using Hutech.Exam.Shared.DTO;

namespace Hutech.Exam.Server.DAL.Helper
{
    public class SinhVienHelper
    {
        public static DataTable ToDataTable(List<SinhVienDto> sinhVienList)
        {
            var dt = new DataTable();
            dt.Columns.Add("MaSoSinhVien ", typeof(string));
            dt.Columns.Add("HoVaTenLot", typeof(string));
            dt.Columns.Add("TenSinhVien", typeof(string));
            dt.Columns.Add("GioiTinh", typeof(bool));
            foreach (var item in sinhVienList)
            {
                dt.Rows.Add(
                    item.MaSinhVien,
                    item.HoVaTenLot,
                    item.TenSinhVien,
                    item.GioiTinh
                );
            }
            return dt;
        }
    }
}
