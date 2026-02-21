using QuantityMeasurementApp.Models;

namespace QuantityMeasurementApp.Services
{
    public class QuantityMeasurementService
    {
        // Validates equality between two feet measurements
        public bool ValidateFeetEquality(double firstValue, double secondValue)
        {
            // Create first Feet object
            var firstMeasurement = new Feet(firstValue);

            // Create second Feet object
            var secondMeasurement = new Feet(secondValue);

            // Compare the two objects using overridden Equals
            return firstMeasurement.Equals(secondMeasurement);
        }
    }
}