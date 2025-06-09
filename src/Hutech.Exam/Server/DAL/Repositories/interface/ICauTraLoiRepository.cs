using System.Data;
using Hutech.Exam.Shared.DTO;

namespace Hutech.Exam.Server.DAL.Repositories
{
    public interface ICauTraLoiRepository
    {
        Task<CauTraLoiDto> SelectOne(int ma_cau_tra_loi);

        Task<List<CauTraLoiDto>> SelectBy_MaCauHoi(int ma_cau_hoi);

        Task<int> Insert(int ma_cau_hoi, int thu_tu, string noi_dung, bool la_dap_an, bool hoan_vi);

        Task<bool> Update(int ma_cau_tra_loi, int ma_cau_hoi, int thu_tu, string noi_dung, bool la_dap_an, bool hoan_vi);

        Task<bool> Remove(int ma_cau_tra_loi);
    }
}
