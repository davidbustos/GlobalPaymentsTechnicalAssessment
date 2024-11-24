using GlobalPaymentsTechnicalAssesment.Models;

namespace GlobalPaymentsTechnicalAssesment.Services
{
    public interface IElevatorService
    {
        Task ProcessFloorRequestAsync(FloorRequest request);
        ElevatorState GetElevatorState();
    }

    public class ElevatorService : IElevatorService
    {
        private readonly ElevatorState _state = new();

        public async Task ProcessFloorRequestAsync(FloorRequest request)
        {
            // Simulate door closing
            _state.State = "Closing";
            _state.DoorState = "Closing";
            await Task.Delay(_state.TimeToCloseDoors * 1000);
            _state.DoorState = "Closed";

            // Simulate moving
            _state.State = "Moving";
            var distance = Math.Abs(request.Floor - _state.CurrentFloor) * _state.FloorHeight;
            var travelTime = distance / _state.Speed;

            for (double t = 0; t < travelTime; t += 0.1)
            {
                _state.Position += (_state.Speed * 0.1) * Math.Sign(request.Floor - _state.CurrentFloor);
                await Task.Delay(100); // Update every 100ms
            }

            _state.Position = request.Floor * _state.FloorHeight;
            _state.CurrentFloor = request.Floor;

            // Simulate door opening
            _state.State = "Opening";
            _state.DoorState = "Opening";
            await Task.Delay(_state.TimeToOpenDoors * 1000);
            _state.DoorState = "Opened";

            // Return to idle
            _state.State = "Idle";
        }

        public ElevatorState GetElevatorState()
        {
            return _state;
        }
    }
}
