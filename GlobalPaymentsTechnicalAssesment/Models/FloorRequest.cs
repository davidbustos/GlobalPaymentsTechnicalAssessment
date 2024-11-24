namespace GlobalPaymentsTechnicalAssesment.Models
{
    public class FloorRequest
    {
        public string Source { get; set; } // Internal or External
        public int Floor { get; set; } // Requested floor
        public string Direction { get; set; } = "None"; // "Up", "Down", or "None" (default for internal)
    }
}
