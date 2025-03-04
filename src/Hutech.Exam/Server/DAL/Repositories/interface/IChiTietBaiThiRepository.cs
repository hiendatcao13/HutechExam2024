using System.Data;

namespace Hutech.Exam.Server.DAL.Repositories
{
    public interface IChiTietBaiThiRepository
    {
        public Task<object?> Insert(int ma_chi_tiet_ca_thi, long MaDeHV, int MaNhom, int MaCauHoi, DateTime NgayTao, int ThuTu);
        public Task<int> Update(long MaChiTietBaiThi, int CauTraLoi, DateTime NgayCapNhat, bool KetQua);
        public Task<IDataReader> SelectBy_ma_chi_tiet_ca_thi(int ma_chi_tiet_ca_thi);
        public Task<int> Delete(long ma_chi_tiet_bai_thi);
        public Task<IDataReader> SelectOne_v2(int ma_chi_tiet_ca_thi, long ma_de_hv, int ma_nhom, int ma_cau_hoi);
    }
}
