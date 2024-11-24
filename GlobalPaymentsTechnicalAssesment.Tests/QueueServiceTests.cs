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
        public async Task DequeueRequestAsync_ShouldReturnNull_WhenQueueIsEmpty()
        {
            // Act
            var result = await _queueService.DequeueRequestAsync();

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public async Task EnqueueRequestAsync_ShouldAddRequestToQueue()
        {
            // Arrange
            var request = new FloorRequest { Source = "External", Floor = 3 };

            // Act
            await _queueService.EnqueueRequestAsync(request);

            // Assert
            var queue = _queueService.GetCurrentQueue();
            Assert.AreEqual(1, queue.Count);
            Assert.AreEqual(request.Floor, queue.First().Floor);
            Assert.AreEqual(request.Source, queue.First().Source);
        }

        [Test]
        public async Task DequeueRequestAsync_ShouldRemoveRequestFromQueue()
        {
            // Arrange
            var request = new FloorRequest { Source = "External", Floor = 3 };
            await _queueService.EnqueueRequestAsync(request);

            // Act
            var dequeuedRequest = await _queueService.DequeueRequestAsync();

            // Assert
            Assert.AreEqual(request.Floor, dequeuedRequest.Floor);
            Assert.AreEqual(request.Source, dequeuedRequest.Source);
            Assert.AreEqual(0, _queueService.GetCurrentQueue().Count); // Queue should be empty
        }

        [Test]
        public void AddToHistory_ShouldLogProcessedRequest()
        {
            // Arrange
            var request = new FloorRequest { Source = "External", Floor = 3 };

            // Act
            _queueService.AddToHistory(request);

            // Assert
            var history = _queueService.GetRequestHistory();
            Assert.AreEqual(1, history.Count);
            Assert.AreEqual(request.Floor, history.First().Floor);
            Assert.AreEqual(request.Source, history.First().Source);
            Assert.NotNull(history.First().ProcessedAt); // Ensure timestamp is recorded
        }
    }
}
