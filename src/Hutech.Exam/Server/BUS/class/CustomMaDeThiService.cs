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
                Chuong = dataReader.GetInt32(0 + start),
                MaNhom = dataReader.GetInt32(1 + start),
                HoanViNhom = dataReader.GetBoolean(2 + start),
                ThuTuNhom = dataReader.GetInt32(3 + start),
                MaCauHoi = !dataReader.IsDBNull(7) ? dataReader.GetInt32(4 + start) : null,
                HoanViCauHoi = !dataReader.IsDBNull(7) ? dataReader.GetBoolean(5 + start) : null,
                ThuTuCauHoi = !dataReader.IsDBNull(7) ? dataReader.GetInt32(6 + start) : null
            };
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
                    customMaDeThi.CauTraLoiKhongHoanVi = !dataReader.IsDBNull(7) ? dataReader.GetString(7).Split(",", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList(): [];
                    result.Add(customMaDeThi);
                }
            }

            DeleteEmptyList(result); // xóa các phần tử là chương

            return result;
        }


        private void DeleteEmptyList(List<CustomThongTinMaDeThi> customThongTinMaDeThis)
        {
            customThongTinMaDeThis.RemoveAll(x => x.MaCauHoi == null || x.HoanViCauHoi == null || x.ThuTuCauHoi == null);
        }
    }
}
