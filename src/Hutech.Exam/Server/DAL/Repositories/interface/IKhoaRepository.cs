using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.DTO.Page;
using System.Data;

namespace Hutech.Exam.Server.DAL.Repositories
{
    public interface IKhoaRepository
    {
        KhoaDto GetProperty(IDataReader dataReader, int start = 0);

        Task<KhoaDto> SelectOne(int ma_khoa);

        Task<int> Insert(string ten_khoa, DateTime ngay_thanh_lap);

        Task<bool> Update(int ma_khoa, string ten_khoa, DateTime ngay_thanh_lap);

        Task<bool> Remove(int ma_khoa);

        Task<bool> ForceRemove(int ma_khoa);

        Task<List<KhoaDto>> GetAll();

        Task<Paged<KhoaDto>> GetAll_Paged(int pageNumber, int pageSize);
    }
}
