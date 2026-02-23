using QuantityMeasurementApp.Interface;

namespace QuantityMeasurementApp.Models
{
    public sealed class VolumeUnit : IMeasurable
    {
        private readonly double _toBaseFactor;
        private readonly string _name;

        private VolumeUnit(double toBaseFactor, string name)
        {
            _toBaseFactor = toBaseFactor;
            _name = name;
        }

        public static readonly VolumeUnit MILLILITRE = new VolumeUnit(1.0, "Millilitre");
        public static readonly VolumeUnit LITRE = new VolumeUnit(1000.0, "Litre");
        public static readonly VolumeUnit GALLON = new VolumeUnit(3785.41, "Gallon");

        public double ConvertToBaseUnit(double value)
            => value * _toBaseFactor;

        public double ConvertFromBaseUnit(double baseValue)
            => baseValue / _toBaseFactor;

        public string GetUnitName()
            => _name;
        public bool SupportsArithmetic() => true;

        public void ValidateOperationSupport(string operation)
        {
            
        }
    }
}