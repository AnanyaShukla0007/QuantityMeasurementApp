using QuantityMeasurementApp.Models;

namespace QuantityMeasurementApp.Services
{
    /// <summary>
    /// Provides validation methods for comparing Feet and Inches measurements separately.
    /// </summary>
    public class QuantityMeasurementService
    {
        // Validates equality between two Feet values
        public bool ValidateFeetEquality(double firstValue, double secondValue)
        {
            // Create first Feet object
            var first = new Feet(firstValue);

            // Create second Feet object
            var second = new Feet(secondValue);

            // Return equality comparison result
            return first.Equals(second);
        }

        // Validates equality between two Inches values
        public bool ValidateInchesEquality(double firstValue, double secondValue)
        {
            // Create first Inches object
            var first = new Inches(firstValue);

            // Create second Inches object
            var second = new Inches(secondValue);

            // Return equality comparison result
            return first.Equals(second);
        }
    }
}