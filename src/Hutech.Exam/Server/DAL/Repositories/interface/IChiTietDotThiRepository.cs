using Hutech.Exam.Server.DAL.DataReader;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.DTO.Page;
using System.Data;

namespace Hutech.Exam.Server.DAL.Repositories
{
    public interface IChiTietDotThiRepository
    {
        ChiTietDotThiDto GetProperty(IDataReader dataReader, int start = 0);

        Task<List<ChiTietDotThiDto>> SelectBy_MaDotThi(int ma_dot_thi);

        Task<Paged<ChiTietDotThiDto>> SelectBy_MaDotThi_Paged(int ma_dot_thi, int pageNumber, int pageSize);

        Task<List<ChiTietDotThiDto>> SelectBy_MaDotThi_MaLopAo(int ma_dot_thi, int ma_lop_ao);

        Task<ChiTietDotThiDto> SelectBy_MaDotThi_MaLopAo_LanThi(int ma_dot_thi, int ma_lop_ao, int lan_thi);

        Task<ChiTietDotThiDto> SelectOne(int ma_chi_tiet_dot_thi);

        Task<List<ChiTietDotThiDto>> GetAll();

        Task<int> Insert(string ten_chi_tiet_dot_thi, int ma_lop_ao, int ma_dot_thi, int lan_thi);

        Task<bool> Update(int ma_chi_tiet_dot_thi, string ten_chi_tiet_dot_thi, int ma_lop_ao, int ma_dot_thi, int lan_thi);

        Task<bool> Remove(int ma_chi_tiet_dot_thi);

        Task<bool> ForceRemove(int ma_chi_tiet_dot_thi);
    }
}
