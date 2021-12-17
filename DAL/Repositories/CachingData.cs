using System;
using DAL.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace DAL.Repositories
{
    public class CachingData<T> : ICachingData<T> where T : class
    {
        private readonly IMemoryCache _cache;

        private readonly MemoryCacheEntryOptions _cacheOptions = new()
        {
            AbsoluteExpiration = null,
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5),
            Priority = CacheItemPriority.Low,
            Size = null,
            SlidingExpiration = null
        };

        public CachingData(IMemoryCache cache)
        {
            _cache = cache;
        }

        public bool CheckCacheData(string key, out T data)
        {
            var cacheKey = GetDataCacheKey(key);

            return !_cache.TryGetValue(cacheKey, out data);
        }

        public void SetCacheData(string key, T data)
        {
            var cacheKey = GetDataCacheKey(key);
            _cache.Set(cacheKey, data, _cacheOptions);
        }


        public void RemoveCacheData(string key)
        {
            var cacheKey = GetDataCacheKey(key);
            _cache.Remove(cacheKey);
        }

        private string GetDataCacheKey(string key) => typeof(T) + "_" + key;
    }
}