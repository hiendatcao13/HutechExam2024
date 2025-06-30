using Hutech.Exam.Server.DAL.Repositories;
using Hutech.Exam.Shared.DTO.Custom;

namespace Hutech.Exam.Server.BUS
{
    public class SystemService(ICustomThongKeRepository customThongKeRepository, ISinhVienRepository sinhVienRepository)
    {
        private readonly ICustomThongKeRepository _customThongKeRepository = customThongKeRepository;
        private readonly ISinhVienRepository _sinhVienRepository = sinhVienRepository;

        public async Task<List<CustomThongKeDoPhanManh>> ThongKeDoPhanManh()
        {
            return await _customThongKeRepository.ThongKeDoPhanManh();
        }

        public async Task RebuildOrReorganizeChiMuc()
        {
            await _customThongKeRepository.RebuildOrReorganizeChiMuc();
        }

        public async Task<long> GetTotalOnlineStudent()
        {
            return await _sinhVienRepository.LoginCount();
        }
    }
}
