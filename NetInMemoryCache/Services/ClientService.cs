using Bogus.DataSets;
using Microsoft.EntityFrameworkCore;
using NetInMemoryCache.Infrastructure;
using NetInMemoryCache.Models;
using System.Diagnostics.CodeAnalysis;

namespace NetInMemoryCache.Services
{
    public class ClientService : IClientService
    {
        private readonly ApplicationDbContext _dbContext;

        public ClientService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Client>> GetAllAsync(CancellationToken ct)
        {
            return await _dbContext.Clients!.ToListAsync(cancellationToken: ct);
        }
    }
}
