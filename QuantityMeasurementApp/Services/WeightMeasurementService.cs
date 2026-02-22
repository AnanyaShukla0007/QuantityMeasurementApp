using QuantityMeasurementApp.Models;

namespace QuantityMeasurementApp.Services
{
    public class WeightMeasurementService
    {
        public bool ValidateWeightEquality(double v1, WeightUnit u1,
                                           double v2, WeightUnit u2)
        {
            var first = new QuantityWeight(v1, u1);
            var second = new QuantityWeight(v2, u2);

            return first.Equals(second);
        }

        public double Convert(double value,
                              WeightUnit from,
                              WeightUnit to)
        {
            return QuantityWeight.Convert(value, from, to);
        }

        public QuantityWeight AddWeight(
            QuantityWeight first,
            QuantityWeight second)
        {
            return QuantityWeight.Add(first, second);
        }

        public QuantityWeight AddWeight(
            QuantityWeight first,
            QuantityWeight second,
            WeightUnit targetUnit)
        {
            return QuantityWeight.Add(first, second, targetUnit);
        }
    }
}