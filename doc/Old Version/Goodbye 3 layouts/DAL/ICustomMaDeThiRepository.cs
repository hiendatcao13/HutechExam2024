using Hutech.Exam.Shared.DTO.Custom;
using System.Data;

namespace Hutech.Exam.Server.DAL.Repositories
{
    public interface ICustomMaDeThiRepository
    {
        CustomThongTinMaDeThi GetProperty(IDataReader dataReader, int start = 0);

        Task<List<CustomThongTinMaDeThi>> LayMaThongTinDeThi(long ma_de_thi);
    }
}
