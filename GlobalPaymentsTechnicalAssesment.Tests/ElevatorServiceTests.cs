using GlobalPaymentsTechnicalAssesment.Models;
using GlobalPaymentsTechnicalAssesment.Services;
using NUnit.Framework;
using System.Threading.Tasks;

namespace GlobalPaymentsTechnicalAssesment.Tests
{
    [TestFixture]
    public class ElevatorServiceTests
    {
        private ElevatorService _elevatorService;

        [SetUp]
        public void Setup()
        {
            _elevatorService = new ElevatorService();
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
    }
}
