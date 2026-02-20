using QuantityMeasurementApp.Models;

namespace QuantityMeasurementApp.Services
{
    /// <summary>
    /// Service layer for validating quantity equality.
    /// </summary>
    public class QuantityMeasurementService
    {
        public bool ValidateLengthEquality(double firstValue, LengthUnit firstUnit,
                                           double secondValue, LengthUnit secondUnit)
        {
            var firstQuantity = new QuantityLength(firstValue, firstUnit);
            var secondQuantity = new QuantityLength(secondValue, secondUnit);

            return firstQuantity.Equals(secondQuantity);
        }
    }
}