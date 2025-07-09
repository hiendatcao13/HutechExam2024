using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.DTO.Page;
using Hutech.Exam.Shared.DTO.Request.DeThi;
using System.Data;

namespace Hutech.Exam.Server.DAL.Repositories
{
    public interface IDeThiRepository
    {
        DeThiDto GetProperty(IDataReader dataReader, int start = 0);

        Task<int> Insert(int ma_mon_hoc, string ten_de_thi, Guid guid, DateTime ngay_tao, string ky_hieu_de);

        Task<bool> Update(long ma_de_thi, int ma_mon_hoc, string ten_de_thi, Guid guid, string ky_hieu_de);

        Task Save_Batch(List<DeThiDto> deThis);

        Task<bool> Delete(long ma_de_thi);

        Task<bool> ForceDelete(long ma_de_thi);

        Task<DeThiDto> SelectOne(long ma_de_thi);

        Task<DeThiDto> SelectBy_ma_de_hv(long ma_de_hv);

        Task<List<DeThiDto>> GetAll();

        Task<List<DeThiDto>> SelectByMonHoc(int ma_mon_hoc);

        Task<Paged<DeThiDto>> SelectByMonHoc_Paged(int ma_mon_hoc, int pageNumber, int pageSize);

    }
}
