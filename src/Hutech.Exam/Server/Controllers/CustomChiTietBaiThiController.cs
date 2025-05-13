using AutoMapper;
using Hutech.Exam.Server.BUS;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.DTO.Request;
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
    public class CustomChiTietBaiThiController(ChiTietBaiThiService chiTietBaiThiService, IMapper mapper) : Controller
    {
        private readonly ChiTietBaiThiService _chiTietBaiThiService = chiTietBaiThiService;
        private readonly IMapper _mapper = mapper;

        [HttpGet("GetBaiThi_DaThi")] // khi này sinh viên đã thi trước đó và tiếp tục thi nên không cần insert chi tiet bai thi nữa mà chỉ lấy -> tối ưu API
        public async Task<ActionResult<List<int>>> GetBaiThi_DaThi([FromQuery] int ma_chi_tiet_ca_thi)
        {
            return Ok(await _chiTietBaiThiService.DaThi(ma_chi_tiet_ca_thi));
        }
        //[HttpPut("Update")]
        //public async Task<ActionResult> Update([FromBody] List<ChiTietBaiThiRequest> chiTietBaiThis)
        //{
        //    try
        //    {
        //        List<int>? listDapAn = await this.GetListDapAnWithCacheAsync(chiTietBaiThis[0].MaDeHv);
        //        foreach (var item in chiTietBaiThis)
        //        {
        //            item.KetQua = listDapAn?.Contains(item.CauTraLoi ?? -1) ?? false;
        //        }
        //        var result = _mapper.Map<List<ChiTietBaiThiDto>>(chiTietBaiThis);
        //        await _chiTietBaiThiService.Save_Batch(result);
        //        return Ok();
        //    }
        //    catch(Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //        return BadRequest();
        //    }
        //}





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
            List<int> listDapAn = await _chiTietBaiThiService.SelectBy_MaDe_DapAn(maDeHv);

            // Lưu vào cache
            await cacheService.SetCacheResponseAsync(cacheKey, listDapAn, TimeSpan.FromMinutes(120));

            return listDapAn;
        }
    }
}
