using System.Data.SqlClient;
using Hutech.Exam.Server.BUS;
using Hutech.Exam.Server.DAL.Helper;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.DTO.API.Response;
using Hutech.Exam.Shared.DTO.Page;
using Hutech.Exam.Shared.DTO.Request.Khoa;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hutech.Exam.Server.Controllers
{
    [Route("api/khoas")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class KhoaController(KhoaService khoaService) : Controller
    {
        #region Private Fields

        private readonly KhoaService _khoaService = khoaService;

        #endregion

        #region Get Methods

        [HttpGet("{id:int}")]
        public async Task<IActionResult> SelectOne([FromRoute] int id)
        {
            var result = await _khoaService.SelectOne(id);
            if (result == null || result.MaKhoa == 0)
            {
                return NotFound(APIResponse<KhoaDto>.NotFoundResponse(message: "Không tìm thấy khoa"));
            }
            return Ok(APIResponse<KhoaDto>.SuccessResponse(data: result, message: "Lấy khoa thành công"));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllKhoa([FromQuery] int? pageNumber, [FromQuery] int? pageSize)
        {
            if (pageNumber.HasValue && pageSize.HasValue)
            {
                var pagedResult = await _khoaService.GetAll_Paged(pageNumber.Value, pageSize.Value);
                return Ok(APIResponse<Paged<KhoaDto>>.SuccessResponse(pagedResult, "Lấy danh sách khoa thành công"));
            }
            else
            {
                return Ok(APIResponse<List<KhoaDto>>.SuccessResponse(data: await _khoaService.GetAll(), message: "Lấy danh sách khoa thành công"));
            }
        }

        #endregion

        #region Post Methods

        [HttpPost]
        public async Task<IActionResult> Insert([FromBody] KhoaCreateRequest khoaCreateRequest)
        {
            try
            {
                var id = await _khoaService.Insert(khoaCreateRequest);
                return Ok(APIResponse<KhoaDto>.SuccessResponse(data: await _khoaService.SelectOne(id), message: "Thêm khoa thành công"));
            }
            catch (SqlException sqlEx)
            {
                return SQLExceptionHelper<LopAoDto>.HandleSqlException(sqlEx);
            }
            catch (Exception ex)
            {
                return BadRequest(APIResponse<LopAoDto>.ErrorResponse(message: "Thêm khoa không thành công", errorDetails: ex.Message));
            }
        }

        #endregion

        #region Put Methods

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] KhoaUpdateRequest khoa)
        {
            try
            {
                var result = await _khoaService.Update(id, khoa);
                if (!result)
                {
                    return NotFound(APIResponse<KhoaDto>.NotFoundResponse(message: "Không tìm thấy khoa để cập nhật"));
                }
                return Ok(APIResponse<KhoaDto>.SuccessResponse(data: await _khoaService.SelectOne(id), message: "Cập nhật khoa thành công"));
            }
            catch (SqlException sqlEx)
            {
                return SQLExceptionHelper<KhoaDto>.HandleSqlException(sqlEx);
            }
            catch (Exception ex)
            {
                return BadRequest(APIResponse<KhoaDto>.ErrorResponse(message: "Cập nhật khoa không thành công", errorDetails: ex.Message));
            }
        }

        #endregion

        #region Patch Methods



        #endregion

        #region Delete Methods

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                var result = await _khoaService.Remove(id);
                if (!result)
                {
                    return NotFound(APIResponse<KhoaDto>.NotFoundResponse(message: "Xóa khoa không thành công hoặc dính ràng buộc khóa ngoại"));
                }
                return Ok(APIResponse<KhoaDto>.SuccessResponse(message: "Xóa khoa thành công"));
            }
            catch (SqlException sqlEx)
            {
                return SQLExceptionHelper<KhoaDto>.HandleSqlException(sqlEx);
            }
            catch (Exception ex)
            {
                return BadRequest(APIResponse<KhoaDto>.ErrorResponse(message: "Xóa khoa không thành công", errorDetails: ex.Message));
            }
        }

        [HttpDelete("{id:int}/force")]
        public async Task<IActionResult> ForceDelete([FromRoute] int id)
        {
            try
            {
                var result = await _khoaService.ForceRemove(id);
                if (!result)
                {
                    return NotFound(APIResponse<KhoaDto>.NotFoundResponse(message: "Xóa khoa không thành công"));
                }
                return Ok(APIResponse<KhoaDto>.SuccessResponse(message: "Xóa khoa thành công"));
            }
            catch (SqlException sqlEx)
            {
                return SQLExceptionHelper<KhoaDto>.HandleSqlException(sqlEx);
            }
            catch (Exception ex)
            {
                return BadRequest(APIResponse<KhoaDto>.ErrorResponse(message: "Xóa khoa không thành công", errorDetails: ex.Message));
            }
        }

        #endregion

        #region Private Methods


        #endregion

    }
}
