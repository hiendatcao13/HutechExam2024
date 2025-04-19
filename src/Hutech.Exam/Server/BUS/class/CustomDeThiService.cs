using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.DTO.Custom;
using Hutech.Exam.Shared.Models;

namespace Hutech.Exam.Server.BUS
{
    public class CustomDeThiService
    {
        private readonly CustomDeThi _customDeThi;
        private readonly ChiTietDeThiHoanViService _chiTietDeThiHoanViService;
        private readonly NhomCauHoiService _nhomCauHoiService;
        private readonly CauHoiService _cauHoiService;
        private readonly CauTraLoiService _cauTraLoiService;
        public readonly DeThiService _deThiService;

        public CustomDeThiService(CustomDeThi customDeThi, ChiTietDeThiHoanViService chiTietDeThiHoanViService, NhomCauHoiService nhomCauHoiService, CauHoiService cauHoiService, CauTraLoiService cauTraLoiService, DeThiService deThiService)
        {
            _customDeThi = customDeThi;
            _chiTietDeThiHoanViService = chiTietDeThiHoanViService;
            _nhomCauHoiService = nhomCauHoiService;
            _cauHoiService = cauHoiService;
            _cauTraLoiService = cauTraLoiService;
            _deThiService = deThiService;
        }       
        public async Task<List<CustomDeThi>> handleDeThi(long ma_de_hoan_vi)
        { 
            List<CustomDeThi> result = new List<CustomDeThi>();
            List<ChiTietDeThiHoanViDto> chiTietDeThiHoanVis = await getNoiDungChiTietDeThiHV(ma_de_hoan_vi);
            foreach (var item in chiTietDeThiHoanVis)
                result.Add(await getNoiDungFromCTDeThiHV(item));
            return result;
        }
        private async Task<List<ChiTietDeThiHoanViDto>> getNoiDungChiTietDeThiHV(long ma_de_hoan_vi)
        {
            return await _chiTietDeThiHoanViService.SelectBy_MaDeHV(ma_de_hoan_vi); ;
        }
        private async Task<NhomCauHoiDto> getNoiDungCauHoiNhom(int ma_cau_hoi_nhom)
        {
            return await _nhomCauHoiService.SelectOne(ma_cau_hoi_nhom);
        }
        private async Task<CauHoiDto> getNoiDungCauHoi(int ma_cau_hoi)
        {
            return await _cauHoiService.SelectOne(ma_cau_hoi);
        }
        private async Task<List<CauTraLoiDto>> getNoiDungCauTraLoi(int ma_cau_hoi)
        {
            return await _cauTraLoiService.SelectBy_MaCauHoi(ma_cau_hoi);
        }
        private async Task<CustomDeThi> getNoiDungFromCTDeThiHV(ChiTietDeThiHoanViDto chiTietDeThiHoanVi)
        {
            CustomDeThi chiTietDeThi = new();
            NhomCauHoiDto nhomCauHoi = await getNoiDungCauHoiNhom(chiTietDeThiHoanVi.MaNhom);
            CauHoiDto cauHoi = await getNoiDungCauHoi(chiTietDeThiHoanVi.MaCauHoi);
            List<CauTraLoiDto> cauTraLois = await getNoiDungCauTraLoi(chiTietDeThiHoanVi.MaCauHoi);

            chiTietDeThi.MaNhom = nhomCauHoi.MaNhom;
            chiTietDeThi.MaCauHoi = cauHoi.MaCauHoi;

            // lấy nội dung của mã nhóm cha (nếu có)
            if (nhomCauHoi.MaNhomCha != -1)
            {
                var ketQua = await getNoiDungCauHoiNhom(nhomCauHoi.MaNhomCha);
                chiTietDeThi.NoiDungCauHoiNhomCha = ketQua.NoiDung;
            }
            chiTietDeThi.NoiDungCauHoiNhom = nhomCauHoi.NoiDung;
            chiTietDeThi.NoiDungCauHoi = cauHoi.NoiDung;
            chiTietDeThi.KieuNoiDungCauHoi = cauHoi.KieuNoiDung;
            chiTietDeThi.MaClo = cauHoi.MaClo;
            chiTietDeThi.CauTraLois = new Dictionary<int, string?>();
            // Xem coi đề thi có bỏ chương phần không
            var deThi = await _deThiService.SelectBy_ma_de_hv(chiTietDeThiHoanVi.MaDeHv);
            chiTietDeThi.BoChuongPhan = deThi.BoChuongPhan;

            // lấy nội dung câu hỏi bằng dictionary
            chiTietDeThi.CauTraLois = new Dictionary<int, string?>();
            foreach (var item in cauTraLois)
                chiTietDeThi.CauTraLois.Add(item.MaCauTraLoi, item.NoiDung);
            return chiTietDeThi;
        }
    }
}
