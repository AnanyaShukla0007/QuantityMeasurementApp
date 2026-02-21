using QuantityMeasurementApp.Models;

namespace QuantityMeasurementApp.Services
{
    /// <summary>
    /// Service layer exposing UC1–UC7 functionality.
    /// Provides equality, conversion, and addition APIs.
    /// </summary>
    public class QuantityMeasurementService
    {
        // ---------------- UC1 ----------------
        public bool ValidateFeetEquality(double v1, double v2)
        {
            return ValidateLengthEquality(v1, LengthUnit.Feet,
                                          v2, LengthUnit.Feet);
        }

        // ---------------- UC2 ----------------
        public bool ValidateInchesEquality(double v1, double v2)
        {
            return ValidateLengthEquality(v1, LengthUnit.Inches,
                                          v2, LengthUnit.Inches);
        }

        // ---------------- UC3 + UC4 ----------------
        public bool ValidateLengthEquality(double v1, LengthUnit u1,
                                           double v2, LengthUnit u2)
        {
            var first = new QuantityLength(v1, u1);
            var second = new QuantityLength(v2, u2);

            return first.Equals(second);
        }

        // ---------------- UC5 ----------------
        public double Convert(double value,
                              LengthUnit from,
                              LengthUnit to)
        {
            return QuantityLength.Convert(value, from, to);
        }

        // ---------------- UC6 ----------------
        // Addition using unit of first operand
        public QuantityLength AddLength(
            QuantityLength first,
            QuantityLength second)
        {
            if (first is null || second is null)
                throw new ArgumentException("Operands cannot be null.");

            return QuantityLength.Add(first, second);
        }

        // ---------------- UC7 ----------------
        // Addition using explicit target unit
        public QuantityLength AddLength(
            QuantityLength first,
            QuantityLength second,
            LengthUnit targetUnit)
        {
            if (first is null || second is null)
                throw new ArgumentException("Operands cannot be null.");

            return QuantityLength.Add(first, second, targetUnit);
        }
    }
}