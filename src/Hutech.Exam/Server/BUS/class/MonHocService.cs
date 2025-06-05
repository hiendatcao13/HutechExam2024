using AutoMapper;
using Hutech.Exam.Server.DAL.Repositories;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.DTO.Page;
using Hutech.Exam.Shared.DTO.Request.MonHoc;
using Hutech.Exam.Shared.Models;
using System.Data;
using System.Data.Common;

namespace Hutech.Exam.Server.BUS
{
    public class MonHocService(IMonHocRepository monHocRepository, IMapper mapper)
    {
        private readonly IMonHocRepository _monHocRepository = monHocRepository;
        private readonly IMapper _mapper = mapper;

        public static readonly int COLUMN_LENGTH = 3; // số lượng cột trong bảng MonHoc

        public MonHocDto GetProperty(IDataReader dataReader, int start = 0)
        {
            MonHoc monHoc = new()
            {
                MaMonHoc = dataReader.GetInt32(0 + start),
                MaSoMonHoc = dataReader.IsDBNull(1 + start) ? null : dataReader.GetString(1 + start),
                TenMonHoc = dataReader.IsDBNull(2 + start) ? null : dataReader.GetString(2 + start)
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
                    monHoc = GetProperty(dataReader);
                }
            }
            return monHoc;
        }

        public async Task<List<MonHocDto>> GetAll()
        {
            List<MonHocDto> result = [];
            using (IDataReader dataReader = await _monHocRepository.GetAll())
            {
                while (dataReader.Read())
                {
                    result.Add(GetProperty(dataReader));
                }
            }
            return result;
        }

        public async Task<Paged<MonHocDto>> GetAll_Paged(int pageNumber, int pageSize)
        {
            List<MonHocDto> result = [];
            int tong_so_ban_ghi = 0, tong_so_trang = 0;
            using (IDataReader dataReader = await _monHocRepository.GetAll_Paged(pageNumber, pageSize))
            {
                while (dataReader.Read())
                {
                    result.Add(GetProperty(dataReader));
                }

                //chuyển sang bảng thứ 2 đọc tổng số lượng bản ghi và tổng số lượng trang
                if (dataReader.NextResult())
                {
                    while (dataReader.Read())
                    {
                        tong_so_ban_ghi = dataReader.GetInt32(0);
                        tong_so_trang = dataReader.GetInt32(1);
                    }
                }
            }
            return new Paged<MonHocDto> { Data = result, TotalPages = tong_so_trang, TotalRecords = tong_so_ban_ghi};
        }

        public async Task<int> Insert(MonHocCreateRequest monHoc)
        {
            return Convert.ToInt32(await _monHocRepository.Insert(monHoc.MaSoMonHoc, monHoc.TenMonHoc) ?? -1);
        }

        public async Task<bool> Update(int id, MonHocUpdateRequest monHoc)
        {
            return await _monHocRepository.Update(id, monHoc.MaSoMonHoc, monHoc.TenMonHoc) != 0;
        }
        public async Task<bool> Remove(int ma_mon_hoc)
        {
            return await _monHocRepository.Remove(ma_mon_hoc) != 0;
        }

        public async Task<bool> ForceRemove(int ma_mon_hoc)
        {
            return await _monHocRepository.ForceRemove(ma_mon_hoc) != 0;
        }
    }
}
