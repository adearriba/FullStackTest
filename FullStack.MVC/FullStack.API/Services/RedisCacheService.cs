using FullStack.API.Services.Interfaces;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace FullStack.API.Services
{
    public class RedisCacheService : ICacheService
    {
        private readonly IRedisDbConnectionService _redisConnection;
        private readonly ILogger<RedisCacheService> _logger;

        public RedisCacheService(
            IRedisDbConnectionService redisConnection, 
            ILogger<RedisCacheService> logger)
        {
            _redisConnection = redisConnection;
            _logger = logger;
        }

        public async Task<string> GetValueAsync(string key)
        {
            try
            {
                var db = _redisConnection.GetDatabase();
                return await db.StringGetAsync(key);
            }
            catch (RedisConnectionException redisEx)
            {
                _logger.LogError(redisEx, "Connection problem with Redis Service.");
                return null;
            }
        }

        public async Task SetValueAsync(string key, string value)
        {
            try
            {
                var db = _redisConnection.GetDatabase();
                await db.StringSetAsync(key, value, TimeSpan.FromSeconds(60));
            }
            catch(RedisConnectionException redisEx)
            {
                _logger.LogError(redisEx, "Connection problem with Redis Service.");
            }
        }
    }
}
