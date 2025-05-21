using Hutech.Exam.Server.BUS;
using Hutech.Exam.Shared.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hutech.Exam.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AudioListenedController(AudioListenedService audioListenedService) : Controller
    {
        private readonly AudioListenedService _audioListenedService = audioListenedService;

        //[HttpGet("GetSoLanNghe")]
        //public async Task<ActionResult<int>> GetSoLanNghe([FromQuery] int ma_chi_tiet_ca_thi, [FromQuery] string filename)
        //{
        //    return Ok(await _audioListenedService.SelectOne(ma_chi_tiet_ca_thi, filename));
        //}
        [HttpPut("GetSoLanNghe")]
        public async Task<ActionResult<int>> GetSoLanNghe([FromBody] AudioListenedDto audio)
        {
            return Ok(await _audioListenedService.Save(audio.MaChiTietCaThi, audio.MaNhom));
        }
    }
}
