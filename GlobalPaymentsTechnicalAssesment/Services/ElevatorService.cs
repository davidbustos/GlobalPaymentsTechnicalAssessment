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
            // Simulate door closing
            _state.State = "Closing";
            _state.DoorState = "Closing";
            await Task.Delay(_state.TimeToCloseDoors * 1000);
            _state.DoorState = "Closed";

            // Simulate moving
            _state.State = "Moving";
            var targetPosition = request.Floor * _state.FloorHeight;
            var travelTime = (int)(Math.Abs(targetPosition - _state.Position) / _state.Speed * 1000);

            while (_state.Position != targetPosition)
            {
                // Move towards the target position
                if (_state.Position < targetPosition)
                {
                    _state.Position += _state.Speed * 0.1;
                    if (_state.Position >= targetPosition)
                    {
                        _state.Position = targetPosition;
                    }
                }
                else
                {
                    _state.Position -= _state.Speed * 0.1;
                    if (_state.Position <= targetPosition)
                    {
                        _state.Position = targetPosition;
                    }
                }

                // Update current floor dynamically
                _state.CurrentFloor = (int)(_state.Position / _state.FloorHeight) + 1;

                // Delay to simulate real-time movement
                await Task.Delay(100);
            }

            // Elevator reaches the target floor
            _state.CurrentFloor = request.Floor;
            _state.State = "Opening";
            _state.DoorState = "Opening";

            // Simulate door opening
            await Task.Delay(_state.TimeToOpenDoors * 1000);
            _state.DoorState = "Opened";

            // Simulate door closing after a short delay
            await Task.Delay(2000); // Door remains open for 2 seconds
            _state.State = "Closing";
            _state.DoorState = "Closing";
            await Task.Delay(_state.TimeToCloseDoors * 1000);
            _state.DoorState = "Closed";

            // Return to idle
            _state.State = "Idle";

            // Add to history once processed
            _queueService.AddToHistory(request);
        }

        public ElevatorState GetElevatorState()
        {
            return _state;
        }
    }
}
