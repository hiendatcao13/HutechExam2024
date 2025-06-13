using AutoMapper;
using Hutech.Exam.Server.DAL.Repositories;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.DTO.Page;
using Hutech.Exam.Shared.DTO.Request.ChiTietDotThi;
using Hutech.Exam.Shared.Models;
using System.Collections.Generic;
using System.Data;

namespace Hutech.Exam.Server.BUS
{
    public class ChiTietDotThiService(IChiTietDotThiRepository chiTietDotThiRepository)
    {
        #region Private Fields
        private readonly IChiTietDotThiRepository _chiTietDotThiResposity = chiTietDotThiRepository;
        #endregion

        #region Public Methods
        public async Task<List<ChiTietDotThiDto>> SelectBy_MaDotThi(int ma_dot_thi)
        {
            return await _chiTietDotThiResposity.SelectBy_MaDotThi(ma_dot_thi);
        }

        public async Task<Paged<ChiTietDotThiDto>> SelectBy_MaDotThi_Paged(int ma_dot_thi, int pageNumber, int pageSize)
        {
            return await _chiTietDotThiResposity.SelectBy_MaDotThi_Paged(ma_dot_thi, pageNumber, pageSize);
        }

        public async Task<List<ChiTietDotThiDto>> SelectBy_MaDotThi_MaLopAo(int ma_dot_thi, int ma_lop_ao)
        {
            return await _chiTietDotThiResposity.SelectBy_MaDotThi_MaLopAo(ma_dot_thi, ma_lop_ao);
        }

        public async Task<ChiTietDotThiDto> SelectBy_MaDotThi_MaLopAo_LanThi(int ma_dot_thi, int ma_lop_ao, int lan_thi)
        {
            return await _chiTietDotThiResposity.SelectBy_MaDotThi_MaLopAo_LanThi(ma_dot_thi, ma_lop_ao, lan_thi);
        }
        public async Task<ChiTietDotThiDto> SelectOne(int ma_chi_tiet_dot_thi)
        {
            return await _chiTietDotThiResposity.SelectOne(ma_chi_tiet_dot_thi);
        }
        public async Task<List<ChiTietDotThiDto>> GetAll()
        {
            return await _chiTietDotThiResposity.GetAll();
        }
        public async Task<int> Insert(ChiTietDotThiCreateRequest chiTietDotThi)
        {
            return await _chiTietDotThiResposity.Insert(chiTietDotThi.TenChiTietDotThi, chiTietDotThi.MaLopAo, chiTietDotThi.MaDotThi, chiTietDotThi.LanThi);
        }
        public async Task<bool> Update(int id, ChiTietDotThiUpdateRequest chiTietDotThi)
        {
            return await _chiTietDotThiResposity.Update(id, chiTietDotThi.TenChiTietDotThi, chiTietDotThi.MaLopAo, chiTietDotThi.MaDotThi, chiTietDotThi.LanThi);
        }
        public async Task<bool> Remove(int ma_chi_tiet_dot_thi)
        {
            return await _chiTietDotThiResposity.Remove(ma_chi_tiet_dot_thi);
        }

        public async Task<bool> ForceRemove(int ma_chi_tiet_dot_thi)
        {
            return await _chiTietDotThiResposity.ForceRemove(ma_chi_tiet_dot_thi);
        }
        #endregion

    }
}
