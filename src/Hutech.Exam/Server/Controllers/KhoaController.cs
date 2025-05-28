using Hutech.Exam.Server.BUS;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.DTO.API.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hutech.Exam.Server.Controllers
{
    [Route("api/khoas")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class KhoaController(KhoaService khoaService) : Controller
    {
        private readonly KhoaService _khoaService = khoaService;

        //////////////////CREATE//////////////////////////

        //////////////////READ////////////////////////////


        [HttpGet]
        public async Task<ActionResult<List<KhoaDto>>> GetAllKhoa()
        {
            return Ok(APIResponse<List<KhoaDto>>.SuccessResponse(data: await _khoaService.GetAll(), message: "Lấy danh sách khoa thành công"));
        }

        //////////////////UDATE///////////////////////////

        //////////////////PATCH///////////////////////////

        //////////////////DELETE//////////////////////////


        //////////////////OTHERS///////////////////////////

        //////////////////PRIVATE///////////////////////////


    }
}
