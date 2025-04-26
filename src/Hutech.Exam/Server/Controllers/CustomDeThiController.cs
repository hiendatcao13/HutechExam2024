using Hutech.Exam.Server.Attributes;
using Hutech.Exam.Server.BUS;
using Hutech.Exam.Shared.DTO.Custom;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hutech.Exam.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CustomDeThiController : Controller
    {
        private readonly CustomDeThiService _customDeThiService;

        public CustomDeThiController(CustomDeThiService customDeThiService)
        {
            _customDeThiService = customDeThiService;
        }
        [HttpGet("GetDeThi")]
        [Cache(120)]
        public async Task<ActionResult<List<CustomDeThi>>> GetDeThi([FromQuery] long ma_de_thi_hoan_vi)
        {
            try
            {
                return Ok(await _customDeThiService.handleDeThi(ma_de_thi_hoan_vi));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
