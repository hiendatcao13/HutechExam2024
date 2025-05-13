using AutoMapper;
using Hutech.Exam.Server.DAL.Repositories;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.Models;
using System.Data;

namespace Hutech.Exam.Server.BUS
{
    public class CauTraLoiService(ICauTraLoiRepository cauTraLoiRepository, IMapper mapper)
    {
        private readonly ICauTraLoiRepository _cauTraLoiRepository = cauTraLoiRepository;
        private readonly IMapper _mapper = mapper;

        public static readonly int COLUMN_LENGTH = 6; // số lượng cột trong bảng CauTraLoi

        public CauTraLoiDto GetProperty(IDataReader dataReader, int start = 0)
        {
            CauTraLoi cauTraLoi = new()
            {
                MaCauTraLoi = dataReader.GetInt32(0 + start),
                MaCauHoi = dataReader.GetInt32(1 + start),
                ThuTu = dataReader.GetInt32(2 + start),
                NoiDung = dataReader.IsDBNull(3 + start) ? null : dataReader.GetString(3 + start),
                LaDapAn = dataReader.GetBoolean(4 + start),
                HoanVi = dataReader.GetBoolean(5 + start)
            };
            return _mapper.Map<CauTraLoiDto>(cauTraLoi);
        }
        public async Task<int> Insert(int ma_cau_hoi, int thu_tu, string noi_dung, bool la_dap_an, bool hoan_vi)
        {
            return Convert.ToInt32(await _cauTraLoiRepository.Insert(ma_cau_hoi, thu_tu, noi_dung, la_dap_an, hoan_vi) ?? -1);
        }
        public async Task<int> Update(int ma_cau_tra_loi, int ma_cau_hoi, int thu_tu, string noi_dung, bool la_dap_an, bool hoan_vi)
        {
            return await _cauTraLoiRepository.Update(ma_cau_tra_loi, ma_cau_hoi, thu_tu, noi_dung, la_dap_an, hoan_vi);
        }
        public async Task<int> Remove(int ma_cau_tra_loi)
        {
            return await _cauTraLoiRepository.Remove(ma_cau_tra_loi);
        }

        public async Task<List<CauTraLoiDto>> SelectBy_MaCauHoi(int ma_cau_hoi)
        {
            List<CauTraLoiDto> result = [];
            using (IDataReader dataReader = await _cauTraLoiRepository.SelectBy_MaCauHoi(ma_cau_hoi))
            {
                while (dataReader.Read())
                {
                    CauTraLoiDto cauTraLoi = GetProperty(dataReader);
                    result.Add(cauTraLoi);
                }
            }
            return result;
        }
    }
}
