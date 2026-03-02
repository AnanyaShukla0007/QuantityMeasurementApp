using QuantityMeasurementApp.Models;

namespace QuantityMeasurementApp.Service.Mappers
{
    public static class LengthUnitMapper
    {
        public static LengthUnit Map(string unit)
        {
            return unit.ToLower() switch
            {
                "feet" => LengthUnit.Feet,
                "inches" => LengthUnit.Inches,
                "yards" => LengthUnit.Yards,
                "centimeters" => LengthUnit.Centimeters,
                _ => throw new ArgumentException("Invalid Length unit")
            };
        }
    }
}