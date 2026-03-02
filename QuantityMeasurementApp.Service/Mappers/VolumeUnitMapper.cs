using QuantityMeasurementApp.Models;

namespace QuantityMeasurementApp.Service.Mappers
{
    public static class VolumeUnitMapper
    {
        public static VolumeUnit Map(string unit)
        {
            return unit.ToLower() switch
            {
                "millilitre" => VolumeUnit.MILLILITRE,
                "litre" => VolumeUnit.LITRE,
                "gallon" => VolumeUnit.GALLON,
                _ => throw new ArgumentException("Invalid Volume unit")
            };
        }
    }
}