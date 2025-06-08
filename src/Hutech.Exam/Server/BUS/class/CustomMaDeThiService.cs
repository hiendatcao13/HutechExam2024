using System.Data;
using Hutech.Exam.Server.DAL.Repositories;
using Hutech.Exam.Shared.DTO.Custom;

namespace Hutech.Exam.Server.BUS
{
    public class CustomMaDeThiService(ICustomRepository customRepository)
    {
        private readonly ICustomRepository _customRepository = customRepository;

        public CustomThongTinMaDeThi GetProperty(IDataReader dataReader, int start = 0)
        {
            var result = new CustomThongTinMaDeThi
            {
                MaChuong = dataReader.GetInt32(0 + start),
                MaNhom = dataReader.GetInt32(1 + start),
                HoanViNhom = dataReader.GetBoolean(2 + start),
                ThuTuNhom = dataReader.GetInt32(3 + start),
                MaCauHoi = !dataReader.IsDBNull(4) ? dataReader.GetInt32(4 + start) : null,
                HoanViCauHoi = !dataReader.IsDBNull(5) ? dataReader.GetBoolean(5 + start) : null,
                DapAn = !dataReader.IsDBNull(6) ? dataReader.GetInt32(6 + start) : null, // phòng trường hợp câu hỏi chưa có đáp án
                ThuTuCauHoi = !dataReader.IsDBNull(7) ? dataReader.GetInt32(7 + start) : null
            };

            // xử lí danh sách tạo chỗ

            return result;
        }

        public async Task<List<CustomThongTinMaDeThi>> GetThongTinMaDeThi(int ma_de_thi)
        {
            List<CustomThongTinMaDeThi> result = [];
            using (IDataReader dataReader = await _customRepository.LayMaThongTinDeThi(ma_de_thi))
            {
                while (dataReader.Read())
                {
                    var customMaDeThi = GetProperty(dataReader);
                    customMaDeThi.CauTraLoiKhongHoanVi = !dataReader.IsDBNull(8) ? dataReader.GetString(8).Split(";;;", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList(): [];
                    result.Add(customMaDeThi);
                }
            }
            return result;
        }


    }
}
