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
        private readonly IQueueService _queueService;

        public ElevatorService(IQueueService queueService)
        {
            _queueService = queueService;
        }

        public async Task ProcessFloorRequestAsync(FloorRequest request)
        {
            // Handle external and internal requests
            if (request.Source == "External")
            {
                await HandleExternalRequestAsync(request);
            }
            else if (request.Source == "Internal")
            {
                await HandleInternalRequestAsync(request);
            }
            _queueService.AddToHistory(request);
        }

        private async Task HandleExternalRequestAsync(FloorRequest request)
        {
            // Simulate movement to the floor
            await SimulateMovementAsync(request.Floor);

            // Simulate doors opening and waiting
            _state.State = "Opening";
            _state.DoorState = "Opening";
            await Task.Delay(_state.TimeToOpenDoors * 1000);

            _state.DoorState = "Opened";
            await Task.Delay(2000); // Wait for 2 seconds before closing

            _state.State = "Closing";
            _state.DoorState = "Closing";
            await Task.Delay(_state.TimeToCloseDoors * 1000);
            _state.DoorState = "Closed";
            _state.State = "Idle";
        }

        private async Task HandleInternalRequestAsync(FloorRequest request)
        {
            // Simulate movement to the requested floor
            await SimulateMovementAsync(request.Floor);

            // Simulate doors opening and closing
            _state.State = "Opening";
            _state.DoorState = "Opening";
            await Task.Delay(_state.TimeToOpenDoors * 1000);

            _state.DoorState = "Opened";
            await Task.Delay(2000); // Wait for 2 seconds before closing

            _state.State = "Closing";
            _state.DoorState = "Closing";
            await Task.Delay(_state.TimeToCloseDoors * 1000);
            _state.DoorState = "Closed";
            _state.State = "Idle";
        }

        private async Task SimulateMovementAsync(int targetFloor)
        {
            // Update state to moving and calculate movement time
            _state.State = "Moving";
            var targetPosition = targetFloor * _state.FloorHeight;
            var travelTime = (int)(Math.Abs(targetPosition - _state.Position) / _state.Speed * 1000);

            while (_state.Position != targetPosition)
            {
                if (_state.Position < targetPosition)
                {
                    _state.Position += _state.Speed * 0.1;
                    if (_state.Position >= targetPosition) _state.Position = targetPosition;
                }
                else
                {
                    _state.Position -= _state.Speed * 0.1;
                    if (_state.Position <= targetPosition) _state.Position = targetPosition;
                }

                _state.CurrentFloor = (int)(_state.Position / _state.FloorHeight) + 1;
                await Task.Delay(100); // Simulate real-time movement
            }

            _state.Position = targetPosition;
            _state.CurrentFloor = targetFloor;
        }

        public ElevatorState GetElevatorState()
        {
            return _state;
        }
    }
}
