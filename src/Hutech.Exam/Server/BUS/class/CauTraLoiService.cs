using AutoMapper;
using Hutech.Exam.Server.DAL.Repositories;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.DTO.Request.CauTraLoi;
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
        public async Task<CauTraLoiDto> SelectOne(int ma_cau_tra_loi)
        {
            CauTraLoiDto cauTraLoi = new();
            using (IDataReader dataReader = await _cauTraLoiRepository.SelectOne(ma_cau_tra_loi))
            {
                if (dataReader.Read())
                {
                    cauTraLoi = GetProperty(dataReader);
                }
            }
            return cauTraLoi;
        }
        public async Task<int> Insert(CauTraLoiCreateRequest cauTraLoi)
        {
            return Convert.ToInt32(await _cauTraLoiRepository.Insert(cauTraLoi.MaCauHoi, cauTraLoi.ThuTu, cauTraLoi.NoiDung, cauTraLoi.LaDapAn, cauTraLoi.HoanVi) ?? -1);
        }
        public async Task<bool> Update(int id, CauTraLoiUpdateRequest cauTraLoi)
        {
            return await _cauTraLoiRepository.Update(id, cauTraLoi.MaCauHoi, cauTraLoi.ThuTu, cauTraLoi.NoiDung, cauTraLoi.LaDapAn, cauTraLoi.HoanVi) != 0;
        }
        public async Task<bool> Remove(int ma_cau_tra_loi)
        {
            return await _cauTraLoiRepository.Remove(ma_cau_tra_loi) != 0;
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
