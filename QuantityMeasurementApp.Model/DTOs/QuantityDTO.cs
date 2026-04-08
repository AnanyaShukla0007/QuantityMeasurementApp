using QuantityMeasurementApp.Model.Enums;

namespace QuantityMeasurementApp.Model.DTOs
{
    public class QuantityDTO
    {
        public double Value { get; set; }

        public string Unit { get; set; } = string.Empty;

        public MeasurementCategory Category { get; set; }
    }
}