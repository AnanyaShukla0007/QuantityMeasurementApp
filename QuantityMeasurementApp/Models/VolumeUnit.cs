using QuantityMeasurementApp.Interface;

namespace QuantityMeasurementApp.Models
{
    public enum VolumeUnit
    {
        LITRE,
        MILLILITRE,
        GALLON
    }

    public static class VolumeUnitExtensions
    {
        public static double ConvertToBaseUnit(this VolumeUnit unit, double value)
        {
            return unit switch
            {
                VolumeUnit.LITRE => value,
                VolumeUnit.MILLILITRE => value * 0.001,
                VolumeUnit.GALLON => value * 3.78541,
                _ => throw new ArgumentException("Invalid Volume Unit")
            };
        }

        public static double ConvertFromBaseUnit(this VolumeUnit unit, double baseValue)
        {
            return unit switch
            {
                VolumeUnit.LITRE => baseValue,
                VolumeUnit.MILLILITRE => baseValue / 0.001,
                VolumeUnit.GALLON => baseValue / 3.78541,
                _ => throw new ArgumentException("Invalid Volume Unit")
            };
        }

        public static string GetUnitName(this VolumeUnit unit)
        {
            return unit.ToString();
        }
    }
}