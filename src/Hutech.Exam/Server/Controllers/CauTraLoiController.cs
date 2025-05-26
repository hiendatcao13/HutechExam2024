using Hutech.Exam.Server.BUS;
using Hutech.Exam.Shared.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Hutech.Exam.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class CauTraLoiController(CauTraLoiService cauTraLoiService, IResponseCacheService cacheService) : Controller
    {
        private readonly CauTraLoiService _cauTraLoiService = cauTraLoiService;
        private readonly IResponseCacheService _cacheService = cacheService;

        [HttpGet("SelectOne")]
        public async Task<ActionResult<CauTraLoiDto>> SelectOne([FromQuery] int ma_cau_tra_loi)
        {
            return Ok(await _cauTraLoiService.SelectBy_MaCauHoi(ma_cau_tra_loi));
        }

        [HttpPost("Insert")]
        public async Task<ActionResult<int>> Insert([FromBody] CauTraLoiDto cauTraLoiDto)
        {
            int id = await _cauTraLoiService.Insert(cauTraLoiDto.MaCauHoi, cauTraLoiDto.ThuTu, cauTraLoiDto.NoiDung ?? "", cauTraLoiDto.LaDapAn, cauTraLoiDto.HoanVi);
            return Ok(id);
        }
        [HttpPut("Update")]
        public async Task<ActionResult> Update([FromBody] CauTraLoiDto cauTraLoiDto)
        {
            await _cauTraLoiService.Update(cauTraLoiDto.MaCauTraLoi, cauTraLoiDto.MaCauHoi, cauTraLoiDto.ThuTu, cauTraLoiDto.NoiDung, cauTraLoiDto.LaDapAn, cauTraLoiDto.HoanVi);
            return Ok();
        }
        [HttpDelete("Remove")]
        public async Task<ActionResult> Remove([FromQuery] int ma_cau_hoi)
        {
            await _cauTraLoiService.Remove(ma_cau_hoi);
            return Ok();
        }


        [HttpGet("GetDapAn")]
        public async Task<Dictionary<int, int>> GetDapAn(long maDeHV)
        {
            var cacheKey = $"DapAn:{maDeHV}";

            // Kiểm tra xem có dữ liệu trong cache không
            var cachedData = await _cacheService.GetCacheResponseAsync<Dictionary<int, int>>(cacheKey);

            if (cachedData != null && cachedData.Count != 0)
            {
                return cachedData;
            }

            // Nếu không có, thực hiện logic lấy dữ liệu từ database
            Dictionary<int, int> listDapAn = await _cauTraLoiService.SelectBy_MaDeHV_DapAn(maDeHV);

            // Lưu vào cache
            await _cacheService.SetCacheResponseAsync(cacheKey, listDapAn, TimeSpan.FromMinutes(150));

            return listDapAn;
        }
    }
}
