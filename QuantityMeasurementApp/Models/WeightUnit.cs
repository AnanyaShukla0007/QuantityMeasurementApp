using QuantityMeasurementApp.Interface;

namespace QuantityMeasurementApp.Models
{
    public sealed class WeightUnit : IMeasurable
    {
        private readonly double _toBaseFactor;
        private readonly string _name;

        private WeightUnit(double toBaseFactor, string name)
        {
            _toBaseFactor = toBaseFactor;
            _name = name;
        }

        public static readonly WeightUnit Gram = new WeightUnit(1.0, "Gram");
        public static readonly WeightUnit Kilogram = new WeightUnit(1000.0, "Kilogram");
        public static readonly WeightUnit Pound = new WeightUnit(453.592, "Pound");

        public double ConvertToBaseUnit(double value)
            => value * _toBaseFactor;

        public double ConvertFromBaseUnit(double baseValue)
            => baseValue / _toBaseFactor;

        public string GetUnitName()
            => _name;
    }
}