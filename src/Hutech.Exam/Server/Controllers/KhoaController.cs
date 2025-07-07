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
    [Authorize(Roles = "QuanTri")]
    public class KhoaController(KhoaService khoaService) : Controller
    {
        #region Private Fields

        private readonly KhoaService _khoaService = khoaService;

        private const string NotFoundMessage = "Không tìm thấy khoa";

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
        [Authorize(Roles = "DaoTao")]
        public async Task<IActionResult> Insert([FromBody] KhoaCreateRequest khoaCreateRequest)
        {
            var id = await _khoaService.Insert(khoaCreateRequest);
            return Ok(APIResponse<KhoaDto>.SuccessResponse(data: await _khoaService.SelectOne(id), message: "Thêm khoa thành công"));
        }

        #endregion

        #region Put Methods

        [HttpPut("{id:int}")]
        [Authorize(Roles = "DaoTao")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] KhoaUpdateRequest khoa)
        {
            var result = await _khoaService.Update(id, khoa);
            if (!result)
            {
                return NotFound(APIResponse<KhoaDto>.NotFoundResponse(message: NotFoundMessage));
            }
            return Ok(APIResponse<KhoaDto>.SuccessResponse(data: await _khoaService.SelectOne(id), message: "Cập nhật khoa thành công"));
        }

        #endregion

        #region Patch Methods



        #endregion

        #region Delete Methods

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "DaoTao")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var result = await _khoaService.Remove(id);
            if (!result)
            {
                return NotFound(APIResponse<KhoaDto>.NotFoundResponse(message: NotFoundMessage));
            }
            return Ok(APIResponse<KhoaDto>.SuccessResponse(message: "Xóa khoa thành công"));
        }

        [HttpDelete("{id:int}/force")]
        [Authorize(Roles = "DaoTao")]
        public async Task<IActionResult> ForceDelete([FromRoute] int id)
        {
            var result = await _khoaService.ForceRemove(id);
            if (!result)
            {
                return NotFound(APIResponse<KhoaDto>.NotFoundResponse(message: NotFoundMessage));
            }
            return Ok(APIResponse<KhoaDto>.SuccessResponse(message: "Xóa khoa thành công"));
        }

        #endregion

        #region Private Methods


        #endregion

    }
}
