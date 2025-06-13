using AutoMapper;
using Hutech.Exam.Server.DAL.Repositories;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.DTO.Request.LopAo;
using Hutech.Exam.Shared.Models;
using System.Data;
using System.Data.Common;

namespace Hutech.Exam.Server.BUS
{
    public class LopAoService(ILopAoRepository lopAoRepository)
    {
        #region Private Fields
        private readonly ILopAoRepository _lopAoRepository = lopAoRepository;
        #endregion

        #region Public Methods
        public async Task<LopAoDto> SelectOne(int ma_lop_ao)
        {
            return await _lopAoRepository.SelectOne(ma_lop_ao);
        }

        public async Task<List<LopAoDto>> SelectBy_ma_mon_hoc(int ma_mon_hoc)
        {
            return await _lopAoRepository.SelectBy_ma_mon_hoc(ma_mon_hoc);
        }

        public async Task<List<LopAoDto>> SelectBy_ListChiTietDotThi(List<ChiTietDotThi> list)
        {
            return await _lopAoRepository.SelectBy_ma_mon_hoc(list.FirstOrDefault()?.MaLopAo ?? 0);
        }

        public async Task<List<LopAoDto>> GetAll()
        {
            return await _lopAoRepository.GetAll();
        }

        public async Task<int> Insert(LopAoCreateRequest lopAo)
        {
            return await _lopAoRepository.Insert(lopAo.TenLopAo, lopAo.NgayBatDau, lopAo.MaMonHoc);
        }

        public async Task<bool> Update(int id, LopAoUpdateRequest lopAo)
        {
            return await _lopAoRepository.Update(id, lopAo.TenLopAo, lopAo.NgayBatDau, lopAo.MaMonHoc);
        }

        public async Task<bool> Remove(int ma_lop_ao)
        {
            return await _lopAoRepository.Remove(ma_lop_ao);
        }

        public async Task<bool> ForceRemove(int ma_lop_ao)
        {
            return await _lopAoRepository.ForceRemove(ma_lop_ao);
        }

        #endregion

    }
}
