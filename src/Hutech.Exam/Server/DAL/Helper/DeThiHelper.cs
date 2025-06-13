using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.DTO.Request.ChiTietCaThi;
using Hutech.Exam.Shared.DTO.Request.ChiTietDeThiHoanVi;
using System.Data;

namespace Hutech.Exam.Server.DAL.Helper
{
    public class DeThiHelper
    {
        public static DataTable ToDataTable(List<ChiTietDeThiHoanViCreateBatchRequest> chiTietDeThiHoanVis)
        {
            var dt = new DataTable();
            dt.Columns.Add("MaNhom", typeof(int));
            dt.Columns.Add("ThuTuNhom", typeof(int));

            dt.Columns.Add("MaCauHoi", typeof(int));
            dt.Columns.Add("ThuTuCauHoi", typeof(int));
            dt.Columns.Add("HoanViTraLoi", typeof(string));
            dt.Columns.Add("DapAn", typeof(int));

            foreach (var item in chiTietDeThiHoanVis)
            {
                var row = dt.NewRow();

                row["MaNhom"] = item.MaNhom;
                row["ThuTuNhom"] = item.ThuTuNhom;

                row["MaCauHoi"] = item.MaCauHoi;
                row["ThuTuCauHoi"] = item.ThuTuCauHoi;

                row["HoanViTraLoi"] = item.HoanViTraLoi ?? (object)DBNull.Value;
                row["DapAn"] = item.DapAn;

                dt.Rows.Add(row);
            }

            return dt;
        }
    }
}
