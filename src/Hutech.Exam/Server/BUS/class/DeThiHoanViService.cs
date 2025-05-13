using AutoMapper;
using Hutech.Exam.Server.DAL.Repositories;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.Models;
using System.Data;

namespace Hutech.Exam.Server.BUS
{
    public class DeThiHoanViService(IDeThiHoanViRepository deThiHoanViRepository, IMapper mapper)
    {
        private readonly IDeThiHoanViRepository _deThiHoanViRepository = deThiHoanViRepository;
        private readonly IMapper _mapper = mapper;

        public static readonly int COLUMN_LENGTH = 5; // số lượng cột trong bảng DeThiHoanVi

        public DeThiHoanViDto GetProperty(IDataReader dataReader, int start = 0)
        {
            DeThiHoanVi deThiHoanVi = new()
            {
                MaDeHv = dataReader.GetInt64(0 + start),
                MaDeThi = dataReader.GetInt32(1 + start),
                KyHieuDe = dataReader.IsDBNull(2 + start) ? null : dataReader.GetString(2 + start),
                NgayTao = dataReader.GetDateTime(3 + start),
                Guid = dataReader.IsDBNull(4 + start) ? null : dataReader.GetGuid(4 + start)
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
                    DeThiHoanViDto deThiHoanVi = GetProperty(dataReader);
                    deThiHoanVi.MaDeThiNavigation = new();
                    deThiHoanVis.Add(GetProperty(dataReader));
                }
                
            }
            return deThiHoanVis;
        }
    }
}
