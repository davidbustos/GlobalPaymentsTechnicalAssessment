namespace GlobalPaymentsTechnicalAssesment.Models
{
    public class ElevatorState
    {
        public int CurrentFloor { get; set; } = 1;  // Default floor
        public string State { get; set; } = "Idle"; // Idle, Moving, Opening, Closing
        public string DoorState { get; set; } = "Closed"; // Closed, Opening, Opened, Closing
        public double Position { get; set; } = 0.0; // Position in meters
        public double Speed { get; set; } = .5;   // Speed in meters per second
        public double FloorHeight { get; set; } = 2.5; // Height between floors in meters
        public int TimeToOpenDoors { get; set; } = 1; // Time to open doors in seconds
        public int TimeToCloseDoors { get; set; } = 1; // Time to close doors in seconds
    }
}
