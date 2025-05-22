using System.Text.Json;
using System.Net.Http.Json;
using Hutech.Exam.Shared.DTO.API.Response;

namespace Hutech.Exam.Client.API
{
    public class SenderAPI : ISenderAPI
    {
        private readonly HttpClient _http;
        public SenderAPI(HttpClient http)
        {
            _http = http;
            _http.Timeout = TimeSpan.FromSeconds(30); // đặt thời gian chờ cho mỗi yêu cầu
        }

        private async Task<ResponseAPI<TResult?>> HandleResponseAsync<TResult>(HttpResponseMessage response, string requestUri)
        {
            var content = await response.Content.ReadAsStringAsync();

            try
            {
                var apiResponse = JsonSerializer.Deserialize<ResponseAPI<TResult?>>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (apiResponse != null)
                    return apiResponse;
            }
            catch (JsonException ex) // có lỗi khi giải nén file JSON
            {
                return ResponseAPI<TResult?>.ErrorResponse(
                    message: $"Có lỗi xảy ra khi cố giải nén tệp JSON",
                    errorDetails: ex.Message
                );
            }
            catch (TaskCanceledException ex) // timeout
            {
                return ResponseAPI<TResult?>.ErrorResponse(
                    message: $"Máy chủ hiện không phản hồi. Vui lòng đợi và thực hiện lại lại trong giây lát",
                    errorDetails: ex.Message
                );
            }

            return ResponseAPI<TResult?>.InternalServerErrorResponse(
                message: $"Máy chủ hiện không phản hồi",
                errorDetails: content
            );
        }

        public async Task<ResponseAPI<TResult?>> DeleteAsync<TResult>(string requestUri)
        {
            var response = await _http.DeleteAsync(requestUri);
            return await HandleResponseAsync<TResult>(response, requestUri);
        }

        public async Task<ResponseAPI<TResult?>> GetAsync<TResult>(string requestUri)
        {
            var response = await _http.GetAsync(requestUri);
            return await HandleResponseAsync<TResult>(response, requestUri);
        }

        public async Task<ResponseAPI<TResult?>> PostAsync<TResult>(string requestUri, object data)
        {
            var response = await _http.PostAsJsonAsync(requestUri, data);
            return await HandleResponseAsync<TResult>(response, requestUri);
        }

        public async Task<ResponseAPI<TResult?>> PutAsync<TResult>(string requestUri, object data)
        {
            var response = await _http.PutAsJsonAsync(requestUri, data);
            return await HandleResponseAsync<TResult>(response, requestUri);
        }
    }
}
