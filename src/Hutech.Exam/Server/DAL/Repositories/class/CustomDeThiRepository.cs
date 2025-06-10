using Hutech.Exam.Server.DAL.DataReader;
using Hutech.Exam.Shared.DTO.Custom;
using System.Data;

namespace Hutech.Exam.Server.DAL.Repositories
{
    public class CustomDeThiRepository : ICustomDeThiRepository
    {
        public CustomDeThi GetProperty(IDataReader dataReader, int start = 0)
        {
            CustomDeThi customDeThi = new()
            {
                MaNhom = dataReader.GetInt32(0 + start),
                MaCauHoi = dataReader.GetInt32(1 + start),
                MaNhomCha = dataReader.GetInt32(2 + start),
                MaCLO = dataReader.GetInt32(3 + start),
                MaSoCLO = dataReader.IsDBNull(4 + start) ? null : dataReader.GetString(4 + start),
                NoiDungCauHoiNhomCha = dataReader.IsDBNull(5 + start) ? null : dataReader.GetString(5 + start),
                NoiDungCauHoiNhom = dataReader.IsDBNull(6 + start) ? null : dataReader.GetString(6 + start),
                NoiDungCauHoi = dataReader.IsDBNull(7 + start) ? null : dataReader.GetString(7 + start),
                KieuNoiDungCauHoi = dataReader.GetInt32(8 + start),
                KieuNoiDungCauHoiNhom = dataReader.GetInt32(9 + start),
            };
            string? ma_dap_an_gop = dataReader.IsDBNull(10 + start) ? null : dataReader.GetString(10 + start);
            customDeThi.HoanViCauTraLoi = dataReader.IsDBNull(11 + start) ? null : dataReader.GetString(11 + start);
            string? noi_dung_dap_an_gop = dataReader.IsDBNull(12 + start) ? null : dataReader.GetString(12 + start);
            customDeThi.CauTraLois = HandleDapAnGop(ma_dap_an_gop, noi_dung_dap_an_gop);
            customDeThi.ThuTuCauHoi = dataReader.GetInt64(13 + start);
            return customDeThi;
        }

        public async Task<List<CustomDeThi>> GetDeThi(long ma_de_hoan_vi)
        {
            using DatabaseReader sql = new("Custom_GetDeThi");

            sql.SqlParams("@MaDeThiHoanVi", SqlDbType.BigInt, ma_de_hoan_vi);

            using var dataReader = await sql.ExecuteReaderAsync();
            List<CustomDeThi> result = [];

            while (dataReader != null && dataReader.Read())
            {
                result.Add(GetProperty(dataReader));
            }

            // hoán vị thứ tự câu trả lời
            HandleHoanViTraLoi(result);

            return result;
        }


        private Dictionary<int, string?> HandleDapAnGop(string? ma_dap_an_gop, string? noi_dung_dap_an_gop)
        {
            if (string.IsNullOrEmpty(ma_dap_an_gop) || string.IsNullOrEmpty(noi_dung_dap_an_gop))
            {
                return [];
            }
            // Xử lý dữ liệu của ma_dap_an_gop và noi_dung_dap_an_gop
            // Chia nhỏ chuỗi ma_dap_an_gop thành các phần tử riêng biệt
            string[] maDapAnParts = ma_dap_an_gop.Split(";;;", StringSplitOptions.RemoveEmptyEntries);
            string[] noiDungDapAnParts = noi_dung_dap_an_gop.Split(";;;", StringSplitOptions.RemoveEmptyEntries);

            // Tạo một Dictionary để lưu trữ các cặp giá trị
            Dictionary<int, string?> cauTraLois = [];
            for (int i = 0; i < maDapAnParts.Length; i++)
            {
                int maDapAn = int.Parse(maDapAnParts[i]);
                string? noiDungDapAn = i < noiDungDapAnParts.Length ? noiDungDapAnParts[i] : null;
                cauTraLois.Add(maDapAn, noiDungDapAn);
            }
            return cauTraLois;
        }

        private void HandleHoanViTraLoi(List<CustomDeThi> deThis)
        {
            foreach (var item in deThis)
            {
                if (item.CauTraLois == null || item.CauTraLois.Count == 0 || string.IsNullOrEmpty(item.HoanViCauTraLoi))
                {
                    continue;
                }

                var cauTraLoisList = item.CauTraLois.ToList(); // Chuyển sang list để truy cập theo chỉ số
                var cauTraLoiHoanVi = new Dictionary<int, string?>();

                bool invalid = false;
                foreach (char c in item.HoanViCauTraLoi)
                {
                    if (!char.IsDigit(c) || !int.TryParse(c.ToString(), out int index))
                    {
                        invalid = true;
                        break;
                    }

                    if (index > 0 || index <= cauTraLoisList.Count)
                    {
                        var pair = cauTraLoisList[index - 1];
                        cauTraLoiHoanVi[pair.Key] = pair.Value;
                    }
                }


                // Nếu có lỗi định dạng thì giữ nguyên thứ tự gốc
                item.CauTraLois = invalid ? item.CauTraLois : cauTraLoiHoanVi;
            }


        }


    }
}
