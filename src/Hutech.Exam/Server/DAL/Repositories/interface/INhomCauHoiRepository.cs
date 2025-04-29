using System.Data;

namespace Hutech.Exam.Server.DAL.Repositories
{
    public interface INhomCauHoiRepository
    {
        public Task<IDataReader> SelectAllBy_MaDeThi(int ma_de_thi);
        public Task<IDataReader> SelectOne(int ma_nhom);
        public Task<object?> Insert(int ma_de_thi, string ten_nhom, string noi_dung, int so_cau_hoi, bool hoan_vi, int thu_tu, int ma_nhom_cha, int so_cau_lay, bool la_cau_hoi_nhom);
        public Task<int> Update(int ma_nhom, int ma_de_thi, string ten_nhom, string noi_dung, int so_cau_hoi, bool hoan_vi, int thu_tu, int ma_nhom_cha);
        public Task<int> Remove(int ma_nhom);

    }
}
