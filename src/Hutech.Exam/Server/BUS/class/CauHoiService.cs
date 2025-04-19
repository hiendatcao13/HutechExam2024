using AutoMapper;
using Hutech.Exam.Server.DAL.Repositories;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.Models;
using System.Data;

namespace Hutech.Exam.Server.BUS
{
    public class CauHoiService
    {
        private readonly ICauHoiRepository _cauHoiRepository;
        private IMapper _mapper;
        public CauHoiService(ICauHoiRepository cauHoiRepository, IMapper mapper)
        {
            _cauHoiRepository = cauHoiRepository;
            _mapper = mapper;
        }
        private CauHoiDto getProperty(IDataReader dataReader)
        {
            CauHoi cauHoi = new();
            cauHoi.MaCauHoi = dataReader.GetInt32(0);
            cauHoi.MaClo = dataReader.GetInt32(1);
            cauHoi.TieuDe = dataReader.IsDBNull(2) ? null : dataReader.GetString(2);
            cauHoi.KieuNoiDung = dataReader.GetInt32(3);
            cauHoi.NoiDung = dataReader.IsDBNull(4) ? null : dataReader.GetString(4);
            cauHoi.GhiChu = dataReader.IsDBNull(5) ? null : dataReader.GetString(5);
            cauHoi.HoanVi = dataReader.IsDBNull(6) ? null : dataReader.GetBoolean(6);
            return _mapper.Map<CauHoiDto>(cauHoi);
        }
        public async Task<CauHoiDto> SelectOne(int ma_cau_hoi)
        {
            CauHoiDto cauHoi = new();
            using(IDataReader dataReader = await _cauHoiRepository.SelectOne(ma_cau_hoi))
            {
                if (dataReader.Read())
                {
                    cauHoi = getProperty(dataReader);
                }
            }
            return cauHoi;
        }
        public async Task<int> SelectDapAn(int ma_cau_hoi)
        {
            // chỉ trả về duy nhất 1 cột là MaTraLoi
            int dapAn = -1;
            using (IDataReader dataReader = await _cauHoiRepository.SelectDapAn(ma_cau_hoi))
            {
                if (dataReader.Read())
                {
                    dapAn = dataReader.GetInt32(0);
                }
            }
            return dapAn;
        }
    }
}
