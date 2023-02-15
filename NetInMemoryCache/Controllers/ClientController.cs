using Bogus.DataSets;
using Microsoft.AspNetCore.Mvc;
using NetInMemoryCache.Models;
using NetInMemoryCache.Services;

namespace NetInMemoryCache.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientController : Controller
    {
        private readonly IClientService _clientService;

        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var clients = await _clientService.GetAllAsync(new CancellationToken(false));
            return Ok(clients);
        }
    }
}