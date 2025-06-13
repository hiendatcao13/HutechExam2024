using System.Data.SqlClient;
using Hutech.Exam.Server.BUS;
using Hutech.Exam.Server.DAL.Helper;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.DTO.API.Response;
using Hutech.Exam.Shared.DTO.Request.CauHoi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hutech.Exam.Server.Controllers
{
    [Route("api/cauhois")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class CauHoiController(CauHoiService cauHoiService) : Controller
    {
        #region Private Fields

        private readonly CauHoiService _cauHoiService = cauHoiService;

        #endregion

        #region Get Methods

        [HttpGet("{id}")]
        public async Task<ActionResult<CauHoiDto>> SelectOne([FromRoute] int id)
        {
            var result = await _cauHoiService.SelectOne(id);
            if (result.MaCauHoi == 0)
            {
                return NotFound(APIResponse<CauHoiDto>.NotFoundResponse(message: "Không tìm thấy câu hỏi"));
            }
            return Ok(APIResponse<CauHoiDto>.SuccessResponse(data: result, message: "Lấy câu hỏi thành công"));
        }

        [HttpGet("filter-by-nhomcauhoi")]
        public async Task<ActionResult<List<CauHoiDto>>> SelectBy_MaNhom([FromQuery] int maNhomCauHoi)
        {
            var result = await _cauHoiService.SelectBy_MaNhom(maNhomCauHoi);
            return Ok(APIResponse<List<CauHoiDto>>.SuccessResponse(data: result, message: "Lấy danh sách câu hỏi thành công"));
        }

        #endregion

        #region Post Methods

        [HttpPost]
        public async Task<ActionResult<CauHoiDto>> Insert([FromBody] CauHoiCreateRequest cauHoi)
        {
            try
            {
                var id = await _cauHoiService.Insert(cauHoi);
                return Ok(APIResponse<CauHoiDto>.SuccessResponse(data: await _cauHoiService.SelectOne(id), message: "Thêm câu hỏi thành công"));
            }
            catch (SqlException sqlEx)
            {
                return SQLExceptionHelper<CauHoiDto>.HandleSqlException(sqlEx);
            }
            catch (Exception ex)
            {
                return BadRequest(APIResponse<CauHoiDto>.ErrorResponse(message: "Thêm câu hỏi không thành công", errorDetails: ex.Message));
            }
        }

        #endregion

        #region Put Methods

        [HttpPut("{id}")]
        public async Task<ActionResult> Update([FromRoute] int id, [FromBody] CauHoiUpdateRequest cauHoi)
        {
            try
            {
                var result = await _cauHoiService.Update(id, cauHoi);
                if (!result)
                {
                    return NotFound(APIResponse<CauHoiDto>.NotFoundResponse(message: "Không tìm thấy câu hỏi cần cập nhật"));
                }
                return Ok(APIResponse<CauHoiDto>.SuccessResponse(data: await _cauHoiService.SelectOne(id), message: "Cập nhật câu hỏi thành công"));
            }
            catch (SqlException sqlEx)
            {
                return SQLExceptionHelper<CauHoiDto>.HandleSqlException(sqlEx);
            }
            catch (Exception ex)
            {
                return BadRequest(APIResponse<CauHoiDto>.ErrorResponse(message: "Cập nhật câu hỏi không thành công", errorDetails: ex.Message));
            }
        }

        #endregion

        #region Patch Methods



        #endregion

        #region Delete Methods

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            try
            {
                var result = await _cauHoiService.Remove(id);
                if (!result)
                {
                    return NotFound(APIResponse<CauHoiDto>.NotFoundResponse(message: "Không tìm thấy câu hỏi cần xóa"));
                }
                return Ok(APIResponse<CauHoiDto>.SuccessResponse(message: "Xóa câu hỏi thành công"));
            }
            catch (SqlException sqlEx)
            {
                return SQLExceptionHelper<CauHoiDto>.HandleSqlException(sqlEx);
            }
            catch (Exception ex)
            {
                return BadRequest(APIResponse<CauHoiDto>.ErrorResponse(message: "Xóa câu hỏi không thành công hoặc đang dính phải ràng buộc khóa ngoại", errorDetails: ex.Message));
            }
        }

        [HttpDelete("{id}/force")]
        public async Task<ActionResult> ForceDelete([FromRoute] int id)
        {
            try
            {
                var result = await _cauHoiService.ForceRemove(id);
                if (!result)
                {
                    return NotFound(APIResponse<CauHoiDto>.NotFoundResponse(message: "Không tìm thấy câu hỏi cần xóa"));
                }
                return Ok(APIResponse<CauHoiDto>.SuccessResponse(message: "Xóa câu hỏi thành công"));
            }
            catch (SqlException sqlEx)
            {
                return SQLExceptionHelper<CauHoiDto>.HandleSqlException(sqlEx);
            }
            catch (Exception ex)
            {
                return BadRequest(APIResponse<CauHoiDto>.ErrorResponse(message: "Xóa câu hỏi không thành công", errorDetails: ex.Message));
            }
        }

        #endregion

        #region Private Methods


        #endregion

    }
}
