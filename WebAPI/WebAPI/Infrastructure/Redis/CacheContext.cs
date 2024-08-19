using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using StackExchange.Redis;
using StackExchange.Redis.Extensions.Core.Abstractions;
using WebAPI.Core.Interfaces.Database;

namespace WebAPI.Infrastructure.Redis
{
    public class CacheContext : ICacheContext
    {
        private readonly IRedisCacheClient _redisCacheClient;
        private readonly ILogger<CacheContext> _logger;

        public CacheContext(
            IRedisCacheClient redisCacheClient,
            ILogger<CacheContext> logger)
        {
            _redisCacheClient = redisCacheClient;
            _logger = logger;
        }

        public async Task<T> Get<T>(string key)
        {
            try
            {
                var redis = GetRedis();

                var @string = await redis.StringGetAsync(key);

                return @string.HasValue
                    ? JsonConvert.DeserializeObject<T>(@string)
                    : default;
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"The error was occured during getting string value from Redis by key: {key}");

                return default;
            }
        }

        public async Task Set<T>(string key, T value, TimeSpan? expiry = null)
        {
            try
            {
                var redis = GetRedis();

                var isSet = await redis.StringSetAsync(key, JsonConvert.SerializeObject(value), expiry);

                if (!isSet)
                {
                    _logger.LogWarning($"String with {key} was not saved in Redis");
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"The error was occured during saving string value with {key} key in Redis");
            }
        }

        private IDatabase GetRedis() =>
            _redisCacheClient.GetDbFromConfiguration().Database;
    }
}