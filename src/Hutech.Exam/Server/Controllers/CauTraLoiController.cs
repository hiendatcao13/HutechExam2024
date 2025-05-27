using Hutech.Exam.Server.BUS;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.DTO.Request;
using Hutech.Exam.Shared.DTO.Request.CauTraLoi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Hutech.Exam.Server.Controllers
{
    [Route("api/cautralois")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class CauTraLoiController(CauTraLoiService cauTraLoiService, IResponseCacheService cacheService) : Controller
    {
        private readonly CauTraLoiService _cauTraLoiService = cauTraLoiService;

        private readonly IResponseCacheService _cacheService = cacheService;

        //////////////////CRUD///////////////////////////
        [HttpGet("{id}")]
        public async Task<ActionResult<CauTraLoiDto>> SelectOne([FromRoute] int id)
        {
            return Ok(await _cauTraLoiService.SelectBy_MaCauHoi(id));
        }

        [HttpPost]
        public async Task<ActionResult<int>> Insert([FromBody] CauTraLoiCreateRequest cauTraLoi)
        {
            int id = await _cauTraLoiService.Insert(cauTraLoi.MaCauHoi, cauTraLoi.ThuTu, cauTraLoi.NoiDung, cauTraLoi.LaDapAn, cauTraLoi.HoanVi);
            return Ok(id);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update([FromRoute] int id, [FromBody] CauTraLoiUpdateRequest cauTraLoi)
        {
            await _cauTraLoiService.Update(id, cauTraLoi.MaCauHoi, cauTraLoi.ThuTu, cauTraLoi.NoiDung, cauTraLoi.LaDapAn, cauTraLoi.HoanVi);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            await _cauTraLoiService.Remove(id);
            return Ok();
        }

        //////////////////FILTER///////////////////////////
        
        //////////////////OTHERS///////////////////////////

        //////////////////PRIVATE//////////////////////////
    }
}
