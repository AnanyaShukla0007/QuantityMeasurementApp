namespace QuantityMeasurementApp.Model.DTOs
{
    public class QuantityResponse
    {
        public bool Success { get; set; }

        public string Message { get; set; } = string.Empty;

        public string? FormattedResult { get; set; }
    }
}