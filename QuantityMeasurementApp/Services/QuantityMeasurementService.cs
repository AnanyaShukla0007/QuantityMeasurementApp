using QuantityMeasurementApp.Models;

namespace QuantityMeasurementApp.Services
{
    /// <summary>
    /// Provides equality and conversion APIs for length quantities,
    /// preserving UC1 through UC5 functionality.
    /// </summary>
    public class QuantityMeasurementService
    {
        // -------------------- UC1 --------------------

        // Validates Feet equality
        public bool ValidateFeetEquality(double firstValue, double secondValue)
        {
            return ValidateLengthEquality(firstValue, LengthUnit.Feet,
                                          secondValue, LengthUnit.Feet);
        }

        // -------------------- UC2 --------------------

        // Validates Inches equality
        public bool ValidateInchesEquality(double firstValue, double secondValue)
        {
            return ValidateLengthEquality(firstValue, LengthUnit.Inches,
                                          secondValue, LengthUnit.Inches);
        }

        // -------------------- UC3 + UC4 --------------------

        // Generic equality comparison
        public bool ValidateLengthEquality(double v1, LengthUnit u1,
                                           double v2, LengthUnit u2)
        {
            var q1 = new QuantityLength(v1, u1);
            var q2 = new QuantityLength(v2, u2);

            return q1.Equals(q2);
        }

        // -------------------- UC5 --------------------

        // Static-style conversion API
        public double Convert(double value, LengthUnit from, LengthUnit to)
        {
            return QuantityLength.Convert(value, from, to);
        }

        // Overloaded demonstration method (raw values)
        public double DemonstrateLengthConversion(double value,
                                                  LengthUnit from,
                                                  LengthUnit to)
        {
            return Convert(value, from, to);
        }

        // Overloaded demonstration method (existing object)
        public double DemonstrateLengthConversion(QuantityLength length,
                                                  LengthUnit to)
        {
            return length.ConvertTo(to).Value;
        }
    }
}