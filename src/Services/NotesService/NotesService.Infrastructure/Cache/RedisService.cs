using NotesService.Application.Interfaces;
using StackExchange.Redis;
using System.Text.Json;

namespace NotesService.Infrastructure.Cache
{
    // Handles Redis operations
    public class RedisService : ICacheService
    {
        private readonly IDatabase _db;

        public RedisService(string connectionString)
        {
            var options = ConfigurationOptions.Parse(connectionString);
            options.AbortOnConnectFail = false;
            var redis = ConnectionMultiplexer.Connect(options);
            _db = redis.GetDatabase();
        }

        // Get data from cache
        public async Task<T?> GetAsync<T>(string key)
        {
            var value = await _db.StringGetAsync(key);

            if (value.IsNullOrEmpty)
                return default;

            return JsonSerializer.Deserialize<T>(value);
        }

        // Set cache
        public async Task SetAsync<T>(string key, T value)
        {
            var json = JsonSerializer.Serialize(value);

            await _db.StringSetAsync(key, json, TimeSpan.FromMinutes(10));
        }

        // Remove cache
        public async Task RemoveAsync(string key)
        {
            await _db.KeyDeleteAsync(key);
        }
    }
}
