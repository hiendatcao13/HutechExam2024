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
            cauHoi.MaNhom = dataReader.GetInt32(1);
            cauHoi.MaClo = dataReader.GetInt32(2);
            cauHoi.TieuDe = dataReader.IsDBNull(3) ? null : dataReader.GetString(3);
            cauHoi.KieuNoiDung = dataReader.GetInt32(4);
            cauHoi.NoiDung = dataReader.IsDBNull(5) ? null : dataReader.GetString(5);
            cauHoi.GhiChu = dataReader.IsDBNull(6) ? null : dataReader.GetString(6);
            cauHoi.HoanVi = dataReader.IsDBNull(7) ? null : dataReader.GetBoolean(7);
            return _mapper.Map<CauHoiDto>(cauHoi);
        }
        public async Task<int> Insert(int ma_clo, int ma_nhom, string tieu_de, int kieu_noi_dung, string noi_dung, string ghi_chu, bool hoan_vi)
        {
            return Convert.ToInt32(await _cauHoiRepository.Insert(ma_clo, ma_nhom, tieu_de, kieu_noi_dung, noi_dung, ghi_chu, hoan_vi) ?? -1);
        }
        public async Task<int> Update(int ma_cau_hoi, int ma_nhom, int ma_clo, string tieu_de, int kieu_noi_dung, string noi_dung, string ghi_chu, bool hoan_vi)
        {
            return await _cauHoiRepository.Update(ma_cau_hoi, ma_nhom, ma_clo, tieu_de, kieu_noi_dung, noi_dung, ghi_chu, hoan_vi);
        }
        public async Task<int> Remove(int ma_cau_hoi)
        {
            return await _cauHoiRepository.Remove(ma_cau_hoi);
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
        public async Task<List<CauHoiDto>> SelectBy_MaNhom(int ma_nhom)
        {
            List<CauHoiDto> listCauHoi = new();
            using (IDataReader dataReader = await _cauHoiRepository.SelectBy_MaNhom(ma_nhom))
            {
                while (dataReader.Read())
                {
                    CauHoiDto cauHoi = getProperty(dataReader);
                    listCauHoi.Add(cauHoi);
                }
            }
            return listCauHoi;
        }
    }
}
