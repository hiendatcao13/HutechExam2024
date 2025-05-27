using Hutech.Exam.Server.BUS;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.DTO.Request.MonHoc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hutech.Exam.Server.Controllers
{
    [Route("api/monhocs")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class MonHocController(MonHocService monHocService) : Controller
    {
        private readonly MonHocService _monHocService = monHocService;

        //////////////////CRUD///////////////////////////

        [HttpGet]
        public async Task<ActionResult<List<MonHocDto>>> GetAll()
        {
            return Ok(await _monHocService.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MonHocDto>> SelectOne([FromRoute] int id)
        {
            return Ok(await _monHocService.SelectOne(id));
        }

        [HttpPost]
        public async Task<ActionResult<int>> Insert([FromBody] MonHocCreateRequest monHoc)
        {
            var result = await _monHocService.Insert(monHoc.MaSoMonHoc, monHoc.TenMonHoc);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> Update([FromRoute] int id, [FromBody] MonHocUpdateRequest monHoc)
        {
            var result = await _monHocService.Update(id, monHoc.MaSoMonHoc, monHoc.TenMonHoc);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete([FromRoute] int id)
        {
            var result = await _monHocService.Remove(id);
            return Ok(result);
        }

        //////////////////FILTER///////////////////////////

        //////////////////OTHERS///////////////////////////

        //////////////////PRIVATE///////////////////////////


    }
}
