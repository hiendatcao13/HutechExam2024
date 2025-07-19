using System.Data.SqlClient;
using Hutech.Exam.Server.BUS;
using Hutech.Exam.Server.DAL.Helper;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.DTO.API.Response;
using Hutech.Exam.Shared.DTO.Request.Clo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hutech.Exam.Server.Controllers
{
    [Route("api/clos")]
    [ApiController]
    [Authorize(Roles = "QuanTri")]
    public class CloController(CloService cloService) : Controller
    {
        #region Private Fields

        private readonly CloService _cloService = cloService;

        #endregion

        #region Get Methods

        [HttpGet("{id:int}")]
        public async Task<IActionResult> SelectOne([FromRoute] int id)
        {
            var result = await _cloService.SelectOne(id);
            if (result.MaClo == 0)
            {
                return NotFound(APIResponse<CloDto>.NotFoundResponse(message: "Không tìm thấy CLO"));
            }
            return Ok(APIResponse<CloDto>.SuccessResponse(data: result, message: "Lấy CLO thành công"));
        }

        [HttpGet("filter-by-monhoc")]
        public async Task<IActionResult> SelectBy_MaMonHoc([FromQuery] int maMonHoc)
        {
            return Ok(APIResponse<List<CloDto>>.SuccessResponse(data: await _cloService.SelectBy_MaMonHoc(maMonHoc), message: "Lấy danh sách CLO thành công"));
        }

        #endregion

        #region Post Methods

        [HttpPost]
        [Authorize(Roles = "KhaoThi,Admin")]
        public async Task<IActionResult> Insert([FromBody] CloCreateRequest clo)
        {
            try
            {
                var id = await _cloService.Insert(clo);
                return Ok(APIResponse<CloDto>.SuccessResponse(data: await _cloService.SelectOne(id), message: "Thêm CLO thành công"));
            }
            catch (SqlException sqlEx)
            {
                return SQLExceptionHelper<CloDto>.HandleSqlException(sqlEx);
            }
            catch (Exception ex)
            {
                return BadRequest(APIResponse<CloDto>.ErrorResponse(message: "Thêm CLO không thành công", errorDetails: ex.Message));
            }
        }

        #endregion

        #region Put Methods

        [HttpPut("{id:int}")]
        [Authorize(Roles = "KhaoThi,Admin")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] CloUpdateRequest clo)
        {
            try
            {
                var result = await _cloService.Update(id, clo);
                if (!result)
                {
                    return NotFound(APIResponse<CloDto>.NotFoundResponse(message: "Không tìm thấy CLO cần cập nhật"));
                }
                return Ok(APIResponse<CloDto>.SuccessResponse(data: await _cloService.SelectOne(id), message: "Cập nhật CLO thành công"));
            }
            catch (SqlException sqlEx)
            {
                return SQLExceptionHelper<CloDto>.HandleSqlException(sqlEx);
            }
            catch (Exception ex)
            {
                return BadRequest(APIResponse<CloDto>.ErrorResponse(message: "Cập nhật CLO không thành công", errorDetails: ex.Message));
            }
        }

        #endregion

        #region Patch Methods



        #endregion

        #region Delete Methods

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "KhaoThi,Admin")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                var result = await _cloService.Remove(id);
                if (!result)
                {
                    return NotFound(APIResponse<CloDto>.NotFoundResponse(message: "Xóa CLO không thành công hoặc đang dính phải ràng buộc khóa ngoại"));
                }
                return Ok();
            }
            catch (SqlException sqlEx)
            {
                return SQLExceptionHelper<CloDto>.HandleSqlException(sqlEx);
            }
            catch (Exception ex)
            {
                return BadRequest(APIResponse<CloDto>.ErrorResponse(message: "Xóa CLO không thành công", errorDetails: ex.Message));
            }
        }

        [HttpDelete("{id:int}/force")]
        [Authorize(Roles = "KhaoThi,Admin")]
        public async Task<IActionResult> ForceDelete([FromRoute] int id)
        {
            try
            {
                var result = await _cloService.ForceRemove(id);
                if (!result)
                {
                    return NotFound(APIResponse<CloDto>.NotFoundResponse(message: "Xóa CLO không thành công"));
                }
                return Ok();
            }
            catch (SqlException sqlEx)
            {
                return SQLExceptionHelper<CloDto>.HandleSqlException(sqlEx);
            }
            catch (Exception ex)
            {
                return BadRequest(APIResponse<CloDto>.ErrorResponse(message: "Xóa CLO không thành công", errorDetails: ex.Message));
            }
        }

        #endregion

        #region Private Methods


        #endregion

    }
}
