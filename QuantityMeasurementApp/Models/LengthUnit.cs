using QuantityMeasurementApp.Interface;

namespace QuantityMeasurementApp.Models
{
    public sealed class LengthUnit : IMeasurable
    {
        private readonly double _toBaseFactor;
        private readonly string _name;

        private LengthUnit(double toBaseFactor, string name)
        {
            _toBaseFactor = toBaseFactor;
            _name = name;
        }

        public static readonly LengthUnit Feet = new LengthUnit(12.0, "Feet");
        public static readonly LengthUnit Inches = new LengthUnit(1.0, "Inches");
        public static readonly LengthUnit Yards = new LengthUnit(36.0, "Yards");
        public static readonly LengthUnit Centimeters = new LengthUnit(1 / 2.54, "Centimeters");

        public double ConvertToBaseUnit(double value)
            => value * _toBaseFactor;

        public double ConvertFromBaseUnit(double baseValue)
            => baseValue / _toBaseFactor;

        public string GetUnitName()
            => _name;
    }
}