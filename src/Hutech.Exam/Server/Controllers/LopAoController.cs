using System.Data.SqlClient;
using Hutech.Exam.Server.BUS;
using Hutech.Exam.Server.DAL.Helper;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.DTO.API.Response;
using Hutech.Exam.Shared.DTO.Request.LopAo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hutech.Exam.Server.Controllers
{
    [Route("api/lopaos")]
    [ApiController]
    [Authorize(Roles = "QuanTri")]
    public class LopAoController(LopAoService lopAoService) : Controller
    {
        #region Private Fields

        private readonly LopAoService _lopAoService = lopAoService;

        private const string NotFoundMessage = "Không tìm thấy lớp ảo";

        #endregion

        #region Get Methods

        [HttpGet("{id:int}")]
        public async Task<IActionResult> SelectOne([FromRoute] int id)
        {
            var result = await _lopAoService.SelectOne(id);
            if (result.MaLopAo == 0)
            {
                return NotFound(APIResponse<LopAoDto>.NotFoundResponse(message: "Không tìm thấy phòng thi"));
            }
            return Ok(APIResponse<LopAoDto>.SuccessResponse(data: result, message: "Lấy phòng thi thành công"));
        }

        [HttpGet("filter-by-monhoc")]
        public async Task<IActionResult> SelectBy_MaMonHoc([FromQuery] int maMonHoc)
        {
            return Ok(APIResponse<List<LopAoDto>>.SuccessResponse(data: await _lopAoService.SelectBy_ma_mon_hoc(maMonHoc), message: "Lấy danh sách phòng thi thành công"));
        }

        #endregion

        #region Post Methods

        [HttpPost]
        public async Task<IActionResult> Insert([FromBody] LopAoCreateRequest lopAo)
        {
            var id = await _lopAoService.Insert(lopAo);
            return Ok(APIResponse<LopAoDto>.SuccessResponse(data: await _lopAoService.SelectOne(id), message: "Thêm phòng thi thành công"));
        }

        #endregion

        #region Put Methods

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] LopAoUpdateRequest lopAo)
        {
            var result = await _lopAoService.Update(id, lopAo);
            if (!result)
            {
                return NotFound(APIResponse<LopAoDto>.NotFoundResponse(message: NotFoundMessage));
            }
            return Ok(APIResponse<LopAoDto>.SuccessResponse(data: await _lopAoService.SelectOne(id), message: "Cập nhật phòng thi thành công"));
        }

        #endregion

        #region Patch Methods



        #endregion

        #region Delete Methods

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var result = await _lopAoService.Remove(id);
            if (!result)
            {
                return NotFound(APIResponse<LopAoDto>.NotFoundResponse(message: NotFoundMessage));
            }
            return Ok(APIResponse<LopAoDto>.SuccessResponse("Xóa phòng thi thành công"));
        }

        [HttpDelete("{id:int}/force")]
        public async Task<IActionResult> ForceDelete([FromRoute] int id)
        {
            var result = await _lopAoService.ForceRemove(id);
            if (!result)
            {
                return NotFound(APIResponse<LopAoDto>.NotFoundResponse(message: NotFoundMessage));
            }
            return Ok(APIResponse<LopAoDto>.SuccessResponse("Xóa phòng thi thành công"));
        }

        #endregion

        #region Private Methods


        #endregion

    }
}
