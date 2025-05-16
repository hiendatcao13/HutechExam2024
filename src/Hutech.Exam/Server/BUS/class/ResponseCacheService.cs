using Hutech.Exam.Server.BUS;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using StackExchange.Redis;

namespace Hutech.Exam.Server.BUS.@class
{
    public class ResponseCacheService : IResponseCacheService
    {
        private readonly IDistributedCache _distributedCache;
        private readonly IConnectionMultiplexer _connectionMultiplexer; // kết nối với nhiều redis

        public ResponseCacheService(IDistributedCache distributedCache, IConnectionMultiplexer connectionMultiplexer)
        {
            _distributedCache = distributedCache;
            _connectionMultiplexer = connectionMultiplexer;
        }
        public async Task<string> GetCacheResponseAsync(string cacheKey)
        {
            var cacheResponse = await _distributedCache.GetStringAsync(cacheKey);
            return string.IsNullOrEmpty(cacheResponse) ? "" : cacheResponse;
        }

        public async Task RemoveCacheResponseAsync(string pattern)
        {
            if (string.IsNullOrWhiteSpace(pattern))
                throw new ArgumentException("Value can not be null or whitespace");
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
            var serializerResponse = JsonConvert.SerializeObject(response, new JsonSerializerSettings()
            {
                // đưa các dạng response dạng Camel vì mặc định là tất cả chữ thường
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });
            await _distributedCache.SetStringAsync(cacheKey, serializerResponse, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = timeOut
            });
        }


        public async Task SetHashAsync(string hashKey, string fieldKey, object value, TimeSpan? timeOut = null)
        {
            if (string.IsNullOrEmpty(hashKey) || string.IsNullOrEmpty(fieldKey) || value == null)
                return;

            var db = _connectionMultiplexer.GetDatabase();

            // Serialize object to JSON string
            var serializedValue = JsonConvert.SerializeObject(value);

            // Thêm vào Redis Hash
            await db.HashSetAsync(hashKey, fieldKey, serializedValue);

            // Thiết lập thời gian hết hạn nếu có
            if (timeOut.HasValue)
            {
                await db.KeyExpireAsync(hashKey, DateTime.UtcNow.Add(timeOut.Value)); // Đặt thời gian hết hạn cho Hash
            }
        }

        // Lấy (object) từ Redis Hash
        public async Task<object?> GetHashAsync(string hashKey, string fieldKey)
        {
            if (string.IsNullOrEmpty(hashKey) || string.IsNullOrEmpty(fieldKey))
                return null;

            var db = _connectionMultiplexer.GetDatabase();

            // Lấy giá trị từ Redis Hash
            var serializedValue = await db.HashGetAsync(hashKey, fieldKey);

            if (serializedValue.IsNullOrEmpty)
                return null;

            // Deserialize the JSON string back to object
            var value = JsonConvert.DeserializeObject<object>(serializedValue.ToString());

            return value;
        }

        // Lấy tất cả các field và giá trị trong Redis Hash
        public async Task<Dictionary<string, object>?> GetAllFieldsFromHashAsync(string hashKey)
        {
            if (string.IsNullOrEmpty(hashKey))
                return null;

            var db = _connectionMultiplexer.GetDatabase();

            // Lấy tất cả các field và giá trị từ Redis Hash
            var hashEntries = await db.HashGetAllAsync(hashKey);

            var result = new Dictionary<string, object>();

            foreach (var entry in hashEntries)
            {
                // Deserialize giá trị từ JSON về object
                var value = JsonConvert.DeserializeObject<object>(entry.Value.ToString());
                if(value != null)
                    result[entry.Name.ToString()] = value;
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

            // Serialize object to JSON string
            var serializedAnswer = JsonConvert.SerializeObject(answer);

            // Thêm vào cuối danh sách
            await db.ListRightPushAsync(listKey, serializedAnswer);

            // Thiết lập thời gian hết hạn nếu có
            if (timeOut.HasValue)
            {
                await db.KeyExpireAsync(listKey, DateTime.UtcNow.Add(timeOut.Value)); // Đặt thời gian hết hạn cho key
            }
        }

        // Lấy tất cả đáp án (object) từ Redis List và deserialize
        public async Task<List<object>?> GetAllFromListAsync(string listKey)
        {
            if (string.IsNullOrEmpty(listKey))
                return null;

            var db = _connectionMultiplexer.GetDatabase();
            var listLength = await db.ListLengthAsync(listKey);

            var answers = new List<object>();
            for (long i = 0; i < listLength; i++)
            {
                var serializedAnswer = await db.ListGetByIndexAsync(listKey, i);

                // Deserialize the JSON string back to object
                var answer = JsonConvert.DeserializeObject<object>(serializedAnswer.ToString());
                if(answer != null)
                    answers.Add(answer);
            }

            return answers;
        }

        // Xóa đáp án (object) từ Redis List
        public async Task RemoveFromListAsync(string listKey, object answer)
        {
            if (string.IsNullOrEmpty(listKey) || answer == null)
                return;

            var db = _connectionMultiplexer.GetDatabase();

            // Serialize object to JSON string
            var serializedAnswer = JsonConvert.SerializeObject(answer);

            // Xóa đáp án khỏi list
            await db.ListRemoveAsync(listKey, serializedAnswer);
        }



    }
}
