using AutoMapper;
using Hutech.Exam.Server.DAL.Repositories;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.DTO.Page;
using Hutech.Exam.Shared.DTO.Request.CaThi;
using Hutech.Exam.Shared.Models;
using System.Data;

namespace Hutech.Exam.Server.BUS
{
    public class CaThiService(ICaThiRepository caThiRepository, DotThiService dotThiService, IMapper mapper)
    {
        private readonly ICaThiRepository _caThiRepository = caThiRepository;
        private readonly DotThiService _dotThiService = dotThiService;
        private readonly IMapper _mapper = mapper;

        public static readonly int COLUMN_LENGTH = 14; // số lượng cột trong bảng CaThi

        public CaThiDto GetProperty(IDataReader dataReader, int start = 0)
        {
            CaThi caThi = new()
            {
                MaCaThi = dataReader.GetInt32(0 + start),
                TenCaThi = dataReader.IsDBNull(1 + start) ? null : dataReader.GetString(1 + start),
                MaChiTietDotThi = dataReader.GetInt32(2 + start),
                ThoiGianBatDau = dataReader.GetDateTime(3 + start),
                MaDeThi = dataReader.GetInt32(4 + start),
                IsActivated = dataReader.GetBoolean(5 + start),
                ActivatedDate = dataReader.IsDBNull(6 + start) ? null : dataReader.GetDateTime(6 + start),
                ThoiGianThi = dataReader.GetInt32(7 + start),
                KetThuc = dataReader.GetBoolean(8 + start),
                ThoiDiemKetThuc = dataReader.IsDBNull(9 + start) ? null : dataReader.GetDateTime(9 + start),
                MatMa = dataReader.IsDBNull(10 + start) ? null : dataReader.GetString(10 + start),
                Approved = dataReader.GetBoolean(11 + start),
                ApprovedDate = dataReader.IsDBNull(12 + start) ? null : dataReader.GetDateTime(12 + start),
                ApprovedComments = dataReader.IsDBNull(13 + start) ? null : dataReader.GetString(13 + start)
            };
            return _mapper.Map<CaThiDto>(caThi);
        }

        public async Task<List<CaThiDto>> SelectBy_MaDotThi_MaLop_LanThi(int ma_dot_thi, int ma_lop, int lan_thi)
        {
            List<CaThiDto> result = [];
            using (IDataReader dataReader = await _caThiRepository.SelectBy_MaDotThi_MaLop_LanThi(ma_dot_thi, ma_lop, lan_thi))
            {
                while (dataReader.Read())
                {
                    result.Add(GetProperty(dataReader));
                }
            }
            return result;
        }

        public async Task<Paged<CaThiDto>> SelectBy_ma_chi_tiet_dot_thi_Paged(int ma_chi_tiet_dot_thi, int pageNumber, int pageSize)
        {
            List<CaThiDto> result = [];
            int tong_so_ban_ghi = 0, tong_so_trang = 0;
            using (IDataReader dataReader = await _caThiRepository.SelectBy_ma_chi_tiet_dot_thi_Paged(ma_chi_tiet_dot_thi, pageNumber, pageSize))
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
            return new Paged<CaThiDto> { Data = result, TotalPages= tong_so_trang, TotalRecords = tong_so_ban_ghi};
        }

        public async Task<Paged<CaThiDto>> SelectBy_ma_chi_tiet_dot_thi_Search_Paged(int ma_chi_tiet_dot_thi, string keyword, int pageNumber, int pageSize)
        {
            List<CaThiDto> result = [];
            int tong_so_ban_ghi = 0, tong_so_trang = 0;
            using (IDataReader dataReader = await _caThiRepository.SelectBy_ma_chi_tiet_dot_thi_Search_Paged(ma_chi_tiet_dot_thi, keyword, pageNumber, pageSize))
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
            return new Paged<CaThiDto> { Data = result, TotalPages = tong_so_trang, TotalRecords = tong_so_ban_ghi };
        }

        public async Task<CaThiDto> SelectOne(int ma_ca_thi)
        {
            CaThiDto caThi = new();
            using (IDataReader dataReader = await _caThiRepository.SelectOne(ma_ca_thi))
            {
                if (dataReader.Read())
                {
                    caThi = GetProperty(dataReader);
                }
            }
            return caThi;
        }
        public async Task<List<CaThiDto>> GetAll()
        {
            List<CaThiDto> result = [];
            using (IDataReader dataReader = await _caThiRepository.GetAll())
            {
                while (dataReader.Read())
                {
                    CaThiDto caThi = GetProperty(dataReader);
                    result.Add(caThi);
                }
            }
            return result;
        }
        public async Task<bool> Activate(int ma_ca_thi, bool IsActivated)
        {
            return await _caThiRepository.Activate(ma_ca_thi, IsActivated) != 0;
        }

        public async Task<bool> HuyKichHoat(int ma_ca_thi)
        {
            return await _caThiRepository.HuyKichHoat(ma_ca_thi) != 0;
        }

        public async Task<bool> Ketthuc(int ma_ca_thi)
        {
            return await _caThiRepository.Ketthuc(ma_ca_thi) != 0;
        }

        public async Task<int> Insert(CaThiCreateRequest caThi)
        {
            return Convert.ToInt32(await _caThiRepository.Insert(caThi.TenCaThi, caThi.MaChiTietDotThi, caThi.ThoiGianBatDau, caThi.MaDeThi, caThi.ThoiGianThi) ?? -1);
        }

        public async Task<bool> Remove(int ma_ca_thi)
        {
            return await _caThiRepository.Remove(ma_ca_thi) != 0;
        }

        public async Task<bool> ForceRemove(int ma_ca_thi)
        {
            return await _caThiRepository.ForceRemove(ma_ca_thi) != 0;
        }

        public async Task<bool> Update(int id, CaThiUpdateRequest caThi)
        {
            return await _caThiRepository.Update(id, caThi.TenCaThi, caThi.MaChiTietDotThi, caThi.ThoiGianBatDau, caThi.MaDeThi, caThi.ThoiGianThi) != 0;
        }
    }
}
