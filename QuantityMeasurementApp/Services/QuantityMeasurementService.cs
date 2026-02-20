using QuantityMeasurementApp.Models;

namespace QuantityMeasurementApp.Services
{
    /// <summary>
    /// Provides equality validation for Feet and Inches.
    /// </summary>
    public class QuantityMeasurementService
    {
        /// <summary>
        /// Validates equality between two Feet measurements.
        /// </summary>
        public bool ValidateFeetEquality(double firstValue, double secondValue)
        {
            var firstMeasurement = new Feet(firstValue);
            var secondMeasurement = new Feet(secondValue);

            return firstMeasurement.Equals(secondMeasurement);
        }

        /// <summary>
        /// Validates equality between two Inches measurements.
        /// </summary>
        public bool ValidateInchesEquality(double firstValue, double secondValue)
        {
            var firstMeasurement = new Inches(firstValue);
            var secondMeasurement = new Inches(secondValue);

            return firstMeasurement.Equals(secondMeasurement);
        }
    }
}