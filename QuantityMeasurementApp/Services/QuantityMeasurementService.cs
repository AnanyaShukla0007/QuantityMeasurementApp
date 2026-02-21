using QuantityMeasurementApp.Models;

namespace QuantityMeasurementApp.Services
{
    /// <summary>
    /// Service layer exposing UC1–UC6 functionality.
    /// </summary>
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
            return new QuantityLength(v1, u1)
                .Equals(new QuantityLength(v2, u2));
        }

        public double Convert(double value, LengthUnit from, LengthUnit to)
        {
            return QuantityLength.Convert(value, from, to);
        }

        // ✅ UC6 addition
        public QuantityLength AddLength(QuantityLength first, QuantityLength second)
        {
            return QuantityLength.Add(first, second);
        }
    }
}