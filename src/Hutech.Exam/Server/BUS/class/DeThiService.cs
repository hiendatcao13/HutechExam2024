using AutoMapper;
using Hutech.Exam.Server.DAL.Repositories;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.DTO.Page;
using Hutech.Exam.Shared.DTO.Request.DeThi;
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

        public async Task<int> Insert(DeThiCreateRequest deThi)
        {
            return Convert.ToInt32(await _deThiRepository.Insert(deThi.MaMonHoc, deThi.TenDeThi, deThi.NgayTao, deThi.NguoiTao, deThi.GhiChu ?? string.Empty, deThi.BoChuongPhan));
        }

        public async Task<bool> Update(int id, DeThiUpdateRequest deThi)
        {
            return (await _deThiRepository.Update(id, deThi.MaMonHoc, deThi.TenDeThi, deThi.NgayTao, deThi.NguoiTao, deThi.GhiChu ?? string.Empty, deThi.BoChuongPhan)) != 0;
        }

        public async Task<bool> Delete(int ma_de_thi)
        {
            return (await _deThiRepository.Delete(ma_de_thi)) != 0;
        }

        public async Task<bool> ForceDelete(int ma_de_thi)
        {
            return (await _deThiRepository.ForceDelete(ma_de_thi)) != 0;
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

        public async Task<Paged<DeThiDto>> SelectByMonHoc_Paged(int ma_mon_hoc, int pageNumber, int pageSize)
        {
            List<DeThiDto> result = [];
            int tong_so_ban_ghi = 0, tong_so_trang = 0;
            using (IDataReader dataReader = await _deThiRepository.SelectByMonHoc_Paged(ma_mon_hoc, pageNumber, pageSize))
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
            return new Paged<DeThiDto> { Data = result, TotalPages = tong_so_trang, TotalRecords = tong_so_ban_ghi};
        }
    }
}
