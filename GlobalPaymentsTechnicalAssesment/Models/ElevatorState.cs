namespace GlobalPaymentsTechnicalAssesment.Models
{
    public enum ElevatorState
    {
        Idle, 
        Moving, 
        OpeningDoor, 
        ClosingDoor
    }
    public class ElevatorStateMachine
    {
        public ElevatorState CurrentState { get; private set; } = ElevatorState.Idle;
        private readonly Queue<int> _floorRequests = new Queue<int>();
        public float yPosition { get; set; }
        public int currFloor { get; set; }
        public float doorState { get; set; }

        public void TransitionToState(ElevatorState newState)
        {
            Console.WriteLine($"Transitioning from {CurrentState} to {newState}.");
            CurrentState = newState;

            switch (newState) { 
                case ElevatorState.Idle:
                    {

                        break;
                    }
                    case ElevatorState.Moving:
                    {
                        break;
                    }

                    case ElevatorState.OpeningDoor:
                    {

                        break;
                    }
                    case ElevatorState.ClosingDoor:
                        {
                             break;
                        }
            }
        }
    }
}
