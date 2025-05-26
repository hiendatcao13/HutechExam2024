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
    public class ChiTietBaiThiController(ChiTietBaiThiService chiTietBaiThiService) : Controller
    {
        private readonly ChiTietBaiThiService _chiTietBaiThiService = chiTietBaiThiService;

        [HttpGet("GetBaiThi_DaThi")] // khi này sinh viên đã thi trước đó và tiếp tục thi nên không cần insert chi tiet bai thi nữa mà chỉ lấy -> tối ưu API
        public async Task<ActionResult<List<int>>> GetBaiThi_DaThi([FromQuery] int ma_chi_tiet_ca_thi)
        {
            return Ok(await _chiTietBaiThiService.DaThi(ma_chi_tiet_ca_thi));
        }
        [HttpGet("SelectBy_MaChiTietCaThi")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<ChiTietBaiThiDto>>> SelectBy_ma_chi_tiet_ca_thi([FromQuery] int ma_chi_tiet_ca_thi)
        {
            return Ok(await _chiTietBaiThiService.SelectBy_ma_chi_tiet_ca_thi(ma_chi_tiet_ca_thi));
        }

    }
}
