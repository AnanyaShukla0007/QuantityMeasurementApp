using QuantityMeasurementApp.Models;

namespace QuantityMeasurementApp.Service.Mappers
{
    public static class TemperatureUnitMapper
    {
        public static TemperatureUnit Map(string unit)
        {
            return unit.ToLower() switch
            {
                "celsius" => TemperatureUnit.CELSIUS,
                "fahrenheit" => TemperatureUnit.FAHRENHEIT,
                "kelvin" => TemperatureUnit.KELVIN,
                _ => throw new ArgumentException("Invalid Temperature unit")
            };
        }
    }
}