using AutoMapper;
using Hutech.Exam.Server.DAL.Repositories;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.DTO.Request.Clo;
using Hutech.Exam.Shared.Models;
using System.Data;

namespace Hutech.Exam.Server.BUS
{
    public class CloService(ICloRepository cloRepository, IMapper mapper)
    {
        private readonly ICloRepository _cloRepository = cloRepository;
        private IMapper _mapper = mapper;

        public static readonly int COLUMN_LENGTH = 7; // số lượng cột trong bảng Clo

        public CloDto GetProperty(IDataReader dataReader, int start = 0)
        {
            Clo clo = new()
            {
                MaClo = dataReader.GetInt32(0 + start),
                MaMonHoc = dataReader.GetInt32(1 + start),
                MaSoClo = dataReader.GetString(2 + start),
                TieuDe = dataReader.GetString(3 + start),
                NoiDung = dataReader.IsDBNull(4 + start) ? null : dataReader.GetString(4 + start),
                TieuChi = dataReader.GetInt32(5 + start),
                SoCau = dataReader.GetInt32(6 + start)
            };
            return _mapper.Map<CloDto>(clo);
        }

        public async Task<CloDto> SelectOne(int ma_clo)
        {
            return await _cloRepository.SelectOne(ma_clo);
        }

        public async Task<int> Insert(CloCreateRequest clo)
        {
            return await _cloRepository.Insert(clo.MaMonHoc, clo.MaSoClo, clo.TieuDe, clo.NoiDung, clo.TieuChi, clo.SoCau);
        }

        public async Task<bool> Update(int id, CloUpdateRequest clo)
        {
            return await _cloRepository.Update(id, clo.MaMonHoc, clo.MaSoClo, clo.TieuDe, clo.NoiDung, clo.TieuChi, clo.SoCau);
        }

        public async Task<bool> Remove(int ma_clo)
        {
            return await _cloRepository.Remove(ma_clo);
        }
        public async Task<bool> ForceRemove(int ma_clo)
        {
            return await _cloRepository.ForceRemove(ma_clo);
        }

        public async Task<List<CloDto>> SelectBy_MaMonHoc(int ma_mon_hoc)
        {
            return await _cloRepository.SelectBy_MaMonHoc(ma_mon_hoc);
        }
    }
}
