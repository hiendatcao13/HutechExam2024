using System.Data;
using Hutech.Exam.Shared.DTO;

namespace Hutech.Exam.Server.DAL.Repositories
{
    public interface IChiTietDeThiHoanViRepository
    {
        Task<List<ChiTietDeThiHoanViDto>> SelectBy_MaDeHV(long maDeHV);

        Task<List<ChiTietDeThiHoanViDto>> SelectBy_MaDeHV_MaNhom(long ma_de_hoan_vi, int ma_nhom);
    }
}
