using AutoMapper;
using Hutech.Exam.Server.DAL.Repositories;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.DTO.Custom;
using Hutech.Exam.Shared.DTO.Page;
using Hutech.Exam.Shared.DTO.Request.DeThi;
using Hutech.Exam.Shared.Models;
using Ninject.Planning;
using System.Data;

namespace Hutech.Exam.Server.BUS
{
    public class DeThiService(IDeThiRepository deThiRepository)
    {
        #region Private Fields
        private readonly IDeThiRepository _deThiRepository = deThiRepository;
        #endregion

        #region Public Methods
        public async Task<int> Insert(DeThiCreateRequest deThi)
        {
            return await _deThiRepository.Insert(deThi.MaMonHoc, deThi.TenDeThi, deThi.Guid, deThi.NgayTao, deThi.KyHieuDe);
        }

        public async Task<bool> Update(long id, DeThiUpdateRequest deThi)
        {
            return (await _deThiRepository.Update(id, deThi.MaMonHoc, deThi.TenDeThi, deThi.Guid, deThi.KyHieuDe));
        }

        public async Task Save_Batch(List<DeThiDto> deThis)
        {
            await _deThiRepository.Save_Batch(deThis);
        }

        public async Task<bool> Delete(long ma_de_thi)
        {
            return (await _deThiRepository.Delete(ma_de_thi));
        }

        public async Task<bool> ForceDelete(long ma_de_thi)
        {
            return (await _deThiRepository.ForceDelete(ma_de_thi));
        }

        public async Task<DeThiDto> SelectOne(long ma_de_thi)
        {
            return await _deThiRepository.SelectOne(ma_de_thi);
        }

        public async Task<DeThiDto> SelectBy_DeThiHV(long ma_de_hv)
        {
            return await _deThiRepository.SelectBy_ma_de_hv(ma_de_hv);
        }

        public async Task<List<DeThiDto>> GetAll()
        {
            return await _deThiRepository.GetAll();
        }

        public async Task<List<DeThiDto>> SelectByMonHoc(int ma_mon_hoc)
        {
            return await _deThiRepository.SelectByMonHoc(ma_mon_hoc);
        }

        public async Task<Paged<DeThiDto>> SelectByMonHoc_Paged(int ma_mon_hoc, int pageNumber, int pageSize)
        {
            return await _deThiRepository.SelectByMonHoc_Paged(ma_mon_hoc, pageNumber, pageSize);
        }

        public List<CustomDeThi> ConvertToCustomDeThi(NoiDungDeThiMock mockDeThis)
        {
            var result = new List<CustomDeThi>();
            Dictionary<Guid, string> nhomCauHois = []; // lưu vết để lấy nội dung ra

            foreach (var phan in mockDeThis.Phans)
            {
                if(!nhomCauHois.ContainsKey(phan.MaPhan))
                {
                    nhomCauHois[phan.MaPhan] = phan.NoiDung;
                }    

                foreach (var cauHoi in phan.CauHois)
                {
                    var item = new CustomDeThi
                    {
                        MaNhom = phan.MaPhan,
                        MaNhomCha = phan.MaPhanCha ?? new Guid(),
                        MaCauHoi = cauHoi.MaCauHoi,
                        NoiDungCauHoiNhom = phan.NoiDung,
                        NoiDungCauHoiNhomCha = nhomCauHois.TryGetValue(phan.MaPhanCha ?? new Guid(), out string? value) ? value : null,
                        NoiDungCauHoi = cauHoi.NoiDung,
                        KieuNoiDungCauHoiNhom = phan.KieuNoiDung,
                        KieuNoiDungCauHoi = -1,
                        CauTraLois = cauHoi.CauTraLois.ToDictionary(
                            ctl => ctl.MaCauTraLoi,
                            ctl => (string?)ctl.NoiDung
                        ),
                    };
                    

                    result.Add(item);
                }
            }

            return result;
        }

        #endregion

    }
}
