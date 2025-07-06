using System.Data.SqlClient;
using System.Threading.Tasks;
using Hutech.Exam.Server.BUS;
using Hutech.Exam.Server.DAL.Helper;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.DTO.API.Response;
using Hutech.Exam.Shared.DTO.Page;
using Hutech.Exam.Shared.DTO.Request.Lop;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hutech.Exam.Server.Controllers
{
    [Route("api/lops")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class LopController(LopService lopService) : Controller
    {
        #region Private Fields

        private readonly LopService _lopService = lopService;

        private const string NotFoundMessage = "Không tìm thấy lớp học";

        #endregion

        #region Get Methods

        [HttpGet("{id}")]
        public async Task<IActionResult> SelectOne([FromRoute] int id)
        {
            var result = await _lopService.SelectOne(id);
            if (result == null || result.MaLop == 0)
            {
                return NotFound(APIResponse<LopDto>.NotFoundResponse(message: "Không tìm thấy lớp học"));
            }
            return Ok(APIResponse<LopDto>.SuccessResponse(data: result, message: "Lấy lớp học thành công"));
        }


        [HttpGet("filter-by-khoa")]
        public async Task<IActionResult> SelectBy_ma_khoa_Paged([FromQuery] int maKhoa, [FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            return Ok(APIResponse<Paged<LopDto>>.SuccessResponse(data: await _lopService.SelectBy_ma_khoa_Paged(maKhoa, pageNumber, pageSize), message: "Lấy danh sách lớp thành công"));
        }

        #endregion

        #region Post Methods

        [HttpPost]
        public async Task<IActionResult> Insert([FromBody] LopCreateRequest lop)
        {
            var id = await _lopService.Insert(lop);
            return Ok(APIResponse<LopDto>.SuccessResponse(data: await _lopService.SelectOne(id), message: "Thêm lớp học thành công"));
        }

        #endregion

        #region Put Methods

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] LopUpdateRequest lop)
        {
            var result = await _lopService.Update(id, lop);
            if (!result)
            {
                return NotFound(APIResponse<LopDto>.NotFoundResponse(message: NotFoundMessage));
            }
            return Ok(APIResponse<LopDto>.SuccessResponse(data: await _lopService.SelectOne(id), message: "Cập nhật lớp học thành công"));
        }

        #endregion

        #region Patch Methods



        #endregion

        #region Delete Methods

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var result = await _lopService.Remove(id);
            if (!result)
            {
                return NotFound(APIResponse<LopDto>.NotFoundResponse(message: "Xóa lớp học không thành công hoặc dính phải các khóa ràng buộc"));
            }
            return Ok(APIResponse<LopDto>.SuccessResponse(message: "Xóa lớp học thành công"));

        }

        [HttpDelete("{id}/force")]
        public async Task<IActionResult> ForceDelete([FromRoute] int id)
        {
            var result = await _lopService.ForceRemove(id);
            if (!result)
            {
                return NotFound(APIResponse<LopDto>.NotFoundResponse(message: NotFoundMessage));
            }
            return Ok(APIResponse<LopDto>.SuccessResponse(message: "Xóa lớp học thành công"));

        }

        #endregion

        #region Private Methods


        #endregion

    }
}
