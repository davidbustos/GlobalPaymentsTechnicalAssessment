using GlobalPaymentsTechnicalAssesment.Models;
using GlobalPaymentsTechnicalAssesment.Services;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;

namespace GlobalPaymentsTechnicalAssesment.Tests
{
    [TestFixture]
    public class ElevatorServiceTests
    {
        private Mock<IQueueService> _queueServiceMock;
        private ElevatorService _elevatorService;

        [SetUp]
        public void Setup()
        {
            _queueServiceMock = new Mock<IQueueService>();
            _elevatorService = new ElevatorService(_queueServiceMock.Object);
        }

        [Test]
        public async Task ProcessFloorRequestAsync_ShouldUpdateCurrentFloor()
        {
            // Arrange
            var request = new FloorRequest { Source = "External", Floor = 5 };

            // Act
            await _elevatorService.ProcessFloorRequestAsync(request);

            // Assert
            var state = _elevatorService.GetElevatorState();
            Assert.AreEqual(5, state.CurrentFloor);
            Assert.AreEqual("Idle", state.State); // Ensure state is reset after processing
        }

        [Test]
        public void GetElevatorState_ShouldReturnDefaultStateInitially()
        {
            // Act
            var state = _elevatorService.GetElevatorState();

            // Assert
            Assert.AreEqual(1, state.CurrentFloor);
            Assert.AreEqual("Idle", state.State);
        }
        [Test]
        public async Task ProcessFloorRequestAsync_ShouldUpdateStateAndLogHistory()
        {
            // Arrange
            var request = new FloorRequest { Source = "External", Floor = 3 };

            // Act
            await _elevatorService.ProcessFloorRequestAsync(request);

            // Assert
            var state = _elevatorService.GetElevatorState();
            Assert.AreEqual(3, state.CurrentFloor);
            Assert.AreEqual("Idle", state.State); // Final state should be Idle
            Assert.AreEqual("Closed", state.DoorState);

            // Verify history logging
            _queueServiceMock.Verify(q => q.AddToHistory(It.Is<FloorRequest>(r => r.Floor == 3 && r.Source == "External")), Times.Once);
        }

        [Test]
        public async Task ProcessFloorRequestAsync_ShouldSimulateDoorStateChanges()
        {
            // Arrange
            var request = new FloorRequest { Source = "External", Floor = 2 };

            // Act
            await _elevatorService.ProcessFloorRequestAsync(request);

            // Assert
            var state = _elevatorService.GetElevatorState();
            Assert.AreEqual("Closed", state.DoorState); // Ensure doors are closed after processing
            Assert.AreEqual(2, state.CurrentFloor);
        }

        [Test]
        public async Task ProcessFloorRequestAsync_ShouldUpdatePositionWhileMoving()
        {
            // Arrange
            var request = new FloorRequest { Source = "External", Floor = 5 };

            // Act
            var task = _elevatorService.ProcessFloorRequestAsync(request);

            // Simulate intermediate polling during movement
            await Task.Delay(500); // Wait for the elevator to start moving
            var stateWhileMoving = _elevatorService.GetElevatorState();
            Assert.AreEqual("Moving", stateWhileMoving.State);
            Assert.IsTrue(stateWhileMoving.Position > 0.0 && stateWhileMoving.Position < stateWhileMoving.FloorHeight * 5);

            // Wait for the elevator to finish
            await task;
            var finalState = _elevatorService.GetElevatorState();
            Assert.AreEqual(5, finalState.CurrentFloor);
            Assert.AreEqual(stateWhileMoving.FloorHeight * 5, finalState.Position);
        }
    }
}
