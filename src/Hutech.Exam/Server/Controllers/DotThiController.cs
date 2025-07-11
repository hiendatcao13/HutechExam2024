﻿using System.Data.SqlClient;
using Hutech.Exam.Server.BUS;
using Hutech.Exam.Server.DAL.Helper;
using Hutech.Exam.Server.Hubs;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.DTO.API.Response;
using Hutech.Exam.Shared.DTO.Page;
using Hutech.Exam.Shared.DTO.Request.DotThi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Hutech.Exam.Server.Controllers
{
    [Route("api/dotthis")]
    [ApiController]
    [Authorize(Roles = "QuanTri")]
    public class DotThiController(DotThiService dotThiService, IHubContext<AdminHub> mainHub) : Controller
    {
        #region Private Fields

        private readonly DotThiService _dotThiService = dotThiService;

        private readonly IHubContext<AdminHub> _mainHub = mainHub;

        private const string NotFoundMessage = "Không tìm thấy đợt thi";

        #endregion

        #region Get Methods

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int? pageNumber, [FromQuery] int? pageSize)
        {
            if (pageNumber.HasValue && pageSize.HasValue)
            {
                var pagedResult = await _dotThiService.GetAll_Paged(pageNumber.Value, pageSize.Value);
                return Ok(APIResponse<Paged<DotThiDto>>.SuccessResponse(pagedResult, "Lấy danh sách đợt thi thành công"));
            }
            var list = await _dotThiService.GetAll();
            return Ok(APIResponse<List<DotThiDto>>.SuccessResponse(list, "Lấy danh sách đợt thi thành công"));
        }


        [HttpGet("{id:int}")]
        public async Task<IActionResult> SelectOne([FromRoute] int id)
        {
            return Ok(APIResponse<DotThiDto>.SuccessResponse(data: await _dotThiService.SelectOne(id), message: "Lấy đợt thi thành công"));
        }

        #endregion

        #region Post Methods

        [HttpPost]
        [Authorize(Roles = "DaoTao,Admin")]
        public async Task<IActionResult> Insert([FromBody] DotThiCreateRequest dotThi)
        {
            var id = await _dotThiService.Insert(dotThi);
            return Ok(APIResponse<DotThiDto>.SuccessResponse(data: await _dotThiService.SelectOne(id), message: "Thêm đợt thi thành công"));
        }

        #endregion

        #region Put Methods

        [HttpPut("{id:int}")]
        [Authorize(Roles = "DaoTao,Admin")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] DotThiUpdateRequest dotThi)
        {
            var result = await _dotThiService.Update(id, dotThi);
            if (!result)
            {
                return NotFound(APIResponse<DotThiDto>.NotFoundResponse(message: NotFoundMessage));
            }
            return Ok(APIResponse<DotThiDto>.SuccessResponse(data: await _dotThiService.SelectOne(id), message: "Cập nhật đợt thi thành công"));
        }


        #endregion

        #region Patch Methods



        #endregion

        #region Delete Methods

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "DaoTao,Admin")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var result = await _dotThiService.Remove(id);
            if (!result)
            {
                return NotFound(APIResponse<DotThiDto>.NotFoundResponse(message: NotFoundMessage));
            }
            return Ok(APIResponse<DotThiDto>.SuccessResponse(message: "Xóa đợt thi thành công"));
        }

        [HttpDelete("{id:int}/force")]
        [Authorize(Roles = "DaoTao,Admin")]
        public async Task<IActionResult> ForceDelete([FromRoute] int id)
        {
            var result = await _dotThiService.ForceRemove(id);
            if (!result)
            {
                return NotFound(APIResponse<DotThiDto>.NotFoundResponse(message: NotFoundMessage));
            }
            return Ok(APIResponse<DotThiDto>.SuccessResponse(message: "Xóa đợt thi thành công"));
        }

        #endregion

        #region Private Methods


        #endregion

    }
}
