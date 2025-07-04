using Hutech.Exam.Server.DAL.Repositories;
using Hutech.Exam.Shared.DTO.Custom;

namespace Hutech.Exam.Server.BUS
{
    public class CustomThongKeService(ICustomThongKeRepository customThongKeRepository)
    {
        #region Private Fields
        private ICustomThongKeRepository _customThongKeRepository = customThongKeRepository;

        #endregion

        #region Public Methods
        public async Task<List<CustomThongKeCauHoi>> ThongKeCauHoi_SelectBy_DeThi(int maDeThi)
        {
            return await _customThongKeRepository.ThongKeCauHoi_SelectBy_DeThi(maDeThi);
        }

        public async Task<List<CustomThongKeDiem>> ThongKeDiem_SelectBy_DeThi(int maDeThi)
        {
            return await _customThongKeRepository.ThongKeDiem_SelectBy_DeThi(maDeThi);
        }

        public async Task<CustomThongKeCapBacSV> ThongKeCapBacSV_SelectBy_DeThi(int maDeThi)
        {
            return await _customThongKeRepository.ThongKeCapBacSV_SelectBy_DeThi(maDeThi);
        }

        #endregion
    }
}
