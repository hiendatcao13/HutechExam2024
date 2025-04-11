using AutoMapper;
using Hutech.Exam.Server.DAL.Repositories;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.Models;
using System.Data;

namespace Hutech.Exam.Server.BUS
{
    public class CaThiService
    {
        private readonly ICaThiRepository _caThiRepository;
        private readonly IMapper _mapper;
        public CaThiService(ICaThiRepository caThiRepository, IMapper mapper)
        {
            _caThiRepository = caThiRepository;
            _mapper = mapper;
        }
        private CaThiDto getProperty(IDataReader dataReader)
        {
            CaThi caThi = new()
            {
                MaCaThi = dataReader.GetInt32(0),
                TenCaThi = dataReader.IsDBNull(1) ? null : dataReader.GetString(1),
                MaChiTietDotThi = dataReader.GetInt32(2),
                ThoiGianBatDau = dataReader.GetDateTime(3),
                MaDeThi = dataReader.GetInt32(4),
                IsActivated = dataReader.GetBoolean(5),
                ActivatedDate = dataReader.IsDBNull(6) ? null : dataReader.GetDateTime(6),
                ThoiGianThi = dataReader.GetInt32(7),
                KetThuc = dataReader.GetBoolean(8),
                ThoiDiemKetThuc = dataReader.IsDBNull(9) ? null : dataReader.GetDateTime(9),
                MatMa = dataReader.IsDBNull(10) ? null : dataReader.GetString(10),
                Approved = dataReader.GetBoolean(11),
                ApprovedDate = dataReader.IsDBNull(12) ? null : dataReader.GetDateTime(12),
                ApprovedComments = dataReader.IsDBNull(13) ? null : dataReader.GetString(13)
            };
            return _mapper.Map<CaThiDto>(caThi);
        }
        public async Task<List<CaThiDto>> SelectBy_ma_chi_tiet_dot_thi(int ma_chi_tiet_dot_thi)
        {
            List<CaThiDto> result = [];
            using (IDataReader dataReader = await _caThiRepository.SelectBy_ma_chi_tiet_dot_thi(ma_chi_tiet_dot_thi))
            {
                while (dataReader.Read())
                {
                    CaThiDto caThi = getProperty(dataReader);
                    result.Add(caThi);
                }
            }
            return result;
        }
        public async Task<CaThiDto> SelectOne(int ma_ca_thi)
        {
            CaThiDto caThi = new();
            using (IDataReader dataReader = await _caThiRepository.SelectOne(ma_ca_thi))
            {
                if (dataReader.Read())
                {
                    caThi = getProperty(dataReader);
                }
            }
            return caThi;
        }
        public async Task<List<CaThiDto>> ca_thi_GetAll()
        {
            List<CaThiDto> result = [];
            using (IDataReader dataReader = await _caThiRepository.ca_thi_GetAll())
            {
                while (dataReader.Read())
                {
                    CaThiDto caThi = getProperty(dataReader);
                    result.Add(caThi);
                }
            }
            return result;
        }
        public async Task<int> ca_thi_Activate(int ma_ca_thi, bool IsActivated)
        {
            return await _caThiRepository.ca_thi_Activate(ma_ca_thi, IsActivated);
        }
        public async Task<int> ca_thi_Ketthuc(int ma_ca_thi)
        {
            return await _caThiRepository.ca_thi_Ketthuc(ma_ca_thi);
        }
        public async Task<int> Insert(string ten_ca_thi, int ma_chi_tiet_dot_thi, DateTime thoi_gian_bat_dau, int ma_de_thi, int thoi_gian_thi)
        {
            return Convert.ToInt32(await _caThiRepository.Insert(ten_ca_thi, ma_chi_tiet_dot_thi, thoi_gian_bat_dau, ma_de_thi, thoi_gian_thi) ?? -1);
        }
        public async Task<int> Remove(int ma_ca_thi)
        {
            return await _caThiRepository.Remove(ma_ca_thi);
        }
        public async Task<int> Update(int ma_ca_thi, string ten_ca_thi, int ma_chi_tiet_dot_thi, DateTime thoi_gian_bat_dau, int ma_de_thi, int thoi_gian_thi)
        {
            return await _caThiRepository.Update(ma_ca_thi, ten_ca_thi, ma_chi_tiet_dot_thi, thoi_gian_bat_dau, ma_de_thi, thoi_gian_thi);
        }
    }
}
