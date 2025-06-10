using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.DTO.Page;
using System.Data;

namespace Hutech.Exam.Server.DAL.Repositories
{
    public interface IDeThiRepository
    {
        DeThiDto GetProperty(IDataReader dataReader, int start = 0);

        Task<int> Insert(int ma_mon_hoc, string ten_de_thi, DateTime ngay_tao, int nguoi_tao, string ghi_chu, bool bo_chuong_phan);

        Task<bool> Update(int ma_de_thi, int ma_mon_hoc, string ten_de_thi, DateTime ngay_tao, int nguoi_tao, string ghi_chu, bool bo_chuong_phan);

        Task<bool> Delete(int ma_de_thi);

        Task<bool> ForceDelete(int ma_de_thi);

        Task<DeThiDto> SelectOne(int ma_de_thi);

        Task<DeThiDto> SelectBy_ma_de_hv(long ma_de_hv);

        Task<List<DeThiDto>> GetAll();

        Task<List<DeThiDto>> SelectByMonHoc(int ma_mon_hoc);

        Task<Paged<DeThiDto>> SelectByMonHoc_Paged(int ma_mon_hoc, int pageNumber, int pageSize);

    }
}
