using System.Text.Json;
using System.Net.Http.Json;
using Hutech.Exam.Shared.DTO.API.Response;
using MudBlazor;
using Microsoft.AspNetCore.Components;
using System.Text;

namespace Hutech.Exam.Client.API
{
    public class SenderAPI : ISenderAPI
    {
        private readonly HttpClient _http;

        private readonly ISnackbar _snackbar;
        public SenderAPI(HttpClient http, ISnackbar snackbar)
        {
            _http = http;
            _http.Timeout = TimeSpan.FromSeconds(30); // đặt thời gian chờ cho mỗi yêu cầu
            _snackbar = snackbar;
        }

        private async Task<APIResponse<TResult?>> HandleResponseAsync<TResult>(HttpResponseMessage response, string requestUri)
        {
            var content = await response.Content.ReadAsStringAsync();

            try
            {
                var apiResponse = JsonSerializer.Deserialize<APIResponse<TResult?>>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                // hiện ra thông điệp message và trả về APIResponse
                if (apiResponse != null)
                {
                    if(!string.IsNullOrEmpty(apiResponse.Message))
                    {   
                        _snackbar.Add(apiResponse.Message, (apiResponse.Success) ? Severity.Success : Severity.Error);
                    }    
                    return apiResponse;
                }    

            }
            catch (JsonException ex) // có lỗi khi giải nén file JSON
            {
                return APIResponse<TResult?>.ErrorResponse(
                    message: $"Có lỗi xảy ra khi cố giải nén tệp JSON",
                    errorDetails: ex.Message
                );
            }
            catch (TaskCanceledException ex) // timeout
            {
                return APIResponse<TResult?>.ErrorResponse(
                    message: $"Máy chủ hiện không phản hồi. Vui lòng đợi và thực hiện lại lại trong giây lát",
                    errorDetails: ex.Message
                );
            }

            return APIResponse<TResult?>.InternalServerErrorResponse(
                message: $"Máy chủ hiện không phản hồi",
                errorDetails: content
            );
        }

        public async Task<APIResponse<TResult?>> DeleteAsync<TResult>(string requestUri)
        {
            var response = await _http.DeleteAsync(requestUri);
            return await HandleResponseAsync<TResult>(response, requestUri);
        }

        public async Task<APIResponse<TResult?>> GetAsync<TResult>(string requestUri)
        {
            var response = await _http.GetAsync(requestUri);
            return await HandleResponseAsync<TResult>(response, requestUri);
        }

        public async Task<APIResponse<TResult?>> PostAsync<TResult>(string requestUri, object? data)
        {
            var response = await _http.PostAsJsonAsync(requestUri, data);
            return await HandleResponseAsync<TResult>(response, requestUri);
        }
        public async Task<APIResponse<TResult?>> PatchAsync<TResult>(string requestUri, object? data)
        {
            var response = await _http.PatchAsJsonAsync(requestUri, data);
            return await HandleResponseAsync<TResult>(response, requestUri);
        }

        public async Task<APIResponse<TResult?>> PutAsync<TResult>(string requestUri, object? data)
        {
            var response = await _http.PutAsJsonAsync(requestUri, data);
            return await HandleResponseAsync<TResult>(response, requestUri);
        }
    }
}
