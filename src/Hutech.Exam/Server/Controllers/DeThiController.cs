using System.Data.SqlClient;
using System.Text.Json;
using AutoMapper;
using Hutech.Exam.Server.BUS;
using Hutech.Exam.Server.DAL.Helper;
using Hutech.Exam.Server.DAL.Repositories;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.DTO.API.Response;
using Hutech.Exam.Shared.DTO.Custom;
using Hutech.Exam.Shared.DTO.Page;
using Hutech.Exam.Shared.DTO.Request.DeThi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hutech.Exam.Server.Controllers
{
    [Route("api/dethis")]
    [ApiController]
    [Authorize]
    public partial class DeThiController : Controller
    {
        #region Private Fields

        private readonly DeThiService _deThiService;
        private readonly RedisService _redisService;
        private readonly string _approvedExamsUrl;
        private readonly string _sampleExamUrl;
        //private readonly CustomMaDeThiService _customMaDeThiService = customMaDeThiService;
        //private readonly CustomThongKeService _customThongKeService = customThongKeService;

        private readonly IMapper _mapper;


        private const string NotFoundMessage = "Không tìm thấy đề thi";

        public DeThiController(DeThiService deThiService, RedisService redisService, IConfiguration configuration, IMapper mapper)
        {
            _deThiService = deThiService;
            _redisService = redisService;

            _approvedExamsUrl = configuration["ExternalApiSettings:ApprovedExamsUrl"]!;
            _sampleExamUrl = configuration["ExternalApiSettings:SampleExamUrl"]!;
            _mapper = mapper;
        }
        #endregion

        #region Get Methods


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(APIResponse<List<DeThiDto>>.SuccessResponse(data: await _deThiService.GetAll(), message: "Lấy danh sách đề thi thành công"));
        }

        [HttpGet("mock")]
        public async Task<IActionResult> GetAllDeThi()
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (compatible; MyApp/1.0)");

            var response = await httpClient.GetAsync(_approvedExamsUrl);
            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode, "Không tìm thấy API từ bên ngoài");
            }

            var jsonData = await response.Content.ReadAsStringAsync();

            var exams = JsonSerializer.Deserialize<APIResponse<List<DeThiMock>>>(jsonData) ?? new();
            var result = _mapper.Map<List<DeThiDto>>(exams.Data) ?? [];

            return Ok(APIResponse<List<DeThiDto>>.SuccessResponse(data: result, message: "Lấy danh sách đề thi thành công"));
        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> SelectOne([FromRoute] long id)
        {
            var result = await _deThiService.SelectOne(id);
            if (result.MaDeThi == 0)
            {
                return NotFound(APIResponse<DeThiDto>.NotFoundResponse(message: "Không tìm thấy đề thi"));
            }
            return Ok(APIResponse<DeThiDto>.SuccessResponse(data: result, message: "Lấy đề thi thành công"));
        }

        [HttpGet("{id:long}/de-thi")]
        public async Task<IActionResult> GetDeThi([FromRoute] long id)
        {
            var deThi = await _deThiService.SelectOne(id);
            var result = await _redisService.GetDeThiAsync(deThi.Guid);

            return Ok(APIResponse<List<CustomDeThi>>.SuccessResponse(data: result, message: "Lấy nội dung đề thi thành công"));
        }

        [HttpGet("{id:long}/dap-an")]
        public async Task<IActionResult> GetDapAn([FromRoute] long id)
        {
            var deThi = await _deThiService.SelectOne(id);
            var result = await _redisService.GetDapAnAsync(deThi.Guid);

            return Ok(APIResponse<Dictionary<Guid, Guid>>.SuccessResponse(data: result, message: "Lấy nội dung đề thi thành công"));
        }

        [HttpGet("filter-by-monhoc")]
        [Authorize(Roles = "QuanTri")]
        public async Task<IActionResult> SelectByMonHoc([FromQuery] int maMonHoc, [FromQuery] int? pageNumber, [FromQuery] int? pageSize)
        {
            if (pageNumber.HasValue && pageSize.HasValue)
            {
                var pagedResult = await _deThiService.SelectByMonHoc_Paged(maMonHoc, pageNumber.Value, pageSize.Value);
                return Ok(APIResponse<Paged<DeThiDto>>.SuccessResponse(pagedResult, message: "Lấy danh sách đề thi thành công"));
            }
            return Ok(APIResponse<List<DeThiDto>>.SuccessResponse(data: await _deThiService.SelectByMonHoc(maMonHoc), message: "Lấy danh sách đề thi thành công"));
        }

        [HttpGet("{id:long}/report")]
        public async Task<IActionResult> ThongKeDiem_SelectBy_DeThi([FromRoute] long id)
        {
            var result = await _deThiService.Report(id);
            return Ok(APIResponse<List<CustomThongKeDiem>>.SuccessResponse(data: result, message: "Lấy dữ liệu thống kê thành công"));
        }

        #endregion

        #region Post Methods

        [HttpPost]
        [Authorize(Roles = "KhaoThi,Admin")]
        public async Task<IActionResult> Insert([FromBody] DeThiCreateRequest deThi)
        {
            var id = await _deThiService.Insert(deThi);
            return Ok(APIResponse<DeThiDto>.SuccessResponse(data: await _deThiService.SelectOne(id), message: "Thêm đề thi thành công"));
        }

        [HttpPost("batch")]
        [Authorize(Roles = "KhaoThi,Admin")]
        public async Task<IActionResult> Save_Batch([FromBody] List<DeThiDto> deThis)
        {
            await _deThiService.Save_Batch(deThis);
            return Ok(APIResponse<DeThiDto>.SuccessResponse(message: "Thêm danh sách đề thi thành công"));
        }


        #endregion

        #region Put Methods

        [HttpPut("{id:long}")]
        [Authorize(Roles = "KhaoThi,Admin")]
        public async Task<IActionResult> Update([FromRoute] long id, [FromBody] DeThiUpdateRequest deThi)
        {
            var result = await _deThiService.Update(id, deThi);
            if (!result)
            {
                return NotFound(APIResponse<DeThiDto>.NotFoundResponse(message: NotFoundMessage));
            }
            return Ok(APIResponse<DeThiDto>.SuccessResponse(data: await _deThiService.SelectOne(id), message: "Cập nhật đề thi thành công"));
        }

        #endregion

        #region Patch Methods



        #endregion

        #region Delete Methods

        [HttpDelete("{id:long}")]
        [Authorize(Roles = "KhaoThi,Admin")]
        public async Task<IActionResult> Delete([FromRoute] long id)
        {
            var result = await _deThiService.Delete(id);
            if (!result)
            {
                return NotFound(APIResponse<DeThiDto>.NotFoundResponse(message: NotFoundMessage));
            }
            return Ok(APIResponse<DeThiDto>.SuccessResponse(message: "Xóa đề thi thành công"));
        }

        [HttpDelete("{id:long}/force")]
        [Authorize(Roles = "KhaoThi,Admin")]
        public async Task<IActionResult> ForceDelete([FromRoute] long id)
        {
            var result = await _deThiService.ForceDelete(id);
            if (!result)
            {
                return NotFound(APIResponse<DeThiDto>.NotFoundResponse(message: NotFoundMessage));
            }
            return Ok(APIResponse<DeThiDto>.SuccessResponse(message: "Xóa đề thi thành công"));
        }

        #endregion

        #region Private Methods


        #endregion

    }
}
