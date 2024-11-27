
using System.Runtime.Caching;

using IPStackDLL.Models;

using IPStackApi.Models;

namespace IPStackApi.Services
{
    public class IPCacheService 
    {
        private readonly ObjectCache _cache = MemoryCache.Default;
        private readonly CacheItemPolicy _policy;

        public IPCacheService() 
        {
            _policy = new CacheItemPolicy
            {
                AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(1)
            };
        }

        public void AddToCache(string ip, IPDetails ipDetails)
        {
            _cache.Add(ip, ipDetails, _policy);
        }

        public IPDetails? GetFromCache(string ip)
        {
            return _cache.Get(ip) as IPDetails;
        }
    }
}