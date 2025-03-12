using Hutech.Exam.Server.Attributes;
using Hutech.Exam.Server.BUS;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.DTO.Custom;
using Hutech.Exam.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text.Json;
using System.Xml.Linq;

namespace Hutech.Exam.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ExamController : Controller
    {
        private readonly ChiTietDeThiHoanViService _chiTietDeThiHoanViService;
        private readonly ChiTietBaiThiService _chiTietBaiThiService;
        private readonly AudioListenedService _audioListenedService;
        private readonly CustomDeThiService _customDeThiService;
        private readonly CaThiService _caThiService;
        private readonly RabbitMqCTBTService _rabbitMqService;
        public ExamController(ChiTietDeThiHoanViService chiTietDeThiHoanViService, ChiTietBaiThiService chiTietBaiThiService, 
            AudioListenedService audioListenedService, CustomDeThiService customDeThiService, CaThiService caThiService, RabbitMqCTBTService rabbitMqService)
        {
            _chiTietDeThiHoanViService = chiTietDeThiHoanViService;
            _chiTietBaiThiService = chiTietBaiThiService;
            _audioListenedService = audioListenedService;
            _customDeThiService = customDeThiService;
            _caThiService = caThiService;
            _rabbitMqService = rabbitMqService;
        }
        [HttpGet("GetDeThi")]
        [Cache]
        public async Task<ActionResult<List<CustomDeThi>>> GetDeThi([FromQuery] long ma_de_thi_hoan_vi)
        {
            List<CustomDeThi> result = await _customDeThiService.handleDeThi(ma_de_thi_hoan_vi);
            return result;
        }
        public async Task<List<int>?> GetListDapAnWithCacheAsync(long maDeHv)
        {
            var cacheKey = $"/api/Exam/GetDeThi|DapAn-{maDeHv}";
            var cacheService = HttpContext.RequestServices.GetRequiredService<IResponseCacheService>();

            // Kiểm tra xem có dữ liệu trong cache không
            var cachedData = await cacheService.GetCacheResponseAsync(cacheKey);
            if (!string.IsNullOrEmpty(cachedData))
            {
                // Nếu có, deserialization và trả về dữ liệu
                return JsonSerializer.Deserialize<List<int>>(cachedData);
            }

            // Nếu không có, thực hiện logic lấy dữ liệu từ database
            List<ChiTietDeThiHoanViDto> chiTietDeThiHoanVis = await _chiTietDeThiHoanViService.SelectBy_MaDeHV(maDeHv);
            List<int> listDapAn = new List<int>();
            foreach (var item in chiTietDeThiHoanVis)
            {
                if (item.DapAn != null)
                    listDapAn.Add((int)item.DapAn);
            }

            // Lưu vào cache
            await cacheService.SetCacheResponseAsync(cacheKey, listDapAn, TimeSpan.FromMinutes(120));

            return listDapAn;
        }
        [HttpGet("IsActiveCaThi")]
        public async Task<ActionResult<bool>> IsActiveCaThi([FromQuery] int ma_ca_thi)
        {
            CaThiDto caThi = await _caThiService.SelectOne(ma_ca_thi);
            return (caThi.IsActivated);
        }
        // Insert (có trả về list bài thi) giúp cho sinh viên tiếp tục thi trong trường hợp treo máy

        [HttpPost("InsertChiTietBaiThi")]
        public async Task<ActionResult<List<ChiTietBaiThiDto>>> InsertChiTietBaiThi([FromQuery] int ma_chi_tiet_ca_thi ,[FromQuery] long ma_de_thi_hoan_vi)
        {
            var ketQua = await this.GetDeThi(ma_de_thi_hoan_vi);
            List<CustomDeThi>? customDeThis = ketQua.Value;
            await _chiTietBaiThiService.InsertChiTietBaiThis_SelectByChiTietDeThiHV(customDeThis, ma_chi_tiet_ca_thi, ma_de_thi_hoan_vi);

            // tránh trường hợp lấy đề của những môn khác
            List<ChiTietBaiThiDto> result = await _chiTietBaiThiService.SelectBy_ma_chi_tiet_ca_thi(ma_chi_tiet_ca_thi);

            return result;
        }
        [HttpGet("InsertCTBT_DaVaoThi")] // khi này sinh viên đã thi trước đó và tiếp tục thi nên không cần insert chi tiet bai thi nữa mà chỉ lấy -> tối ưu API
        public async Task<ActionResult<List<ChiTietBaiThiDto>>> InsertCTBT_DaVaoThi([FromQuery] int ma_chi_tiet_ca_thi, [FromQuery] long ma_de_thi_hoan_vi)
        {
            // tránh trường hợp lấy đề của những môn khác
            return await _chiTietBaiThiService.SelectBy_ma_chi_tiet_ca_thi(ma_chi_tiet_ca_thi);
        }

        [HttpPost("UpdateChiTietBaiThi")]
        [AllowAnonymous]
        public async Task<ActionResult<List<ChiTietBaiThiDto>>> UpdateChiTietBaiThi([FromBody] List<ChiTietBaiThiDto> chiTietBaiThis)
        {
            //List<int>? listDapAn = await this.GetListDapAnWithCacheAsync(chiTietBaiThis[0].MaDeHv);
            //if(chiTietBaiThis.Count != 0)
            //{
            //    foreach (var item in chiTietBaiThis)
            //    {
            //        if (listDapAn != null && item.CauTraLoi != null)
            //            item.KetQua = (listDapAn.Contains((int)item.CauTraLoi)) ? true : false;
            //    }
            //    try
            //    {
            //        await _rabbitMqService.PublishMessage(chiTietBaiThis);
            //    }
            //    catch (Exception ex)
            //    {
            //        return StatusCode(500, new { Success = false, Error = ex.Message });
            //    }
            //}
            //return Ok(new { Success = true, Message = "Message sent successfully!" });

            if (chiTietBaiThis.Count == 0)
                return Ok();
            List<int>? listDapAn = await this.GetListDapAnWithCacheAsync(chiTietBaiThis[0].MaDeHv);
            // nếu thứ tự là 0 là đã insert trước đó, chỉ update và ngược lại thì insert và update
            foreach (var item in chiTietBaiThis)
            {
                if (item.ThuTu != 0)
                    await _chiTietBaiThiService.Insert(item.MaChiTietCaThi, item.MaDeHv, item.MaNhom, item.MaCauHoi, DateTime.Now, item.ThuTu);
                if (listDapAn != null && item.CauTraLoi != null)
                    item.KetQua = (listDapAn.Contains((int)item.CauTraLoi)) ? true : false;
                await _chiTietBaiThiService.Update(item.MaChiTietBaiThi, item.CauTraLoi ?? -1, DateTime.Now, item.KetQua ?? false); 
            }
            return Ok();
        }

        [HttpGet("AudioListendCount")]
        public async Task<ActionResult<int>> AudioListendCount([FromQuery] int ma_chi_tiet_ca_thi, [FromQuery] string filename)
        {
            return await _audioListenedService.SelectOne(ma_chi_tiet_ca_thi, filename);
        }
        [HttpGet("AddOrUpdateAudio")]
        public async Task<ActionResult> AddOrUpdate([FromQuery] int ma_chi_tiet_ca_thi, [FromQuery] string filename)
        {
            await _audioListenedService.Save(ma_chi_tiet_ca_thi, filename);
            return Ok();
        }

    }
}
