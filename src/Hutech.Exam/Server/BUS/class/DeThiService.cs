using AutoMapper;
using Hutech.Exam.Server.DAL.Repositories;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.Models;
using System.Data;

namespace Hutech.Exam.Server.BUS
{
    public class DeThiService
    {
        private readonly IDeThiRepository _deThiRepository;
        private readonly IMapper _mapper;
        public DeThiService(IDeThiRepository deThiRepository, IMapper mapper)
        {
            _deThiRepository = deThiRepository;
            _mapper = mapper;
        }
        private DeThiDto getProperty(IDataReader dataReader)
        {
            TblDeThi deThi = new()
            {
                MaDeThi = dataReader.GetInt32(0),
                MaMonHoc = dataReader.GetInt32(1),
                TenDeThi = dataReader.GetString(2),
                NgayTao = dataReader.GetDateTime(3),
                NguoiTao = dataReader.GetInt32(4),
                GhiChu = dataReader.IsDBNull(5) ? null : dataReader.GetString(5),
                LuuTam = dataReader.GetBoolean(6),
                DaDuyet = dataReader.GetBoolean(7),
                TongSoDeHoanVi = dataReader.IsDBNull(8) ? null : dataReader.GetInt32(8),
                BoChuongPhan = dataReader.GetBoolean(9)
            };
            return _mapper.Map<DeThiDto>(deThi);
        }
        public async Task<DeThiDto> SelectOne(int ma_de_thi)
        {
            DeThiDto deThi = new();
            using(IDataReader dataReader = await _deThiRepository.SelectOne(ma_de_thi))
            {
                if (dataReader.Read())
                {
                    deThi = getProperty(dataReader);
                }
            }
            return deThi;
        }
        public async Task<DeThiDto> SelectBy_ma_de_hv(long ma_de_hv)
        {
            DeThiDto deThi = new();
            using (IDataReader dataReader = await _deThiRepository.SelectBy_ma_de_hv(ma_de_hv))
            {
                if (dataReader.Read())
                {
                    deThi = getProperty(dataReader);
                }
            }
            return deThi;
        }
        public async Task<List<DeThiDto>> GetAll()
        {
            List<DeThiDto> result = new();
            using (IDataReader dataReader = await _deThiRepository.GetAll())
            {
                while (dataReader.Read())
                {
                    result.Add(getProperty(dataReader));
                }
            }
            return result;
        }
    }
}
