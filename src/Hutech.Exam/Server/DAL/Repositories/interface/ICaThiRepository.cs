using System.Data;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.DTO.Page;

namespace Hutech.Exam.Server.DAL.Repositories
{
    public interface ICaThiRepository
    {
        Task<Paged<CaThiDto>> SelectBy_ma_chi_tiet_dot_thi_Paged(int ma_chi_tiet_dot_thi, int pageNumber, int pageSize);

        Task<Paged<CaThiDto>> SelectBy_ma_chi_tiet_dot_thi_Search_Paged(int ma_chi_tiet_dot_thi, string keyword, int pageNumber, int pageSize);

        Task<CaThiDto> SelectOne(int ma_ca_thi);

        Task<List<CaThiDto>> GetAll();

        Task<bool> Activate(int ma_ca_thi, bool IsActivated);

        Task<bool> HuyKichHoat(int ma_ca_thi);

        Task<bool> Ketthuc(int ma_ca_thi);

        Task<int> Insert(string ten_ca_thi, int ma_chi_tiet_dot_thi, DateTime thoi_gian_bat_dau, int ma_de_thi, int thoi_gian_thi);

        Task<bool> Remove(int ma_ca_thi);

        Task<bool> ForceRemove(int ma_ca_thi);

        Task<bool> Update(int ma_ca_thi, string ten_ca_thi, int ma_chi_tiet_dot_thi, DateTime thoi_gian_bat_dau, int ma_de_thi, int thoi_gian_thi);

        Task<List<CaThiDto>> SelectBy_MaDotThi_MaLop_LanThi(int ma_dot_thi, int ma_lop, int lan_thi);
    }
}
