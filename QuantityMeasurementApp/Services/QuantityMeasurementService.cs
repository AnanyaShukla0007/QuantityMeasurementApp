using QuantityMeasurementApp.Models;

namespace QuantityMeasurementApp.Services
{
    public class QuantityMeasurementService
    {
        public bool ValidateFeetEquality(double v1, double v2)
        {
            return ValidateLengthEquality(v1, LengthUnit.Feet,
                                          v2, LengthUnit.Feet);
        }

        public bool ValidateInchesEquality(double v1, double v2)
        {
            return ValidateLengthEquality(v1, LengthUnit.Inches,
                                          v2, LengthUnit.Inches);
        }

        public bool ValidateLengthEquality(double v1, LengthUnit u1,
                                           double v2, LengthUnit u2)
        {
            var q1 = new QuantityLength(v1, u1);
            var q2 = new QuantityLength(v2, u2);

            return q1.Equals(q2);
        }

        public double Convert(double value, LengthUnit from, LengthUnit to)
        {
            var q = new QuantityLength(value, from);
            return q.ConvertTo(to).Value;
        }

        public QuantityLength AddLength(QuantityLength first, QuantityLength second)
        {
            return first.Add(second);
        }

        public QuantityLength AddLength(QuantityLength first,
                                        QuantityLength second,
                                        LengthUnit targetUnit)
        {
            return first.Add(second, targetUnit);
        }
    }
}