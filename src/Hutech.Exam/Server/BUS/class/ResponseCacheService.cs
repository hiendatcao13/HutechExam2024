using Hutech.Exam.Server.BUS;
using MessagePack;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using StackExchange.Redis;

namespace Hutech.Exam.Server.BUS.@class
{
    public class ResponseCacheService : IResponseCacheService
    {
        #region Private Fields
        private readonly IDistributedCache _distributedCache;
        private readonly IConnectionMultiplexer _connectionMultiplexer; // kết nối với nhiều redis
        #endregion

        #region Public Methods
        public ResponseCacheService(IDistributedCache distributedCache, IConnectionMultiplexer connectionMultiplexer)
        {
            _distributedCache = distributedCache;
            _connectionMultiplexer = connectionMultiplexer;
        }


        // Serialize đối tượng thành MessagePack rồi lưu vào Redis

        public async Task<T?> GetCacheResponseAsync<T>(string cacheKey)
        {
            if (string.IsNullOrWhiteSpace(cacheKey))
                return default;

            var cacheResponse = await _distributedCache.GetAsync(cacheKey);

            if (cacheResponse == null || cacheResponse.Length == 0)
            {
                //Console.WriteLine($"[Redis] No data found for key: {cacheKey}");
                return default;
            }

            try
            {
                var result = MessagePackSerializer.Deserialize<T>(cacheResponse);
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Redis] Deserialization failed for key '{cacheKey}': {ex.Message}");
                return default;
            }
        }

        public async Task RemoveCacheResponseAsync(string pattern)
        {
            if (string.IsNullOrWhiteSpace(pattern))
                return;
            await foreach (var key in GetkeyAsync(pattern + "*")) // delete full
            {
                await _distributedCache.RemoveAsync(key);
            }
        }

        private async IAsyncEnumerable<string> GetkeyAsync(string pattern)
        {
            if (string.IsNullOrWhiteSpace(pattern))
                throw new ArgumentException("Value can not be null or whitespace");
            foreach(var endPoint in _connectionMultiplexer.GetEndPoints())
            {
                var server = _connectionMultiplexer.GetServer(endPoint);
                if (!server.IsConnected) continue;
                foreach (var key in server.Keys(pattern: pattern))
                {
                    // yield - continue foreach loop
                    yield return key.ToString();
                    await Task.Yield();
                }
            }
        }

        public async Task SetCacheResponseAsync(string cacheKey, object response, TimeSpan timeOut)
        {
            if (response == null)
                return;

            byte[] serializedResponse = MessagePackSerializer.Serialize(response);  // Serialize thành MessagePack
            await _distributedCache.SetAsync(cacheKey, serializedResponse, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = timeOut
            });
        }


        public async Task SetHashAsync(string hashKey, string fieldKey, object value, TimeSpan? timeOut = null)
        {
            if (string.IsNullOrEmpty(hashKey) || string.IsNullOrEmpty(fieldKey) || value == null)
                return;

            var db = _connectionMultiplexer.GetDatabase();

            // Serialize object to MessagePack
            var serializedValue = MessagePackSerializer.Serialize(value);

            // Thêm vào Redis Hash
            await db.HashSetAsync(hashKey, fieldKey, serializedValue);

            // Thiết lập thời gian hết hạn nếu có
            if (timeOut.HasValue)
            {
                await db.KeyExpireAsync(hashKey, DateTime.UtcNow.Add(timeOut.Value)); // Đặt thời gian hết hạn cho Hash
            }
        }

        // Lấy (object) từ Redis Hash
        public async Task<T?> GetHashAsync<T>(string hashKey, string fieldKey)
        {
            if (string.IsNullOrEmpty(hashKey) || string.IsNullOrEmpty(fieldKey))
                return default;

            var db = _connectionMultiplexer.GetDatabase();
            var serializedValue = await db.HashGetAsync(hashKey, fieldKey);

            if (serializedValue.IsNullOrEmpty)
                return default;

            try
            {
                var value = MessagePackSerializer.Deserialize<T>(serializedValue);
                return value;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Redis] Deserialize error for field '{fieldKey}' in hash '{hashKey}': {ex.Message}");
                return default;
            }
        }

        // Lấy tất cả các field và giá trị trong Redis Hash
        public async Task<Dictionary<Tkey, Tdata>?> GetAllFieldsFromHashAsync<Tkey, Tdata>(string hashKey) where Tkey : notnull  // Đảm bảo Tkey không phải là kiểu nullable
        {
            if (string.IsNullOrEmpty(hashKey))
                return null;

            var db = _connectionMultiplexer.GetDatabase();
            var hashEntries = await db.HashGetAllAsync(hashKey);


            if (hashEntries == null || hashEntries.Length == 0)
                return [];

            var result = new Dictionary<Tkey, Tdata>();

            foreach (var entry in hashEntries)
            {
                try
                {
                    // Deserialize giá trị từ MessagePack
                    var value = MessagePackSerializer.Deserialize<Tdata>(entry.Value);

                    // Convert Name (key) của Redis entry thành kiểu Tkey
                    Tkey key = (Tkey)Convert.ChangeType(entry.Name.ToString(), typeof(Tkey));

                    // Thêm key-value vào Dictionary
                    result[key] = value;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Deserialize failed for {entry.Name}: {ex.Message}");
                    // Có thể bỏ qua hoặc xử lý fallback
                }
            }

            return result;
        }

        // Xóa một field khỏi Redis Hash
        public async Task RemoveFieldFromHashAsync(string hashKey, string fieldKey)
        {
            if (string.IsNullOrEmpty(hashKey) || string.IsNullOrEmpty(fieldKey))
                return;

            var db = _connectionMultiplexer.GetDatabase();

            // Xóa field khỏi Redis Hash
            await db.HashDeleteAsync(hashKey, fieldKey);
        }




        // Thêm đáp án vào cuối List trong Redis
        public async Task SetListAsync(string listKey, object answer, TimeSpan? timeOut = null)
        {
            if (string.IsNullOrEmpty(listKey) || answer == null)
                return;

            var db = _connectionMultiplexer.GetDatabase();

            // Serialize object to MessagePack
            var serializedAnswer = MessagePackSerializer.Serialize(answer);

            // Thêm vào cuối danh sách
            await db.ListRightPushAsync(listKey, serializedAnswer);

            // Thiết lập thời gian hết hạn nếu có
            if (timeOut.HasValue)
            {
                await db.KeyExpireAsync(listKey, DateTime.UtcNow.Add(timeOut.Value)); // Đặt thời gian hết hạn cho key
            }
        }

        // Lấy tất cả đáp án (object) từ Redis List và deserialize
        public async Task<List<T>> GetAllFromListAsync<T>(string listKey)
        {
            if (string.IsNullOrWhiteSpace(listKey))
                return new List<T>();

            var db = _connectionMultiplexer.GetDatabase();
            var listLength = await db.ListLengthAsync(listKey);

            var results = new List<T>();

            for (long i = 0; i < listLength; i++)
            {
                var serializedItem = await db.ListGetByIndexAsync(listKey, i);

                if (serializedItem.IsNullOrEmpty)
                    continue;

                try
                {
                    var item = MessagePackSerializer.Deserialize<T>(serializedItem);
                    results.Add(item);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[Redis] Deserialization failed at index {i} in list '{listKey}': {ex.Message}");
                    // Bạn có thể log lỗi hoặc bỏ qua tuỳ nhu cầu
                }
            }

            return results;
        }

        // Xóa đáp án (object) từ Redis List
        public async Task RemoveFromListAsync(string listKey, object answer)
        {
            if (string.IsNullOrEmpty(listKey) || answer == null)
                return;

            var db = _connectionMultiplexer.GetDatabase();

            // Serialize object to JSON string
            var serializedAnswer = MessagePackSerializer.Serialize(answer);

            // Xóa đáp án khỏi list
            await db.ListRemoveAsync(listKey, serializedAnswer);
        }


        //////////////JSON - để test, nên để MessagePack cho gọn nhẹ
        // Serialize đối tượng thành MessagePack rồi lưu vào Redis

        //public void SetToRedis(string key, object value)
        //{
        //    byte[] serializedData = MessagePackSerializer.Serialize(value);
        //    var db = _connectionMultiplexer.GetDatabase();
        //    db.StringSet(key, serializedData);
        //}

        //// Lấy dữ liệu từ Redis và deserialize thành đối tượng
        //public T? GetFromRedis<T>(string key)
        //{
        //    var db = _connectionMultiplexer.GetDatabase();
        //    var serializedData = db.StringGet(key);

        //    if (serializedData.IsNullOrEmpty)
        //        return default;

        //    return MessagePackSerializer.Deserialize<T>(serializedData);
        //}
        //public async Task<string> GetCacheResponseAsync(string cacheKey)
        //{
        //    var cacheResponse = await _distributedCache.GetStringAsync(cacheKey);
        //    return string.IsNullOrEmpty(cacheResponse) ? "" : cacheResponse;
        //}

        //public async Task RemoveCacheResponseAsync(string pattern)
        //{
        //    if (string.IsNullOrWhiteSpace(pattern))
        //        throw new ArgumentException("Value can not be null or whitespace");
        //    await foreach (var key in GetkeyAsync(pattern + "*")) // delete full
        //    {
        //        await _distributedCache.RemoveAsync(key);
        //    }
        //}

        //private async IAsyncEnumerable<string> GetkeyAsync(string pattern)
        //{
        //    if (string.IsNullOrWhiteSpace(pattern))
        //        throw new ArgumentException("Value can not be null or whitespace");
        //    foreach (var endPoint in _connectionMultiplexer.GetEndPoints())
        //    {
        //        var server = _connectionMultiplexer.GetServer(endPoint);
        //        if (!server.IsConnected) continue;
        //        foreach (var key in server.Keys(pattern: pattern))
        //        {
        //            // yield - continue foreach loop
        //            yield return key.ToString();
        //            await Task.Yield();
        //        }
        //    }
        //}

        //public async Task SetCacheResponseAsync(string cacheKey, object response, TimeSpan timeOut)
        //{
        //    if (response == null)
        //        return;
        //    var serializerResponse = JsonConvert.SerializeObject(response, new JsonSerializerSettings()
        //    {
        //        // đưa các dạng response dạng Camel vì mặc định là tất cả chữ thường
        //        ContractResolver = new CamelCasePropertyNamesContractResolver()
        //    });
        //    await _distributedCache.SetStringAsync(cacheKey, serializerResponse, new DistributedCacheEntryOptions
        //    {
        //        AbsoluteExpirationRelativeToNow = timeOut
        //    });
        //}


        //public async Task SetHashAsync(string hashKey, string fieldKey, object value, TimeSpan? timeOut = null)
        //{
        //    if (string.IsNullOrEmpty(hashKey) || string.IsNullOrEmpty(fieldKey) || value == null)
        //        return;

        //    var db = _connectionMultiplexer.GetDatabase();

        //    // Serialize object to JSON string
        //    var serializedValue = JsonConvert.SerializeObject(value);

        //    // Thêm vào Redis Hash
        //    await db.HashSetAsync(hashKey, fieldKey, serializedValue);

        //    // Thiết lập thời gian hết hạn nếu có
        //    if (timeOut.HasValue)
        //    {
        //        await db.KeyExpireAsync(hashKey, DateTime.UtcNow.Add(timeOut.Value)); // Đặt thời gian hết hạn cho Hash
        //    }
        //}

        //// Lấy (object) từ Redis Hash
        //public async Task<object?> GetHashAsync(string hashKey, string fieldKey)
        //{
        //    if (string.IsNullOrEmpty(hashKey) || string.IsNullOrEmpty(fieldKey))
        //        return null;

        //    var db = _connectionMultiplexer.GetDatabase();

        //    // Lấy giá trị từ Redis Hash
        //    var serializedValue = await db.HashGetAsync(hashKey, fieldKey);

        //    if (serializedValue.IsNullOrEmpty)
        //        return null;

        //    // Deserialize the JSON string back to object
        //    var value = JsonConvert.DeserializeObject<object>(serializedValue.ToString());

        //    return value;
        //}

        //// Lấy tất cả các field và giá trị trong Redis Hash
        //public async Task<Dictionary<string, object>?> GetAllFieldsFromHashAsync(string hashKey)
        //{
        //    if (string.IsNullOrEmpty(hashKey))
        //        return null;

        //    var db = _connectionMultiplexer.GetDatabase();

        //    // Lấy tất cả các field và giá trị từ Redis Hash
        //    var hashEntries = await db.HashGetAllAsync(hashKey);

        //    var result = new Dictionary<string, object>();

        //    foreach (var entry in hashEntries)
        //    {
        //        // Deserialize giá trị từ JSON về object
        //        var value = JsonConvert.DeserializeObject<object>(entry.Value.ToString());
        //        if (value != null)
        //            result[entry.Name.ToString()] = value;
        //    }

        //    return result;
        //}

        //// Xóa một field khỏi Redis Hash
        //public async Task RemoveFieldFromHashAsync(string hashKey, string fieldKey)
        //{
        //    if (string.IsNullOrEmpty(hashKey) || string.IsNullOrEmpty(fieldKey))
        //        return;

        //    var db = _connectionMultiplexer.GetDatabase();

        //    // Xóa field khỏi Redis Hash
        //    await db.HashDeleteAsync(hashKey, fieldKey);
        //}




        //// Thêm đáp án vào cuối List trong Redis
        //public async Task SetListAsync(string listKey, object answer, TimeSpan? timeOut = null)
        //{
        //    if (string.IsNullOrEmpty(listKey) || answer == null)
        //        return;

        //    var db = _connectionMultiplexer.GetDatabase();

        //    // Serialize object to JSON string
        //    var serializedAnswer = JsonConvert.SerializeObject(answer);

        //    // Thêm vào cuối danh sách
        //    await db.ListRightPushAsync(listKey, serializedAnswer);

        //    // Thiết lập thời gian hết hạn nếu có
        //    if (timeOut.HasValue)
        //    {
        //        await db.KeyExpireAsync(listKey, DateTime.UtcNow.Add(timeOut.Value)); // Đặt thời gian hết hạn cho key
        //    }
        //}

        //// Lấy tất cả đáp án (object) từ Redis List và deserialize
        //public async Task<List<object>?> GetAllFromListAsync(string listKey)
        //{
        //    if (string.IsNullOrEmpty(listKey))
        //        return null;

        //    var db = _connectionMultiplexer.GetDatabase();
        //    var listLength = await db.ListLengthAsync(listKey);

        //    var answers = new List<object>();
        //    for (long i = 0; i < listLength; i++)
        //    {
        //        var serializedAnswer = await db.ListGetByIndexAsync(listKey, i);

        //        // Deserialize the JSON string back to object
        //        var answer = JsonConvert.DeserializeObject<object>(serializedAnswer.ToString());
        //        if (answer != null)
        //            answers.Add(answer);
        //    }

        //    return answers;
        //}

        //// Xóa đáp án (object) từ Redis List
        //public async Task RemoveFromListAsync(string listKey, object answer)
        //{
        //    if (string.IsNullOrEmpty(listKey) || answer == null)
        //        return;

        //    var db = _connectionMultiplexer.GetDatabase();

        //    // Serialize object to JSON string
        //    var serializedAnswer = JsonConvert.SerializeObject(answer);

        //    // Xóa đáp án khỏi list
        //    await db.ListRemoveAsync(listKey, serializedAnswer);
        //}
        #endregion

    }
}
