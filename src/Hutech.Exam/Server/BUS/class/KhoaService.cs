using AutoMapper;
using Hutech.Exam.Server.DAL.Repositories;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.DTO.Page;
using Hutech.Exam.Shared.DTO.Request.Khoa;
using Hutech.Exam.Shared.Models;
using System.Data;

namespace Hutech.Exam.Server.BUS
{
    public class KhoaService(IKhoaRepository khoaRepository, IMapper mapper)
    {
        private readonly IKhoaRepository _khoaRepository = khoaRepository;
        private readonly IMapper _mapper = mapper;

        public static readonly int COLUMN_LENGTH = 3; // số lượng cột trong bảng Khoa

        public KhoaDto GetProperty(IDataReader dataReader, int start = 0)
        {
            Khoa khoa = new()
            {
                MaKhoa = dataReader.GetInt32(0 + start),
                TenKhoa = dataReader.IsDBNull(1 + start) ? null : dataReader.GetString(1 + start),
                NgayThanhLap = dataReader.IsDBNull(2 + start) ? null : dataReader.GetDateTime(2 + start)
            };
            return _mapper.Map<KhoaDto>(khoa);
        }

        public async Task<KhoaDto> SelectOne(int ma_khoa)
        {
            KhoaDto khoa = new();
            using (IDataReader dataReader = await _khoaRepository.SelectOne(ma_khoa))
            {
                if (dataReader.Read())
                {
                    khoa = GetProperty(dataReader);
                }
            }
            return khoa;
        }

        public async Task<int> Insert(KhoaCreateRequest khoa)
        {
            return Convert.ToInt32(await _khoaRepository.Insert(khoa.TenKhoa, khoa.NgayThanhLap));
        }

        public async Task<bool> Update(int id, KhoaUpdateRequest khoa)
        {
            return await _khoaRepository.Update(id, khoa.TenKhoa, khoa.NgayThanhLap) != 0;
        }

        public async Task<bool> Remove(int ma_khoa)
        {
            return await _khoaRepository.Remove(ma_khoa) != 0;
        }

        public async Task<bool> ForceRemove(int ma_khoa)
        {
            return await _khoaRepository.ForceRemove(ma_khoa) != 0;
        }

        public async Task<List<KhoaDto>> GetAll()
        {
            List<KhoaDto> results = [];
            using (IDataReader dataReader = await _khoaRepository.GetAll())
            {
                while (dataReader.Read())                {
                    results.Add(GetProperty(dataReader));
                }
            }
            return results;
        }

        public async Task<Paged<KhoaDto>> GetAll_Paged(int pageNumber, int pageSize)
        {
            List<KhoaDto> results = [];
            int tong_so_ban_ghi = 0, tong_so_trang = 0;
            using (IDataReader dataReader = await _khoaRepository.GetAll_Paged(pageNumber, pageSize))
            {
                while (dataReader.Read())
                {
                    results.Add(GetProperty(dataReader));
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
            return new Paged<KhoaDto> { Data = results, TotalPages = tong_so_trang, TotalRecords = tong_so_ban_ghi};
        }
    }
}
