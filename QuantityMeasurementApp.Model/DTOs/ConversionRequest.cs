namespace QuantityMeasurementApp.Model.DTOs
{
    public class ConversionRequest
    {
        public QuantityDTO Source { get; set; } = new();

        public string TargetUnit { get; set; } = string.Empty;
    }
}