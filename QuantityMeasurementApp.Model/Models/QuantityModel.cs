using QuantityMeasurementApp.Model.Enums;

namespace QuantityMeasurementApp.Model.Models
{
    public class QuantityModel
    {
        public double Value { get; set; }

        public string Unit { get; set; } = string.Empty;

        public MeasurementCategory Category { get; set; }

        public QuantityModel() { }

        public QuantityModel(double value, string unit, MeasurementCategory category)
        {
            Value = value;
            Unit = unit;
            Category = category;
        }
    }
}