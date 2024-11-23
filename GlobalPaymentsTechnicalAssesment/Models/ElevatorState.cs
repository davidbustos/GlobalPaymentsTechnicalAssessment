namespace GlobalPaymentsTechnicalAssesment.Models
{
    public class ElevatorState
    {
        public int CurrentFloor { get; set; } = 1; // Default floor
        public string State { get; set; } = "Idle"; // States: Idle, Moving, Loading
    }
}
