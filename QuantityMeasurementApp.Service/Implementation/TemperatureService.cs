using QuantityMeasurementApp.Models;
using QuantityMeasurementApp.Service.Interfaces;
using QuantityMeasurementApp.Service.Mappers;

namespace QuantityMeasurementApp.Service.Implementations
{
    public class TemperatureService : ITemperatureService
    {
        public double Convert(double value, string from, string to)
        {
            var fromUnit = TemperatureUnitMapper.Map(from);
            var toUnit = TemperatureUnitMapper.Map(to);

            var quantity = new Quantity<TemperatureUnit>(value, fromUnit);
            return quantity.ConvertTo(toUnit).Value;
        }

        public bool Equal(double v1, string u1, double v2, string u2)
        {
            var q1 = new Quantity<TemperatureUnit>(v1, TemperatureUnitMapper.Map(u1));
            var q2 = new Quantity<TemperatureUnit>(v2, TemperatureUnitMapper.Map(u2));
            return q1.Equals(q2);
        }
    }
}