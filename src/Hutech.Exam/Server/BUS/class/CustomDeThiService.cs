using Hutech.Exam.Server.DAL.Repositories;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.DTO.Custom;
using Hutech.Exam.Shared.Models;
using System.Data;

namespace Hutech.Exam.Server.BUS
{
    public class CustomDeThiService(ICustomRepository customRepository)
    {
        private readonly ICustomRepository _customRepository = customRepository;

        public CustomDeThi GetProperty(IDataReader dataReader, int start = 0)
        {
            CustomDeThi customDeThi = new()
            {
                MaNhom = dataReader.GetInt32(0 + start),
                MaCauHoi = dataReader.GetInt32(1 + start),
                MaNhomCha = dataReader.GetInt32(2 + start),
                MaSoCLO = dataReader.IsDBNull(3 + start) ? null : dataReader.GetString(3 + start),
                NoiDungCauHoiNhomCha = dataReader.IsDBNull(4 + start) ? null : dataReader.GetString(4 + start),
                NoiDungCauHoiNhom = dataReader.IsDBNull(5 + start) ? null : dataReader.GetString(5 + start),
                NoiDungCauHoi = dataReader.IsDBNull(6 + start) ? null : dataReader.GetString(6 + start),
                KieuNoiDungCauHoi = dataReader.GetInt32(7 + start)
            };
            string? ma_dap_an_gop = dataReader.IsDBNull(8 + start) ? null : dataReader.GetString(8 + start);
            string? noi_dung_dap_an_gop = dataReader.IsDBNull(9 + start) ? null : dataReader.GetString(9 + start);
            customDeThi.CauTraLois = HandleDapAnGop(ma_dap_an_gop, noi_dung_dap_an_gop);
            return customDeThi;
        }
        private Dictionary<int, string?> HandleDapAnGop(string? ma_dap_an_gop, string? noi_dung_dap_an_gop)
        {
            if(string.IsNullOrEmpty(ma_dap_an_gop) || string.IsNullOrEmpty(noi_dung_dap_an_gop))
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

        public async Task<List<CustomDeThi>> GetDeThi(long ma_de_hoan_vi)
        {
            List<CustomDeThi> result = [];
            using (IDataReader dataReader = await _customRepository.GetDeThi(ma_de_hoan_vi))
            {
                while (dataReader.Read())
                {
                    result.Add(GetProperty(dataReader));
                }
            }
            return result;
        }
    }
}
