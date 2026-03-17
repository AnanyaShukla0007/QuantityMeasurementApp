using QuantityMeasurementApp.Models;

namespace QuantityMeasurementApp.Services
{
    public class WeightMeasurementService
    {
        public bool ValidateWeightEquality(double v1, WeightUnit u1,
                                           double v2, WeightUnit u2)
        {
            var q1 = new QuantityWeight(v1, u1);
            var q2 = new QuantityWeight(v2, u2);

            return q1.Equals(q2);
        }

        public double Convert(double value, WeightUnit from, WeightUnit to)
        {
            var q = new QuantityWeight(value, from);
            return q.ConvertTo(to).Value;
        }

        public QuantityWeight AddWeight(QuantityWeight first, QuantityWeight second)
        {
            return first.Add(second);
        }

        public QuantityWeight AddWeight(QuantityWeight first,
                                        QuantityWeight second,
                                        WeightUnit targetUnit)
        {
            return first.Add(second, targetUnit);
        }
    }
}