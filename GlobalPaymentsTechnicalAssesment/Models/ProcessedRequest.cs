namespace GlobalPaymentsTechnicalAssesment.Models
{
    public class ProcessedRequest
    {
        public string Source { get; set; } // Internal or External
        public int Floor { get; set; }    // Requested floor
        public string Direction { get; set; }    // Requested floor
        public DateTime ProcessedAt { get; set; } // Timestamp
    }
}
