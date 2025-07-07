using Hutech.Exam.Server.DAL.Repositories;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.DTO.Page;
using Hutech.Exam.Shared.DTO.Request.CaThi;

namespace Hutech.Exam.Server.BUS
{
    public class CaThiService(ICaThiRepository caThiRepository, BcryptService bcryptService)
    {
        #region Private Fields

        private readonly ICaThiRepository _caThiRepository = caThiRepository;
        private readonly BcryptService _bcryptService = bcryptService;

        #endregion

        #region Public Methods

        public async Task<List<CaThiDto>> SelectBy_MaDotThi_MaLop_LanThi(int ma_dot_thi, int ma_lop, int lan_thi)
        {
            return await _caThiRepository.SelectBy_MaDotThi_MaLop_LanThi(ma_dot_thi, ma_lop, lan_thi);
        }

        public async Task<Paged<CaThiDto>> SelectBy_ma_chi_tiet_dot_thi_Paged(int ma_chi_tiet_dot_thi, int pageNumber, int pageSize)
        {
            return await _caThiRepository.SelectBy_ma_chi_tiet_dot_thi_Paged(ma_chi_tiet_dot_thi, pageNumber, pageSize);
        }

        public async Task<Paged<CaThiDto>> SelectBy_ma_chi_tiet_dot_thi_Search_Paged(int ma_chi_tiet_dot_thi, string keyword, int pageNumber, int pageSize)
        {
            return await _caThiRepository.SelectBy_ma_chi_tiet_dot_thi_Search_Paged(ma_chi_tiet_dot_thi, keyword, pageNumber, pageSize);
        }

        public async Task<CaThiDto> SelectOne(int ma_ca_thi)
        {
            return await _caThiRepository.SelectOne(ma_ca_thi);
        }
        public async Task<List<CaThiDto>> GetAll()
        {
            return await _caThiRepository.GetAll();
        }
        public async Task<bool> Activate(int ma_ca_thi, bool IsActivated, string lichSuHoatDong)
        {
            return await _caThiRepository.KichHoat(ma_ca_thi, IsActivated, lichSuHoatDong);
        }

        public async Task<bool> HuyKichHoat(int ma_ca_thi, string lichSuHoatDong)
        {
            return await _caThiRepository.HuyKichHoat(ma_ca_thi, lichSuHoatDong);
        }

        public async Task<bool> Ketthuc(int ma_ca_thi, string lichSuHoatDong)
        {
            return await _caThiRepository.Ketthuc(ma_ca_thi, lichSuHoatDong);
        }

        public async Task<int> Insert(CaThiCreateRequest caThi)
        {
            if(!string.IsNullOrWhiteSpace(caThi.MatMa))
            {
                caThi.MatMa = _bcryptService.HashPassword(caThi.MatMa, 10);
            }  
            
            return await _caThiRepository.Insert(caThi.TenCaThi, caThi.MaChiTietDotThi, caThi.ThoiGianBatDau, caThi.ThoiGianThi, caThi.MatMa);
        }

        public async Task<bool> Remove(int ma_ca_thi)
        {
            return await _caThiRepository.Remove(ma_ca_thi);
        }

        public async Task<bool> ForceRemove(int ma_ca_thi)
        {
            return await _caThiRepository.ForceRemove(ma_ca_thi);
        }

        public async Task<bool> Update(int id, CaThiUpdateRequest caThi)
        {
            if (!string.IsNullOrWhiteSpace(caThi.MatMa))
            {
                caThi.MatMa = _bcryptService.HashPassword(caThi.MatMa, 10);
            }

            return await _caThiRepository.Update(id, caThi.TenCaThi, caThi.MaChiTietDotThi, caThi.ThoiGianBatDau, caThi.ThoiGianThi, caThi.MatMa);
        }

        public async Task<bool> UpdateDeThi(int id, int maDeThi, bool isOrderMSSV, List<long> dsDeThiHVs)
        {
            return await _caThiRepository.UpdateDeThi(id, maDeThi, isOrderMSSV, dsDeThiHVs);
        }

        public async Task<bool> UpdateLichSuHoatDong(int ma_ca_thi, string lichSuHoatDong)
        {
            return await _caThiRepository.UpdateLichSuHoatDong(ma_ca_thi, lichSuHoatDong);
        }

        public async Task<bool> DuyetDe(int ma_ca_thi, string lichSuHoatDong)
        {
            return await _caThiRepository.DuyetDe(ma_ca_thi, lichSuHoatDong);
        }
        #endregion
    }
}
