using AutoMapper;
using Hutech.Exam.Server.BUS;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.DTO.Custom;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Text.Json;
using static MudBlazor.CategoryTypes;

namespace Hutech.Exam.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CustomChiTietBaiThiController : Controller
    {
        private readonly ChiTietBaiThiService _chiTietBaiThiService;
        private readonly ChiTietDeThiHoanViService _chiTietDeThiHoanViService;
        private readonly IMapper _mapper;
        public CustomChiTietBaiThiController(ChiTietBaiThiService chiTietBaiThiService,  IMapper mapper, ChiTietDeThiHoanViService chiTietDeThiHoanViService)
        {
            _chiTietBaiThiService = chiTietBaiThiService;
            _chiTietDeThiHoanViService = chiTietDeThiHoanViService;
            _mapper = mapper;
        }
        [HttpGet("GetBaiThi_DaThi")] // khi này sinh viên đã thi trước đó và tiếp tục thi nên không cần insert chi tiet bai thi nữa mà chỉ lấy -> tối ưu API
        public async Task<ActionResult<List<CustomChiTietBaiThi>>> GetBaiThi_DaThi([FromQuery] int ma_chi_tiet_ca_thi)
        {
            try
            {
                // tránh trường hợp lấy đề của những môn khác
                var chiTietBaiThis = await _chiTietBaiThiService.SelectBy_ma_chi_tiet_ca_thi(ma_chi_tiet_ca_thi);
                List<CustomChiTietBaiThi> result = [];
                foreach(var item in chiTietBaiThis)
                {
                    result.Add(_mapper.Map<CustomChiTietBaiThi>(item));
                }
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPut("Update")]
        public async Task<ActionResult> Update([FromBody] List<CustomChiTietBaiThi> chiTietBaiThis)
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
            //if (item.ThuTu != 0)
            //{
            //    long ma_chi_tiet_bai_thi = await _chiTietBaiThiService.Insert(item.MaChiTietCaThi, item.MaDeHv, item.MaNhom, item.MaCauHoi, DateTime.Now, item.ThuTu);
            //    await _chiTietBaiThiService.Update(ma_chi_tiet_bai_thi, item.CauTraLoi ?? -1, DateTime.Now, item.KetQua ?? false);
            //}
            //else
            //{
            //    await _chiTietBaiThiService.Update_v2(item.MaChiTietCaThi, item.MaCauHoi, item.CauTraLoi ?? -1, DateTime.Now, item.KetQua ?? false);
            //}
            try
            {
                List<int>? listDapAn = await this.GetListDapAnWithCacheAsync(chiTietBaiThis[0].MaDeHv);
                foreach (var item in chiTietBaiThis)
                {
                    item.KetQua = listDapAn?.Contains(item.CauTraLoi ?? -1) ?? false;
                    await _chiTietBaiThiService.Save(item.MaChiTietCaThi, item.MaDeHv, item.MaNhom, item.MaCauHoi, item.CauTraLoi ?? -1, DateTime.Now, DateTime.Now, item.KetQua ?? false, item.ThuTu);
                }
                return Ok();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest();
            }
        }
        [HttpPut("GetListDungSai")]
        public async Task<ActionResult<int>> GetListDungSai([FromQuery] long ma_de_hv, [FromBody] List<int?> listDapAnKhoanh)
        {
            List<int>? listDapAn = await this.GetListDapAnWithCacheAsync(ma_de_hv);
            List<bool?> result = [];
            foreach (var item in listDapAnKhoanh)
            {
                result.Add((item != null) ? listDapAn?.Contains((int)item) : null);
            }
            return Ok(result);
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
    }
}
