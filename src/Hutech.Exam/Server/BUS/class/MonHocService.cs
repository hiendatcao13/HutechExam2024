using AutoMapper;
using Hutech.Exam.Server.DAL.Repositories;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.Models;
using System.Data;
using System.Data.Common;

namespace Hutech.Exam.Server.BUS
{
    public class MonHocService
    {
        private readonly IMonHocRepository _monHocRepository;
        private readonly IMapper _mapper;
        public MonHocService(IMonHocRepository monHocRepository, IMapper mapper)
        {
            _monHocRepository = monHocRepository;
            _mapper = mapper;
        }
        private MonHocDto getProperty(IDataReader dataReader)
        {
            MonHoc monHoc = new()
            {
                MaMonHoc = dataReader.GetInt32(0),
                MaSoMonHoc = dataReader.IsDBNull(1) ? null : dataReader.GetString(1),
                TenMonHoc = dataReader.IsDBNull(2) ? null : dataReader.GetString(2)
            };
            return _mapper.Map<MonHocDto>(monHoc);
        }
        public MonHocDto SelectOne(int ma_mon_hoc)
        {
            MonHocDto monHoc = new();
            using (IDataReader dataReader = _monHocRepository.SelectOne(ma_mon_hoc))
            {
                if (dataReader.Read())
                {
                    monHoc = getProperty(dataReader);
                }
            }
            return monHoc;
        }
        //public List<MonHocDto> SelectBy_ListLopAo(List<LopAo> list)
        //{
        //    List<MonHocDto> result = new List<MonHocDto>();
        //    foreach(var lopAo in list)
        //    {
        //        MonHocDto monHoc = this.SelectOne((int)lopAo.MaMonHoc);
        //        if (!result.Contains(monHoc))
        //        {
        //            result.Add(monHoc);
        //        }
        //    }
        //    return result;
        //}
    }
}
