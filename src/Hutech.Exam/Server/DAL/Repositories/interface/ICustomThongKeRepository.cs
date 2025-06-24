using Hutech.Exam.Shared.DTO.Custom;
using System.Data;

namespace Hutech.Exam.Server.DAL.Repositories
{
    public interface ICustomThongKeRepository
    {
        CustomThongKeCauHoi GetPropertyCauHoi(IDataReader dataReader);

        Task<List<CustomThongKeCauHoi>> ThongKeCauHoi_SelectBy_DeThi(int MaDeThi);

        Task<List<CustomThongKeDiem>> ThongKeDiem_SelectBy_DeThi(int MaDeThi);
    }
}
