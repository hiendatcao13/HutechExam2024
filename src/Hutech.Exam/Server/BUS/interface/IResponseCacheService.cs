namespace Hutech.Exam.Server.BUS
{
    public interface IResponseCacheService
    {
        Task SetCacheResponseAsync(string cacheKey, object response, TimeSpan timeOut);
        // vì khi lấy ra là dạng document, dùng serialize, deserialize
        Task<T?> GetCacheResponseAsync<T>(string cacheKey);

        Task RemoveCacheResponseAsync(string pattern);

        Task SetHashAsync(string hashKey, string fieldKey, object value, TimeSpan? timeOut = null);

        Task<T?> GetHashAsync<T>(string hashKey, string fieldKey);

        Task<Dictionary<Tkey, Tdata>?> GetAllFieldsFromHashAsync<Tkey, Tdata>(string hashKey) where Tkey : notnull;

        Task RemoveFieldFromHashAsync(string hashKey, string fieldKey);

        Task SetListAsync(string listKey, object answer, TimeSpan? timeOut = null);

        Task<List<T>> GetAllFromListAsync<T>(string listKey);

        Task RemoveFromListAsync(string listKey, object answer);
    }
}
