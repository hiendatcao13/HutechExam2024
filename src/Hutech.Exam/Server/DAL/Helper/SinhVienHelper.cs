using System.Data;
using Hutech.Exam.Shared.DTO;

namespace Hutech.Exam.Server.DAL.Helper
{
    public class SinhVienHelper
    {
        public static DataTable ToDataTable(List<SinhVienDto> sinhVienList)
        {
            var dt = new DataTable();
            dt.Columns.Add("HoVaTenLot", typeof(string));
            dt.Columns.Add("TenSinhVien", typeof(string));
            dt.Columns.Add("GioiTinh", typeof(bool));
            dt.Columns.Add("NgaySinh", typeof(DateTime));
            dt.Columns.Add("MaLop", typeof(int));
            dt.Columns.Add("DiaChi", typeof(string));
            dt.Columns.Add("Email", typeof(string));
            dt.Columns.Add("DienThoai", typeof(string));
            dt.Columns.Add("MaSoSinhVien", typeof(string));
            dt.Columns.Add("StudentId", typeof(Guid));

            foreach (var item in sinhVienList)
            {
                var row = dt.NewRow();

                row["HoVaTenLot"] = item.HoVaTenLot ?? (object)DBNull.Value;
                row["TenSinhVien"] = item.TenSinhVien ?? (object)DBNull.Value;
                row["GioiTinh"] = item.GioiTinh ?? (object)DBNull.Value;
                row["NgaySinh"] = item.NgaySinh ?? (object)DBNull.Value;
                row["MaLop"] = item.MaLop ?? (object)DBNull.Value;
                row["DiaChi"] = item.DiaChi ?? (object)DBNull.Value;
                row["Email"] = item.Email ?? (object)DBNull.Value;
                row["DienThoai"] = item.DienThoai ?? (object)DBNull.Value;
                row["MaSoSinhVien"] = item.MaSoSinhVien ?? (object)DBNull.Value;
                row["StudentId"] = Guid.NewGuid(); // luôn có giá trị

                dt.Rows.Add(row);
            }

            return dt;
        }
    }
}
