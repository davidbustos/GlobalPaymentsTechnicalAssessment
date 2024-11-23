using GlobalPaymentsTechnicalAssesment.Models;
using GlobalPaymentsTechnicalAssesment.Services;
using NUnit.Framework;
using System.Threading.Tasks;

namespace GlobalPaymentsTechnicalAssesment.Tests
{
    [TestFixture]
    public class QueueServiceTests
    {
        private QueueService _queueService;

        [SetUp]
        public void Setup()
        {
            _queueService = new QueueService();
        }

        [Test]
        public async Task EnqueueRequestAsync_ShouldAddRequestToQueue()
        {
            // Arrange
            var request = new FloorRequest { Source = "External", Floor = 3 };

            // Act
            await _queueService.EnqueueRequestAsync(request);

            // Assert
            var dequeuedRequest = await _queueService.DequeueRequestAsync();
            Assert.AreEqual(request.Floor, dequeuedRequest.Floor);
            Assert.AreEqual(request.Source, dequeuedRequest.Source);
        }

        [Test]
        public async Task DequeueRequestAsync_ShouldReturnNull_WhenQueueIsEmpty()
        {
            // Act
            var result = await _queueService.DequeueRequestAsync();

            // Assert
            Assert.IsNull(result);
        }
    }
}
