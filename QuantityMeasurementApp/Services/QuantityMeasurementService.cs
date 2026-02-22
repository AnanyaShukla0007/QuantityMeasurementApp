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
            var first = new QuantityLength(v1, u1);
            var second = new QuantityLength(v2, u2);

            return first.Equals(second);
        }

        public double Convert(double value,
                              LengthUnit from,
                              LengthUnit to)
        {
            return QuantityLength.Convert(value, from, to);
        }

        public QuantityLength AddLength(
            QuantityLength first,
            QuantityLength second)
        {
            return QuantityLength.Add(first, second);
        }

        public QuantityLength AddLength(
            QuantityLength first,
            QuantityLength second,
            LengthUnit targetUnit)
        {
            return QuantityLength.Add(first, second, targetUnit);
        }
    }
}