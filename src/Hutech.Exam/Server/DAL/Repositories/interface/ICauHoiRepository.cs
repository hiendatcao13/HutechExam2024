using System.Data;

namespace Hutech.Exam.Server.DAL.Repositories
{
    public interface ICauHoiRepository
    {
        public Task<IDataReader> SelectOne(int ma_cau_hoi);
        public Task<IDataReader> SelectDapAn(int ma_cau_hoi);
        public Task<IDataReader> SelectBy_MaNhom(int ma_nhom);
        public Task<object?> Insert(int ma_clo, int ma_nhom, string tieu_de, int kieu_noi_dung, string noi_dung, string ghi_chu, bool hoan_vi);
        public Task<int> Update(int ma_cau_hoi, int ma_nhom, int ma_clo, string tieu_de, int kieu_noi_dung, string noi_dung, string ghi_chu, bool hoan_vi);

        public Task<int> Remove(int ma_cau_hoi);
    }
}
