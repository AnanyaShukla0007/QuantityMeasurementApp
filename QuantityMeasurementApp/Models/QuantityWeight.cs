using QuantityMeasurementApp.Models;

namespace QuantityMeasurementApp.Models
{
    public class QuantityWeight
    {
        private readonly Quantity<WeightUnit> _internal;

        public double Value => _internal.Value;
        public WeightUnit Unit => _internal.Unit;

        public QuantityWeight(double value, WeightUnit unit)
        {
            _internal = new Quantity<WeightUnit>(value, unit);
        }

        public bool Equals(QuantityWeight other)
        {
            return _internal.Equals(other._internal);
        }

        public QuantityWeight ConvertTo(WeightUnit target)
        {
            var result = _internal.ConvertTo(target);
            return new QuantityWeight(result.Value, result.Unit);
        }

        public QuantityWeight Add(QuantityWeight other)
        {
            var result = _internal.Add(other._internal);
            return new QuantityWeight(result.Value, result.Unit);
        }

        public QuantityWeight Add(QuantityWeight other, WeightUnit target)
        {
            var result = _internal.Add(other._internal, target);
            return new QuantityWeight(result.Value, result.Unit);
        }

        public QuantityWeight Subtract(QuantityWeight other)
        {
            var result = _internal.Subtract(other._internal);
            return new QuantityWeight(result.Value, result.Unit);
        }

        public QuantityWeight Subtract(QuantityWeight other, WeightUnit target)
        {
            var result = _internal.Subtract(other._internal, target);
            return new QuantityWeight(result.Value, result.Unit);
        }

        public double Divide(QuantityWeight other)
        {
            return _internal.Divide(other._internal);
        }

        public override string ToString()
        {
            return _internal.ToString();
        }
    }
}