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

        [HttpGet("mock-api")]
        public async Task<IActionResult> GetAllDeThi()
        {
            //var httpClient = new HttpClient();
            //httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (compatible; MyApp/1.0)");

            //var response = await httpClient.GetAsync(_approvedExamsUrl);
            //if (!response.IsSuccessStatusCode)
            //{
            //    return StatusCode((int)response.StatusCode, "Không tìm thấy API từ bên ngoài");
            //}

            //var jsonData = await response.Content.ReadAsStringAsync();
            //Console.WriteLine("Hellllllllllllo" +  jsonData);

            //// Nếu bạn biết kiểu dữ liệu trả về, bạn có thể deserialize vào model cụ thể
            //// Ví dụ:
            //var exams = JsonSerializer.Deserialize<List<DeThiDto>>(jsonData) ?? [];

            var exams = JsonSerializer.Deserialize<List<DeThiMock>>(jsonDeThi) ?? [];
            var result = _mapper.Map<List<DeThiDto>>(exams);

            // Tạm thời trả nguyên json về client
            //return Content(jsonData, "application/json");

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

        [HttpGet("{id:long}/mock")]
        public async Task<IActionResult> SelectOneExam([FromRoute] long id)
        {
            var deThi = await _deThiService.SelectOne(id);
            var result = await _redisService.GetDeThiAsync(deThi.Guid);

            return Ok(APIResponse<List<CustomDeThi>>.SuccessResponse(data: TestExam.GetTestExam(), message: "Lấy nội dung đề thi thành công"));
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

        //[HttpGet("{id:int}/thong-tin-ma-de-thi")]
        //public async Task<IActionResult> GetThongTinMaDeThi([FromRoute] int id)
        //{
        //    try
        //    {
        //        var result = await _customMaDeThiService.GetThongTinMaDeThi(id);
        //        if (result.Count == 0)
        //        {
        //            return NotFound(APIResponse<List<CustomThongTinMaDeThi>>.NotFoundResponse(message: "Không tìm thấy thông tin mã đề thi"));
        //        }
        //        return Ok(APIResponse<List<CustomThongTinMaDeThi>>.SuccessResponse(data: result, message: "Lấy thông tin mã đề thi thành công"));
        //    }
        //    catch (SqlException sqlEx)
        //    {
        //        return SQLExceptionHelper<List<CustomThongTinMaDeThi>>.HandleSqlException(sqlEx);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(APIResponse<List<CustomThongTinMaDeThi>>.ErrorResponse(message: "Lấy thông tin mã đề thi không thành công", errorDetails: ex.Message));
        //    }
        //}

        //[HttpGet("{id:int}/report-cauhoi")]
        //public async Task<IActionResult> ThongKeCauHoi_SelectBy_DeThi([FromRoute] int id)
        //{
        //    var result = await _customThongKeService.ThongKeCauHoi_SelectBy_DeThi(id);
        //    return Ok(APIResponse<List<CustomThongKeCauHoi>>.SuccessResponse(data: result, message: "Lấy dữ liệu thống kê thành công"));
        //}

        [HttpGet("{id:long}/report")]
        public async Task<IActionResult> ThongKeDiem_SelectBy_DeThi([FromRoute] long id)
        {
            var result = await _deThiService.Report(id);
            return Ok(APIResponse<List<CustomThongKeDiem>>.SuccessResponse(data: result, message: "Lấy dữ liệu thống kê thành công"));
        }

        //[HttpGet("{id:int}/report-capbacsv")]
        //public async Task<IActionResult> ThongKeCapBacSV_SelectBy_DeThi([FromRoute] int id)
        //{
        //    var result = await _customThongKeService.ThongKeCapBacSV_SelectBy_DeThi(id);
        //    return Ok(APIResponse<CustomThongKeCapBacSV>.SuccessResponse(data: result, message: "Lấy dữ liệu thống kê thành công"));
        //}

        #endregion

        #region Post Methods

        [HttpPost]
        [Authorize(Roles = "KhaoThi")]
        public async Task<IActionResult> Insert([FromBody] DeThiCreateRequest deThi)
        {
            var id = await _deThiService.Insert(deThi);
            return Ok(APIResponse<DeThiDto>.SuccessResponse(data: await _deThiService.SelectOne(id), message: "Thêm đề thi thành công"));
        }

        [HttpPost("batch")]
        [Authorize(Roles = "KhaoThi")]
        public async Task<IActionResult> Save_Batch([FromBody] List<DeThiDto> deThis)
        {
            await _deThiService.Save_Batch(deThis);
            return Ok(APIResponse<DeThiDto>.SuccessResponse(message: "Thêm danh sách đề thi thành công"));
        }


        #endregion

        #region Put Methods

        [HttpPut("{id:long}")]
        [Authorize(Roles = "KhaoThi")]
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
        [Authorize(Roles = "KhaoThi")]
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
        [Authorize(Roles = "KhaoThi")]
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
