using Hutech.Exam.Server.DAL.DataReader;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.Models;
using System.Data;

namespace Hutech.Exam.Server.DAL.Repositories
{
    public interface INhomCauHoiHoanViRepository
    {

        NhomCauHoiHoanViDto GetProperty(IDataReader dataReader, int start = 0);

        Task<NhomCauHoiHoanViDto> SelectOne(long ma_de_hoan_vi, int ma_nhom);

        Task<List<NhomCauHoiHoanViDto>> SelectBy_MaDeHV(long ma_de_hoan_vi);
    }
}
