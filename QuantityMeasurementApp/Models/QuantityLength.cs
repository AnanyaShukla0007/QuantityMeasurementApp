using QuantityMeasurementApp.Models;

namespace QuantityMeasurementApp.Models
{
    public class QuantityLength
    {
        private readonly Quantity<LengthUnit> _internal;

        public double Value => _internal.Value;
        public LengthUnit Unit => _internal.Unit;

        public QuantityLength(double value, LengthUnit unit)
        {
            _internal = new Quantity<LengthUnit>(value, unit);
        }

        public bool Equals(QuantityLength other)
        {
            return _internal.Equals(other._internal);
        }

        public QuantityLength ConvertTo(LengthUnit target)
        {
            var result = _internal.ConvertTo(target);
            return new QuantityLength(result.Value, result.Unit);
        }

        public QuantityLength Add(QuantityLength other)
        {
            var result = _internal.Add(other._internal);
            return new QuantityLength(result.Value, result.Unit);
        }

        public QuantityLength Add(QuantityLength other, LengthUnit target)
        {
            var result = _internal.Add(other._internal, target);
            return new QuantityLength(result.Value, result.Unit);
        }

        public QuantityLength Subtract(QuantityLength other)
        {
            var result = _internal.Subtract(other._internal);
            return new QuantityLength(result.Value, result.Unit);
        }

        public QuantityLength Subtract(QuantityLength other, LengthUnit target)
        {
            var result = _internal.Subtract(other._internal, target);
            return new QuantityLength(result.Value, result.Unit);
        }

        public double Divide(QuantityLength other)
        {
            return _internal.Divide(other._internal);
        }

        public override string ToString()
        {
            return _internal.ToString();
        }
    }
}