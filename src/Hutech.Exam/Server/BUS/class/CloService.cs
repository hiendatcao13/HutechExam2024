using AutoMapper;
using Hutech.Exam.Server.DAL.Repositories;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.Models;
using System.Data;

namespace Hutech.Exam.Server.BUS
{
    public class CloService
    {
        private readonly ICloRepository _cloRepository;
        private IMapper _mapper;
        public CloService(ICloRepository cloRepository, IMapper mapper)
        {
            _cloRepository = cloRepository;
            _mapper = mapper;
        }

        private CloDto getProperty(IDataReader dataReader)
        {
            Clo clo = new()
            {
                MaClo = dataReader.GetInt32(0),
                MaMonHoc = dataReader.GetInt32(1),
                MaSoClo = dataReader.GetString(2),
                TieuDe = dataReader.GetString(3),
                NoiDung = dataReader.IsDBNull(4) ? null : dataReader.GetString(4),
                TieuChi = dataReader.GetInt32(5),
                SoCau = dataReader.GetInt32(6)
            };
            return _mapper.Map<CloDto>(clo);
        }
        public async Task<CloDto> SelectOne(int ma_clo)
        {
            CloDto clo = new();
            using (IDataReader dataReader = await _cloRepository.SelectOne(ma_clo))
            {
                if (dataReader.Read())
                {
                    clo = getProperty(dataReader);
                }
            }
            return clo;
        }
        public async Task<int> Insert(int ma_mon_hoc, string ma_so_clo, string tieu_de, string noi_dung, int tieu_chi, int so_cau)
        {
            return Convert.ToInt32(await _cloRepository.Insert(ma_mon_hoc, ma_so_clo, tieu_de, noi_dung, tieu_chi, so_cau));
        }
        public async Task<int> Update(int ma_clo, int ma_mon_hoc, string ma_so_clo, string tieu_de, string noi_dung, int tieu_chi, int so_cau)
        {
            return await _cloRepository.Update(ma_clo, ma_mon_hoc, ma_so_clo, tieu_de, noi_dung, tieu_chi, so_cau);
        }
        public async Task<int> Remove(int ma_clo)
        {
            return await _cloRepository.Remove(ma_clo);
        }
        public async Task<List<CloDto>> SelectBy_MaMonHoc(int ma_mon_hoc)
        {
            List<CloDto> result = [];
            using (IDataReader dataReader = await _cloRepository.SelectBy_MaMonHoc(ma_mon_hoc))
            {
                while (dataReader.Read())
                {
                    CloDto clo = getProperty(dataReader);
                    result.Add(clo);
                }
            }
            return result;
        }
    }
}
