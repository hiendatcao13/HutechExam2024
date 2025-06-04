using AutoMapper;
using Hutech.Exam.Server.DAL.Repositories;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.DTO.Page;
using Hutech.Exam.Shared.DTO.Request.Khoa;
using Hutech.Exam.Shared.DTO.Request.Lop;
using Hutech.Exam.Shared.Models;
using System.Data;

namespace Hutech.Exam.Server.BUS
{
    public class LopService(ILopRepository lopRepository, IMapper mapper)
    {
        private readonly ILopRepository _lopRepository = lopRepository;
        private readonly IMapper _mapper = mapper;

        public static readonly int COLUMN_LENGTH = 4; // số lượng cột trong bảng Lop

        public LopDto GetProperty(IDataReader dataReader, int start = 0)
        {
            Lop lop = new()
            {
                MaLop = dataReader.GetInt32(0 + start),
                TenLop = dataReader.IsDBNull(1 + start) ? null : dataReader.GetString(1 + start),
                NgayBatDau = dataReader.IsDBNull(2 + start) ? null : dataReader.GetDateTime(2 + start),
                MaKhoa = dataReader.IsDBNull(3 + start) ? null : dataReader.GetInt32(3 + start)
            };
            return _mapper.Map<LopDto>(lop);
        }
        public async Task<LopDto> SelectBy_ten_lop(string ten_lop)
        {
            LopDto lop = new();
            using(IDataReader dataReader = await _lopRepository.SelectBy_ten_lop(ten_lop))
            {
                if (dataReader.Read())
                {
                    lop = GetProperty(dataReader);
                }
            }
            return lop;
        }

        public async Task<Paged<LopDto>> SelectBy_ma_khoa_Paged(int ma_khoa, int pageNumber, int pageSize)
        {
            List<LopDto> result = [];
            int tong_so_ban_ghi = 0, tong_so_trang = 0;
            using (IDataReader dataReader = await _lopRepository.SelectBy_ma_khoa_Paged(ma_khoa, pageNumber, pageSize))
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
            return new Paged<LopDto> { Data = result, TotalPages = tong_so_trang, TotalRecords = tong_so_ban_ghi};
        }
        public async Task<LopDto> SelectOne(int ma_lop)
        {
            LopDto lop = new();
            using (IDataReader dataReader = await _lopRepository.SelectOne(ma_lop))
            {
                if (dataReader.Read())
                {
                    lop = GetProperty(dataReader);
                }
            }
            return lop;
        }
        public async Task<int> Insert(LopCreateRequest lop)
        {
            return Convert.ToInt32(await _lopRepository.Insert(lop.TenLop, lop.NgayBatDau, lop.MaKhoa) ?? -1);
        }

        public async Task<bool> Update(int id, LopUpdateRequest lop)
        {
            return await _lopRepository.Update(id, lop.TenLop, lop.NgayBatDau, lop.MaKhoa) != 0;
        }

        public async Task<bool> Remove(int ma_lop)
        {
            return await _lopRepository.Remove(ma_lop) != 0;
        }

        public async Task<bool> ForceRemove(int ma_lop)
        {
            return await _lopRepository.ForceRemove(ma_lop) != 0;
        }

    }
}
