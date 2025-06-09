using System.Data;
using Hutech.Exam.Shared.DTO;

namespace Hutech.Exam.Server.DAL.Repositories
{
    public interface ICauHoiRepository
    {
        Task<CauHoiDto> SelectOne(int ma_cau_hoi);

        Task<List<CauHoiDto>> SelectBy_MaNhom(int ma_nhom);

        Task<int> Insert(int ma_clo, int ma_nhom, string tieu_de, int kieu_noi_dung, string noi_dung, int thu_tu, string ghi_chu, bool hoan_vi);

        Task<bool> Update(int ma_cau_hoi, int ma_nhom, int ma_clo, string tieu_de, int kieu_noi_dung, string noi_dung, int thu_tu, string ghi_chu, bool hoan_vi);

        Task<bool> Remove(int ma_cau_hoi);

        Task<bool> ForceRemove(int ma_nhom);
    }
}
