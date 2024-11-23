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
            Console.WriteLine($"[ElevatorService] Processing request to floor {request.Floor}...");

            _state.State = "Moving";
            await Task.Delay(Math.Abs(_state.CurrentFloor - request.Floor) * 1000); // Simulate travel time

            _state.CurrentFloor = request.Floor;
            _state.State = "Idle";

            Console.WriteLine($"[ElevatorService] Arrived at floor {_state.CurrentFloor}.");
        }

        public ElevatorState GetElevatorState()
        {
            return _state;
        }
    }
}
