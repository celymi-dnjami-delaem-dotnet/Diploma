using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using StackExchange.Redis.Extensions.Core.Abstractions;
using WebAPI.Core.Configuration;
using WebAPI.Core.Interfaces.Database;

namespace WebAPI.Infrastructure.Redis
{
    public class CacheContextProxy : ICacheContext
    {
        private readonly IServiceProvider _serviceProvider;

        private readonly bool _enableCache;

        public CacheContextProxy(AppSettings appSettings, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;

            _enableCache = appSettings.Redis.EnableRedis;
        }

        public Task<T> Get<T>(string key)
        {
            if (!_enableCache)
            {
                return Task.FromResult<T>(default);
            }

            var cacheContext = GetCacheContext();

            return cacheContext.Get<T>(key);
        }

        public Task Set<T>(string key, T value, TimeSpan? expiry = null)
        {
            if (!_enableCache)
            {
                return Task.CompletedTask;
            }

            var cacheContext = GetCacheContext();

            return cacheContext.Set(key, value, expiry);
        }

        private ICacheContext GetCacheContext() =>
            new CacheContext(
                _serviceProvider.GetRequiredService<IRedisCacheClient>(),
                _serviceProvider.GetRequiredService<ILogger<CacheContext>>());
    }
}