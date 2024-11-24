using Moq;
using NUnit.Framework;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using GlobalPaymentsTechnicalAssesment.Controllers;
using GlobalPaymentsTechnicalAssesment.Models;
using GlobalPaymentsTechnicalAssesment.Services;

namespace GlobalPaymentsTechnicalAssesment.Tests
{
    [TestFixture]
    public class ElevatorApiControllerTests
    {
        private Mock<IQueueService> _queueServiceMock;
        private Mock<IElevatorService> _elevatorServiceMock;
        private ElevatorApiController _controller;

        [SetUp]
        public void Setup()
        {
            _queueServiceMock = new Mock<IQueueService>();
            _elevatorServiceMock = new Mock<IElevatorService>();
            _controller = new ElevatorApiController(_queueServiceMock.Object, _elevatorServiceMock.Object);
        }

        [Test]
        public async Task RequestFloor_ShouldReturnOk_WhenFloorIsValid()
        {
            // Arrange
            var request = new FloorRequest { Source = "External", Floor = 3 };

            // Act
            var result = await _controller.RequestFloor(request);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            _queueServiceMock.Verify(q => q.EnqueueRequestAsync(It.IsAny<FloorRequest>()), Times.Once);
        }

        [Test]
        public async Task RequestFloor_ShouldReturnBadRequest_WhenFloorIsInvalid()
        {
            // Arrange
            var request = new FloorRequest { Source = "External", Floor = 6 };

            // Act
            var result = await _controller.RequestFloor(request);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            _queueServiceMock.Verify(q => q.EnqueueRequestAsync(It.IsAny<FloorRequest>()), Times.Never);
        }

        [Test]
        public void GetElevatorState_ShouldReturnElevatorState()
        {
            // Arrange
            var state = new ElevatorState { CurrentFloor = 3, State = "Idle" };
            _elevatorServiceMock.Setup(e => e.GetElevatorState()).Returns(state);

            // Act
            var result = _controller.GetElevatorState() as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(state, result.Value);
        }

        [Test]
        public void GetCurrentQueue_ShouldReturnQueue()
        {
            // Arrange
            var queue = new List<FloorRequest>
            {
                new FloorRequest { Source = "External", Floor = 2 }
            };
            _queueServiceMock.Setup(q => q.GetCurrentQueue()).Returns(queue);

            // Act
            var result = _controller.GetCurrentQueue() as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            var resultQueue = result.Value as List<FloorRequest>;
            Assert.AreEqual(1, resultQueue.Count);
            Assert.AreEqual(2, resultQueue[0].Floor);
        }

        [Test]
        public void GetRequestHistory_ShouldReturnHistory()
        {
            // Arrange
            var history = new List<ProcessedRequest>
            {
                new ProcessedRequest { Source = "External", Floor = 3, ProcessedAt = System.DateTime.Now }
            };
            _queueServiceMock.Setup(q => q.GetRequestHistory()).Returns(history);

            // Act
            var result = _controller.GetRequestHistory() as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            var resultHistory = result.Value as List<ProcessedRequest>;
            Assert.AreEqual(1, resultHistory.Count);
            Assert.AreEqual(3, resultHistory[0].Floor);
        }
    }
}
