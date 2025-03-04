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
        public async Task<MonHocDto> SelectOne(int ma_mon_hoc)
        {
            MonHocDto monHoc = new();
            using (IDataReader dataReader = await _monHocRepository.SelectOne(ma_mon_hoc))
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
        public async Task<List<MonHocDto>> GetAll()
        {
            List<MonHocDto> result = new();
            using (IDataReader dataReader = await _monHocRepository.GetAll())
            {
                while (dataReader.Read())
                {
                    result.Add(getProperty(dataReader));
                }
            }
            return result;
        }
        public async Task<int> Insert(string ma_so_mon_hoc, string ten_mon_hoc)
        {
            return (int)(await _monHocRepository.Insert(ma_so_mon_hoc, ten_mon_hoc) ?? -1);
        }
        public async Task<int> Update(int ma_mon_hoc, string ma_so_mon_hoc, string ten_mon_hoc)
        {
            return await _monHocRepository.Update(ma_mon_hoc, ma_so_mon_hoc, ten_mon_hoc);
        }
        public async Task<int> Remove(int ma_mon_hoc)
        {
            return await _monHocRepository.Remove(ma_mon_hoc);
        }
    }
}
