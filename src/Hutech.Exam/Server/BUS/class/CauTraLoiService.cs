using AutoMapper;
using Hutech.Exam.Server.DAL.Repositories;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.Models;
using System.Data;

namespace Hutech.Exam.Server.BUS
{
    public class CauTraLoiService
    {
        private readonly ICauTraLoiRepository _cauTraLoiRepository;
        private readonly IMapper _mapper;
        public CauTraLoiService(ICauTraLoiRepository cauTraLoiRepository, IMapper mapper)
        {
            _cauTraLoiRepository = cauTraLoiRepository;
            _mapper = mapper;
        }
        private CauTraLoiDto getProperty(IDataReader dataReader)
        {
            CauTraLoi cauTraLoi = new()
            {
                MaCauTraLoi = dataReader.GetInt32(0),
                MaCauHoi = dataReader.GetInt32(1),
                ThuTu = dataReader.GetInt32(2),
                NoiDung = dataReader.IsDBNull(3) ? null : dataReader.GetString(3),
                LaDapAn = dataReader.GetBoolean(4),
                HoanVi = dataReader.GetBoolean(5)
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
            List<CauTraLoiDto> result = new();
            using (IDataReader dataReader = await _cauTraLoiRepository.SelectBy_MaCauHoi(ma_cau_hoi))
            {
                while (dataReader.Read())
                {
                    CauTraLoiDto cauTraLoi = getProperty(dataReader);
                    result.Add(cauTraLoi);
                }
            }
            return result;
        }

    }
}
