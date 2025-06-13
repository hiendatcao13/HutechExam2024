using AutoMapper;
using Hutech.Exam.Server.DAL.Repositories;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.DTO.Request.ChiTietDeThiHoanVi;
using Hutech.Exam.Shared.Models;
using System.Data;

namespace Hutech.Exam.Server.BUS
{
    public class ChiTietDeThiHoanViService(IChiTietDeThiHoanViRepository chiTietDeThiHoanViRepository)
    {
        #region Private Fields
        private readonly IChiTietDeThiHoanViRepository _chiTietDeThiHoanViRepository = chiTietDeThiHoanViRepository;
        #endregion

        #region Public Methods
        public async Task Insert_Batch(int maDeThi, string kyHieuDe, List<ChiTietDeThiHoanViCreateBatchRequest> chiTietDeThiHoanVis)
        {
            await _chiTietDeThiHoanViRepository.Insert_Batch(maDeThi, kyHieuDe, chiTietDeThiHoanVis);
        }

        public async Task<List<ChiTietDeThiHoanViDto>> SelectBy_MaDeHV(long maDeHV)
        {
            return await _chiTietDeThiHoanViRepository.SelectBy_MaDeHV(maDeHV);
        }

        public async Task<List<ChiTietDeThiHoanViDto>> SelectBy_MaDeHV_MaNhom(long ma_de_hoan_vi, int ma_nhom)
        {
            return await _chiTietDeThiHoanViRepository.SelectBy_MaDeHV_MaNhom(ma_de_hoan_vi, ma_nhom);
        }
        #endregion
    }
}
