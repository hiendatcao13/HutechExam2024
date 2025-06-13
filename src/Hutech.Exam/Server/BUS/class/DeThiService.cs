using AutoMapper;
using Hutech.Exam.Server.DAL.Repositories;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.DTO.Page;
using Hutech.Exam.Shared.DTO.Request.DeThi;
using Hutech.Exam.Shared.Models;
using System.Data;

namespace Hutech.Exam.Server.BUS
{
    public class DeThiService(IDeThiRepository deThiRepository)
    {
        #region Private Fields
        private readonly IDeThiRepository _deThiRepository = deThiRepository;
        #endregion

        #region Public Methods
        public async Task<int> Insert(DeThiCreateRequest deThi)
        {
            return await _deThiRepository.Insert(deThi.MaMonHoc, deThi.TenDeThi, deThi.NgayTao, deThi.NguoiTao, deThi.GhiChu ?? string.Empty, deThi.BoChuongPhan);
        }

        public async Task<bool> Update(int id, DeThiUpdateRequest deThi)
        {
            return (await _deThiRepository.Update(id, deThi.MaMonHoc, deThi.TenDeThi, deThi.NgayTao, deThi.NguoiTao, deThi.GhiChu ?? string.Empty, deThi.BoChuongPhan));
        }

        public async Task<bool> Delete(int ma_de_thi)
        {
            return (await _deThiRepository.Delete(ma_de_thi));
        }

        public async Task<bool> ForceDelete(int ma_de_thi)
        {
            return (await _deThiRepository.ForceDelete(ma_de_thi));
        }

        public async Task<DeThiDto> SelectOne(int ma_de_thi)
        {
            return await _deThiRepository.SelectOne(ma_de_thi);
        }

        public async Task<DeThiDto> SelectBy_ma_de_hv(long ma_de_hv)
        {
            return await _deThiRepository.SelectBy_ma_de_hv(ma_de_hv);
        }

        public async Task<List<DeThiDto>> GetAll()
        {
            return await _deThiRepository.GetAll();
        }

        public async Task<List<DeThiDto>> SelectByMonHoc(int ma_mon_hoc)
        {
            return await _deThiRepository.SelectByMonHoc(ma_mon_hoc);
        }

        public async Task<Paged<DeThiDto>> SelectByMonHoc_Paged(int ma_mon_hoc, int pageNumber, int pageSize)
        {
            return await _deThiRepository.SelectByMonHoc_Paged(ma_mon_hoc, pageNumber, pageSize);
        }
        #endregion

    }
}
