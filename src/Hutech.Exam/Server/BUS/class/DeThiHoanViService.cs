using AutoMapper;
using Hutech.Exam.Server.DAL.Repositories;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.Models;
using System.Data;

namespace Hutech.Exam.Server.BUS
{
    public class DeThiHoanViService
    {
        private readonly IDeThiHoanViRepository _deThiHoanViRepository;
        private readonly IMapper _mapper;
        public DeThiHoanViService(IDeThiHoanViRepository deThiHoanViRepository, IMapper mapper)
        {
            _deThiHoanViRepository = deThiHoanViRepository;
            _mapper = mapper;
        }
        private DeThiHoanViDto getProperty(IDataReader dataReader)
        {
            TblDeThiHoanVi deThiHoanVi = new()
            {
                MaDeHv = dataReader.GetInt64(0),
                MaDeThi = dataReader.GetInt32(1),
                KyHieuDe = dataReader.IsDBNull(2) ? null : dataReader.GetString(2),
                NgayTao = dataReader.GetDateTime(3),
                Guid = dataReader.IsDBNull(4) ? null : dataReader.GetGuid(4)
            };
            return _mapper.Map<DeThiHoanViDto>(deThiHoanVi);
        }
        //public TblDeThiHoanVi SelectOne(long ma_de_hoan_vi)
        //{
        //    TblDeThiHoanVi deThiHoanVi = new TblDeThiHoanVi();
        //    using(IDataReader dataReader = _deThiHoanViRepository.SelectOne(ma_de_hoan_vi))
        //    {
        //        if (dataReader.Read())
        //        {
        //            deThiHoanVi = getProperty(dataReader);
        //        }
        //        TblDeThi deThi = _deThiService.SelectOne(deThiHoanVi.MaDeThi);
        //        // trường đặc biệt maDeThiNavigation - đối tượng là Đề thi
        //        deThiHoanVi.MaDeThiNavigation = deThi;
        //    }
        //    return deThiHoanVi;
        //}
        public async Task<List<DeThiHoanViDto>> SelectBy_MaDeThi(int ma_de_thi)
        {
            List<DeThiHoanViDto> deThiHoanVis = new();
            using (IDataReader dataReader = await _deThiHoanViRepository.SelectBy_MaDeThi(ma_de_thi))
            {
                while (dataReader.Read())
                {
                    DeThiHoanViDto deThiHoanVi = getProperty(dataReader);
                    deThiHoanVi.MaDeThiNavigation = new();
                    deThiHoanVis.Add(getProperty(dataReader));
                }
                
            }
            return deThiHoanVis;
        }
    }
}
