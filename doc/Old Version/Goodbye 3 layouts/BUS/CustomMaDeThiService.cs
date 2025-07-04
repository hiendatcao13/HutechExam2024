using System.Data;
using Hutech.Exam.Server.DAL.Repositories;
using Hutech.Exam.Shared.DTO.Custom;

namespace Hutech.Exam.Server.BUS
{
    public class CustomMaDeThiService(ICustomMaDeThiRepository customMaDeThiRepository)
    {
        #region Private Fields
        private readonly ICustomMaDeThiRepository _customMaDeThiRepository = customMaDeThiRepository;
        #endregion

        #region Public Methods
        public async Task<List<CustomThongTinMaDeThi>> GetThongTinMaDeThi(int ma_de_thi)
        {
            return await _customMaDeThiRepository.LayMaThongTinDeThi(ma_de_thi);
        }
        #endregion

    }
}
