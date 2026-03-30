namespace QuantityMeasurementApp.Model.DTOs
{
    public class BinaryQuantityRequest
    {
        public QuantityDTO Quantity1 { get; set; } = new();

        public QuantityDTO Quantity2 { get; set; } = new();

        public string? TargetUnit { get; set; }
    }
}