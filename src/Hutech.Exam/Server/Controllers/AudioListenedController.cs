using Hutech.Exam.Server.BUS;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.DTO.API.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hutech.Exam.Server.Controllers
{
    [Route("api/audios")]
    [ApiController]
    [Authorize]
    public class AudioListenedController(AudioListenedService audioListenedService) : Controller
    {
        private readonly AudioListenedService _audioListenedService = audioListenedService;

        
        [HttpPut] // vì là không biết update hay insert nên không dùng id ở đây được
        public async Task<IActionResult> GetSoLanNghe([FromBody] AudioListenedDto audio)
        {
            // do nếu NotFound thì tự động tạo mới nên sẽ không có lỗi NotFound ở đây
            var so_lan_nghe = await _audioListenedService.Save(audio.MaChiTietCaThi, audio.MaNhom);
            return Ok(APIResponse<int>.SuccessResponse(so_lan_nghe));
        }
    }
}
