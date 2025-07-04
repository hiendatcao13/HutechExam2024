using Hutech.Exam.Shared.DTO;
using System.Data;

namespace Hutech.Exam.Server.DAL.Repositories
{
    public interface INhomCauHoiRepository
    {
        NhomCauHoiDto GetProperty(IDataReader dataReader, int start = 0);
        Task<List<NhomCauHoiDto>> SelectAllBy_MaDeThi(int ma_de_thi);

        Task<NhomCauHoiDto> SelectOne(int ma_nhom);

        Task<int> Insert(int ma_de_thi, string ten_nhom, int kieu_noi_dung, string noi_dung, int so_cau_hoi, bool hoan_vi, int thu_tu, int ma_nhom_cha, int so_cau_lay, bool la_cau_hoi_nhom);

        Task<bool> Update(int ma_nhom, int ma_de_thi, string ten_nhom, int kieu_noi_dung, string noi_dung, int so_cau_hoi, bool hoan_vi, int thu_tu, int ma_nhom_cha);

        Task<bool> Remove(int ma_nhom);

        Task<bool> ForceRemove(int ma_nhom);

    }
}
