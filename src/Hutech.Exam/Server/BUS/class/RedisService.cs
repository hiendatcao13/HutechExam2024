using System.Text.Json;
using AutoMapper;
using Hutech.Exam.Server.DAL.Repositories;
using Hutech.Exam.Server.Hubs;
using Hutech.Exam.Shared.DTO.API.Response;
using Hutech.Exam.Shared.DTO.Custom;
using Hutech.Exam.Shared.DTO.Request.ChiTietBaiThi;
using MessagePack;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Hutech.Exam.Server.BUS
{
    public class RedisService
    {
        #region Private Fields
        private readonly DeThiService _deThiService;
        private string _examUrl;
        private readonly Guid _sampleExamGuid;

        private readonly IResponseCacheService _cacheService;
        private readonly ILogger _logger;

        string jsonNoiDungDeThi = "{\"success\":true,\"message\":\"Lấy thông tin đề thi thành công\",\"data\":{\"MaDeThi\":\"6A429A3A-97AB-4043-8F8A-476BEDB7476B\",\"TenDeThi\":\"TEST_CAU_HOI_NHOM\",\"NgayTao\":\"08/07/2025 02:42:01\",\"Phans\":[{\"MaPhan\":\"C2CA7266-5800-41BF-80BC-3003E5C48546\",\"MaPhanCha\":null,\"TenPhan\":\"Database Fundamentals\",\"KieuNoiDung\":-1,\"NoiDung\":\"Kiến thức nền tảng về cơ sở dữ liệu\",\"SoLuongCauHoi\":10,\"LaCauHoiNhom\":false,\"CauHois\":[{\"MaCauHoi\":\"D6E4AFA0-DF8D-48A7-A486-08F40E495CAF\",\"NoiDung\":\"Which is NOT a type of database relationship?\",\"CauTraLois\":[{\"MaCauTraLoi\":\"92F099BC-FE06-4874-A4E0-C588A36ECB2C\",\"NoiDung\":\"One-to-One\",\"LaDapAn\":false},{\"MaCauTraLoi\":\"6657607A-69B6-4D39-8442-BF067B59AD64\",\"NoiDung\":\"One-to-Many\",\"LaDapAn\":false},{\"MaCauTraLoi\":\"2559C472-5A12-4146-89FD-F87BF2877065\",\"NoiDung\":\"Many-to-Many\",\"LaDapAn\":false},{\"MaCauTraLoi\":\"9F59585F-BA56-4307-8091-07BF21654763\",\"NoiDung\":\"All-to-All\",\"LaDapAn\":true}],\"IsParentQuestion\":false},{\"MaCauHoi\":\"E47FB077-B555-4462-AF6D-CD25E3A6F0B2\",\"NoiDung\":\"What is a foreign key? <img src=\\\"https://datauploads.sgp1.digitaloceanspaces.com/images/er_diagram.webp\\\" style=\\\"max-width: 400px; height: auto; border-radius: 8px; box-shadow: 0 2px 8px rgba(0,0,0,0.1);\\\">\",\"CauTraLois\":[{\"MaCauTraLoi\":\"B9DE6A36-1440-4BD4-AC9B-DDE57ABC936C\",\"NoiDung\":\"Unique identifier\",\"LaDapAn\":false},{\"MaCauTraLoi\":\"A056623B-669A-4D40-90EE-7CBB2FA8C733\",\"NoiDung\":\"Reference to primary key\",\"LaDapAn\":true},{\"MaCauTraLoi\":\"37F315D7-92C2-4067-AEA9-89B8B35D4843\",\"NoiDung\":\"Index for searching\",\"LaDapAn\":false},{\"MaCauTraLoi\":\"F9067690-6216-4F96-852D-F21473DB14A7\",\"NoiDung\":\"Data validation\",\"LaDapAn\":false}],\"IsParentQuestion\":false},{\"MaCauHoi\":\"AD5A7EA5-DDC9-49AA-A5C5-9028711C7260\",\"NoiDung\":\"What does ACID stand for in databases?\",\"CauTraLois\":[{\"MaCauTraLoi\":\"D60251EF-E3A3-47A5-AAD8-686369D866FA\",\"NoiDung\":\"Atomicity Consistency Isolation Durability\",\"LaDapAn\":true},{\"MaCauTraLoi\":\"8F655F71-3C36-41A8-A1AE-0760EC0A2C1E\",\"NoiDung\":\"Access Control Identity Database\",\"LaDapAn\":false},{\"MaCauTraLoi\":\"1B7ACBFB-4DF4-4F88-980E-BB86D906B984\",\"NoiDung\":\"Automatic Calculation Index Data\",\"LaDapAn\":false},{\"MaCauTraLoi\":\"B107C4B8-29E8-4CB6-A9B1-372650AAD3FA\",\"NoiDung\":\"Advanced Computer Information Design\",\"LaDapAn\":false}],\"IsParentQuestion\":false},{\"MaCauHoi\":\"4C8FAA09-E208-41AC-9A36-73FB4568188D\",\"NoiDung\":\"What does ACID stand for in databases?\",\"CauTraLois\":[{\"MaCauTraLoi\":\"7EC4A10F-CA85-4F60-94D4-588C63589B5D\",\"NoiDung\":\"Atomicity Consistency Isolation Durability\",\"LaDapAn\":true},{\"MaCauTraLoi\":\"2F198A28-B3C9-45AF-9ACB-F9AC2F08055C\",\"NoiDung\":\"Access Control Identity Database\",\"LaDapAn\":false},{\"MaCauTraLoi\":\"40FB439B-FE9D-4C61-BE97-8CDB6BF0219F\",\"NoiDung\":\"Automatic Calculation Index Data\",\"LaDapAn\":false},{\"MaCauTraLoi\":\"82A73225-A29B-4F32-982B-307BED3AAD9F\",\"NoiDung\":\"Advanced Computer Information Design\",\"LaDapAn\":false}],\"IsParentQuestion\":false},{\"MaCauHoi\":\"1D2CFA89-161C-4761-96CF-16DF38AA5183\",\"NoiDung\":\"Which SQL command is used to retrieve data?\",\"CauTraLois\":[{\"MaCauTraLoi\":\"68465816-1E2C-42B9-8307-C400B44D19E9\",\"NoiDung\":\"INSERT\",\"LaDapAn\":false},{\"MaCauTraLoi\":\"24FE2EAE-2325-4A03-B6E5-43DB33747AA0\",\"NoiDung\":\"SELECT\",\"LaDapAn\":true},{\"MaCauTraLoi\":\"28B4D13E-7086-48BF-95B0-BABB706549CE\",\"NoiDung\":\"UPDATE\",\"LaDapAn\":false},{\"MaCauTraLoi\":\"C1E311F7-F5E6-4D0E-A7D1-5C2D19CD6422\",\"NoiDung\":\"DELETE\",\"LaDapAn\":false}],\"IsParentQuestion\":false}]}]}}\r\n";
        public RedisService(DeThiService deThiService, IResponseCacheService cacheService, ILogger<RedisService> logger, IConfiguration configuration)
        {
            _deThiService = deThiService;
            _cacheService = cacheService;
            _logger = logger;
            _examUrl = configuration["ExternalApiSettings:LoadExamUrl"]!;
            _sampleExamGuid = Guid.Parse(configuration["ExternalApiSettings:SampleGuidExam"]!);
        }

        #endregion

        //#region Public Methods
        public async Task SetConnectionIdAsync(long ma_sinh_vien, string connectionId)
        {
            try
            {
                var cacheKey = $"connection:{ma_sinh_vien}";
                await _cacheService.SetCacheResponseAsync(cacheKey, connectionId, TimeSpan.FromMinutes(150));
            }
            catch (Exception ex)
            {
                _logger.LogError("[Redis] Có lỗi khi set khóa ConnectionId: {message}", ex.Message);
            }
        }

        public async Task<string> GetConnectionIdAsync(long ma_sinh_vien)
        {
            try
            {
                var cacheKey = $"connection:{ma_sinh_vien}";
                var cacheData = await _cacheService.GetCacheResponseAsync<string>(cacheKey);

                return string.IsNullOrEmpty(cacheData) ? string.Empty : cacheData.ToString();
            }
            catch (Exception ex)
            {
                _logger.LogError("[Redis] Có lỗi khi cố gắng lấy ConnectionId: {message}", ex.Message);
                return string.Empty;
            }
        }


        public async Task RemoveCacheAsync(string key)
        {
            await _cacheService.RemoveCacheResponseAsync(key);
        }

        public async Task<T?> GetCacheAsync<T>(string key)
        {
            var cacheKey = $"{key}";

            // Kiểm tra xem có dữ liệu trong cache không
            var cachedData = await _cacheService.GetCacheResponseAsync<T>(cacheKey);

            if (cachedData != null)
            {
                return cachedData;
            }

            return default;
        }

        public async Task SetCacheAsync<T>(string key, T data, int minutes)
        {
            var cacheKey = $"{key}";
            await _cacheService.SetCacheResponseAsync(cacheKey, data!, TimeSpan.FromMinutes(minutes));
        }    


        public async Task<Dictionary<Guid, TData>?> GetHashChiTietBaiThiAsync<TData>(string key)
        {
            try
            {
                // do string không trực tiếp ko convert được Guid
                var stringDict = await _cacheService.GetAllFieldsFromHashAsync<string, TData>(key);

                if (stringDict == null)
                    return null;

                var result = new Dictionary<Guid, TData>();

                foreach (var kvp in stringDict)
                {
                    if (Guid.TryParse(kvp.Key, out var guidKey))
                    {
                        result[guidKey] = kvp.Value;
                    }
                    else
                    {
                        _logger.LogWarning("[Redis] Có lỗi khi cố convert tiết bài thi thành GUID: {key}", kvp.Key);
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError("[Redis] Có lỗi khi lấy dữ liệu chi tiết bài thi: {message}", ex.Message);
                return default;
            }
        }

        public async Task<Dictionary<Guid, ChiTietBaiThiRequest>> GetChiTietBaiThiAsync(int ma_chi_tiet_ca_thi)
        {
            try
            {
                var cacheKey = $"ChiTietBaiThi:{ma_chi_tiet_ca_thi}";
                var cacheData = await _cacheService.GetAllFieldsFromHashAsync<string, ChiTietBaiThiRequest>(cacheKey);

                if (cacheData == null || cacheData.Count == 0)
                {
                    _logger.LogWarning("[Redis] Không tìm thấy bài thi: {Key}", cacheKey);
                    return [];
                }

                Dictionary<Guid, ChiTietBaiThiRequest> data = [];

                foreach (var item in cacheData)
                {
                    if (Guid.TryParse(item.Key, out Guid guidKey))
                    {
                        data[guidKey] = item.Value;
                    }
                    else
                    {
                        _logger.LogWarning("[Redis] Khóa của chi tiết bài thi không phải GUID. Vui lòng kiểm tra : {Key}", item.Key);
                    }
                }

                return data;
            }
            catch (Exception ex)
            {
                _logger.LogError("[Redis] Có lỗi khi cố gắng lấy dữ liệu từ chi tiết bài thi: {message}", ex.Message);
                return [];
            }
        }

        public async Task SetChiTietBaiThi(Guid key, ChiTietBaiThiRequest value, int ma_chi_tiet_ca_thi)
        {
            try
            {
                var cacheKey = $"ChiTietBaiThi:{ma_chi_tiet_ca_thi}";
                await _cacheService.SetHashAsync(cacheKey, key.ToString(), value, TimeSpan.FromMinutes(180));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[Redis] Có lỗi khi lưu dữ liệu chi tiết bài thi.");
            }
        }

        public async Task RemoveChiTietBaiThiAsync(int ma_chi_tiet_tiet_ca_thi)
        {
            try
            {
                var cacheKey = $"ChiTietBaiThi:{ma_chi_tiet_tiet_ca_thi}";
                await _cacheService.RemoveCacheResponseAsync(cacheKey);
            }
            catch (Exception ex)
            {
                _logger.LogError("[Redis] Có lỗi khi xóa dữ liệu chi tiết bài thi: {message}", ex.Message);
            }
        }

        public async Task SetDapAnAsync(Guid maDeThi, Dictionary<Guid, Guid> dapAns)
        {
            try
            {
                var cacheKey = $"DapAn:{maDeThi}";
                // Lưu đáp án vào cache
                await _cacheService.SetCacheResponseAsync(cacheKey, dapAns, TimeSpan.FromMinutes(150));
            }
            catch (Exception ex)
            {
                _logger.LogError("[Redis] Có lỗi khi lưu dữ liệu đáp án. {message}", ex.Message);
            }
        }

        public async Task<Dictionary<Guid, Guid>> GetDapAnAsync(Guid maDeThi)
        {
            try
            {
                var cacheKey = $"DapAn:{maDeThi}";

                var cachedData = await _cacheService.GetCacheResponseAsync<Dictionary<string, string>>(cacheKey);

                if (cachedData != null && cachedData.Count > 0)
                {
                    var result = new Dictionary<Guid, Guid>();

                    foreach (var kvp in cachedData)
                    {
                        if (Guid.TryParse(kvp.Key, out var keyGuid) &&
                            Guid.TryParse(kvp.Value, out var valueGuid))
                        {
                            result[keyGuid] = valueGuid;
                        }
                    }

                    return result;
                }
                else
                {
                    // Lấy dữ liệu từ đề thi lại
                    await GetDeThiAsync(maDeThi);

                    var cachedDataAgain = await _cacheService.GetCacheResponseAsync<Dictionary<string, string>>(cacheKey);
                    if (cachedDataAgain != null && cachedDataAgain.Count > 0)
                    {
                        var result = new Dictionary<Guid, Guid>();

                        foreach (var kvp in cachedDataAgain)
                        {
                            if (Guid.TryParse(kvp.Key, out var keyGuid) &&
                                Guid.TryParse(kvp.Value, out var valueGuid))
                            {
                                result[keyGuid] = valueGuid;
                            }
                        }

                        return result;
                    }
                }

                return [];
            }
            catch (Exception ex)
            {
                _logger.LogError("[Redis] Có lỗi khi lấy cố gắng lấy dữ liệu đáp án: {message}", ex.Message);
                return [];
            }
        }

        public async Task<List<CustomDeThi>> GetDeThiAsync(Guid id)
        {
            try
            {
                var cacheKey = $"DeThi:{id}";

                // Kiểm tra xem có dữ liệu trong cache không
                var cachedData = await _cacheService.GetCacheResponseAsync<List<CustomDeThi>>(cacheKey);

                if (cachedData != null && cachedData.Count != 0)
                {
                    return cachedData;
                }

                // Nếu không có, thực hiện logic lấy dữ liệu từ database

                //Kiểm tra xem có phải đề mẫu không
                if(id == _sampleExamGuid)
                {
                    var testResult = TestExam.GetTestExam();
                    await _cacheService.SetCacheResponseAsync(cacheKey, testResult, TimeSpan.FromMinutes(150));

                    var testAnswers = TestExam.GetAnswerTest();
                    await SetDapAnAsync(id, testAnswers);

                    return testResult;
                }

                //var httpClient = new HttpClient();
                //httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (compatible; MyApp/1.0)");
                //_examUrl = _examUrl.Replace("*", id.ToString());
                //var response = await httpClient.GetAsync(_examUrl);
                //if (!response.IsSuccessStatusCode)
                //{
                //    return StatusCode((int)response.StatusCode, "Không tìm thấy API từ bên ngoài");
                //}

                //var jsonData = await response.Content.ReadAsStringAsync();

                //// Nếu bạn biết kiểu dữ liệu trả về, bạn có thể deserialize vào model cụ thể
                //// Ví dụ:
                //var exams = JsonSerializer.Deserialize<NoiDungDeThiMock>(jsonData) ?? new();


                var exams = JsonSerializer.Deserialize<APIResponse<NoiDungDeThiMock>>(jsonNoiDungDeThi) ?? new();

                //Console.WriteLine("Hellllllllllllllllllllo" + JsonSerializer.Serialize(exams));

                // xử lí luôn cho đáp án
                var dict = exams.Data!.Phans
                    .SelectMany(p => p.CauHois) // lấy tất cả câu hỏi từ các phần
                    .Where(ch => ch.CauTraLois.Any(ct => ct.LaDapAn)) // lọc câu hỏi có ít nhất 1 đáp án đúng
                    .ToDictionary(
                        ch => ch.MaCauHoi, // key: mã câu hỏi
                        ch => ch.CauTraLois.First(ct => ct.LaDapAn).MaCauTraLoi // value: mã câu trả lời đầu tiên có LaDapAn = true
                    );
                await SetDapAnAsync(exams.Data!.MaDeThi, dict);

                var result = _deThiService.ConvertToCustomDeThi(exams.Data!);


                // Lưu vào cache
                await _cacheService.SetCacheResponseAsync(cacheKey, result, TimeSpan.FromMinutes(150));

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError("[Redis] Có lỗi xảy ra khi lấy đề thi: {message}", ex.Message);
                return [];
            }
        }

    }
}
