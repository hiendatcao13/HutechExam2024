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
        #region Private Fields

        private readonly AudioListenedService _audioListenedService = audioListenedService;

        #endregion

        #region Get Methods



        #endregion

        #region Post Methods



        #endregion

        #region Put Methods

        [HttpPut] // vì là không biết update hay insert nên không dùng id ở đây được
        public async Task<IActionResult> GetSoLanNghe([FromBody] AudioListenedDto audio)
        {
            // do nếu NotFound thì tự động tạo mới nên sẽ không có lỗi NotFound ở đây
            var so_lan_nghe = await _audioListenedService.Save(audio);
            return Ok(APIResponse<int?>.SuccessResponse(data: so_lan_nghe, message: ""));
        }

        #endregion

        #region Patch Methods



        #endregion

        #region Delete Methods



        #endregion

        #region Private Methods


        #endregion

    }
}
