using AutoMapper;
using Hutech.Exam.Server.DAL.Repositories;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.DTO.Request.CauHoi;
using Hutech.Exam.Shared.Models;
using System.Data;

namespace Hutech.Exam.Server.BUS
{
    public class CauHoiService(ICauHoiRepository cauHoiRepository, CloService cloService, CauTraLoiService cauTraLoiService, IMapper mapper)
    {
        private readonly ICauHoiRepository _cauHoiRepository = cauHoiRepository;
        private readonly CloService _cloService = cloService;
        private readonly CauTraLoiService _cauTraLoiService = cauTraLoiService;
        private IMapper _mapper = mapper;
        public static readonly int COLUMN_LENGTH = 8; // số lượng cột trong bảng CauHoi

        public CauHoiDto GetProperty(IDataReader dataReader, int start = 0)
        {
            CauHoi cauHoi = new()
            {
                MaCauHoi = dataReader.GetInt32(0 + start),
                MaNhom = dataReader.GetInt32(1 + start),
                MaClo = dataReader.GetInt32(2 + start),
                TieuDe = dataReader.IsDBNull(3 + start) ? null : dataReader.GetString(3 + start),
                KieuNoiDung = dataReader.GetInt32(4 + start),
                NoiDung = dataReader.IsDBNull(5 + start) ? null : dataReader.GetString(5 + start),
                GhiChu = dataReader.IsDBNull(6 + start) ? null : dataReader.GetString(6 + start),
                HoanVi = dataReader.IsDBNull(7 + start) ? null : dataReader.GetBoolean(7 + start)
            };
            return _mapper.Map<CauHoiDto>(cauHoi);
        }

        public async Task<int> Insert(CauHoiCreateRequest cauHoi)
        {
            return Convert.ToInt32(await _cauHoiRepository.Insert(cauHoi.MaClo, cauHoi.MaNhom, cauHoi.TieuDe, cauHoi.KieuNoiDung, cauHoi.NoiDung, cauHoi.GhiChu, cauHoi.HoanVi) ?? -1);
        }

        public async Task<bool> Update(int id, CauHoiUpdateRequest cauHoi)
        {
            return await _cauHoiRepository.Update(id, cauHoi.MaNhom, cauHoi.MaClo, cauHoi.TieuDe, cauHoi.KieuNoiDung, cauHoi.NoiDung, cauHoi.GhiChu, cauHoi.HoanVi) != 0;
        }

        public async Task<bool> Remove(int ma_cau_hoi)
        {
            return await _cauHoiRepository.Remove(ma_cau_hoi) != 0;
        }

        public async Task<bool> ForceRemove(int ma_cau_hoi)
        {
            return await _cauHoiRepository.ForceRemove(ma_cau_hoi) != 0;
        }

        public async Task<CauHoiDto> SelectOne(int ma_cau_hoi)
        {
            CauHoiDto cauHoi = new();
            using (IDataReader dataReader = await _cauHoiRepository.SelectOne(ma_cau_hoi))
            {
                if (dataReader.Read())
                {
                    cauHoi = GetProperty(dataReader);
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
            List<CauHoiDto> listCauHoi = [];
            Dictionary<int, CauHoiDto> cauHoiMap = []; // Để tra cứu nhanh theo MaCauHoi

            using (IDataReader dataReader = await _cauHoiRepository.SelectBy_MaNhom(ma_nhom))
            {
                while (dataReader.Read())
                {
                    int maCauHoi = dataReader.GetInt32(0);

                    if (!cauHoiMap.TryGetValue(maCauHoi, out var cauHoi))
                    {
                        cauHoi = GetProperty(dataReader);
                        cauHoi.MaCloNavigation = _cloService.GetProperty(dataReader, COLUMN_LENGTH);
                        cauHoiMap[maCauHoi] = cauHoi;
                        listCauHoi.Add(cauHoi);
                    }

                    var cauTraLoi = _cauTraLoiService.GetProperty(dataReader, COLUMN_LENGTH + CloService.COLUMN_LENGTH);
                    cauHoi.CauTraLois.Add(cauTraLoi);
                }
            }
            return listCauHoi;
        }
    }
}
