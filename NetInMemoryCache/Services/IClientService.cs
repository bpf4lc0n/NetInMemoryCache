using NetInMemoryCache.Models;

namespace NetInMemoryCache.Services
{
    public interface IClientService
    {
        Task<List<Client>> GetAllAsync(CancellationToken ct);
    }
}
