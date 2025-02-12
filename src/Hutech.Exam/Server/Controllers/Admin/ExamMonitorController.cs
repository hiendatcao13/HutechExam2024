using Hutech.Exam.Server.BUS;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hutech.Exam.Server.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class ExamMonitorController : Controller
    {
        private readonly LopService _lopService;
        private readonly SinhVienService _sinhvienService;
        private readonly ChiTietCaThiService _chiTietCaThiService;
        private readonly KhoaService _khoaService;
        private readonly CaThiService _caThiService;
        private readonly DeThiHoanViService _deThiHoanViService;

        public ExamMonitorController(LopService lopService, SinhVienService sinhVienService, ChiTietCaThiService chiTietCaThiService, KhoaService khoaService, 
            CaThiService caThiService, DeThiHoanViService deThiHoanViService)
        {
            _lopService = lopService;
            _sinhvienService = sinhVienService;
            _chiTietCaThiService = chiTietCaThiService;
            _khoaService = khoaService;
            _caThiService = caThiService;
            _deThiHoanViService = deThiHoanViService;
        }
        [HttpDelete("RemoveCTCT")]
        public async Task<ActionResult> RemoveCTCT([FromQuery] int ma_chi_tiet_ca_thi)
        {
            await _chiTietCaThiService.Remove(ma_chi_tiet_ca_thi);
            return Ok();
        }
        [HttpPost("UpdateCTCT")]
        public async Task<ActionResult> UpdateCTCT([FromBody] ChiTietCaThiDto chiTietCaThi)
        {
            await _chiTietCaThiService.Update(chiTietCaThi.MaChiTietCaThi, chiTietCaThi.MaCaThi, chiTietCaThi.MaSinhVien, chiTietCaThi.MaDeThi, chiTietCaThi.TongSoCau);
            SinhVienDto? sinhVien = chiTietCaThi.MaSinhVienNavigation;
            if (sinhVien != null)
                await _sinhvienService.Update(sinhVien.MaSinhVien, sinhVien.HoVaTenLot, sinhVien.TenSinhVien, sinhVien.GioiTinh, sinhVien.NgaySinh, sinhVien.MaLop, sinhVien.DiaChi, sinhVien.Email, sinhVien.DienThoai, sinhVien.MaSoSinhVien);
            return Ok();
        }
        [HttpGet("GetAllDeThi")]
        public async Task<ActionResult<List<long>>> GetAllDeThi([FromQuery] int ma_ca_thi)
        {
            List<long> deThiHVs = new List<long>();
            var ketQua = await _caThiService.SelectOne(ma_ca_thi);
            int ma_de_thi = ketQua.MaDeThi;
            var items = await _deThiHoanViService.SelectBy_MaDeThi(ma_de_thi);
            List<DeThiHoanViDto> deThiHoanVis = items.ToList();
            foreach(var item in deThiHoanVis)
                deThiHVs.Add(item.MaDeHv);
            return deThiHVs;
        }
        [HttpPost("InsertSV")]
        public async Task<ActionResult> InsertSV([FromQuery] string? ten_lop,[FromQuery] int ma_ca_thi, [FromBody] SinhVienDto sinhVien)
        {
            if (ten_lop != "" && ten_lop != null)
            {
                // nếu lớp không tồn tại trong CSDL, sẽ để giá trị null và thêm sau vì nếu tạo lớp thì sẽ phải tạo trường Khoa
                var lop = await _lopService.SelectBy_ten_lop(ten_lop);
                int ma_lop = lop.MaLop;
                sinhVien.MaLop = (ma_lop == 0) ? await _lopService.Insert(ten_lop, null, null) : ma_lop;
            }
            sinhVien.StudentId = Guid.NewGuid();
            long msssv = await _sinhvienService.Insert(sinhVien);
            await this.InsertCTCT(ma_ca_thi, msssv);
            return Ok();
        }
        [HttpPost("InsertCTCT")]
        public async Task<ActionResult> InsertCTCT([FromQuery] int ma_ca_thi, [FromQuery] long ma_sinh_vien)
        {
            // lấy ngẫu nhiên đề
            var ketQua = await this.GetAllDeThi(ma_ca_thi);
            List<long>? maDeHVs = ketQua.Value;
            Random rd = new Random();
            int numb = (maDeHVs != null) ? rd.Next(maDeHVs.Count) : -1;

            await _chiTietCaThiService.Insert(ma_ca_thi, ma_sinh_vien, (maDeHVs != null) ? maDeHVs[numb] : -1, 0);
            return Ok();
        }

        [HttpGet("GetAllKhoa")]
        public async Task<ActionResult<List<KhoaDto>>> GetAllKhoa()
        {
            return await _khoaService.GetAll();
        }
        [HttpPost("InsertListSV")]
        public async Task<ActionResult<bool>> InsertListSV([FromQuery] int ma_khoa, [FromQuery] int ma_ca_thi, [FromBody] List<SinhVienDto> sinhViens)
        {
            // lấy ngẫu nhiên đề
            var ketQua = await this.GetAllDeThi(ma_ca_thi);
            List<long>? maDeHVs = ketQua.Value;
            Random rd = new Random();

            // lưu Dictionary <tenlop, malop> để tránh gọi lấy lớp quá nhiều lần nếu giống nhau
            Dictionary<string, int> lops = new Dictionary<string, int>();
            foreach (var item in sinhViens)
            {
                int numb = (maDeHVs != null) ? rd.Next(maDeHVs.Count) : -1;

                // trước đó, tên lớp sinh viên được lưu tạm thời tại địa chỉ
                if (item.DiaChi != null && !lops.ContainsKey(item.DiaChi))
                {
                    var lop = await _lopService.SelectBy_ten_lop(item.DiaChi);
                    int ma_lop = lop.MaLop;
                    // mã khoa mặc định là -1 theo combobox chọn
                    ma_lop = (ma_lop == 0) ? await _lopService.Insert(item.DiaChi, null, (ma_khoa == -1) ? null : ma_khoa) : ma_lop;
                    item.MaLop = ma_lop;
                    lops.Add(item.DiaChi, ma_lop);
                }
                item.MaLop = lops.GetValueOrDefault(item.DiaChi);
                item.DiaChi = ""; // trả mặc định lại cho địa chỉ
                if (item.MaSoSinhVien != null) // thêm sinh viên vào ca thi
                {
                    SinhVienDto sinhVien = await _sinhvienService.SelectBy_ma_so_sinh_vien(item.MaSoSinhVien);
                    if(sinhVien.MaSinhVien == 0)
                    {
                        long msv = await _sinhvienService.Insert(item);
                        await _chiTietCaThiService.Insert(ma_ca_thi, msv, (maDeHVs != null) ? maDeHVs[numb] : -1, 0); // tạo mới hoàn toàn 1 đối tượng sinh viên
                    }
                    else
                    {
                        await _chiTietCaThiService.Insert(ma_ca_thi, sinhVien.MaSinhVien, (maDeHVs != null) ? maDeHVs[numb] : -1, 0); // Thêm sinh viên vào ca thi
                    }
                }
            }
            return Ok(true);
        }
    }
}
