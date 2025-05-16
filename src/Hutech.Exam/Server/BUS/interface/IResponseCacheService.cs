namespace Hutech.Exam.Server.BUS
{
    public interface IResponseCacheService
    {
        Task SetCacheResponseAsync(string cacheKey, object response, TimeSpan timeOut);
        // vì khi lấy ra là dạng document, dùng serialize, deserialize
        Task<string> GetCacheResponseAsync(string cacheKey);

        Task RemoveCacheResponseAsync(string pattern);

        Task SetHashAsync(string hashKey, string fieldKey, object value, TimeSpan? timeOut = null);

        Task<object?> GetHashAsync(string hashKey, string fieldKey);

        Task<Dictionary<string, object>?> GetAllFieldsFromHashAsync(string hashKey);

        Task RemoveFieldFromHashAsync(string hashKey, string fieldKey);

        Task SetListAsync(string listKey, object answer, TimeSpan? timeOut = null);

        Task<List<object>?> GetAllFromListAsync(string listKey);

        Task RemoveFromListAsync(string listKey, object answer);
    }
}
