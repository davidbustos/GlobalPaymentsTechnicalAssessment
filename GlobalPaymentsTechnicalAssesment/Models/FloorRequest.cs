namespace GlobalPaymentsTechnicalAssesment.Models
{
    public class FloorRequest
    {
        public string Source { get; set; } // "Internal" or "External"
        public int Floor { get; set; }    // Target floor (1-5)
    }
}
