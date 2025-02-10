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
            TblCauHoi cauHoi = new();
            cauHoi.MaCauHoi = dataReader.GetInt32(0);
            cauHoi.TieuDe = dataReader.IsDBNull(1) ? null : dataReader.GetString(1);
            cauHoi.KieuNoiDung = dataReader.GetInt32(2);
            cauHoi.NoiDung = dataReader.IsDBNull(3) ? null : dataReader.GetString(3);
            cauHoi.GhiChu = dataReader.IsDBNull(4) ? null : dataReader.GetString(4);
            cauHoi.HoanVi = dataReader.IsDBNull(5) ? null : dataReader.GetBoolean(5);
            return _mapper.Map<CauHoiDto>(cauHoi);
        }
        public CauHoiDto SelectOne(int ma_cau_hoi)
        {
            CauHoiDto cauHoi = new();
            using(IDataReader dataReader = _cauHoiRepository.SelectOne(ma_cau_hoi))
            {
                if (dataReader.Read())
                {
                    cauHoi = getProperty(dataReader);
                }
            }
            return cauHoi;
        }
        public int SelectDapAn(int ma_cau_hoi)
        {
            // chỉ trả về duy nhất 1 cột là MaTraLoi
            int dapAn = -1;
            using (IDataReader dataReader = _cauHoiRepository.SelectDapAn(ma_cau_hoi))
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
