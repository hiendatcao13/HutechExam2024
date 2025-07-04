using AutoMapper;
using Hutech.Exam.Server.DAL.Repositories;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.Models;
using System.Data;

namespace Hutech.Exam.Server.BUS
{
    public class DeThiHoanViService(IDeThiHoanViRepository deThiHoanViRepository)
    {
        #region Private Fields
        private readonly IDeThiHoanViRepository _deThiHoanViRepository = deThiHoanViRepository;
        #endregion

        #region Public Methods
        public async Task<List<DeThiHoanViDto>> SelectBy_MaDeThi(int ma_de_thi)
        {
            return await _deThiHoanViRepository.SelectBy_MaDeThi(ma_de_thi);
        }

        public async Task<Dictionary<int, int>> DapAn(long ma_de_hv)
        {
            return await _deThiHoanViRepository.DapAn(ma_de_hv);
        }
        #endregion

    }
}
