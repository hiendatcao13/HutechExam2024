﻿using System.Data;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.DTO.Page;

namespace Hutech.Exam.Server.DAL.Repositories
{
    public interface ICaThiRepository
    {
        CaThiDto GetProperty(IDataReader dataReader, int start = 0);

        Task<Paged<CaThiDto>> SelectBy_ma_chi_tiet_dot_thi_Paged(int ma_chi_tiet_dot_thi, int pageNumber, int pageSize);

        Task<Paged<CaThiDto>> SelectBy_ma_chi_tiet_dot_thi_Search_Paged(int ma_chi_tiet_dot_thi, string keyword, int pageNumber, int pageSize);

        Task<CaThiDto> SelectOne(int ma_ca_thi);

        Task<List<CaThiDto>> GetAll();

        Task<bool> KichHoat(int ma_ca_thi, bool kich_hoat, string actionHistory);

        Task<bool> HuyKichHoat(int ma_ca_thi, string actionHistory);

        Task<bool> Ketthuc(int ma_ca_thi, string actionHistory);

        Task<int> Insert(string ten_ca_thi, int ma_chi_tiet_dot_thi, DateTime thoi_gian_bat_dau, int thoi_gian_thi, string mat_ma);

        Task<bool> Remove(int ma_ca_thi);

        Task<bool> ForceRemove(int ma_ca_thi);

        Task<bool> Update(int ma_ca_thi, string ten_ca_thi, int ma_chi_tiet_dot_thi, DateTime thoi_gian_bat_dau, int thoi_gian_thi, string mat_ma);

        Task<bool> UpdateDeThi(int ma_ca_thi, bool isOrderMSSV, string lichSuHoatDong, List<long> dsDeThis);

        Task<bool> UpdateAllResetLogin(int maCaThi);

        Task<List<CaThiDto>> SelectBy_MaDotThi_MaLop_LanThi(int ma_dot_thi, int ma_lop, int lan_thi);

        Task<bool> UpdateLichSuHoatDong(int ma_ca_thi, string lichSuHoatDong);

        Task<bool> DuyetDe(int ma_ca_thi, string lichSuHoatDong);
    }
}
