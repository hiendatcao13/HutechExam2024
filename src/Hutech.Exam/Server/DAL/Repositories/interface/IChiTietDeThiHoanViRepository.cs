using System.Data;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.DTO.Request.ChiTietDeThiHoanVi;

namespace Hutech.Exam.Server.DAL.Repositories
{
    public interface IChiTietDeThiHoanViRepository
    {
        ChiTietDeThiHoanViDto GetProperty(IDataReader dataReader, int start = 0);

        Task Insert_Batch(int maDeThi, string kyHieuDe, int soLuongDe, List<ChiTietDeThiHoanViCreateBatchRequest> chiTietDeThiHoanVis);

        Task<List<ChiTietDeThiHoanViDto>> SelectBy_MaDeHV(long maDeHV);

        Task<List<ChiTietDeThiHoanViDto>> SelectBy_MaDeHV_MaNhom(long ma_de_hoan_vi, int ma_nhom);
    }
}
