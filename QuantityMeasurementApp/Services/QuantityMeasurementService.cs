using QuantityMeasurementApp.Models;

namespace QuantityMeasurementApp.Services
{
    /// <summary>
    /// Handles quantity measurement comparison logic.
    /// </summary>
    public class QuantityMeasurementService
    {
        public bool ConvertUnits(Feet firstMeasurement, Feet secondMeasurement)
        {
            if (firstMeasurement is null || secondMeasurement is null)
                return false;

            return firstMeasurement.Equals(secondMeasurement);
        }
    }
}