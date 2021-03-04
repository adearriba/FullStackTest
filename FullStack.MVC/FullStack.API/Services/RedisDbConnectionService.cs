using FullStack.API.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FullStack.API.Services
{
    public class RedisDbConnectionService : IRedisDbConnectionService
    {
        private readonly ILogger<RedisDbConnectionService> _logger;
        private IConnectionMultiplexer _connectionMultiplexer;
        private string _connectionString = String.Empty;
        private bool _isConnected = false;

        public RedisDbConnectionService(IConfiguration configuration, ILogger<RedisDbConnectionService> logger)
        {
            _logger = logger;
            _connectionString = configuration["RedisConnectionsString"];
            Connect();
        }

        private void Connect()
        {
            try
            {
                _connectionMultiplexer = ConnectionMultiplexer.Connect(_connectionString);
                _isConnected = true;
                _logger.LogInformation("Connected to Redis Cache.");
            }
            catch (RedisConnectionException redisEx)
            {
                _isConnected = false;
                _logger.LogError(redisEx, "There is a problem connection to Redis Cache.");
            }

        }

        public IDatabase GetDatabase()
        {
            if(_isConnected == false)
            {
                try
                {
                    Connect();
                }
                catch 
                {
                    throw;
                }
            }

            return _connectionMultiplexer.GetDatabase();
        }
    }
}
