using Hutech.Exam.Server.BUS;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.DTO.API.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hutech.Exam.Server.Controllers
{
    [Route("api/dethis")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class DeThiController(DeThiService deThiService) : Controller
    {
        private readonly DeThiService _deThiService = deThiService;


        //////////////////POST//////////////////////////

        //////////////////GET////////////////////////////

        [HttpGet]
        public async Task<ActionResult<List<DeThiDto>>> GetAll()
        {
            return Ok(APIResponse<List<DeThiDto>>.SuccessResponse(data: await _deThiService.GetAll(), message: "Lấy danh sách đề thi thành công"));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DeThiDto>> SelectOne([FromRoute] int id)
        {
            var result = await _deThiService.SelectOne(id);
            if(result.MaDeThi == 0)
            {
                return NotFound(APIResponse<DeThiDto>.NotFoundResponse(message: "Không tìm thấy đề thi"));
            }    
            return Ok(APIResponse<DeThiDto>.SuccessResponse(data: result, message: "Lấy đề thi thành công"));
        }

        [HttpGet("filter-by-monhoc")]
        public async Task<ActionResult<List<DeThiDto>>> SelectByMonHoc([FromQuery] int maMonHoc)
        {
            return Ok(APIResponse<List<DeThiDto>>.SuccessResponse(data: await _deThiService.SelectByMonHoc(maMonHoc), message: "Lấy danh sách đề thi thành công"));
        }

        //////////////////POST///////////////////////////

        //////////////////PATCH///////////////////////////

        //////////////////DELETE//////////////////////////


        //////////////////OTHERS///////////////////////////

        //////////////////PRIVATE///////////////////////////



    }
}
