using AutoMapper;
using Hutech.Exam.Server.DAL.Repositories;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.Models;
using System.Data;

namespace Hutech.Exam.Server.BUS
{
    public class DeThiService(IDeThiRepository deThiRepository, IMapper mapper)
    {
        private readonly IDeThiRepository _deThiRepository = deThiRepository;
        private readonly IMapper _mapper = mapper;

        public static readonly int COLUMN_LENGTH = 10; // số lượng cột trong bảng DeThi

        public DeThiDto GetProperty(IDataReader dataReader, int start = 0)
        {
            DeThi deThi = new()
            {
                MaDeThi = dataReader.GetInt32(0 + start),
                MaMonHoc = dataReader.GetInt32(1 + start),
                TenDeThi = dataReader.GetString(2 + start),
                NgayTao = dataReader.GetDateTime(3 + start),
                NguoiTao = dataReader.GetInt32(4 + start),
                GhiChu = dataReader.IsDBNull(5 + start) ? null : dataReader.GetString(5 + start),
                LuuTam = dataReader.GetBoolean(6 + start),
                DaDuyet = dataReader.GetBoolean(7 + start),
                TongSoDeHoanVi = dataReader.IsDBNull(8 + start) ? null : dataReader.GetInt32(8 + start),
                BoChuongPhan = dataReader.GetBoolean(9 + start)
            };
            return _mapper.Map<DeThiDto>(deThi);
        }
        public async Task<DeThiDto> SelectOne(int ma_de_thi)
        {
            DeThiDto deThi = new();
            using (IDataReader dataReader = await _deThiRepository.SelectOne(ma_de_thi))
            {
                if (dataReader.Read())
                {
                    deThi = GetProperty(dataReader);
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
                    deThi = GetProperty(dataReader);
                }
            }
            return deThi;
        }
        public async Task<List<DeThiDto>> GetAll()
        {
            List<DeThiDto> result = [];
            using (IDataReader dataReader = await _deThiRepository.GetAll())
            {
                while (dataReader.Read())
                {
                    result.Add(GetProperty(dataReader));
                }
            }
            return result;
        }
        public async Task<List<DeThiDto>> SelectByMonHoc(int ma_mon_hoc)
        {
            List<DeThiDto> result = [];
            using (IDataReader dataReader = await _deThiRepository.SelectByMonHoc(ma_mon_hoc))
            {
                while (dataReader.Read())
                {
                    result.Add(GetProperty(dataReader));
                }
            }
            return result;
        }
    }
}
