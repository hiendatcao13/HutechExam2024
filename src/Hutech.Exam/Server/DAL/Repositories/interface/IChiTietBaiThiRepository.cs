using Hutech.Exam.Shared.DTO;
using System.Data;

namespace Hutech.Exam.Server.DAL.Repositories
{
    public interface IChiTietBaiThiRepository
    {
        ChiTietBaiThiDto GetProperty(IDataReader dataReader, int start = 0);

        Task<int> Insert(int ma_chi_tiet_ca_thi, long MaDeThi, Guid MaNhom, Guid MaCauHoi, DateTime NgayTao, int ThuTu);
        
        Task Insert_Batch(List<ChiTietBaiThiDto> chiTietBaiThis);
        
        Task<bool> Update(long MaChiTietBaiThi, Guid CauTraLoi, DateTime NgayCapNhat, bool KetQua);
        
        Task<bool> Update_v2(int MaChiTietCaThi, Guid MaCauHoi, Guid CauTraLoi, DateTime NgayCapNhat, bool KetQua);
       
        Task<bool> Save(int MaChiTietCaThi, long MaDeThi, Guid MaNhom, Guid MaCauHoi, Guid CauTraLoi, DateTime NgayTao, DateTime NgayCapNhat, bool KetQua, int ThuTu);
       
        Task Save_Batch(List<ChiTietBaiThiDto> chiTietBaiThiDtos);
      
        Task<List<ChiTietBaiThiDto>> SelectBy_ma_chi_tiet_ca_thi(int ma_chi_tiet_ca_thi);
      
        Task<bool> Delete(long ma_chi_tiet_bai_thi);
      
        Task<ChiTietBaiThiDto> SelectOne_v2(int ma_chi_tiet_ca_thi, long maDeThi, Guid ma_nhom, Guid ma_cau_hoi);
      
    }
}
