using Hutech.Exam.Server.DAL.Repositories;
using Hutech.Exam.Shared.DTO.Custom;
using System.Data;

namespace Hutech.Exam.Server.BUS
{
    public class CustomDeThiService(ICustomDeThiRepository customRepository)
    {
        private readonly ICustomDeThiRepository _customRepository = customRepository;

        public async Task<List<CustomDeThi>> GetDeThi(long ma_de_hoan_vi)
        {
            return await _customRepository.GetDeThi(ma_de_hoan_vi);
        }

    }
}
