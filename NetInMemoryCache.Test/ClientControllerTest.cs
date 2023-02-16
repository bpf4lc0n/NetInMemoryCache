using Microsoft.Extensions.Caching.Memory;
using Moq;
using NetInMemoryCache.Infrastructure;
using NetInMemoryCache.Services;
using Xunit;
using Xunit.Sdk;

namespace NetInMemoryCache.Test
{
    public class ClientControllerTest
    {
        private readonly Mock<IClientService> _mockRepo;
        private readonly Mock<IMemoryCache> _memoryCache;
        public ClientControllerTest()
        {
            _mockRepo = new Mock<IClientService>();
            _memoryCache = new Mock<IMemoryCache>();
            //_controller = new ClientController(_mockRepo.Object);
        }

        [Fact]
        public async void Index_ActionExecutes_ReturnsViewForIndex()
        {
            var mockContext = new Mock<ApplicationDbContext>();

            var service = new ClientService(mockContext.Object);         
            var watch = System.Diagnostics.Stopwatch.StartNew();
            await service.GetAllAsync(new CancellationToken(false));
            watch.Stop();
            var elapsedMs1 = watch.ElapsedMilliseconds;

            var serviceInMemory = new ClientServiceInMemoryCache(_mockRepo.Object, _memoryCache.Object);
            watch = System.Diagnostics.Stopwatch.StartNew();
            await serviceInMemory.GetAllAsync(new CancellationToken(false));
            watch.Stop();
            var elapsedMs2 = watch.ElapsedMilliseconds;

            Assert.Greater(elapsedMs2, elapsedMs1, $"The InMemory value {elapsedMs2} is less that without cached{elapsedMs1}");
        }
    }
}