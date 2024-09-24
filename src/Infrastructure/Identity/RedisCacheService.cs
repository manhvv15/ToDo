using Newtonsoft.Json;
using StackExchange.Redis;
using ToDo.Application.Common.Interfaces;

namespace ToDo.Infrastructure.Identity;
public class RedisCacheService : ICacheService
{
    private readonly IDatabase _database;
    public RedisCacheService(IConnectionMultiplexer connectionMultiplexer)
    {
        _database = connectionMultiplexer.GetDatabase();
    }
    public async Task<T?> GetAsync<T>(string key)
    {
        try
        {
            var json = await _database.StringGetAsync(key);
            var jsonString = json.ToString();
            if (string.IsNullOrEmpty(jsonString))
            {
                return default;
            }
            var result = JsonConvert.DeserializeObject<T>(jsonString);
            return result;
        }
        catch (JsonException ex)
        {
            Console.WriteLine($"Error deserializing JSON: {ex.Message}");
            return default;
        }
    }

    public async Task<T> GetOrSetAsync<T>(string key, Func<Task<T>> factory, TimeSpan? expiration = null)
    {
        var cacheValue = await GetAsync<T>(key);
        if (cacheValue != null)
        {
            return cacheValue;
        }
        var newValue = await factory();
        await SetAsync(key, newValue);
        return newValue;
    }

    public async Task SetAsync<T>(string key, T value, TimeSpan? expiration = null)
    {
        var jsonString = JsonConvert.SerializeObject(value);
        await _database.StringSetAsync(key, jsonString, expiration);
    }
}
