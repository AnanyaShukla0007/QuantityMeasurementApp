using QuantityMeasurementApp.Models;

namespace QuantityMeasurementApp.Services
{
    /// <summary>
    /// Provides validation methods for comparing length quantities while preserving UC1–UC4 compatibility.
    /// </summary>
    public class QuantityMeasurementService
    {
        // UC1 compatibility
        public bool ValidateFeetEquality(double firstValue, double secondValue)
        {
            return ValidateLengthEquality(firstValue, LengthUnit.Feet,
                                          secondValue, LengthUnit.Feet);
        }

        // UC2 compatibility
        public bool ValidateInchesEquality(double firstValue, double secondValue)
        {
            return ValidateLengthEquality(firstValue, LengthUnit.Inches,
                                          secondValue, LengthUnit.Inches);
        }

        // UC3 + UC4 generic comparison
        public bool ValidateLengthEquality(double firstValue, LengthUnit firstUnit,
                                           double secondValue, LengthUnit secondUnit)
        {
            var first = new QuantityLength(firstValue, firstUnit);
            var second = new QuantityLength(secondValue, secondUnit);

            return first.Equals(second);
        }
    }
}