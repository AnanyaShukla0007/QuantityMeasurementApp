namespace QuantityMeasurementApp.Model.DTOs
{
    public class DivisionResponse
    {
        public bool Success { get; set; }

        public string Message { get; set; } = string.Empty;

        public double Ratio { get; set; }

        public string Interpretation { get; set; } = string.Empty;
    }
}