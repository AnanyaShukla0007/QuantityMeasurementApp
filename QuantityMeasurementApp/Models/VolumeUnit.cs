using System;

namespace QuantityMeasurementApp.Models
{
    public enum VolumeUnit
    {
        Liter,
        Milliliter,
        Gallon
    }

    public static class VolumeUnitExtensions
    {
        public static double ConversionFactor(this VolumeUnit unit)
        {
            return unit switch
            {
                VolumeUnit.Liter => 1.0,
                VolumeUnit.Milliliter => 0.001,
                VolumeUnit.Gallon => 3.78541,
                _ => throw new ArgumentException("Unsupported unit.")
            };
        }

        public static double ConvertToBaseUnit(this VolumeUnit unit, double value)
            => value * unit.ConversionFactor();

        public static double ConvertFromBaseUnit(this VolumeUnit unit, double baseValue)
            => baseValue / unit.ConversionFactor();
    }
}