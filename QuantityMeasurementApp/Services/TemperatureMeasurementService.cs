using QuantityMeasurementApp.Models;

namespace QuantityMeasurementApp.Services
{
    public class TemperatureMeasurementService
    {
        public bool ValidateEquality(
            double v1, TemperatureUnit u1,
            double v2, TemperatureUnit u2)
        {
            var q1 = new Quantity<TemperatureUnit>(v1, u1);
            var q2 = new Quantity<TemperatureUnit>(v2, u2);
            return q1.Equals(q2);
        }

        public Quantity<TemperatureUnit> Convert(
            double value,
            TemperatureUnit from,
            TemperatureUnit to)
        {
            var q = new Quantity<TemperatureUnit>(value, from);
            return q.ConvertTo(to);
        }
    }
}