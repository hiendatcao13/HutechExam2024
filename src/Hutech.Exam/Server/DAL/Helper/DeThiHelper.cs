using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.DTO.Request.ChiTietCaThi;
using Hutech.Exam.Shared.DTO.Request.ChiTietDeThiHoanVi;
using Hutech.Exam.Shared.DTO.Request.DeThi;
using System.Data;

namespace Hutech.Exam.Server.DAL.Helper
{
    public class DeThiHelper
    {
        public static DataTable ToDataTable(List<DeThiDto> deThis)
        {
            var dt = new DataTable();
            dt.Columns.Add("MaMonHoc", typeof(int));
            dt.Columns.Add("TenDeThi", typeof(string));
            dt.Columns.Add("Guid", typeof(Guid));
            dt.Columns.Add("KyHieuDe", typeof(string));
            dt.Columns.Add("NgayTao", typeof(DateTime));

            foreach (var item in deThis)
            {
                var row = dt.NewRow();

                row["MaMonHoc"] = item.MaMonHoc;
                row["TenDeThi"] = item.TenDeThi;

                row["Guid"] = item.Guid;
                row["KyHieuDe"] = item.KyHieuDe;

                row["NgayTao"] = item.NgayTao;

                dt.Rows.Add(row);
            }

            return dt;
        }
    }
}
