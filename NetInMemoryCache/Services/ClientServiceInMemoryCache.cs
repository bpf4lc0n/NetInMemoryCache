using Bogus.DataSets;
using Microsoft.Extensions.Caching.Memory;
using NetInMemoryCache.Models;

namespace NetInMemoryCache.Services
{
    public class ClientServiceInMemoryCache : IClientService
    {
        private const string ClientListCacheKey = "ClientList";
        private readonly IMemoryCache _memoryCache;
        private readonly IClientService _clientService;

        public ClientServiceInMemoryCache(
            IClientService clientService,
            IMemoryCache memoryCache)
        {
            _clientService = clientService;
            _memoryCache = memoryCache;
        }

        /// <summary>
        /// Data will be cleared in 15seg if no requests are made. Absolute Expiration time will be 30seg.
        /// </summary>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<List<Client>> GetAllAsync(CancellationToken ct)
        {
            var cacheOptions = new MemoryCacheEntryOptions()                
                .SetSlidingExpiration(TimeSpan.FromSeconds(15))
                .SetAbsoluteExpiration(TimeSpan.FromSeconds(30));

            if (_memoryCache.TryGetValue(ClientListCacheKey, out List<Client>? query))
                return query ?? new List<Client>();

            query = await _clientService.GetAllAsync(ct);

            _memoryCache.Set(ClientListCacheKey, query, cacheOptions);

            return query;

        }
    }
}
