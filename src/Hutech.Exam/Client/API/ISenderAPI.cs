using Hutech.Exam.Shared.DTO.API.Response;

namespace Hutech.Exam.Client.API
{
    public interface ISenderAPI
    {
        Task<ResponseAPI<TResult?>> GetAsync<TResult>(string requestUri); //GET

        Task<ResponseAPI<TResult?>> PostAsync<TResult>(string requestUri, object data); //POST

        Task<ResponseAPI<TResult?>> PutAsync<TResult>(string requestUri, object data); //PUT

        Task<ResponseAPI<TResult?>> DeleteAsync<TResult>(string requestUri); //DELETE
    }
}
