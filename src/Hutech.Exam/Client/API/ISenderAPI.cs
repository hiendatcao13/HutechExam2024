using Hutech.Exam.Shared.DTO.API.Response;

namespace Hutech.Exam.Client.API
{
    public interface ISenderAPI
    {
        Task<APIResponse<TResult?>> GetAsync<TResult>(string requestUri); //GET

        Task<APIResponse<TResult?>> PostAsync<TResult>(string requestUri, object data); //POST

        Task<APIResponse<TResult?>> PutAsync<TResult>(string requestUri, object data); //PUT

        Task<APIResponse<TResult?>> DeleteAsync<TResult>(string requestUri); //DELETE
    }
}
