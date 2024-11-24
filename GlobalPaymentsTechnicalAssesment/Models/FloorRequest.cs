namespace GlobalPaymentsTechnicalAssesment.Models
{
    public class FloorRequest
    {
        public string Source { get; set; } // External or Internal
        public int Floor { get; set; } // Requested floor
        public string Direction { get; set; } // Up or Down for external requests
    }
}
