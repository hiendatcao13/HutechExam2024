using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.DTO.Request.ChiTietCaThi;
using System.Data;

namespace Hutech.Exam.Server.DAL.Helper
{
    public class SinhVienCaThiHelper
    {
        public static DataTable ToDataTable(List<ChiTietCaThiCreateBatchRequest> chiTietCaThis)
        {
            var dt = new DataTable();
            dt.Columns.Add("MaSoSinhVien", typeof(string));
            dt.Columns.Add("MaCaThi", typeof(int));
            dt.Columns.Add("MaDeThi", typeof(long));

            foreach (var item in chiTietCaThis)
            {
                var row = dt.NewRow();

                row["MaSoSinhVien"] = item.MaSoSinhVien;
                row["MaCaThi"] = item.MaCaThi;
                row["MaDeThi"] = item.MaDeThi ?? (object)DBNull.Value;

                dt.Rows.Add(row);
            }

            return dt;
        }
    }
}
