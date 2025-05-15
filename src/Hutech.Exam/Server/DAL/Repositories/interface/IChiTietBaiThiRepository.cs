using Hutech.Exam.Shared.DTO;
using System.Data;

namespace Hutech.Exam.Server.DAL.Repositories
{
    public interface IChiTietBaiThiRepository
    {
        public Task<object?> Insert(int ma_chi_tiet_ca_thi, long MaDeHV, int MaNhom, int MaCauHoi, int MaClo, DateTime NgayTao, int ThuTu);
        public Task<int> Update(long MaChiTietBaiThi, int CauTraLoi, DateTime NgayCapNhat, bool KetQua);
        public Task<int> Update_v2(int MaChiTietCaThi, int MaCauHoi, int MaClo, int CauTraLoi, DateTime NgayCapNhat, bool KetQua);
        public Task<int> Save(int MaChiTietCaThi, long MaDeHV, int MaNhom, int MaCauHoi, int MaClo, int CauTraLoi, DateTime NgayTao, DateTime NgayCapNhat, bool KetQua, int ThuTu);
        public Task<int> Save_Batch(List<ChiTietBaiThiDto> chiTietBaiThiDtos);
        public Task<IDataReader> SelectBy_ma_chi_tiet_ca_thi(int ma_chi_tiet_ca_thi);
        public Task<int> Delete(long ma_chi_tiet_bai_thi);
        public Task<IDataReader> SelectOne_v2(int ma_chi_tiet_ca_thi, long ma_de_hv, int ma_nhom, int ma_cau_hoi);
        public Task<string> DaThi(int ma_chi_tiet_ca_thi);
    }
}
